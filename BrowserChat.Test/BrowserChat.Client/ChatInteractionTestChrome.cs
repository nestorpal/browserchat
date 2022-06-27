using BrowserChat.Test.BrowserChat.Client.Fixtures;
using BrowserChat.Value;
using BrowserChat.Util;
using OpenQA.Selenium;
using Xunit;

namespace BrowserChat.Test.BrowserChat.Client
{
    public class ChatInteractionTestChrome : IClassFixture<ChromeDriverFixture>
    {
        private readonly ChromeDriverFixture _fixture;
        private readonly string ClientUrl = "http://localhost:5012";
        private readonly static string EncryptionKey = "ce2ea23eb15a1914133b6a5898a4b14c";
        private readonly string DefaultRoom = EncryptRoomId("1");
        private readonly string TestPost = $"This is a test post {DateTime.Now.ToString("ssfff")}";
        private readonly string ValidTestCommand = "/stock=aapl.us";
        private readonly string ValidTestCommandResult = "AAPL.US quote is";
        private readonly string InvalidTestCommand = "/xxx=yyyy";
        private readonly string InvalidTestCommandResult = Constant.MessagesAndExceptions.Bot.Other.InvalidCommand;

        public ChatInteractionTestChrome(ChromeDriverFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("4")]
        public void VerifyPostIsSentToRoom(string roomId)
        {
            string postToSend = $"Room {roomId} => {TestPost}";

            TemplateMethod(EncryptRoomId(roomId), postToSend, postToSend);
        }

        [Fact(Skip = "temporarily")]
        public void VerifyValidCommandIsProcessed()
        {
            TemplateMethod(DefaultRoom, ValidTestCommand, ValidTestCommandResult);
        }

        [Fact(Skip = "temporarily")]
        public void VerifyInvalidCommandIsNotProcessed()
        {
            TemplateMethod(DefaultRoom, InvalidTestCommand, InvalidTestCommandResult);
        }

        private void TemplateMethod(
            string roomId,
            string input,
            string output)
        {
            _fixture.Driver.GoToUrl(ClientUrl);
            LoginUser();
            SelectRoom(roomId);
            SendPost(roomId, input);
            AsserPostIsReceived(roomId, output);
            LogoutUser();
        }

        private void LoginUser()
        {
            var emailInput = _fixture.Driver.WaitAndFindElement(By.XPath("//input[@name='Email']"));
            emailInput.Clear();
            emailInput.SendKeys(Constant.Samples.BaseUser.Email);

            var passwordInput = _fixture.Driver.WaitAndFindElement(By.XPath("//input[@name='Password']"));
            passwordInput.Clear();
            passwordInput.SendKeys(Constant.Samples.BaseUser.Password);
            passwordInput.SendKeys(Keys.Enter);

            emailInput = null;
            passwordInput = null;
        }

        private void SelectRoom(string roomId)
        {
            _fixture.Driver.WaitAndFindElement(By.XPath($"//li[@class='list-group-item'][@room-id='{roomId}']/a")).Click();
        }

        private void SendPost(string roomId, string post)
        {
            var input = _fixture.Driver.WaitAndFindElement(By.XPath($"//div[@class='tab-pane active show'][@room-id='{roomId}']//input"));
            input.SendKeys(post);
            
            var sendButton = _fixture.Driver.WaitAndFindElement(By.XPath($"//div[@class='tab-pane active show'][@room-id='{roomId}']//button"));
            sendButton.Click();

            input = null;
            sendButton = null;
        }

        private void AsserPostIsReceived(string roomId, string expectedResponse)
        {
            System.Threading.Thread.Sleep(1500);

            var postSpan =
                _fixture.Driver
                    .WaitAndFindElement(By.XPath($"//div[@class='tab-pane active show'][@room-id='{roomId}']//div[@class='col-12 messages']/span[last()]"));

            Assert.Contains(expectedResponse, postSpan.Text);
        }

        private void LogoutUser()
        {
            var logoutLink = _fixture.Driver.WaitAndFindElement(By.XPath("//a[@class='nav-link text-dark'][text()='Logout']"));
            logoutLink.Click();

            logoutLink = null;
        }

        private static string EncryptRoomId(string roomId)
        {
            return System.Web.HttpUtility.UrlEncode(StringCipher.Encrypt(EncryptionKey, roomId));
        }
    }
}
