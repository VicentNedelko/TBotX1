using gira_com_by.Logic.Nodes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace gira_com_by.Logic.Nodes
{
    public class TBot : IBot
    {
        private static readonly ITelegramBotClient telegramBotClient = new TelegramBotClient(token: Secret.BotToken);
        private readonly string baseUri = "https://api.telegram.org/";
        public async Task<long> SendMessageAsync(string message)
        {
            var responceMessage = await telegramBotClient.SendTextMessageAsync(
                chatId: Secret.ChatId,
                text: message,
                disableNotification: false);
            if(responceMessage != null)
            {
                return responceMessage.MessageId;
            }
            return -1L;
        }
        public async Task<long> GetChatIdAsync()
        {
            string reqPath = String.Concat(baseUri, "bot", Secret.BotToken, "/getUpdates");
            Uri request = new Uri(reqPath);
            HttpClient httpClient = new HttpClient();
            var responce = await httpClient.GetAsync(request);
            var updateResponce = JsonConvert.DeserializeObject<Root>(await responce.Content.ReadAsStringAsync());
            return updateResponce.Result.FirstOrDefault().Message.Chat.Id;
        }
    }
}
