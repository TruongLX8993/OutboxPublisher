using System.Threading;
using NUnit.Framework;

namespace Publisher.Test
{
    public class Tests
    {
        private const string ConnectionString =
            "Data Source=(DESCRIPTION =(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.15)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ONEMES3BC_NEW)));Password=O1234#ONEMES3BC_NEW;User ID=ONEMES3BC_NEW;Connection Lifetime=0;Connection Timeout=30;Max Pool Size=100;Incr Pool Size=5;Decr Pool Size=1;";

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
            Thread.Sleep(5000);
            _handler.Stop();
        }
    }
}