namespace Waste_inv_application
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblBinsTitle = new System.Windows.Forms.Label();
            this.pnlCards = new System.Windows.Forms.Panel();
            this.lblBinsValue = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnReload = new System.Windows.Forms.Button();
            this.pnlCardBins = new System.Windows.Forms.Panel();
            this.pnlCardWeight = new System.Windows.Forms.Panel();
            this.lblWeightValue = new System.Windows.Forms.Label();
            this.lblWeightTitle = new System.Windows.Forms.Label();
            this.col_bp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cancel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlCards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.pnlCardBins.SuspendLayout();
            this.pnlCardWeight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.lblUser);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1113, 78);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(407, 28);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(317, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🗑️ HỆ THỐNG QUẢN LÝ RÁC THẢI";
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(944, 34);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(44, 17);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Admin";
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.pnlActions);
            this.pnlSummary.Controls.Add(this.pnlCards);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 78);
            this.pnlSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1113, 80);
            this.pnlSummary.TabIndex = 1;
            // 
            // lblBinsTitle
            // 
            this.lblBinsTitle.AutoSize = true;
            this.lblBinsTitle.Location = new System.Drawing.Point(4, 20);
            this.lblBinsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBinsTitle.Name = "lblBinsTitle";
            this.lblBinsTitle.Size = new System.Drawing.Size(151, 17);
            this.lblBinsTitle.TabIndex = 2;
            this.lblBinsTitle.Text = "Số lượng thùng còn lại : ";
            // 
            // pnlCards
            // 
            this.pnlCards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCards.Controls.Add(this.pnlCardWeight);
            this.pnlCards.Controls.Add(this.pnlCardBins);
            this.pnlCards.Location = new System.Drawing.Point(442, 5);
            this.pnlCards.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(667, 66);
            this.pnlCards.TabIndex = 3;
            // 
            // lblBinsValue
            // 
            this.lblBinsValue.AutoSize = true;
            this.lblBinsValue.Location = new System.Drawing.Point(179, 20);
            this.lblBinsValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBinsValue.Name = "lblBinsValue";
            this.lblBinsValue.Size = new System.Drawing.Size(22, 17);
            this.lblBinsValue.TabIndex = 4;
            this.lblBinsValue.Text = "50";
            // 
            // dgvList
            // 
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_bp,
            this.col_date,
            this.col_type,
            this.col_quantity,
            this.col_weight,
            this.col_action,
            this.col_cancel});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 158);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4, 24, 4, 4);
            this.dgvList.Name = "dgvList";
            this.dgvList.Size = new System.Drawing.Size(1113, 515);
            this.dgvList.TabIndex = 2;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(1013, 26);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(88, 30);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(28, 18);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(147, 30);
            this.btnInsert.TabIndex = 4;
            this.btnInsert.Text = "Thêm dữ liệu";
            this.btnInsert.UseVisualStyleBackColor = true;
            // 
            // pnlActions
            // 
            this.pnlActions.BackColor = System.Drawing.Color.White;
            this.pnlActions.Controls.Add(this.btnReload);
            this.pnlActions.Controls.Add(this.btnInsert);
            this.pnlActions.Location = new System.Drawing.Point(4, 8);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(397, 63);
            this.pnlActions.TabIndex = 5;
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(203, 18);
            this.btnReload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(147, 30);
            this.btnReload.TabIndex = 5;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            // 
            // pnlCardBins
            // 
            this.pnlCardBins.Controls.Add(this.lblBinsTitle);
            this.pnlCardBins.Controls.Add(this.lblBinsValue);
            this.pnlCardBins.Location = new System.Drawing.Point(13, 3);
            this.pnlCardBins.Name = "pnlCardBins";
            this.pnlCardBins.Size = new System.Drawing.Size(260, 55);
            this.pnlCardBins.TabIndex = 6;
            // 
            // pnlCardWeight
            // 
            this.pnlCardWeight.Controls.Add(this.lblWeightValue);
            this.pnlCardWeight.Controls.Add(this.lblWeightTitle);
            this.pnlCardWeight.Location = new System.Drawing.Point(387, 8);
            this.pnlCardWeight.Name = "pnlCardWeight";
            this.pnlCardWeight.Size = new System.Drawing.Size(260, 55);
            this.pnlCardWeight.TabIndex = 7;
            // 
            // lblWeightValue
            // 
            this.lblWeightValue.AutoSize = true;
            this.lblWeightValue.Location = new System.Drawing.Point(167, 18);
            this.lblWeightValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeightValue.Name = "lblWeightValue";
            this.lblWeightValue.Size = new System.Drawing.Size(56, 17);
            this.lblWeightValue.TabIndex = 7;
            this.lblWeightValue.Text = "5000 Kg";
            // 
            // lblWeightTitle
            // 
            this.lblWeightTitle.AutoSize = true;
            this.lblWeightTitle.Location = new System.Drawing.Point(44, 18);
            this.lblWeightTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeightTitle.Name = "lblWeightTitle";
            this.lblWeightTitle.Size = new System.Drawing.Size(101, 17);
            this.lblWeightTitle.TabIndex = 6;
            this.lblWeightTitle.Text = "Tổng số lượng :";
            // 
            // col_bp
            // 
            this.col_bp.HeaderText = "Bộ Phận";
            this.col_bp.Name = "col_bp";
            // 
            // col_date
            // 
            this.col_date.HeaderText = "Ngày";
            this.col_date.Name = "col_date";
            // 
            // col_type
            // 
            this.col_type.HeaderText = "Loại Rác";
            this.col_type.Name = "col_type";
            // 
            // col_quantity
            // 
            this.col_quantity.HeaderText = "Số Lượng Thùng";
            this.col_quantity.Name = "col_quantity";
            // 
            // col_weight
            // 
            this.col_weight.HeaderText = "Khối lượng";
            this.col_weight.Name = "col_weight";
            // 
            // col_action
            // 
            this.col_action.HeaderText = "Hành động";
            this.col_action.Name = "col_action";
            // 
            // col_cancel
            // 
            this.col_cancel.HeaderText = "Hủy";
            this.col_cancel.Name = "col_cancel";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1113, 673);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Trang Chủ";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlCards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlCardBins.ResumeLayout(false);
            this.pnlCardBins.PerformLayout();
            this.pnlCardWeight.ResumeLayout(false);
            this.pnlCardWeight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Label lblBinsValue;
        private System.Windows.Forms.Label lblBinsTitle;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Panel pnlCardWeight;
        private System.Windows.Forms.Panel pnlCardBins;
        private System.Windows.Forms.Label lblWeightValue;
        private System.Windows.Forms.Label lblWeightTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_bp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_action;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cancel;
    }
}