namespace ByNoa
{
    partial class AddEditAppointmentForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblExpectedEndTime = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lstCustomerResults = new System.Windows.Forms.ListBox();
            this.lblSelectedCustomer = new System.Windows.Forms.Label();
            this.txtCustomerSearch = new System.Windows.Forms.TextBox();
            this.cmbWorker = new System.Windows.Forms.ComboBox();
            this.cmbTreatment = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dvgScheduleWorker1 = new System.Windows.Forms.DataGridView();
            this.dvgScheduleWorker2 = new System.Windows.Forms.DataGridView();
            this.dvgScheduleWorker3 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbWorkerSelect3 = new System.Windows.Forms.ComboBox();
            this.cmbWorkerSelect2 = new System.Windows.Forms.ComboBox();
            this.cmbWorkerSelect1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgScheduleWorker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgScheduleWorker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgScheduleWorker3)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.lblExpectedEndTime);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.txtStartTime);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.lstCustomerResults);
            this.panel1.Controls.Add(this.lblSelectedCustomer);
            this.panel1.Controls.Add(this.txtCustomerSearch);
            this.panel1.Controls.Add(this.cmbWorker);
            this.panel1.Controls.Add(this.cmbTreatment);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 615);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // lblExpectedEndTime
            // 
            this.lblExpectedEndTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedEndTime.AutoSize = true;
            this.lblExpectedEndTime.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblExpectedEndTime.Location = new System.Drawing.Point(178, 426);
            this.lblExpectedEndTime.Name = "lblExpectedEndTime";
            this.lblExpectedEndTime.Size = new System.Drawing.Size(93, 20);
            this.lblExpectedEndTime.TabIndex = 16;
            this.lblExpectedEndTime.Text = "                     ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(438, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 615);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(392, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(381, 618);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(954, 615);
            this.panel4.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Location = new System.Drawing.Point(182, 463);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 48);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtStartTime
            // 
            this.txtStartTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtStartTime.Location = new System.Drawing.Point(182, 377);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(234, 26);
            this.txtStartTime.TabIndex = 7;
            this.txtStartTime.TextChanged += new System.EventHandler(this.txtStartTime_TextChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpDate.Location = new System.Drawing.Point(182, 329);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(234, 26);
            this.dtpDate.TabIndex = 6;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // lstCustomerResults
            // 
            this.lstCustomerResults.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstCustomerResults.FormattingEnabled = true;
            this.lstCustomerResults.ItemHeight = 20;
            this.lstCustomerResults.Location = new System.Drawing.Point(220, 140);
            this.lstCustomerResults.Name = "lstCustomerResults";
            this.lstCustomerResults.Size = new System.Drawing.Size(196, 64);
            this.lstCustomerResults.TabIndex = 5;
            this.lstCustomerResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstCustomerResults_MouseClick);
            // 
            // lblSelectedCustomer
            // 
            this.lblSelectedCustomer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSelectedCustomer.AutoSize = true;
            this.lblSelectedCustomer.Location = new System.Drawing.Point(163, 184);
            this.lblSelectedCustomer.Name = "lblSelectedCustomer";
            this.lblSelectedCustomer.Size = new System.Drawing.Size(51, 20);
            this.lblSelectedCustomer.TabIndex = 4;
            this.lblSelectedCustomer.Text = "label2";
            // 
            // txtCustomerSearch
            // 
            this.txtCustomerSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCustomerSearch.Location = new System.Drawing.Point(220, 108);
            this.txtCustomerSearch.Name = "txtCustomerSearch";
            this.txtCustomerSearch.Size = new System.Drawing.Size(196, 26);
            this.txtCustomerSearch.TabIndex = 2;
            this.txtCustomerSearch.TextChanged += new System.EventHandler(this.txtCustomerSearch_TextChanged);
            // 
            // cmbWorker
            // 
            this.cmbWorker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbWorker.FormattingEnabled = true;
            this.cmbWorker.Location = new System.Drawing.Point(182, 277);
            this.cmbWorker.Name = "cmbWorker";
            this.cmbWorker.Size = new System.Drawing.Size(234, 28);
            this.cmbWorker.TabIndex = 1;
            this.cmbWorker.SelectedIndexChanged += new System.EventHandler(this.cmbWorker_SelectedIndexChanged);
            // 
            // cmbTreatment
            // 
            this.cmbTreatment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbTreatment.FormattingEnabled = true;
            this.cmbTreatment.Location = new System.Drawing.Point(182, 225);
            this.cmbTreatment.Name = "cmbTreatment";
            this.cmbTreatment.Size = new System.Drawing.Size(234, 28);
            this.cmbTreatment.TabIndex = 0;
            this.cmbTreatment.SelectedIndexChanged += new System.EventHandler(this.cmbTreatment_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.tableLayoutPanel1);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.cmbWorkerSelect3);
            this.panel6.Controls.Add(this.cmbWorkerSelect2);
            this.panel6.Controls.Add(this.cmbWorkerSelect1);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(941, 612);
            this.panel6.TabIndex = 9;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.dvgScheduleWorker1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dvgScheduleWorker2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dvgScheduleWorker3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 78);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 537F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(951, 537);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dvgScheduleWorker1
            // 
            this.dvgScheduleWorker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dvgScheduleWorker1.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dvgScheduleWorker1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgScheduleWorker1.Location = new System.Drawing.Point(3, 3);
            this.dvgScheduleWorker1.Name = "dvgScheduleWorker1";
            this.dvgScheduleWorker1.RowHeadersWidth = 62;
            this.dvgScheduleWorker1.RowTemplate.Height = 28;
            this.dvgScheduleWorker1.Size = new System.Drawing.Size(310, 531);
            this.dvgScheduleWorker1.TabIndex = 0;
            this.dvgScheduleWorker1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgScheduleWorker1_CellClick);
            // 
            // dvgScheduleWorker2
            // 
            this.dvgScheduleWorker2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dvgScheduleWorker2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgScheduleWorker2.Location = new System.Drawing.Point(320, 3);
            this.dvgScheduleWorker2.Name = "dvgScheduleWorker2";
            this.dvgScheduleWorker2.RowHeadersWidth = 62;
            this.dvgScheduleWorker2.RowTemplate.Height = 28;
            this.dvgScheduleWorker2.Size = new System.Drawing.Size(311, 531);
            this.dvgScheduleWorker2.TabIndex = 1;
            this.dvgScheduleWorker2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgScheduleWorker2_CellClick);
            // 
            // dvgScheduleWorker3
            // 
            this.dvgScheduleWorker3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dvgScheduleWorker3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgScheduleWorker3.Location = new System.Drawing.Point(638, 3);
            this.dvgScheduleWorker3.Name = "dvgScheduleWorker3";
            this.dvgScheduleWorker3.RowHeadersWidth = 62;
            this.dvgScheduleWorker3.RowTemplate.Height = 28;
            this.dvgScheduleWorker3.Size = new System.Drawing.Size(310, 531);
            this.dvgScheduleWorker3.TabIndex = 2;
            this.dvgScheduleWorker3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgScheduleWorker3_CellClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(108, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Worker:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(755, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 20);
            this.label9.TabIndex = 4;
            this.label9.Text = "Worker:";
            // 
            // cmbWorkerSelect3
            // 
            this.cmbWorkerSelect3.FormattingEnabled = true;
            this.cmbWorkerSelect3.Location = new System.Drawing.Point(720, 44);
            this.cmbWorkerSelect3.Name = "cmbWorkerSelect3";
            this.cmbWorkerSelect3.Size = new System.Drawing.Size(121, 28);
            this.cmbWorkerSelect3.TabIndex = 5;
            this.cmbWorkerSelect3.SelectedIndexChanged += new System.EventHandler(this.cmbWorkerSelect3_SelectedIndexChanged);
            // 
            // cmbWorkerSelect2
            // 
            this.cmbWorkerSelect2.FormattingEnabled = true;
            this.cmbWorkerSelect2.Location = new System.Drawing.Point(398, 37);
            this.cmbWorkerSelect2.Name = "cmbWorkerSelect2";
            this.cmbWorkerSelect2.Size = new System.Drawing.Size(121, 28);
            this.cmbWorkerSelect2.TabIndex = 6;
            this.cmbWorkerSelect2.SelectedIndexChanged += new System.EventHandler(this.cmbWorkerSelect2_SelectedIndexChanged);
            // 
            // cmbWorkerSelect1
            // 
            this.cmbWorkerSelect1.FormattingEnabled = true;
            this.cmbWorkerSelect1.Location = new System.Drawing.Point(77, 37);
            this.cmbWorkerSelect1.Name = "cmbWorkerSelect1";
            this.cmbWorkerSelect1.Size = new System.Drawing.Size(127, 28);
            this.cmbWorkerSelect1.TabIndex = 7;
            this.cmbWorkerSelect1.SelectedIndexChanged += new System.EventHandler(this.cmbWorkerSelect1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(431, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Worker:";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(448, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(941, 612);
            this.panel5.TabIndex = 0;
            // 
            // AddEditAppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1386, 616);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Name = "AddEditAppointmentForm";
            this.Text = "AddEditAppointmentForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AddEditAppointmentForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgScheduleWorker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgScheduleWorker2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgScheduleWorker3)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbTreatment;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ListBox lstCustomerResults;
        private System.Windows.Forms.TextBox txtCustomerSearch;
        private System.Windows.Forms.ComboBox cmbWorker;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblExpectedEndTime;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dvgScheduleWorker1;
        private System.Windows.Forms.DataGridView dvgScheduleWorker2;
        private System.Windows.Forms.DataGridView dvgScheduleWorker3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbWorkerSelect3;
        private System.Windows.Forms.ComboBox cmbWorkerSelect2;
        private System.Windows.Forms.ComboBox cmbWorkerSelect1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblSelectedCustomer;
    }
}