namespace BrowserChat.Test.BrowserChat.Client.Fixtures
{
    public abstract class DriverFixture : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOOUT = 30;

        public DriverAdapter Driver { get; set; }
        public virtual int WaitForElementTimeout { get; set; } = WAIT_FOR_ELEMENT_TIMEOOUT;

        protected abstract void InitializeDriver();

        public DriverFixture()
        {
            Driver = new DriverAdapter();
            InitializeDriver();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
