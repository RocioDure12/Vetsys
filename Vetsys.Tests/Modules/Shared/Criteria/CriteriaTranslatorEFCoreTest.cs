using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Vetsys.API.Shared.Criteria;
using Vetsys.Tests.Modules.Shared.Models;

namespace Vetsys.Tests.Modules.Shared.Criteria
{
    public class CriteriaTranslatorEFCoreTest
    {
        // 1. Fuente de datos para los tests
        public static IEnumerable<TestCaseData> FilterCombinations
        {
            get
            {
                var customers = new List<Customer>
                {
                    new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", BirthDate = new DateOnly(1990, 1, 1) },
                    new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com", BirthDate = new DateOnly(1985, 5, 15) },
                    new Customer { Id = 3, Name = "Alice Johnson", Email = "alice@example.com", BirthDate = new DateOnly(2000, 10, 30) }
                }.AsQueryable();

                // Filtro por nombre
                var criteria1 = new BaseCriteria();
                criteria1.AddFilter("Name", "John Doe", FilterOperator.Equals);
                yield return new TestCaseData(
                    customers,
                    criteria1,
                    customers.Where(c => c.Name == "John Doe").ToList()
                ).SetName("Filtro por nombre igual");

                // Filtro por email
                var criteria2 = new BaseCriteria();
                criteria2.AddFilter("Email", "jane@example.com", FilterOperator.Equals);
                yield return new TestCaseData(
                    customers,
                    criteria2,
                    customers.Where(c => c.Email == "jane@example.com").ToList()
                ).SetName("Filtro por email igual");

                // Filtro por fecha de nacimiento mayor a 1990
                var criteria3 = new BaseCriteria();
                criteria3.AddFilter("BirthDate", new DateOnly(1990, 1, 1), FilterOperator.GreaterThan);
                yield return new TestCaseData(
                    customers,
                    criteria3,
                    customers.Where(c => c.BirthDate > new DateOnly(1990, 1, 1)).ToList()
                ).SetName("Filtro por fecha mayor a 1990");

     
            }
        }

        // 2. Test parametrizado usando la fuente de datos
        [Test, TestCaseSource(nameof(FilterCombinations))]
        public void TranslatesCriteriaWithValidQueryGeneratesCorrectExpression(
            IQueryable<Customer> customers,
            BaseCriteria criteria,
            List<Customer> expectedCustomers)
        {
            // Act
            var result = CriteriaTranslatorEFCore.ApplyCriteria(customers, criteria).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(expectedCustomers));
        }
    }
}
