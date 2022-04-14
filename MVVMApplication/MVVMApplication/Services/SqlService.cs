using System.Data.SqlClient;

namespace MVVMApplication.Services;

/// <summary>
/// MS SQL 전용 서비스
/// </summary>
public class SqlService : DatabaseService
{
    public SqlService(string connectionString)
        : base(connectionString)
    {
        // 기본 connection을 MS SqlConnection으로 생성합니다.
        Connection = new SqlConnection(connectionString);
        // 기본 커맨드를 MS SqlCommand로 생성합니다.
        Command = new SqlCommand();
    }
}
