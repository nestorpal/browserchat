using BrowserChat.Bot.AsyncServices;
using BrowserChat.Bot.Util;
using BrowserChat.Entity;
using BrowserChat.Value;
using CsvHelper;
using CsvHelper.Configuration;
using System.Dynamic;
using System.Globalization;
using System.Text;
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
                    string response = await GetStockByCompanyCode(request.Value);

                    if (!string.IsNullOrEmpty(response))
                    {
                        string stockValueStr = GetValueFromCSVContent(response, _stockDataKey);

                        if (!string.IsNullOrEmpty(stockValueStr))
                        {
                            if (IsValidNumber(stockValueStr))
                            {
                                resultMessage =
                                    string.Format(
                                        Constant.MessagesAndExceptions.Bot.Command.Stock.ValidResult,
                                        request.Value.ToUpper(),
                                        stockValueStr
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

        private async Task<string> GetStockByCompanyCode(string companyCode)
        {
            string result = string.Empty;

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(string.Format(_stockApi, companyCode)))
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    StreamReader reader = new StreamReader(streamToReadFrom);
                    result = reader.ReadToEnd();
                    reader.Dispose();
                }
            }

            return result;
        }

        private string GetValueFromCSVContent(string content, string property)
        {
            dynamic record;
            string? result = string.Empty;
            property = property.ToLower();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };

            using (var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(content))))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                record = csv.GetRecord<dynamic>();
            }

            if ((object)record != null)
            {
                object? propertyValue = default;
                if (DynamicHasProperty(record, property, out propertyValue))
                {
                    result = propertyValue?.ToString();
                }
            }

            return result ?? string.Empty;
        }

        private bool DynamicHasProperty(dynamic item, string propertyName, out object? propertyValue)
        {
            bool exists = false;

            if (item is ExpandoObject eo)
            {
                exists = (eo as IDictionary<string, object>).TryGetValue(propertyName, out propertyValue);
            }
            else
            {
                propertyValue = item.GetType().GetProperty(propertyName);
                exists = propertyValue != null;
            }

            return exists;
        }
    }
}
