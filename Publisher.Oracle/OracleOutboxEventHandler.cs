using System.ComponentModel;
using Oracle.ManagedDataAccess.Client;

namespace Publisher
{
    public class OracleOutboxEventHandler

    {
    private readonly string _connectionString;

    public OracleOutboxEventHandler(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Start()
    {
        var con = new OracleConnection(_connectionString);

        var cmd = con.CreateCommand();
        cmd.CommandText = "select * from OUTBOX_EVENT";

        OracleDependency.Port = 1005; // default port.
        var depend = new OracleDependency(cmd);
        depend.OnChange += new OnChangeEventHandler(Handle);
    
    }

    private void Handle(object sender, OracleNotificationEventArgs eventArgs)
    {

    }

    private void Listen()
    {

    }

    }
}