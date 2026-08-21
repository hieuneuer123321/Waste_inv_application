using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Waste_inv_application.Properties;
using Waste_inv_application.Properties;
public static class LanguageManager
{
    // 0: Tiếng Việt, 1: Tiếng Trung
    public static int CurrentLanguageIndex { get; set; } = 0;

    // Từ điển từ vựng tập trung (Key -> [Tiếng Việt, Tiếng Trung])
    private static readonly Dictionary<string, string[]> Dictionary = new Dictionary<string, string[]>()
    {
        // --- Form Login (Form1) ---
        { "Lbl_Header", new string[] { "Chương trình quản lý rác thải", "廢料管理程式" } },
        { "Form_Login", new string[] { "Đăng nhập hệ thống", "系統登入" } },
        { "Lbl_Username", new string[] { "Tài khoản:", "帳號:" } },
        { "Lbl_Password", new string[] { "Mật khẩu:", "密碼:" } },
        { "Lbl_Lang", new string[] { "Ngôn ngữ:", "語言:" } },
        { "Btn_Login", new string[] { "Đăng nhập", "登入" } },
        { "Btn_Logout", new string[] { "Đăng xuất", "登出" } },
        { "Btn_Exit", new string[] { "Thoát", "退出" } },

        // --- Form Chính (CounterForm) ---
        { "Form_Counter", new string[] { "Hệ thống Quản lý Phế liệu", "廢料管理程式" } },
        { "Lbl_LoginHeader", new string[] { "🗑 HỆ THỐNG QUẢN LÝ RÁC THẢI", "🗑 廢料管理程式" } }, // Đã bổ sung chuẩn xác
        { "Lbl_Department", new string[] { "Phòng ban (Dept):", "部門 (Dept):" } },
        { "Lbl_DateReport", new string[] { "Ngày báo cáo:", "輸入日期:" } },
        { "Lbl_TypeWaste", new string[] { "Loại rác thải:", "垃圾類別:" } },
        { "Lbl_Quantity", new string[] { "Số lượng (thùng):", "數量(桶):" } },
        { "Lbl_Weight", new string[] { "Trọng lượng (kg):", "重量 (kg):" } },
        { "Lbl_Action", new string[] { "Hành động:", "作業:" } },

        { "Btn_Save", new string[] { "💾 Lưu", "💾 儲存" } },
        { "Btn_Clear", new string[] { "❌ Xóa form", "❌ 清空表單" } },
        { "Btn_Reload", new string[] { "↻ Làm mới", "↻ 重新整理" } },

        { "Grp_ReportList", new string[] { "Danh sách báo cáo rác thải", "廢料報表清單" } },
        { "Lbl_TotalRowsTitle", new string[] { "Tổng số dòng:", "總行數:" } },
        //{ "Lbl_TotalQtyTitle", new string[] { "Tổng số lượng:", "总数量:" } },
        //{ "Lbl_TotalWeightTitle", new string[] { "Tổng trọng lượng (kg):", "总重量 (kg):" } },
        { "Lbl_StatusReady", new string[] { "Sẵn sàng", "準備" } },

        // --- DataGridView Headers (Map theo Name của cột) ---
        { "Col_Cancel", new string[] { "", "" } },
        { "Col_Dept", new string[] { "Phòng ban", "部門" } },
        { "Col_Date", new string[] { "Ngày BC", "輸入日期" } },
        { "Col_Type", new string[] { "Loại rác", "垃圾類別" } },
        { "Col_Qty", new string[] { "Số lượng (thùng)", "數量(桶)" } },
        { "Col_Weight", new string[] { "Trọng lượng (Kg)", "重量 (Kg)" } },
        { "Col_Action", new string[] { "Hành động", "作業" } },
        { "Lbl_GeneralQty", new string[] { "Tổng số lượng rác thải (thùng):", "固態-總數量 (桶):" } },
        { "Lbl_GeneralWeight", new string[] { "Tổng khối lượng Rác thải (Kg):", "固態-總重量 (Kg):" } },
        { "Lbl_WaterQty", new string[] { "Tổng số lượng nước thải (thùng):", "液態-總數量 (桶):" } },
        { "Lbl_WaterWeight", new string[] { "Tổng khối lượng nước thải (Kg):", "液態-總重量 (Kg):" } },
        { "Lbl_Qty_Total", new string[] { "Tổng số lượng (thùng):", "總數量 (桶):" } },
        { "Lbl_Weight_Total", new string[] { "Tổng khối lượng (Kg):", "總重量 (Kg):" } },
    };

    public static string GetText(string key)
    {
        string[] values;
        if (Dictionary.TryGetValue(key, out values))
        {
            if (CurrentLanguageIndex >= 0 && CurrentLanguageIndex < values.Length)
            {
                return values[CurrentLanguageIndex];
            }
        }
        return key;
    }

    public static void ApplyLanguage(Form form)
    {
        if (form == null) return;

        // 1. Cập nhật tiêu đề Form nếu có Tag
        if (form.Tag != null)
        {
            form.Text = GetText(form.Tag.ToString());
        }

        // 2. Dùng Stack để duyệt giao diện
        Stack<Control.ControlCollection> stack = new Stack<Control.ControlCollection>();
        stack.Push(form.Controls);

        while (stack.Count > 0)
        {
            Control.ControlCollection controls = stack.Pop();
            foreach (Control ctrl in controls)
            {
                if (ctrl.Tag != null)
                {
                    ctrl.Text = GetText(ctrl.Tag.ToString());
                }

                // --- Xử lý thông minh cho DataGridView (Tự bắt theo Tên cột Name) ---
                DataGridView dgv = ctrl as DataGridView;
                if (dgv != null)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        string translationKey = null;

                        // Ưu tiên nếu cột có gán Tag riêng
                        if (col.Tag != null)
                        {
                            translationKey = col.Tag.ToString();
                        }
                        else
                        {
                            // Tự động map dựa vào Name của cột trong thiết kế của bạn
                            if (col.Name == "colIsCancel") translationKey = "Col_Cancel";
                            else if (col.Name == "colDepartment") translationKey = "Col_Dept";
                            else if (col.Name == "colDateReport") translationKey = "Col_Date";
                            else if (col.Name == "colTypeWaste") translationKey = "Col_Type";
                            else if (col.Name == "colQuantityWaste") translationKey = "Col_Qty";
                            else if (col.Name == "colWeightWaste") translationKey = "Col_Weight";
                            else if (col.Name == "colAction") translationKey = "Col_Action";
                        }

                        if (!string.IsNullOrEmpty(translationKey))
                        {
                            col.HeaderText = GetText(translationKey);
                        }
                    }
                }

                // Xử lý riêng cho StatusStrip
                StatusStrip statusStrip = ctrl as StatusStrip;
                if (statusStrip != null)
                {
                    foreach (ToolStripItem item in statusStrip.Items)
                    {
                        if (item.Tag != null)
                        {
                            item.Text = GetText(item.Tag.ToString());
                        }
                    }
                }

                if (ctrl.HasChildren)
                {
                    stack.Push(ctrl.Controls);
                }
            }
        }
    }

    /// <summary>
    /// Khởi tạo và đồng bộ ComboBox ngôn ngữ chung cho các Form
    /// </summary>
    private static ImageList _flagImageList;

    public static void InitLanguageComboBox(ComboBox cbo, Form form)
    {
        if (cbo == null) return;

        // 1. Khởi tạo ImageList chứa icon cờ (chỉ chạy 1 lần)
        if (_flagImageList == null)
        {
            _flagImageList = new ImageList();
            _flagImageList.ImageSize = new System.Drawing.Size(20, 14); // Kích thước hiển thị cờ

            try
            {
                // Lấy ảnh từ Resources của dự án
                _flagImageList.Images.Add("vn", Resources.vietnam);
                _flagImageList.Images.Add("tw", Resources.tw);
            }
            catch
            {
                // Phòng hờ nếu chưa add ảnh vào Resource thì bỏ qua không lỗi app
            }
        }

        // 2. Cấu hình ComboBox để bật chế độ vẽ tùy chỉnh (OwnerDraw)
        cbo.DrawMode = DrawMode.OwnerDrawFixed;
        cbo.DropDownStyle = ComboBoxStyle.DropDownList; // Đã sửa từ ComboBoxDropDownStyle thành ComboBoxStyle
        cbo.ItemHeight = 22; // Chỉnh chiều cao mỗi dòng cho đẹp

        if (cbo.Items.Count == 0)
        {
            cbo.Items.Clear();
            cbo.Items.Add("Tiếng Việt");
            cbo.Items.Add("中文");
        }

        // Đăng ký sự kiện vẽ từng dòng (DrawItem)
        cbo.DrawItem -= Cbo_DrawItem;
        cbo.DrawItem += Cbo_DrawItem;

        // Đồng bộ Index hiện tại
        cbo.SelectedIndexChanged -= CboLanguage_SelectedIndexChanged;
        cbo.SelectedIndex = (CurrentLanguageIndex >= 0 && CurrentLanguageIndex < cbo.Items.Count) ? CurrentLanguageIndex : 0;
        cbo.SelectedIndexChanged += CboLanguage_SelectedIndexChanged;

        ApplyLanguage(form);
    }

    // Hàm vẽ hình cờ và chữ tương ứng cho từng dòng trong ComboBox
    private static void Cbo_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0) return;

        e.DrawBackground();
        e.DrawFocusRectangle();

        ComboBox cbo = sender as ComboBox;
        string text = cbo.Items[e.Index].ToString();

        // Vẽ hình ảnh cờ phía trước nếu có đủ ảnh trong ImageList
        if (_flagImageList != null && e.Index < _flagImageList.Images.Count)
        {
            Image img = _flagImageList.Images[e.Index];
            int imgY = e.Bounds.Y + (e.Bounds.Height - img.Height) / 2; // Căn giữa theo chiều dọc
            e.Graphics.DrawImage(img, e.Bounds.X + 4, imgY);

            // Vẽ chữ lệch sang phải để nhường chỗ cho cờ
            using (var brush = new System.Drawing.SolidBrush(e.ForeColor))
            {
                int textX = e.Bounds.X + img.Width + 10;
                int textY = e.Bounds.Y + (e.Bounds.Height - e.Font.Height) / 2;
                e.Graphics.DrawString(text, e.Font, brush, textX, textY);
            }
        }
        else
        {
            // Fallback nếu không có ảnh
            using (var brush = new System.Drawing.SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds.X + 4, e.Bounds.Y + 2);
            }
        }
    }

    private static void CboLanguage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ComboBox cbo = sender as ComboBox;
        if (cbo != null)
        {
            CurrentLanguageIndex = cbo.SelectedIndex;
            Form parentForm = cbo.FindForm();
            if (parentForm != null)
            {
                ApplyLanguage(parentForm);
            }
        }
    }
}