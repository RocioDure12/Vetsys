namespace Vetsys.API.Modules.Notifications.Contracts
{
    public interface ITelegramSender
    {
        Task SendTelegramMessageAsync(string telegramId, string message);
    }
}
