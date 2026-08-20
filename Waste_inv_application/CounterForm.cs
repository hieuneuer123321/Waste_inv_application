using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Waste_inv_application.Helpers;

namespace Waste_inv_application
{
    public partial class CounterForm : Form
    {
        public CounterForm()
        {
            InitializeComponent();
            // Đăng ký sự kiện Shown để load dữ liệu sau khi form đã hiện lên màn hình
            this.Shown += CounterForm_Shown;
        }

        private void CounterForm_Load(object sender, EventArgs e)
        {
            LanguageManager.InitLanguageComboBox(cboLanguage, this);
            dgvResults.CellBeginEdit += dgvResults_CellBeginEdit;
            string user = !string.IsNullOrEmpty(UserSession.CurrentUsername) ? UserSession.CurrentUsername.Trim().ToUpper() : "CM100";
            lblHeaderUser.Text = txtDepartment.Text = user;
            lblStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.cboAction.SelectedIndex = 0;
            this.cboTypeWaste.SelectedIndex = 0;

            // Không gọi Load dữ liệu nặng ở đây để tránh đơ form lúc mở
        }

        // Sự kiện chạy ngay sau khi Form hiển thị hoàn tất lên màn hình
        private async void CounterForm_Shown(object sender, EventArgs e)
        {
            if (lblStatus != null) lblStatus.Text = "Đang kết nối và tải dữ liệu...";
            dgvResults.Enabled = false; // Tạm khóa lưới trong lúc đang nạp dữ liệu

            // Gọi hàm load dữ liệu bất đồng bộ (không làm đơ UI)
            await LoadDataToGridViewAsync();

            dgvResults.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDataToDatabase();
        }

        // --- HÀM ÁNH XẠ TYPE_WASTE TỪ GIAO DIỆN XUỐNG DATABASE ---
        // --- HÀM ÁNH XẠ TYPE_WASTE TỪ GIAO DIỆN XUỐNG DATABASE ---
        private string GetTypeWasteCode()
        {
            string text = cboTypeWaste.Text.Trim();

            // Kiểm tra dựa trên Index hoặc Text để đảm bảo chính xác
            // Index 0: Rác thải / 固態
            // Index 1: Nước thải / 液態
            if (cboTypeWaste.SelectedIndex == 1 || text.Contains("液態") || text.Contains("Nước"))
            {
                return "WATER"; // Mã database cho nước thải
            }

            return "GENERAL"; // Mặc định là Rác thải / 固態
        }

        // --- HÀM ÁNH XẠ TYPE_WASTE TỪ DATABASE LÊN HIỂN THỊ TRÊN LƯỚI ---
        private string GetTypeWasteDisplay(string dbCode)
        {
            if (string.IsNullOrEmpty(dbCode)) return "";
            dbCode = dbCode.Trim().ToUpper();

            bool isChinese = (LanguageManager.CurrentLanguageIndex == 1);

            if (dbCode == "WATER")
            {
                return isChinese ? "液態" : "Nước thải";
            }
            else // Trường hợp GENERAL hoặc các mã khác
            {
                return isChinese ? "固態" : "Rác thải";
            }
        }
        private string GetActionDisplay(int actionVal)
        {
            bool isChinese = (LanguageManager.CurrentLanguageIndex == 1);

            if (actionVal == 1) // Nhập kho
            {
                return isChinese ? "入庫" : "Nhập kho";
            }
            else // Xuất kho
            {
                return isChinese ? "出庫" : "Xuất kho";
            }
        }
        // --- HÀM CẬP NHẬT TỔNG (Dùng chung cho cả Lưu và Hủy) ---
        private void UpdateUserTotals(OracleConnection conn, OracleTransaction trans, string dept)
        {
            string sqlCalc = $@"SELECT 
                NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Quantity_waste} ELSE -{DbSchema.Wastes.COL_Quantity_waste} END), 0),
                NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Weight_waste} ELSE -{DbSchema.Wastes.COL_Weight_waste} END), 0)
                FROM {DbSchema.Wastes.TABLE_NAME} WHERE UPPER({DbSchema.Wastes.COL_Department}) = :dept AND {DbSchema.Wastes.COL_Is_cancel} = 'N'";

            long qty = 0, weight = 0;
            using (var cmd = new OracleCommand(sqlCalc, conn) { Transaction = trans, BindByName = true })
            {
                cmd.Parameters.Add("dept", OracleDbType.Varchar2).Value = dept;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        qty = reader.GetInt64(0);
                        weight = reader.GetInt64(1);
                    }
                }
            }

            string sqlUpdate = $@"UPDATE {DbSchema.Users.TABLE_NAME} SET {DbSchema.Users.COL_Quantity_waste_total} = :qty, 
                                {DbSchema.Users.COL_Weight_waste_total} = :weight, {DbSchema.Users.COL_Last_update_date} = SYSDATE 
                                WHERE UPPER({DbSchema.Users.COL_Username}) = :dept";
            using (var cmd = new OracleCommand(sqlUpdate, conn) { Transaction = trans, BindByName = true })
            {
                cmd.Parameters.Add("qty", OracleDbType.Int64).Value = qty;
                cmd.Parameters.Add("weight", OracleDbType.Int64).Value = weight;
                cmd.Parameters.Add("dept", OracleDbType.Varchar2).Value = dept;
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveDataToDatabase()
        {
            if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                MessageBox.Show("Vui lòng nhập Phòng ban!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDepartment.Focus();
                return;
            }

            if (numQuantityWaste.Value <= 0 || numWeightWaste.Value <= 0)
            {
                MessageBox.Show("Số lượng và khối lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dept = txtDepartment.Text.Trim().ToUpper();
            long qtyCurrent = (long)numQuantityWaste.Value;
            long weightCurrent = (long)numWeightWaste.Value;
            bool isOutput = cboAction.SelectedIndex == 1; // Index 1 là Xuất kho
            if (isOutput)
            {
                string typeCode = GetTypeWasteCode();
                var stock = GetStockForType(dept, typeCode);
                string typeName = (typeCode == "WATER") ? "液態 (Nước thải)" : "固態 (Rác thải)";

                if (qtyCurrent > stock.qty || weightCurrent > stock.weight)
                {
                    MessageBox.Show($"Không đủ tồn kho cho: {typeName}\n" +
                                    $"Tồn hiện tại: {stock.qty} thùng / {stock.weight} kg",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn lưu dữ liệu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string user = lblHeaderUser.Text.Substring(0, Math.Min(lblHeaderUser.Text.Length, 6));
            string typeWasteCode = GetTypeWasteCode(); // Lấy mã chuẩn tiếng Anh (GENERAL, WATER,...)

            try
            {
                DatabaseHelper.ExecuteTransaction((conn, trans) =>
                {
                    string sql = $@"INSERT INTO {DbSchema.Wastes.TABLE_NAME} (
                            {DbSchema.Wastes.COL_Department}, {DbSchema.Wastes.COL_Type_waste}, 
                            {DbSchema.Wastes.COL_Quantity_waste}, {DbSchema.Wastes.COL_Weight_waste}, 
                            {DbSchema.Wastes.COL_Action}, {DbSchema.Wastes.COL_Created_by}, 
                            {DbSchema.Wastes.COL_Date_report}, {DbSchema.Wastes.COL_Is_cancel}) 
                            VALUES (:d, :t, :q, :w, :a, :u, :r, 'N')";

                    using (var cmd = new OracleCommand(sql, conn) { Transaction = trans, BindByName = true })
                    {
                        cmd.Parameters.Add("d", OracleDbType.Varchar2).Value = dept;
                        cmd.Parameters.Add("t", OracleDbType.Varchar2).Value = typeWasteCode; // Lưu mã chuẩn vào DB
                        cmd.Parameters.Add("q", OracleDbType.Int64).Value = (long)numQuantityWaste.Value;
                        cmd.Parameters.Add("w", OracleDbType.Int64).Value = (long)numWeightWaste.Value;
                        cmd.Parameters.Add("a", OracleDbType.Int32).Value = cboAction.SelectedIndex == 0 ? 1 : 0;
                        cmd.Parameters.Add("u", OracleDbType.Varchar2).Value = user;
                        cmd.Parameters.Add("r", OracleDbType.Date).Value = dtpDateReport.Value.Date;
                        cmd.ExecuteNonQuery();
                    }
                    UpdateUserTotals(conn, trans, dept);
                });

                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ = LoadDataToGridViewAsync(); // Load lại dữ liệu bất đồng bộ

                numQuantityWaste.Value = 0;
                numWeightWaste.Value = 0;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu DB: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool CancelWasteRecord(long id)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn HỦY bản ghi này?\nThao tác này sẽ làm thay đổi tổng tồn kho hiện tại.",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes) return false;

            string dept = txtDepartment.Text.Trim().ToUpper();
            try
            {
                DatabaseHelper.ExecuteTransaction((conn, trans) =>
                {
                    string sql = $@"UPDATE {DbSchema.Wastes.TABLE_NAME} SET {DbSchema.Wastes.COL_Is_cancel} = 'Y' WHERE {DbSchema.Wastes.COL_Uid} = :id";
                    using (var cmd = new OracleCommand(sql, conn) { Transaction = trans, BindByName = true })
                    {
                        cmd.Parameters.Add("id", OracleDbType.Int64).Value = id;
                        cmd.ExecuteNonQuery();
                    }
                    UpdateUserTotals(conn, trans, dept);
                });

                MessageBox.Show("Đã hủy bản ghi và cập nhật tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ = LoadDataToGridViewAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy bản ghi: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private (long qty, long weight) GetStockForType(string dept, string type)
        {
            // Hàm này chỉ lấy đúng số lượng của 1 loại cụ thể để kiểm tra lúc xuất
            DataTable dt = GetTotalStockByDepartment(dept);
            foreach (DataRow row in dt.Rows)
            {
                if (row[DbSchema.Wastes.COL_Type_waste].ToString() == type)
                    return (Convert.ToInt64(row["qty"]), Convert.ToInt64(row["weight"]));
            }
            return (0, 0);
        }
        private DataTable GetTotalStockByDepartment(string department)
        {
            // Truy vấn này lấy tổng của từng loại (GENERAL và WATER)
            string sql = $@"SELECT {DbSchema.Wastes.COL_Type_waste}, 
                    SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Quantity_waste} ELSE -{DbSchema.Wastes.COL_Quantity_waste} END) as qty,
                    SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Weight_waste} ELSE -{DbSchema.Wastes.COL_Weight_waste} END) as weight
                   FROM {DbSchema.Wastes.TABLE_NAME}
                   WHERE UPPER({DbSchema.Wastes.COL_Department}) = UPPER(:dept) 
                     AND {DbSchema.Wastes.COL_Is_cancel} = 'N'
                   GROUP BY {DbSchema.Wastes.COL_Type_waste}";

            return DatabaseHelper.ExecuteQuery(sql, new OracleParameter[] { new OracleParameter("dept", department) });
        }

        // Hàm nạp dữ liệu chạy ngầm bất đồng bộ
        // Hàm nạp dữ liệu chạy ngầm bất đồng bộ
        private async Task LoadDataToGridViewAsync()
        {
            string dept = lblHeaderUser.Text.Substring(0, Math.Min(lblHeaderUser.Text.Length, 6));

            DataTable dt = await Task.Run(() =>
            {
                // 1. Thêm điều kiện AND Is_cancel = 'N' để các dòng đã hủy không bao giờ hiện lên lưới
                string sql = $@"SELECT {DbSchema.Wastes.COL_Uid}, {DbSchema.Wastes.COL_Department}, {DbSchema.Wastes.COL_Date_report}, {DbSchema.Wastes.COL_Type_waste}, 
                    {DbSchema.Wastes.COL_Quantity_waste}, {DbSchema.Wastes.COL_Weight_waste}, {DbSchema.Wastes.COL_Is_cancel}, {DbSchema.Wastes.COL_Action} 
                    FROM {DbSchema.Wastes.TABLE_NAME} 
                    WHERE UPPER({DbSchema.Wastes.COL_Department}) = :dept 
                      AND {DbSchema.Wastes.COL_Is_cancel} = 'N' 
                    ORDER BY {DbSchema.Wastes.COL_Uid} DESC";

                return DatabaseHelper.ExecuteQuery(sql, new OracleParameter[] { new OracleParameter("dept", dept) });
            });

            dgvResults.Rows.Clear();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string isCancelStr = row[DbSchema.Wastes.COL_Is_cancel] != DBNull.Value ? row[DbSchema.Wastes.COL_Is_cancel].ToString().Trim() : "N";
                    bool isChecked = (isCancelStr == "Y" || isCancelStr == "1");

                    string dateReportStr = row[DbSchema.Wastes.COL_Date_report] != DBNull.Value ? Convert.ToDateTime(row[DbSchema.Wastes.COL_Date_report]).ToString("dd/MM/yyyy") : "";
                    int actionVal = row[DbSchema.Wastes.COL_Action] != DBNull.Value ? Convert.ToInt32(row[DbSchema.Wastes.COL_Action]) : 1;

                    string dbTypeWaste = row[DbSchema.Wastes.COL_Type_waste] != DBNull.Value ? row[DbSchema.Wastes.COL_Type_waste].ToString() : "GENERAL";
                    string displayTypeWaste = GetTypeWasteDisplay(dbTypeWaste);

                    int r = dgvResults.Rows.Add(
                        isChecked,
                        row[DbSchema.Wastes.COL_Department] != DBNull.Value ? row[DbSchema.Wastes.COL_Department].ToString() : dept,
                        dateReportStr,
                        displayTypeWaste,
                        row[DbSchema.Wastes.COL_Quantity_waste] != DBNull.Value ? Convert.ToDecimal(row[DbSchema.Wastes.COL_Quantity_waste]) : 0,
                        row[DbSchema.Wastes.COL_Weight_waste] != DBNull.Value ? Convert.ToDecimal(row[DbSchema.Wastes.COL_Weight_waste]) : 0,
                        GetActionDisplay(actionVal) // Hiển thị Nhập/Xuất kho theo đúng ngôn ngữ (Việt/Trung)
                    );
                    dgvResults.Rows[r].Tag = row[DbSchema.Wastes.COL_Uid];

                    if (isChecked)
                    {
                        dgvResults.Rows[r].Cells["colIsCancel"].ReadOnly = true;
                    }
                }
            }

            // 2. Tính toán và cập nhật tổng tồn kho cho 2 loại (固態 / 液態)
            DataTable dtTotal = await Task.Run(() => GetTotalStockByDepartment(dept));
            long totalQtyGeneral = 0, totalWeightGeneral = 0;
            long totalQtyWater = 0, totalWeightWater = 0;

            foreach (DataRow row in dtTotal.Rows)
            {
                string type = row[DbSchema.Wastes.COL_Type_waste].ToString();
                long q = Convert.ToInt64(row["qty"]);
                long w = Convert.ToInt64(row["weight"]);

                if (type == "WATER") { totalQtyWater = q; totalWeightWater = w; }
                else { totalQtyGeneral = q; totalWeightGeneral = w; }
            }

            // 3. Đưa lên giao diện các Label tương ứng (Bạn hãy đổi tên lbl... cho khớp với thiết kế của bạn nhé)
            // Ví dụ hiển thị: Số lượng và Khối lượng cho Rác thải (固態) và Nước thải (液態)
            if (lblGeneralQty != null) lblGeneralQty.Text = totalQtyGeneral.ToString("N0");
            if (lblGeneralWeight != null) lblGeneralWeight.Text = totalWeightGeneral.ToString("N0");

            if (lblWaterQty != null) lblWaterQty.Text = totalQtyWater.ToString("N0");
            if (lblWaterWeight != null) lblWaterWeight.Text = totalWeightWater.ToString("N0");

            lblSelectedSamplesVal.Text = dgvResults.Rows.Count.ToString();
            if (lblStatus != null)
            {
                lblStatus.Text = $"Đã nạp {dgvResults.Rows.Count} bản ghi hợp lệ của bộ phận {dept}.";
            }
        }

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvResults.Columns[e.ColumnIndex].Name != "colIsCancel") return;

            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.ReadOnly) return;

            dgvResults.EndEdit();

            bool isChecked = false;
            if (cell.Value != null && cell.Value != DBNull.Value)
            {
                if (cell.Value is bool b) isChecked = b;
                else if (cell.Value.ToString().ToUpper() == "Y" || cell.Value.ToString().ToUpper() == "TRUE" || cell.Value.ToString() == "1") isChecked = true;
            }

            if (isChecked)
            {
                var tagValue = dgvResults.Rows[e.RowIndex].Tag;
                if (tagValue != null)
                {
                    long recordId = Convert.ToInt64(tagValue);
                    bool isSuccess = CancelWasteRecord(recordId);

                    if (isSuccess)
                    {
                        cell.ReadOnly = true;
                    }
                    else
                    {
                        dgvResults.CellValueChanged -= dgvResults_CellContentClick;
                        cell.Value = false;
                        dgvResults.CellValueChanged += dgvResults_CellContentClick;
                    }
                }
            }
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống?", "Xác nhận đăng xuất",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserSession.CurrentUsername = null;
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            numQuantityWaste.Value = 0;
            numWeightWaste.Value = 0;
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            if (lblStatus != null)
            {
                lblStatus.Text = "Đang làm mới dữ liệu...";
            }
            dgvResults.Enabled = false;

            await LoadDataToGridViewAsync();

            dgvResults.Enabled = true;
        }

        private void CounterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguageManager.CurrentLanguageIndex = cboLanguage.SelectedIndex;
            LanguageManager.ApplyLanguage(this);

            // Khi đổi ngôn ngữ, gọi load lại lưới để tự động dịch lại cột Loại rác theo ngôn ngữ mới
            _ = LoadDataToGridViewAsync();
        }

        private void dgvResults_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvResults.Columns[e.ColumnIndex].Name != "colIsCancel")
            {
                e.Cancel = true;
            }
        }
    }
}