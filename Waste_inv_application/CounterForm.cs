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
        private const int MAX_USER_LENGTH = 6;

        public CounterForm()
        {
            InitializeComponent();
            this.Shown += CounterForm_Shown;
        }

        private void CounterForm_Load(object sender, EventArgs e)
        {
            LanguageManager.InitLanguageComboBox(cboLanguage, this);
            dgvResults.CellBeginEdit += dgvResults_CellBeginEdit;

            string user = !string.IsNullOrEmpty(UserSession.CurrentUsername)
                ? UserSession.CurrentUsername.Trim().ToUpper()
                : "CM100";

            lblHeaderUser.Text = txtDepartment.Text = user;
            lblStatus.ForeColor = System.Drawing.Color.DarkRed;
            cboAction.SelectedIndex = 0;
            cboTypeWaste.SelectedIndex = 0;
        }

        private async void CounterForm_Shown(object sender, EventArgs e)
        {
            await RefreshDataAsync("Đang kết nối và tải dữ liệu...");
        }

        private void btnSave_Click(object sender, EventArgs e) => SaveDataToDatabase();
        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();
        private async void btnReload_Click(object sender, EventArgs e) => await RefreshDataAsync("Đang làm mới dữ liệu...");

        private async Task RefreshDataAsync(string statusMessage)
        {
            if (lblStatus != null) lblStatus.Text = statusMessage;
            dgvResults.Enabled = false;

            await LoadDataToGridViewAsync();

            dgvResults.Enabled = true;
        }

        #region Business Logic & Database Operations

        private string GetTypeWasteCode()
        {
            string text = cboTypeWaste.Text.Trim();
            if (cboTypeWaste.SelectedIndex == 1 || text.Contains("液態") || text.Contains("Nước"))
            {
                return "WATER";
            }
            return "GENERAL";
        }

        private string GetTypeWasteDisplay(string dbCode)
        {
            if (string.IsNullOrEmpty(dbCode)) return "";
            bool isChinese = (LanguageManager.CurrentLanguageIndex == 1);
            return dbCode.Trim().ToUpper() == "WATER"
                ? (isChinese ? "液態" : "Nước thải")
                : (isChinese ? "固態" : "Rác thải");
        }

        private string GetActionDisplay(int actionVal)
        {
            bool isChinese = (LanguageManager.CurrentLanguageIndex == 1);
            return actionVal == 1
                ? (isChinese ? "入庫" : "Nhập kho")
                : (isChinese ? "出庫" : "Xuất kho");
        }

        private string GetCurrentDepartment()
        {
            string text = lblHeaderUser.Text;
            return text.Substring(0, Math.Min(text.Length, MAX_USER_LENGTH)).Trim().ToUpper();
        }

        private void UpdateUserTotals(OracleConnection conn, OracleTransaction trans, string dept)
        {
            string sqlCalc = $@"SELECT 
                NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Type_waste} = 'GENERAL' AND {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Quantity_waste} 
                             WHEN {DbSchema.Wastes.COL_Type_waste} = 'GENERAL' AND {DbSchema.Wastes.COL_Action} = 0 THEN -{DbSchema.Wastes.COL_Quantity_waste} ELSE 0 END), 0) AS q_gen,
                NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Type_waste} = 'GENERAL' AND {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Weight_waste} 
                             WHEN {DbSchema.Wastes.COL_Type_waste} = 'GENERAL' AND {DbSchema.Wastes.COL_Action} = 0 THEN -{DbSchema.Wastes.COL_Weight_waste} ELSE 0 END), 0) AS w_gen,
                NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Type_waste} = 'WATER' AND {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Quantity_waste} 
                             WHEN {DbSchema.Wastes.COL_Type_waste} = 'WATER' AND {DbSchema.Wastes.COL_Action} = 0 THEN -{DbSchema.Wastes.COL_Quantity_waste} ELSE 0 END), 0) AS q_wat,
                NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Type_waste} = 'WATER' AND {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Weight_waste} 
                             WHEN {DbSchema.Wastes.COL_Type_waste} = 'WATER' AND {DbSchema.Wastes.COL_Action} = 0 THEN -{DbSchema.Wastes.COL_Weight_waste} ELSE 0 END), 0) AS w_wat
                FROM {DbSchema.Wastes.TABLE_NAME} 
                WHERE UPPER({DbSchema.Wastes.COL_Department}) = :dept AND {DbSchema.Wastes.COL_Is_cancel} = 'N'";

            long qGen = 0, wGen = 0, qWat = 0, wWat = 0;

            using (var cmd = new OracleCommand(sqlCalc, conn) { Transaction = trans, BindByName = true })
            {
                cmd.Parameters.Add("dept", OracleDbType.Varchar2).Value = dept;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        qGen = reader.GetInt64(0);
                        wGen = reader.GetInt64(1);
                        qWat = reader.GetInt64(2);
                        wWat = reader.GetInt64(3);
                    }
                }
            }

            string sqlUpdate = $@"UPDATE {DbSchema.Users.TABLE_NAME} SET 
                                    {DbSchema.Users.COL_Qty_General} = :qGen, 
                                    {DbSchema.Users.COL_Weight_General} = :wGen,
                                    {DbSchema.Users.COL_Qty_Water} = :qWat, 
                                    {DbSchema.Users.COL_Weight_Water} = :wWat, 
                                    {DbSchema.Users.COL_Last_update_date} = SYSDATE 
                                  WHERE UPPER({DbSchema.Users.COL_Username}) = :dept";

            using (var cmd = new OracleCommand(sqlUpdate, conn) { Transaction = trans, BindByName = true })
            {
                cmd.Parameters.Add("qGen", OracleDbType.Int64).Value = qGen;
                cmd.Parameters.Add("wGen", OracleDbType.Int64).Value = wGen;
                cmd.Parameters.Add("qWat", OracleDbType.Int64).Value = qWat;
                cmd.Parameters.Add("wWat", OracleDbType.Int64).Value = wWat;
                cmd.Parameters.Add("dept", OracleDbType.Varchar2).Value = dept;
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveDataToDatabase()
        {
            if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                ShowWarning("Vui lòng nhập Phòng ban!");
                txtDepartment.Focus();
                return;
            }

            if (numQuantityWaste.Value <= 0 || numWeightWaste.Value <= 0)
            {
                ShowWarning("Số lượng và khối lượng phải lớn hơn 0!");
                return;
            }

            string dept = txtDepartment.Text.Trim().ToUpper();
            long qtyCurrent = (long)numQuantityWaste.Value;
            long weightCurrent = (long)numWeightWaste.Value;
            bool isOutput = (cboAction.SelectedIndex == 1);

            if (isOutput)
            {
                string typeCode = GetTypeWasteCode();
                var stock = GetStockForType(dept, typeCode);
                string typeName = (typeCode == "WATER") ? "液態 (Nước thải)" : "固態 (Rác thải)";

                if (qtyCurrent > stock.qty || weightCurrent > stock.weight)
                {
                    ShowWarning($"Không đủ tồn kho cho: {typeName}\nTồn hiện tại: {stock.qty} / {stock.weight}");
                    return;
                }
            }

            if (MessageBox.Show(
                GetMsg("Bạn có chắc chắn muốn lưu dữ liệu không?", "您確定要保存數據嗎？"),
                GetMsg("Xác nhận", "確認"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string user = GetCurrentDepartment();
            string typeWasteCode = GetTypeWasteCode();

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
                        cmd.Parameters.Add("t", OracleDbType.Varchar2).Value = typeWasteCode;
                        cmd.Parameters.Add("q", OracleDbType.Int64).Value = qtyCurrent;
                        cmd.Parameters.Add("w", OracleDbType.Int64).Value = weightCurrent;
                        cmd.Parameters.Add("a", OracleDbType.Int32).Value = isOutput ? 0 : 1;
                        cmd.Parameters.Add("u", OracleDbType.Varchar2).Value = user;
                        cmd.Parameters.Add("r", OracleDbType.Date).Value = dtpDateReport.Value.Date;
                        cmd.ExecuteNonQuery();
                    }
                    UpdateUserTotals(conn, trans, dept);
                });

                MessageBox.Show(
                    GetMsg("Lưu thành công!", "保存成功！"),
                    GetMsg("Thông báo", "通知"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                _ = RefreshDataAsync("Đang cập nhật lại dữ liệu...");
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                 GetMsg("Lỗi lưu DB: ", "資料庫保存錯誤: ") + ex.Message,
                 GetMsg("Lỗi Database", "資料庫錯誤"),
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error
             );
            }
        }

        private bool CancelWasteRecord(long id)
        {
            if (MessageBox.Show(
                GetMsg("Bạn có chắc chắn muốn HỦY bản ghi này?\nThao tác này sẽ làm thay đổi tổng tồn kho hiện tại.",
                       "您確定要取消此記錄嗎？\n此操作將會變更目前的庫存總量。"),
                GetMsg("Xác nhận hủy", "確認取消"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return false;
            }

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

                MessageBox.Show(
                  GetMsg("Đã hủy bản ghi và cập nhật tồn kho thành công!", "已成功取消記錄並更新庫存！"),
                  GetMsg("Thông báo", "通知"),
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information
              );
                _ = RefreshDataAsync("Đang đồng bộ dữ liệu...");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                     GetMsg("Lỗi khi hủy bản ghi: ", "取消記錄時發生錯誤: ") + ex.Message,
                     GetMsg("Lỗi Database", "資料庫錯誤"),
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                 );
                return false;
            }
        }

        private (long qty, long weight) GetStockForType(string dept, string type)
        {
            DataTable dt = GetTotalStockByDepartment(dept);
            foreach (DataRow row in dt.Rows)
            {
                if (row[DbSchema.Wastes.COL_Type_waste].ToString().Trim().ToUpper() == type)
                    return (Convert.ToInt64(row["qty"]), Convert.ToInt64(row["weight"]));
            }
            return (0, 0);
        }

        private DataTable GetTotalStockByDepartment(string department)
        {
            string sql = $@"SELECT {DbSchema.Wastes.COL_Type_waste}, 
                    SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Quantity_waste} ELSE -{DbSchema.Wastes.COL_Quantity_waste} END) as qty,
                    SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Weight_waste} ELSE -{DbSchema.Wastes.COL_Weight_waste} END) as weight
                   FROM {DbSchema.Wastes.TABLE_NAME}
                   WHERE UPPER({DbSchema.Wastes.COL_Department}) = UPPER(:dept) 
                     AND {DbSchema.Wastes.COL_Is_cancel} = 'N'
                   GROUP BY {DbSchema.Wastes.COL_Type_waste}";

            return DatabaseHelper.ExecuteQuery(sql, new OracleParameter[] { new OracleParameter("dept", department) });
        }

        private async Task LoadDataToGridViewAsync()
        {
            string dept = GetCurrentDepartment();

            DataTable dt = await Task.Run(() =>
            {
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
                    string isCancelStr = row[DbSchema.Wastes.COL_Is_cancel]?.ToString().Trim() ?? "N";
                    bool isChecked = (isCancelStr == "Y" || isCancelStr == "1");

                    string dateReportStr = row[DbSchema.Wastes.COL_Date_report] != DBNull.Value
                        ? Convert.ToDateTime(row[DbSchema.Wastes.COL_Date_report]).ToString("dd/MM/yyyy")
                        : "";

                    int actionVal = row[DbSchema.Wastes.COL_Action] != DBNull.Value ? Convert.ToInt32(row[DbSchema.Wastes.COL_Action]) : 1;
                    string dbTypeWaste = row[DbSchema.Wastes.COL_Type_waste]?.ToString() ?? "GENERAL";

                    int r = dgvResults.Rows.Add(
                        isChecked,
                        row[DbSchema.Wastes.COL_Department]?.ToString() ?? dept,
                        dateReportStr,
                        GetTypeWasteDisplay(dbTypeWaste),
                        row[DbSchema.Wastes.COL_Quantity_waste] != DBNull.Value ? Convert.ToDecimal(row[DbSchema.Wastes.COL_Quantity_waste]) : 0,
                        row[DbSchema.Wastes.COL_Weight_waste] != DBNull.Value ? Convert.ToDecimal(row[DbSchema.Wastes.COL_Weight_waste]) : 0,
                        GetActionDisplay(actionVal)
                    );
                    dgvResults.Rows[r].Tag = row[DbSchema.Wastes.COL_Uid];

                    if (isChecked) dgvResults.Rows[r].Cells["colIsCancel"].ReadOnly = true;
                }
            }

            // Tính tổng hiển thị trên UI
            DataTable dtTotal = await Task.Run(() => GetTotalStockByDepartment(dept));
            long totalQtyGeneral = 0, totalWeightGeneral = 0;
            long totalQtyWater = 0, totalWeightWater = 0;

            foreach (DataRow row in dtTotal.Rows)
            {
                string type = row[DbSchema.Wastes.COL_Type_waste].ToString().Trim().ToUpper();
                long q = Convert.ToInt64(row["qty"]);
                long w = Convert.ToInt64(row["weight"]);

                if (type == "WATER") { totalQtyWater = q; totalWeightWater = w; }
                else { totalQtyGeneral = q; totalWeightGeneral = w; }
            }

            if (lblGeneralQty != null) lblGeneralQty.Text = totalQtyGeneral.ToString("N0");
            if (lblGeneralWeight != null) lblGeneralWeight.Text = totalWeightGeneral.ToString("N0");
            if (lblWaterQty != null) lblWaterQty.Text = totalQtyWater.ToString("N0");
            if (lblWaterWeight != null) lblWaterWeight.Text = totalWeightWater.ToString("N0");

            if (lblSelectedSamplesVal != null) lblSelectedSamplesVal.Text = dgvResults.Rows.Count.ToString();
            if (lblStatus != null) lblStatus.Text = $"Đã nạp {dgvResults.Rows.Count} bản ghi hợp lệ của bộ phận {dept}.";
        }

        #endregion

        #region Event Handlers

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra đúng cột checkbox cần click (thay "colIsCancel" bằng tên cột thực tế trên thiết kế của bạn nếu khác)
            if (e.RowIndex < 0 || dgvResults.Columns[e.ColumnIndex].Name != "colIsCancel") return;

            // Ép DataGridView lưu ngay trạng thái ô đang chỉnh sửa
            dgvResults.EndEdit();

            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.ReadOnly) return;

            // 2. Đọc giá trị checkbox an toàn (hỗ trợ cả bool, string 'Y'/'N', số 1/0)
            bool isChecked = false;
            if (cell.Value != null && cell.Value != DBNull.Value)
            {
                if (cell.Value is bool b)
                {
                    isChecked = b;
                }
                else
                {
                    string valStr = cell.Value.ToString().Trim().ToUpper();
                    isChecked = (valStr == "TRUE" || valStr == "Y" || valStr == "1");
                }
            }

            // 3. Nếu người dùng check chọn hủy
            if (isChecked)
            {
                var tagValue = dgvResults.Rows[e.RowIndex].Tag;
                if (tagValue != null && long.TryParse(tagValue.ToString(), out long recordId))
                {
                    // Gọi hàm hủy bản ghi trong database
                    bool isSuccess = CancelWasteRecord(recordId);

                    if (isSuccess)
                    {
                        cell.ReadOnly = true; // Khóa không cho sửa lại nữa nếu hủy thành công
                    }
                    else
                    {
                        // Nếu hủy thất bại hoặc user bấm Cancel ở bảng thông báo xác nhận -> bỏ check lại
                        dgvResults.CellValueChanged -= dgvResults_CellContentClick;
                        cell.Value = false;
                        dgvResults.CellValueChanged += dgvResults_CellContentClick;
                    }
                }
                else
                {
                    MessageBox.Show(
                         GetMsg("Không tìm thấy ID bản ghi (Tag) để thực hiện hủy!", "找不到要取消的記錄 ID (Tag)！"),
                         GetMsg("Lỗi", "錯誤"),
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error
                     );
                    // Trả lại trạng thái uncheck vì không tìm thấy ID
                    dgvResults.CellValueChanged -= dgvResults_CellContentClick;
                    cell.Value = false;
                    dgvResults.CellValueChanged += dgvResults_CellContentClick;
                }
            }
        }

        private void dgvResults_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvResults.Columns[e.ColumnIndex].Name != "colIsCancel")
            {
                e.Cancel = true;
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

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguageManager.CurrentLanguageIndex = cboLanguage.SelectedIndex;
            LanguageManager.ApplyLanguage(this);
            _ = LoadDataToGridViewAsync();
        }

        private void CounterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        #endregion
        private string GetMsg(string viText, string cnText)
        {
            bool isChinese = (LanguageManager.CurrentLanguageIndex == 1);
            return isChinese ? cnText : viText;
        }
        #region Helper Methods

        private void ClearInputs()
        {
            numQuantityWaste.Value = 0;
            numWeightWaste.Value = 0;
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(
                 message,
                 GetMsg("Thông báo", "通知"),
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning
             );
        }

#endregion
    }
}