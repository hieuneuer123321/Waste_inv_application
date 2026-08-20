namespace Waste_inv_application
{
    partial class CounterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblHeaderUser = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.lblDateReport = new System.Windows.Forms.Label();
            this.dtpDateReport = new System.Windows.Forms.DateTimePicker();
            this.lblTypeWaste = new System.Windows.Forms.Label();
            this.lblQuantityWaste = new System.Windows.Forms.Label();
            this.numQuantityWaste = new System.Windows.Forms.NumericUpDown();
            this.lblWeightWaste = new System.Windows.Forms.Label();
            this.numWeightWaste = new System.Windows.Forms.NumericUpDown();
            this.lblAction = new System.Windows.Forms.Label();
            this.cboAction = new System.Windows.Forms.ComboBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.lblWaterWeight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWaterQty = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colIsCancel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateReport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeWaste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantityWaste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWeightWaste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblGeneralWeight = new System.Windows.Forms.Label();
            this.lable20 = new System.Windows.Forms.Label();
            this.lblGeneralQty = new System.Windows.Forms.Label();
            this.lable100 = new System.Windows.Forms.Label();
            this.lblSelectedSamplesVal = new System.Windows.Forms.Label();
            this.lblSelectedSamplesTitle = new System.Windows.Forms.Label();
            this.cboTypeWaste = new System.Windows.Forms.ComboBox();
            this.statusStrip.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantityWaste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeightWaste)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.grpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 646);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(670, 22);
            this.statusStrip.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(54, 17);
            this.lblStatus.Tag = "Lbl_StatusReady";
            this.lblStatus.Text = "Sẵn sàng";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(178)))), ((int)(((byte)(206)))));
            this.pnlHeader.Controls.Add(this.cboLanguage);
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.lblHeaderUser);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(670, 65);
            this.pnlHeader.TabIndex = 0;
            // 
            // cboLanguage
            // 
            this.cboLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(365, 18);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(130, 25);
            this.cboLanguage.TabIndex = 5;
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLanguage_SelectedIndexChanged);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.White;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Location = new System.Drawing.Point(582, 17);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(80, 30);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Tag = "Btn_Logout";
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click_1);
            // 
            // lblHeaderUser
            // 
            this.lblHeaderUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderUser.AutoSize = true;
            this.lblHeaderUser.Font = new System.Drawing.Font("Segoe UI", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderUser.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderUser.Location = new System.Drawing.Point(505, 21);
            this.lblHeaderUser.Name = "lblHeaderUser";
            this.lblHeaderUser.Size = new System.Drawing.Size(59, 20);
            this.lblHeaderUser.TabIndex = 3;
            this.lblHeaderUser.Text = "CM110";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 19);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(311, 25);
            this.lblHeaderTitle.TabIndex = 4;
            this.lblHeaderTitle.Tag = "Lbl_LoginHeader";
            this.lblHeaderTitle.Text = "🗑 HỆ THỐNG QUẢN LÝ RÁC THẢI";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(20, 85);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(104, 15);
            this.lblDepartment.TabIndex = 15;
            this.lblDepartment.Tag = "Lbl_Department";
            this.lblDepartment.Text = "Phòng ban (Dept):";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(130, 82);
            this.txtDepartment.MaxLength = 6;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(150, 23);
            this.txtDepartment.TabIndex = 14;
            // 
            // lblDateReport
            // 
            this.lblDateReport.AutoSize = true;
            this.lblDateReport.Location = new System.Drawing.Point(376, 85);
            this.lblDateReport.Name = "lblDateReport";
            this.lblDateReport.Size = new System.Drawing.Size(83, 15);
            this.lblDateReport.TabIndex = 13;
            this.lblDateReport.Tag = "Lbl_DateReport";
            this.lblDateReport.Text = "Ngày báo cáo:";
            // 
            // dtpDateReport
            // 
            this.dtpDateReport.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateReport.Location = new System.Drawing.Point(476, 82);
            this.dtpDateReport.Name = "dtpDateReport";
            this.dtpDateReport.Size = new System.Drawing.Size(180, 23);
            this.dtpDateReport.TabIndex = 12;
            // 
            // lblTypeWaste
            // 
            this.lblTypeWaste.AutoSize = true;
            this.lblTypeWaste.Location = new System.Drawing.Point(20, 120);
            this.lblTypeWaste.Name = "lblTypeWaste";
            this.lblTypeWaste.Size = new System.Drawing.Size(74, 15);
            this.lblTypeWaste.TabIndex = 11;
            this.lblTypeWaste.Tag = "Lbl_TypeWaste";
            this.lblTypeWaste.Text = "Loại rác thải:";
            // 
            // lblQuantityWaste
            // 
            this.lblQuantityWaste.AutoSize = true;
            this.lblQuantityWaste.Location = new System.Drawing.Point(20, 155);
            this.lblQuantityWaste.Name = "lblQuantityWaste";
            this.lblQuantityWaste.Size = new System.Drawing.Size(57, 15);
            this.lblQuantityWaste.TabIndex = 9;
            this.lblQuantityWaste.Tag = "Lbl_Quantity";
            this.lblQuantityWaste.Text = "Số lượng:";
            // 
            // numQuantityWaste
            // 
            this.numQuantityWaste.Location = new System.Drawing.Point(130, 153);
            this.numQuantityWaste.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numQuantityWaste.Name = "numQuantityWaste";
            this.numQuantityWaste.Size = new System.Drawing.Size(150, 23);
            this.numQuantityWaste.TabIndex = 8;
            // 
            // lblWeightWaste
            // 
            this.lblWeightWaste.AutoSize = true;
            this.lblWeightWaste.Location = new System.Drawing.Point(376, 155);
            this.lblWeightWaste.Name = "lblWeightWaste";
            this.lblWeightWaste.Size = new System.Drawing.Size(99, 15);
            this.lblWeightWaste.TabIndex = 7;
            this.lblWeightWaste.Tag = "Lbl_Weight";
            this.lblWeightWaste.Text = "Trọng lượng (kg):";
            // 
            // numWeightWaste
            // 
            this.numWeightWaste.Location = new System.Drawing.Point(476, 153);
            this.numWeightWaste.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numWeightWaste.Name = "numWeightWaste";
            this.numWeightWaste.Size = new System.Drawing.Size(180, 23);
            this.numWeightWaste.TabIndex = 6;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(376, 120);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(70, 15);
            this.lblAction.TabIndex = 5;
            this.lblAction.Tag = "Lbl_Action";
            this.lblAction.Text = "Hành động:";
            // 
            // cboAction
            // 
            this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAction.Items.AddRange(new object[] {
            "1 - Nhập (In) / 入庫 ",
            "0 - Xuất (Out) / 清運"});
            this.cboAction.Location = new System.Drawing.Point(476, 117);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(180, 23);
            this.cboAction.TabIndex = 4;
            // 
            // pnlActions
            // 
            this.pnlActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlActions.Controls.Add(this.btnReload);
            this.pnlActions.Controls.Add(this.btnClear);
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Location = new System.Drawing.Point(20, 190);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(636, 40);
            this.pnlActions.TabIndex = 2;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.Location = new System.Drawing.Point(543, 5);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(90, 30);
            this.btnReload.TabIndex = 3;
            this.btnReload.Tag = "Btn_Reload";
            this.btnReload.Text = "↻ Làm mới";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(103)))), ((int)(((byte)(103)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(170, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 30);
            this.btnClear.TabIndex = 2;
            this.btnClear.Tag = "Btn_Clear";
            this.btnClear.Text = "❌ Xóa form";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(14, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Tag = "Btn_Save";
            this.btnSave.Text = "💾 Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpResults
            // 
            this.grpResults.Controls.Add(this.lblWaterWeight);
            this.grpResults.Controls.Add(this.label2);
            this.grpResults.Controls.Add(this.lblWaterQty);
            this.grpResults.Controls.Add(this.label4);
            this.grpResults.Controls.Add(this.dgvResults);
            this.grpResults.Controls.Add(this.lblGeneralWeight);
            this.grpResults.Controls.Add(this.lable20);
            this.grpResults.Controls.Add(this.lblGeneralQty);
            this.grpResults.Controls.Add(this.lable100);
            this.grpResults.Controls.Add(this.lblSelectedSamplesVal);
            this.grpResults.Controls.Add(this.lblSelectedSamplesTitle);
            this.grpResults.Location = new System.Drawing.Point(20, 236);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(636, 395);
            this.grpResults.TabIndex = 1;
            this.grpResults.TabStop = false;
            this.grpResults.Tag = "Grp_ReportList";
            this.grpResults.Text = "Danh sách báo cáo rác thải";
            // 
            // lblWaterWeight
            // 
            this.lblWaterWeight.AutoSize = true;
            this.lblWaterWeight.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWaterWeight.Location = new System.Drawing.Point(212, 347);
            this.lblWaterWeight.Name = "lblWaterWeight";
            this.lblWaterWeight.Size = new System.Drawing.Size(17, 19);
            this.lblWaterWeight.TabIndex = 9;
            this.lblWaterWeight.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 347);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 15);
            this.label2.TabIndex = 10;
            this.label2.Tag = "Lbl_WaterWeight";
            this.label2.Text = "Tổng trọng lượng nước thải (kg):";
            // 
            // lblWaterQty
            // 
            this.lblWaterQty.AutoSize = true;
            this.lblWaterQty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWaterQty.Location = new System.Drawing.Point(212, 324);
            this.lblWaterQty.Name = "lblWaterQty";
            this.lblWaterQty.Size = new System.Drawing.Size(17, 19);
            this.lblWaterQty.TabIndex = 11;
            this.lblWaterQty.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 15);
            this.label4.TabIndex = 12;
            this.label4.Tag = "Lbl_WaterQty";
            this.label4.Text = "Tổng số lượng nước thải (thùng):";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIsCancel,
            this.colDepartment,
            this.colDateReport,
            this.colTypeWaste,
            this.colQuantityWaste,
            this.colWeightWaste,
            this.colAction});
            this.dgvResults.Location = new System.Drawing.Point(12, 22);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(624, 279);
            this.dgvResults.TabIndex = 8;
            this.dgvResults.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvResults_CellBeginEdit);
            this.dgvResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellContentClick);
            // 
            // colIsCancel
            // 
            this.colIsCancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colIsCancel.HeaderText = "";
            this.colIsCancel.Name = "colIsCancel";
            this.colIsCancel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colIsCancel.Width = 50;
            // 
            // colDepartment
            // 
            this.colDepartment.HeaderText = "Phòng ban";
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colDateReport
            // 
            this.colDateReport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDateReport.HeaderText = "Ngày BC";
            this.colDateReport.Name = "colDateReport";
            // 
            // colTypeWaste
            // 
            this.colTypeWaste.HeaderText = "Loại rác";
            this.colTypeWaste.Name = "colTypeWaste";
            // 
            // colQuantityWaste
            // 
            this.colQuantityWaste.HeaderText = "Số lượng";
            this.colQuantityWaste.Name = "colQuantityWaste";
            // 
            // colWeightWaste
            // 
            this.colWeightWaste.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colWeightWaste.HeaderText = "Trọng lượng (Kg)";
            this.colWeightWaste.Name = "colWeightWaste";
            // 
            // colAction
            // 
            this.colAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAction.HeaderText = "Hành động";
            this.colAction.Name = "colAction";
            // 
            // lblGeneralWeight
            // 
            this.lblGeneralWeight.AutoSize = true;
            this.lblGeneralWeight.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGeneralWeight.Location = new System.Drawing.Point(530, 347);
            this.lblGeneralWeight.Name = "lblGeneralWeight";
            this.lblGeneralWeight.Size = new System.Drawing.Size(17, 19);
            this.lblGeneralWeight.TabIndex = 2;
            this.lblGeneralWeight.Text = "0";
            // 
            // lable20
            // 
            this.lable20.AutoSize = true;
            this.lable20.Location = new System.Drawing.Point(345, 347);
            this.lable20.Name = "lable20";
            this.lable20.Size = new System.Drawing.Size(170, 15);
            this.lable20.TabIndex = 3;
            this.lable20.Tag = "Lbl_GeneralWeight";
            this.lable20.Text = "Tổng trọng lượng rác thải (kg):";
            // 
            // lblGeneralQty
            // 
            this.lblGeneralQty.AutoSize = true;
            this.lblGeneralQty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGeneralQty.Location = new System.Drawing.Point(530, 324);
            this.lblGeneralQty.Name = "lblGeneralQty";
            this.lblGeneralQty.Size = new System.Drawing.Size(17, 19);
            this.lblGeneralQty.TabIndex = 4;
            this.lblGeneralQty.Text = "0";
            // 
            // lable100
            // 
            this.lable100.AutoSize = true;
            this.lable100.Location = new System.Drawing.Point(345, 324);
            this.lable100.Name = "lable100";
            this.lable100.Size = new System.Drawing.Size(172, 15);
            this.lable100.TabIndex = 5;
            this.lable100.Tag = "Lbl_GeneralQty";
            this.lable100.Text = "Tổng số lượng rác thải (thùng):";
            // 
            // lblSelectedSamplesVal
            // 
            this.lblSelectedSamplesVal.AutoSize = true;
            this.lblSelectedSamplesVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectedSamplesVal.Location = new System.Drawing.Point(530, 371);
            this.lblSelectedSamplesVal.Name = "lblSelectedSamplesVal";
            this.lblSelectedSamplesVal.Size = new System.Drawing.Size(17, 19);
            this.lblSelectedSamplesVal.TabIndex = 6;
            this.lblSelectedSamplesVal.Text = "0";
            // 
            // lblSelectedSamplesTitle
            // 
            this.lblSelectedSamplesTitle.AutoSize = true;
            this.lblSelectedSamplesTitle.Location = new System.Drawing.Point(345, 373);
            this.lblSelectedSamplesTitle.Name = "lblSelectedSamplesTitle";
            this.lblSelectedSamplesTitle.Size = new System.Drawing.Size(84, 15);
            this.lblSelectedSamplesTitle.TabIndex = 7;
            this.lblSelectedSamplesTitle.Tag = "Lbl_TotalRowsTitle";
            this.lblSelectedSamplesTitle.Text = "Tổng số dòng:";
            // 
            // cboTypeWaste
            // 
            this.cboTypeWaste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeWaste.Items.AddRange(new object[] {
            "Rác thải/ 固態",
            "Nước thải / 液態"});
            this.cboTypeWaste.Location = new System.Drawing.Point(130, 117);
            this.cboTypeWaste.Name = "cboTypeWaste";
            this.cboTypeWaste.Size = new System.Drawing.Size(152, 23);
            this.cboTypeWaste.TabIndex = 16;
            // 
            // CounterForm
            // 
            this.ClientSize = new System.Drawing.Size(670, 668);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.cboTypeWaste);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.cboAction);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.numWeightWaste);
            this.Controls.Add(this.lblWeightWaste);
            this.Controls.Add(this.numQuantityWaste);
            this.Controls.Add(this.lblQuantityWaste);
            this.Controls.Add(this.lblTypeWaste);
            this.Controls.Add(this.dtpDateReport);
            this.Controls.Add(this.lblDateReport);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CounterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Form_Counter";
            this.Text = "Trang Chủ - Quản Lý Rác Thải";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CounterForm_FormClosing);
            this.Load += new System.EventHandler(this.CounterForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantityWaste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeightWaste)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderUser;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.ComboBox cboLanguage;

        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.Label lblDateReport;
        private System.Windows.Forms.DateTimePicker dtpDateReport;
        private System.Windows.Forms.Label lblTypeWaste;
        private System.Windows.Forms.Label lblQuantityWaste;
        private System.Windows.Forms.NumericUpDown numQuantityWaste;
        private System.Windows.Forms.Label lblWeightWaste;
        private System.Windows.Forms.NumericUpDown numWeightWaste;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox cboAction;

        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.DataGridView dgvResults;

        private System.Windows.Forms.Label lblSelectedSamplesTitle;
        private System.Windows.Forms.Label lblSelectedSamplesVal;
        private System.Windows.Forms.Label lable100;
        private System.Windows.Forms.Label lblGeneralQty;
        private System.Windows.Forms.Label lable20;
        private System.Windows.Forms.Label lblGeneralWeight;
        private System.Windows.Forms.ComboBox cboTypeWaste;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeWaste;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantityWaste;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWeightWaste;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label lblWaterWeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWaterQty;
        private System.Windows.Forms.Label label4;
    }
}