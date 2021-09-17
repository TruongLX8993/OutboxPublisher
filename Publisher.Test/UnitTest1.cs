using NUnit.Framework;

namespace Publisher.Test
{
    public class Tests
    {
        private const string ConnectionString = "";

        private OracleOutboxEventHandler _handler;

        [OneTimeSetUp]
        public void Setup()
        {
            _handler = new OracleOutboxEventHandler(ConnectionString);
        }

        [Test]
        public void Test1()
        {
            _handler.Start();
            while (true) ;
        }
    }
}