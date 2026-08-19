using System;
using System.Drawing;
using System.Windows.Forms;

namespace Waste_inv_application
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            StyleUI();
        }

        private void StyleUI()
        {
            // ==========================================
            // 1. HEADER PANEL
            // ==========================================
            pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeader.Height = 60;

            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Text = "🗑️ HỆ THỐNG QUẢN LÝ RÁC THẢI";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblUser.ForeColor = Color.FromArgb(203, 213, 225);
            lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Italic);

            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.BackColor = Color.FromArgb(239, 68, 68);
            btnLogout.ForeColor = Color.White;
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogout.Cursor = Cursors.Hand;

            // ==========================================
            // 2. SUMMARY PANEL
            // ==========================================
            pnlSummary.BackColor = Color.FromArgb(241, 245, 249);
            pnlSummary.Height = 80;
            pnlSummary.Padding = new Padding(12, 8, 12, 8);
            pnlSummary.Margin = new Padding(0);

            // Panel Nút Bấm (Bên trái)
            pnlActions.Dock = DockStyle.Left;
            pnlActions.Padding = new Padding(0);

            btnInsert.FlatStyle = FlatStyle.Flat;
            btnInsert.FlatAppearance.BorderSize = 0;
            btnInsert.BackColor = Color.FromArgb(30, 58, 138);
            btnInsert.ForeColor = Color.White;
            btnInsert.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnInsert.Cursor = Cursors.Hand;
            btnInsert.Text = "➕  Thêm dữ liệu";
            btnInsert.Height = 38;

            btnReload.FlatStyle = FlatStyle.Flat;
            btnReload.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnReload.FlatAppearance.BorderSize = 1;
            btnReload.BackColor = Color.White;
            btnReload.ForeColor = Color.FromArgb(71, 85, 105);
            btnReload.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReload.Cursor = Cursors.Hand;
            btnReload.Text = "🔄  Tải lại";
            btnReload.Height = 38;

            // Panel Chứa Thẻ KPI (Bên phải)
            pnlCards.Dock = DockStyle.Right;
            pnlCards.Width = 460;
            pnlCards.Padding = new Padding(0);

            // THẺ TỔNG KHỐI LƯỢNG
            pnlCardWeight.Dock = DockStyle.Right;
            pnlCardWeight.Width = 210;
            pnlCardWeight.BackColor = Color.FromArgb(236, 253, 245); // Emerald-50
            pnlCardWeight.BorderStyle = BorderStyle.None;
            pnlCardWeight.Margin = new Padding(0);
            pnlCardWeight.Padding = new Padding(4);

            lblWeightTitle.AutoSize = false;
            lblWeightTitle.Dock = DockStyle.Top;
            lblWeightTitle.Height = 20;
            lblWeightTitle.Text = "TỔNG KHỐI LƯỢNG";
            lblWeightTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblWeightTitle.ForeColor = Color.FromArgb(4, 120, 87);
            lblWeightTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblWeightValue.AutoSize = false;
            lblWeightValue.Dock = DockStyle.Fill;
            lblWeightValue.Text = "5,000 Kg";
            lblWeightValue.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblWeightValue.ForeColor = Color.FromArgb(6, 78, 59);
            lblWeightValue.TextAlign = ContentAlignment.MiddleCenter;

            // THẺ THÙNG CÒN LẠI
            pnlCardBins.Dock = DockStyle.Right;
            pnlCardBins.Width = 210;
            pnlCardBins.BackColor = Color.FromArgb(239, 246, 255); // Blue-50
            pnlCardBins.BorderStyle = BorderStyle.None;
            pnlCardBins.Margin = new Padding(0, 0, 12, 0); // Khoảng cách giữa 2 thẻ
            pnlCardBins.Padding = new Padding(4);

            lblBinsTitle.AutoSize = false;
            lblBinsTitle.Dock = DockStyle.Top;
            lblBinsTitle.Height = 20;
            lblBinsTitle.Text = "THÙNG CÒN LẠI";
            lblBinsTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblBinsTitle.ForeColor = Color.FromArgb(29, 78, 216);
            lblBinsTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblBinsValue.AutoSize = false;
            lblBinsValue.Dock = DockStyle.Fill;
            lblBinsValue.Text = "50";
            lblBinsValue.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblBinsValue.ForeColor = Color.FromArgb(30, 58, 138);
            lblBinsValue.TextAlign = ContentAlignment.MiddleCenter;

            // 🔴 ĐIỀU CHỈNH Z-ORDER ĐỂ ĐỔI VỊ TRÍ 2 THẺ (ĐẶT TỔNG KHỐI LƯỢNG SANG BÊN TRÁI)
            pnlCardBins.BringToFront();
            pnlCardWeight.SendToBack();

            // ==========================================
            // 3. DATAGRIDVIEW
            // ==========================================
            dgvList.Dock = DockStyle.Fill;
            dgvList.BackgroundColor = Color.FromArgb(241, 245, 249);
            dgvList.BorderStyle = BorderStyle.None;
            dgvList.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvList.GridColor = Color.FromArgb(226, 232, 240);

            dgvList.EnableHeadersVisualStyles = false;
            dgvList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(29, 78, 216);
            dgvList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvList.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            dgvList.ColumnHeadersHeight = 48;

            dgvList.DefaultCellStyle.BackColor = Color.White;
            dgvList.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            dgvList.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvList.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dgvList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvList.RowTemplate.Height = 40;

            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvList.MultiSelect = false;
            dgvList.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}