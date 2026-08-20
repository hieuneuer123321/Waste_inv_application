using System;
using System.Windows.Forms;
using Waste_inv_application;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        bool isRunning = true;
        while (isRunning)
        {
            Form1 login = new Form1();

            // Khi Form1 đóng và trả về DialogResult.OK (đăng nhập thành công)
            if (login.ShowDialog() == DialogResult.OK)
            {
                CounterForm mainForm = new CounterForm();
                DialogResult result = mainForm.ShowDialog(); // Mở form chính và đợi đóng

                // Kiểm tra kết quả trả về từ CounterForm:
                if (result == DialogResult.Retry)
                {
                    // Nếu là Đăng xuất -> Vòng lặp while tiếp tục chạy -> Hiện lại Form1 mới tinh
                    continue;
                }
                else
                {
                    // Nếu là bấm dấu X (DialogResult.Cancel) -> Thoát hẳn vòng lặp, tắt app
                    break;
                }
            }
            else
            {
                // Tắt form login (dấu X ở login) thì thoát hẳn
                break;
            }
        }
    }
}