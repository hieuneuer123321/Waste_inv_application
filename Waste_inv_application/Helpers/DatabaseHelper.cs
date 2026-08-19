using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Waste_inv_application.Helpers
{
    public class DatabaseHelper
    {
      
        private static string host = "10.7.250.202";
        private static string port = "1556";
        private static string serviceName = "TEST"; 
        private static string user = "apps";
        private static string password = "83387850";


        private static string connectionString =
           $"User Id={user};Password={password};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SID={serviceName})))";
        // "User Id=bi;Password=biasps83387850SF;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.1.253.206)(PORT=1521))(CONNECT_DATA=(SID=PROD)))";
        // Hàm lấy kết nối
        public static OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }
        // 3. Hàm DÙNG CHUNG để lấy dữ liệu (SELECT) -> Trả về DataTable cho DataGridView
        public static DataTable ExecuteQuery(string sql, OracleParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi truy vấn CSDL: " + ex.Message);
                }
            }
            return dt;
        }

        // 4. Hàm DÙNG CHUNG để Thêm / Sửa / Xóa (INSERT, UPDATE, DELETE) -> Trả về số dòng bị ảnh hưởng
        public static int ExecuteNonQuery(string sql, OracleParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (OracleConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi thực thi CSDL: " + ex.Message);
                }
            }
            return rowsAffected;
        }

        // 5. Hàm DÙNG CHUNG để lấy 1 giá trị đơn lẻ (như COUNT(*), MAX, SUM...)
        public static object ExecuteScalar(string sql, OracleParameter[] parameters = null)
        {
            object result = null;
            using (OracleConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        result = cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi thực thi CSDL: " + ex.Message);
                }
            }
            return result;
        }
        public static bool ExecuteTransaction(Action<OracleConnection, OracleTransaction> action)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Thực thi các câu lệnh truyền vào qua delegate
                        action(conn, trans);

                        // Nếu không lỗi thì Commit
                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        // Nếu có lỗi bất kỳ thì Rollback
                        trans.Rollback();
                        throw; // Ném lỗi ra để Form bắt và hiển thị thông báo
                    }
                }
            }
        }
    }
}

