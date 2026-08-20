using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Waste_inv_application.Helpers;


namespace Waste_inv_application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {

            // Báo cho Program.cs biết là đăng nhập thành công và đóng form login
            //this.DialogResult = DialogResult.OK;
            //this.Close();
            
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            // 1. Kiểm tra đầu vào
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_username.Focus();
                return;
            }

            try
            {
                // 2. Gọi hàm kiểm tra đăng nhập
                if (ProcessLogin(username, password))
                {                   
                    // Reset lại các ô nhập liệu trên form login nếu cần
                    txt_password.Clear();
                    txt_username.Focus();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!",
                                    "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_password.Clear();
                    txt_password.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối CSDL Oracle:\n" + ex.Message,
                                "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
        private bool ProcessLogin(string username, string password)
        {
            // 1. Truy vấn bổ sung 2 cột tổng tồn kho từ DbSchema.Users
            string sql = $@"SELECT {DbSchema.Users.COL_Uid}, 
                           {DbSchema.Users.COL_Username}, 
                           {DbSchema.Users.COL_Password},
                           {DbSchema.Users.COL_Quantity_waste_total},
                           {DbSchema.Users.COL_Weight_waste_total} 
                    FROM {DbSchema.Users.TABLE_NAME} 
                    WHERE UPPER({DbSchema.Users.COL_Username}) = UPPER(:p_user) 
                      AND {DbSchema.Users.COL_Password} = :p_pass";

            // 2. Khai báo OracleParameter chỉ định rõ kiểu Varchar2 để tránh lỗi bind tham số
            OracleParameter[] parameters = new OracleParameter[]
            {
        new OracleParameter("p_user", OracleDbType.Varchar2) { Value = username != null ? username.Trim() : "" },
        new OracleParameter("p_pass", OracleDbType.Varchar2) { Value = password ?? "" }
            };

            // 3. Thực thi truy vấn
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                int uid = Convert.ToInt32(row[DbSchema.Users.COL_Uid]);
                string dbUsername = row[DbSchema.Users.COL_Username] != DBNull.Value ? row[DbSchema.Users.COL_Username].ToString() : username;

                long qtyTotal = row[DbSchema.Users.COL_Quantity_waste_total] != DBNull.Value
                    ? Convert.ToInt64(row[DbSchema.Users.COL_Quantity_waste_total])
                    : 0;

                long weightTotal = row[DbSchema.Users.COL_Weight_waste_total] != DBNull.Value
                    ? Convert.ToInt64(row[DbSchema.Users.COL_Weight_waste_total])
                    : 0;

                // 4. Lưu toàn bộ thông tin đăng nhập và tồn kho vào UserSession
                UserSession.Login(uid, dbUsername, qtyTotal, weightTotal);

                return true; // Đăng nhập thành công
            }

            return false; // Sai tài khoản hoặc mật khẩu
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
           
            Application.Exit();
            /*
            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Kết nối tới Oracle Database 10.7.250.202 thành công!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message,
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            */
        }
    }
    
}
