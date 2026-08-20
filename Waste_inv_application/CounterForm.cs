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
            bool isOutput = cboAction.SelectedIndex == 1; // Giả sử Index 1 là Xuất kho

            // 1. Kiểm tra tồn kho trước khi xuất
            if (isOutput)
            {
                var (totalQty, totalWeight) = GetCurrentDepartmentTotal(dept);

                if (qtyCurrent > totalQty || weightCurrent > totalWeight)
                {
                    MessageBox.Show($"Lỗi: Không đủ hàng trong kho!\n\n" +
                                    $"Tồn kho hiện tại: {totalQty} thùng / {totalWeight} kg\n" +
                                    $"Bạn đang cố xuất: {qtyCurrent} thùng / {weightCurrent} kg",
                                    "Cảnh báo xuất quá số lượng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn lưu dữ liệu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string user = lblHeaderUser.Text.Substring(0, Math.Min(lblHeaderUser.Text.Length, 6));

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
                        cmd.Parameters.Add("t", OracleDbType.Varchar2).Value = cboTypeWaste.Text;
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

        private (long totalQty, long totalWeight) GetCurrentDepartmentTotal(string department)
        {
            string sql = $@"SELECT 
                    NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Quantity_waste} ELSE -{DbSchema.Wastes.COL_Quantity_waste} END), 0) AS total_qty,
                    NVL(SUM(CASE WHEN {DbSchema.Wastes.COL_Action} = 1 THEN {DbSchema.Wastes.COL_Weight_waste} ELSE -{DbSchema.Wastes.COL_Weight_waste} END), 0) AS total_weight
                 FROM {DbSchema.Wastes.TABLE_NAME}
                 WHERE UPPER({DbSchema.Wastes.COL_Department}) = UPPER(:p_department) 
                   AND {DbSchema.Wastes.COL_Is_cancel} = 'N'";

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, new OracleParameter[] { new OracleParameter("p_department", department) });
                if (dt != null && dt.Rows.Count > 0)
                {
                    long qty = Convert.ToInt64(dt.Rows[0]["total_qty"]);
                    long weight = Convert.ToInt64(dt.Rows[0]["total_weight"]);
                    return (qty, weight);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính tồn kho: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (0, 0);
        }

        // Hàm nạp dữ liệu chạy ngầm bất đồng bộ (Lazy Loading / Background Load)
        private async Task LoadDataToGridViewAsync()
        {
            string dept = lblHeaderUser.Text.Substring(0, Math.Min(lblHeaderUser.Text.Length, 6));

            // Đưa việc query Database ra Background Thread để tránh đơ giao diện
            DataTable dt = await Task.Run(() =>
            {
                string sql = $@"SELECT {DbSchema.Wastes.COL_Uid}, {DbSchema.Wastes.COL_Department}, {DbSchema.Wastes.COL_Date_report}, {DbSchema.Wastes.COL_Type_waste}, 
                              {DbSchema.Wastes.COL_Quantity_waste}, {DbSchema.Wastes.COL_Weight_waste}, {DbSchema.Wastes.COL_Is_cancel}, {DbSchema.Wastes.COL_Action} 
                              FROM {DbSchema.Wastes.TABLE_NAME} WHERE UPPER({DbSchema.Wastes.COL_Department}) = :dept ORDER BY {DbSchema.Wastes.COL_Uid} DESC";

                return DatabaseHelper.ExecuteQuery(sql, new OracleParameter[] { new OracleParameter("dept", dept) });
            });

            // Đổ dữ liệu lên UI Thread
            dgvResults.Rows.Clear();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string isCancelStr = row[DbSchema.Wastes.COL_Is_cancel] != DBNull.Value ? row[DbSchema.Wastes.COL_Is_cancel].ToString().Trim() : "N";
                    bool isChecked = (isCancelStr == "Y" || isCancelStr == "1");

                    string dateReportStr = row[DbSchema.Wastes.COL_Date_report] != DBNull.Value ? Convert.ToDateTime(row[DbSchema.Wastes.COL_Date_report]).ToString("dd/MM/yyyy") : "";
                    int actionVal = row[DbSchema.Wastes.COL_Action] != DBNull.Value ? Convert.ToInt32(row[DbSchema.Wastes.COL_Action]) : 1;

                    int r = dgvResults.Rows.Add(
                        isChecked,
                        row[DbSchema.Wastes.COL_Department] != DBNull.Value ? row[DbSchema.Wastes.COL_Department].ToString() : dept,
                        dateReportStr,
                        row[DbSchema.Wastes.COL_Type_waste] != DBNull.Value ? row[DbSchema.Wastes.COL_Type_waste].ToString() : "",
                        row[DbSchema.Wastes.COL_Quantity_waste] != DBNull.Value ? Convert.ToDecimal(row[DbSchema.Wastes.COL_Quantity_waste]) : 0,
                        row[DbSchema.Wastes.COL_Weight_waste] != DBNull.Value ? Convert.ToDecimal(row[DbSchema.Wastes.COL_Weight_waste]) : 0,
                        actionVal == 1 ? "Nhập kho" : "Xuất kho"
                    );
                    dgvResults.Rows[r].Tag = row[DbSchema.Wastes.COL_Uid];

                    // Khóa cứng ô checkbox nếu bản ghi này đã hủy
                    if (isChecked)
                    {
                        dgvResults.Rows[r].Cells["colIsCancel"].ReadOnly = true;
                    }
                }
            }

            var (totalQty, totalWeight) = await Task.Run(() => GetCurrentDepartmentTotal(dept));
            lblTotalQtyVal.Text = totalQty.ToString("N0");
            lblTotalWeightVal.Text = totalWeight.ToString("N0");
            lblSelectedSamplesVal.Text = dgvResults.Rows.Count.ToString();

            if (lblStatus != null)
            {
                lblStatus.Text = $"Đã nạp {dgvResults.Rows.Count} bản ghi của bộ phận {dept}.";
            }
        }

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvResults.Columns[e.ColumnIndex].Name != "colIsCancel") return;

            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // Nếu ô đã bị khóa, không cho phép tương tác bỏ chọn
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
                        // Hủy thành công -> Khóa cứng ô checkbox lại ngay lập tức
                        cell.ReadOnly = true;
                    }
                    else
                    {
                        // Nếu hủy thất bại, hoàn tác lại trạng thái chưa check
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

                // Gán kết quả là Retry để báo hiệu muốn đăng xuất
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
            // Cập nhật trạng thái và khóa tạm lưới trong lúc đang nạp lại
            if (lblStatus != null)
            {
                lblStatus.Text = "Đang làm mới dữ liệu...";
            }
            dgvResults.Enabled = false;

            // Gọi lại hàm nạp dữ liệu bất đồng bộ
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
    }
}