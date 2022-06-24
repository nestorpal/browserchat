using BrowserChat.Bot.AsyncServices;
using BrowserChat.Entity;
using BrowserChat.Value;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BrowserChat.Bot.Application.Strategy
{
    public class StockCommand : CommandBase, ICommandStrategy
    {
        private readonly IConfiguration _config;
        private string _stockApi = string.Empty;
        private string _stockDataKey = string.Empty;

        public StockCommand(IConfiguration config, BotResponsePublisher publisher) : base(publisher)
        {
            _config = config;
            _stockApi = _config.GetSection("StockCommand").GetValue<string>("Api");
            _stockDataKey = _config.GetSection("StockCommand").GetValue<string>("DataKey").ToLower();
        }

        public void ProcessCommand(BotRequest request)
        {
            bool result = false;
            string resultMessage = string.Empty;

            try
            {
                string requestUrl = _stockApi.Replace("{stock_code}", request.Value);

                var responseStream =
                    new MemoryStream(
                        new WebClient().DownloadData(
                            requestUrl
                        )
                    );

                string responseContent = Encoding.ASCII.GetString(responseStream.ToArray());
                if (!string.IsNullOrEmpty(responseContent))
                {
                    List<string> csvHeader = responseContent.Split("\r\n")[0].Split(",").Select(h => h.ToLower()).ToList();
                    List<string> csvValue = responseContent.Split("\r\n")[1].Split(",").ToList();

                    int dataKeyIndex = csvHeader.IndexOf(_stockDataKey);
                    
                    if (dataKeyIndex >= 0)
                    {
                        string stockValueStr = csvValue[dataKeyIndex];

                        if (IsValidNumber(stockValueStr))
                        {
                            resultMessage = $"{request.Value.ToUpper()} quote is ${csvValue[dataKeyIndex]} per share";
                            result = true;
                        }
                        else
                        {
                            resultMessage = $"{request.Value.ToUpper()} quote data not defined";
                        }
                    }
                    else
                    {
                        resultMessage = $"Could not get quote value for {request.Value.ToUpper()}";
                    }
                }
            }
            catch
            {
                resultMessage = $"{request.Value.ToUpper()} is not a valid value";
            }


            Publish(
                new BotResponse
                {
                    RoomId = request.RoomId,
                    Message = resultMessage,
                    CommandError = !result,
                    CommandErrorType = !result ? BotCommandErrorType.InvalidValue : 0
                }
            );
        }

        private bool IsValidNumber(string value)
        {
            Regex regex = new Regex("[^a-zA-Z/]");
            return regex.Match(value).Success;
        }
    }
}
