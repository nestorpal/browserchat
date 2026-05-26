namespace BrowserChat.Test.BrowserChat.Client.Fixtures
{
    public class EdgeDriverFixture : DriverFixture
    {
        protected override void InitializeDriver()
        {
            Driver.Start(BrowserType.Edge);
        }

        public override int WaitForElementTimeout => 30;
    }
}
