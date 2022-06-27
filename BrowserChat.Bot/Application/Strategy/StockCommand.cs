using BrowserChat.Bot.AsyncServices;
using BrowserChat.Bot.Util;
using BrowserChat.Entity;
using BrowserChat.Value;
using System.Text.RegularExpressions;

namespace BrowserChat.Bot.Application.Strategy
{
    public class StockCommand : CommandBase, ICommandStrategy
    {
        private string _stockApi = string.Empty;
        private string _stockDataKey = string.Empty;

        public StockCommand(BotResponsePublisher publisher) : base(publisher)
        {
            _stockApi = ConfigurationHelper.StockCommand_Api;
            _stockDataKey = ConfigurationHelper.StockCommand_DataKey;
        }

        public async void ProcessCommand(BotRequest request)
        {
            bool result = false;
            string resultMessage = string.Empty;

            try
            {
                if (IsValidValue(request.Value))
                {
                    string requestUrl = string.Format(ConfigurationHelper.StockCommand_Api, request.Value);

                    string responseContent = string.Empty;

                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = await client.GetAsync(requestUrl))
                        using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                        {
                            StreamReader reader = new StreamReader(streamToReadFrom);
                            responseContent = reader.ReadToEnd();
                            reader.Dispose();
                        }
                    }

                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        List<string> csvHeader = responseContent.Split("\r\n")[0].Split(",").Select(h => h.ToLower()).ToList();
                        List<string> csvValue = responseContent.Split("\r\n")[1].Split(",").ToList();

                        int dataKeyIndex = csvHeader.IndexOf(_stockDataKey.ToLower());

                        if (dataKeyIndex >= 0)
                        {
                            string stockValueStr = csvValue[dataKeyIndex];

                            if (IsValidNumber(stockValueStr))
                            {
                                resultMessage =
                                    string.Format(
                                        Constant.MessagesAndExceptions.Bot.Command.Stock.ValidResult,
                                        request.Value.ToUpper(),
                                        csvValue[dataKeyIndex]
                                    );

                                result = true;
                            }
                            else
                            {
                                resultMessage =
                                    string.Format(
                                        Constant.MessagesAndExceptions.Bot.Command.Stock.QuoteDataNotDefined,
                                        request.Value.ToUpper()
                                    );
                            }
                        }
                        else
                        {
                            resultMessage =
                                string.Format(
                                    Constant.MessagesAndExceptions.Bot.Command.Stock.QuoteNotAvailable,
                                    request.Value.ToUpper()
                                );
                        }
                    }
                }
                else
                {
                    resultMessage =
                        string.Format(
                            Constant.MessagesAndExceptions.Bot.Command.Stock.InvalidValue,
                            request.Value
                        );
                }
            }
            catch (Exception ex)
            {
                resultMessage =
                    string.Format(
                        Constant.MessagesAndExceptions.General.UnexpectedError,
                        ex.Message.ToString()
                    );
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

        private bool IsValidValue(string value)
        {
            Regex regex = new Regex("^[a-zA-Z]+\\.[a-zA-Z]+$");
            return regex.Match(value).Success;
        }
    }
}
