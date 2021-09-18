using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Oracle.ManagedDataAccess.Client;

namespace Publisher
{
    public class OracleOutboxEventHandler

    {
        private readonly string _connectionString;
        private OracleConnection _connection;
        private bool _flag;

        public OracleOutboxEventHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Start()
        {
            _connection = (OracleConnection)OracleClientFactory.Instance.CreateConnection();
            _connection.ConnectionString = _connectionString;
            _connection.Open();

            var cmd = (OracleCommand)_connection.CreateCommand();
            cmd.CommandText = "select * from OUTBOX_EVENT";
            cmd.AddRowid = true;
            // cmd.Notification.IsNotifiedOnce = false;
            OracleDependency.Port = 1005; // default port.
            var depend = new OracleDependency(cmd);
            depend.OnChange += Handle;
            cmd.ExecuteNonQuery();

            // var tran = _connection.BeginTransaction();
            // var cmdUpdate = _connection.CreateCommand();
            // cmdUpdate.Transaction = tran;
            // cmdUpdate.CommandText = @"update OUTBOX_EVENT
            // set STATUS = STATUS + 1
            // where id = '48725bad-a25e-4ea2-961a-84f9d82fb422'";
            // cmd.ExecuteNonQuery();
            // cmdUpdate.ExecuteNonQuery();
            // tran.Commit();
            while (!_flag)
            {
                Thread.Sleep(1000);
                Console.WriteLine("nothing");
            }
        }

        private void Handle(object sender, OracleNotificationEventArgs args)
        {
            _flag = true;
            DataRow detailRow = args.Details.Rows[0];
            string rowid = detailRow["Rowid"]
                .ToString();
            // Console.WriteLine(rowid);
            // for (int i = 1; i < args.Details.Rows.Count; i++)
            // {
            //     detailRow = args.Details.Rows[i];
            //     rowid = detailRow["Rowid"]
            //         .ToString();
            //     Console.WriteLine(rowid);
            // }
            var cmdTxt = @$"update OUTBOX_EVENT
            set STATUS = STATUS + 1
            where ROWID = '{rowid}'";
            var trans = _connection.BeginTransaction();
            var cmd = new OracleCommand(cmdTxt, _connection);
            
        }


        public void Stop()
        {
            _connection?.Close();
        }
    }
}