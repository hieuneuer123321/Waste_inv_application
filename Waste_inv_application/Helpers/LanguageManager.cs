using System.Collections.Generic;
using System.Windows.Forms;

public static class LanguageManager
{
    // 0: Tiếng Việt, 1: Tiếng Trung
    public static int CurrentLanguageIndex { get; set; } = 0;

    // Từ điển từ vựng tập trung (Key -> [Tiếng Việt, Tiếng Trung])
    private static readonly Dictionary<string, string[]> Dictionary = new Dictionary<string, string[]>()
    {
        // --- Form Login (Form1) ---
        { "Lbl_Header", new string[] { "Chương trình quản lý rác thải", "废料管理程序" } },
        { "Form_Login", new string[] { "Đăng nhập hệ thống", "系统登录" } },
        { "Lbl_Username", new string[] { "Tài khoản:", "账号:" } },
        { "Lbl_Password", new string[] { "Mật khẩu:", "密码:" } },
        { "Lbl_Lang", new string[] { "Ngôn ngữ:", "语言:" } },
        { "Btn_Login", new string[] { "Đăng nhập", "登录" } },
        { "Btn_Logout", new string[] { "Đăng xuất", "退出" } },

        // --- Form Chính (CounterForm) ---
        { "Form_Counter", new string[] { "Hệ thống Quản lý Phế liệu", "废料管理系统" } },
        { "Lbl_LoginHeader", new string[] { "🗑 HỆ THỐNG QUẢN LÝ RÁC THẢI", "🗑 废料管理系统" } }, // Đã bổ sung chuẩn xác
        { "Lbl_Department", new string[] { "Phòng ban (Dept):", "部門 (Dept):" } },
        { "Lbl_DateReport", new string[] { "Ngày báo cáo:", "輸入日期:" } },
        { "Lbl_TypeWaste", new string[] { "Loại rác thải:", "垃圾類別:" } },
        { "Lbl_Quantity", new string[] { "Số lượng:", "數量:" } },
        { "Lbl_Weight", new string[] { "Trọng lượng (kg):", "重量 (kg):" } },
        { "Lbl_Action", new string[] { "Hành động:", "作業:" } },

        { "Btn_Save", new string[] { "💾 Lưu", "💾 保存" } },
        { "Btn_Clear", new string[] { "❌ Xóa form", "❌ 清空表单" } },
        { "Btn_Reload", new string[] { "↻ Làm mới", "↻ 刷新" } },

        { "Grp_ReportList", new string[] { "Danh sách báo cáo rác thải", "废料报告列表" } },
        { "Lbl_TotalRowsTitle", new string[] { "Tổng số dòng:", "总行数:" } },
        { "Lbl_TotalQtyTitle", new string[] { "Tổng số lượng:", "总数量:" } },
        { "Lbl_TotalWeightTitle", new string[] { "Tổng trọng lượng (kg):", "总重量 (kg):" } },
        { "Lbl_StatusReady", new string[] { "Sẵn sàng", "就绪" } },

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
    public static void InitLanguageComboBox(ComboBox cbo, Form form)
    {
        if (cbo == null) return;

        if (cbo.Items.Count == 0)
        {
            cbo.Items.Clear();
            cbo.Items.Add("🇻🇳 Tiếng Việt");
            cbo.Items.Add("🇨🇳 中文");
        }

        cbo.SelectedIndexChanged -= CboLanguage_SelectedIndexChanged;
        cbo.SelectedIndex = CurrentLanguageIndex;
        cbo.SelectedIndexChanged += CboLanguage_SelectedIndexChanged;

        ApplyLanguage(form);
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