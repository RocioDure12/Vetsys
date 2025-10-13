namespace Vetsys.API.Shared.Criteria
{
    public record FilterCriteria
    {   
        public required string PropertyName { get; init; }
        public required object Value { get; init; }
        public required FilterOperator Operator { get; init; }
    }
}
