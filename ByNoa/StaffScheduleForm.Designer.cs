namespace ByNoa
{
    partial class StaffScheduleForm
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
            this.cmbWorkerSelect = new System.Windows.Forms.ComboBox();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.dgvWeeklySchedule = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDay1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDay2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDay3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDay4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDay5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDay6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDay7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeeklySchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbWorkerSelect
            // 
            this.cmbWorkerSelect.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWorkerSelect.FormattingEnabled = true;
            this.cmbWorkerSelect.Location = new System.Drawing.Point(253, 97);
            this.cmbWorkerSelect.Name = "cmbWorkerSelect";
            this.cmbWorkerSelect.Size = new System.Drawing.Size(206, 38);
            this.cmbWorkerSelect.TabIndex = 0;
            this.cmbWorkerSelect.SelectedIndexChanged += new System.EventHandler(this.cmbWorkerSelect_SelectedIndexChanged);
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpScheduleDate.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpScheduleDate.Location = new System.Drawing.Point(732, 95);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(314, 37);
            this.dtpScheduleDate.TabIndex = 1;
            this.dtpScheduleDate.ValueChanged += new System.EventHandler(this.dtpScheduleDate_ValueChanged);
            // 
            // dgvWeeklySchedule
            // 
            this.dgvWeeklySchedule.AllowUserToAddRows = false;
            this.dgvWeeklySchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWeeklySchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeeklySchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colDay1,
            this.colDay2,
            this.colDay3,
            this.colDay4,
            this.colDay5,
            this.colDay6,
            this.colDay7});
            this.dgvWeeklySchedule.Location = new System.Drawing.Point(12, 171);
            this.dgvWeeklySchedule.Name = "dgvWeeklySchedule";
            this.dgvWeeklySchedule.ReadOnly = true;
            this.dgvWeeklySchedule.RowHeadersVisible = false;
            this.dgvWeeklySchedule.RowHeadersWidth = 62;
            this.dgvWeeklySchedule.RowTemplate.Height = 28;
            this.dgvWeeklySchedule.Size = new System.Drawing.Size(1071, 419);
            this.dgvWeeklySchedule.TabIndex = 2;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Time";
            this.colTime.MinimumWidth = 8;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colDay1
            // 
            this.colDay1.HeaderText = "Column1";
            this.colDay1.MinimumWidth = 8;
            this.colDay1.Name = "colDay1";
            this.colDay1.ReadOnly = true;
            // 
            // colDay2
            // 
            this.colDay2.HeaderText = "Column1";
            this.colDay2.MinimumWidth = 8;
            this.colDay2.Name = "colDay2";
            this.colDay2.ReadOnly = true;
            // 
            // colDay3
            // 
            this.colDay3.HeaderText = "Column1";
            this.colDay3.MinimumWidth = 8;
            this.colDay3.Name = "colDay3";
            this.colDay3.ReadOnly = true;
            // 
            // colDay4
            // 
            this.colDay4.HeaderText = "Column1";
            this.colDay4.MinimumWidth = 8;
            this.colDay4.Name = "colDay4";
            this.colDay4.ReadOnly = true;
            // 
            // colDay5
            // 
            this.colDay5.HeaderText = "Column1";
            this.colDay5.MinimumWidth = 8;
            this.colDay5.Name = "colDay5";
            this.colDay5.ReadOnly = true;
            // 
            // colDay6
            // 
            this.colDay6.HeaderText = "Column1";
            this.colDay6.MinimumWidth = 8;
            this.colDay6.Name = "colDay6";
            this.colDay6.ReadOnly = true;
            // 
            // colDay7
            // 
            this.colDay7.HeaderText = "Column1";
            this.colDay7.MinimumWidth = 8;
            this.colDay7.Name = "colDay7";
            this.colDay7.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(495, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 34);
            this.label1.TabIndex = 5;
            this.label1.Text = "Schedule";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(146, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 30);
            this.label2.TabIndex = 6;
            this.label2.Text = "Worker:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(648, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "Date:";
            // 
            // StaffScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 602);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvWeeklySchedule);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this.cmbWorkerSelect);
            this.Name = "StaffScheduleForm";
            this.Text = "StaffScheduleForm";
            this.Load += new System.EventHandler(this.StaffScheduleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeeklySchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWorkerSelect;
        private System.Windows.Forms.DateTimePicker dtpScheduleDate;
        private System.Windows.Forms.DataGridView dgvWeeklySchedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDay1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDay2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDay3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDay4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDay5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDay6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDay7;
    }
}