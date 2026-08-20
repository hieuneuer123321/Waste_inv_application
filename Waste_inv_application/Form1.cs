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

            //Báo cho Program.cs biết là đăng nhập thành công và đóng form login
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Lbl_Header.Tag = "Lbl_Header";
            // lblUsername.Tag = "Lbl_Username";
            // lblPassword.Tag = "Lbl_Password";
            // btnLogin.Tag = "Btn_Login";
            // btnLogout.Tag = "Btn_Logout";
            // this.Tag = "Form_Login";

            // Gọi hàm dùng chung để setup trọn gói ComboBox và đồng bộ ngôn ngữ
            LanguageManager.InitLanguageComboBox(cboLanguage, this);
        }
        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa chọn gì thì thoát
            if (cboLanguage.SelectedIndex < 0) return;

            // Lưu lại lựa chọn (0: Tiếng Việt, 1: Tiếng Trung)
            LanguageManager.CurrentLanguageIndex = cboLanguage.SelectedIndex;

            // Gọi hàm quét và đổi ngôn ngữ toàn bộ Form1
            LanguageManager.ApplyLanguage(this);
        }
        private void cboLanguage_DrawItem(object sender, DrawItemEventArgs e)
        {
            //if (e.Index < 0) return;

            //e.DrawBackground();

            //// Lấy tên ngôn ngữ
            //string text = cboLanguage.Items[e.Index].ToString();

            //// Vẽ icon cờ (giả sử dùng Resource đã add vào project)
            //// Tùy theo thứ tự bạn add vào cbo: 0 là VN, 1 là Trung
            //System.Drawing.Image img = (e.Index == 0) ? Properties.Resources.vietnam: Properties.Resources.china;

            //e.Graphics.DrawImage(img, e.Bounds.X + 2, e.Bounds.Y + 2, 20, 20);
            //e.Graphics.DrawString(text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.X + 25, e.Bounds.Y + 2);

            //e.DrawFocusRectangle();
        }

      
    }
    
}
