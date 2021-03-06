using System.Threading.Tasks;
using Telegram.Bot;

namespace gira_com_by.Logic.Nodes
{
    public class TBot : IBot
    {
        private static readonly ITelegramBotClient telegramBotClient = new TelegramBotClient(token: Secret.BotToken);
        //private readonly string baseUri = "https://api.telegram.org/";
        public async Task<bool> SendMessageAsync(string message)
        {
            var responceMessage = await telegramBotClient.SendTextMessageAsync(
                chatId: Secret.ChatId,
                text: message,
                disableNotification: false);
            if(responceMessage != null)
            {
                return true;
            }
            return false;
        }

        //public async Task<long> GetChatIdAsync()
        //{
        //    string reqPath = String.Concat(baseUri, "bot", Secret.BotToken, "/getUpdates");
        //    Uri request = new Uri(reqPath);
        //    HttpClient httpClient = new HttpClient();
        //    var responce = await httpClient.GetAsync(request);
        //    var updateResponce = JsonConvert.DeserializeObject<Root>(await responce.Content.ReadAsStringAsync());
        //    return updateResponce.Result.FirstOrDefault().Message.Chat.Id;
        //}
    }
}
