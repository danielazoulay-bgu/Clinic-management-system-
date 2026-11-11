namespace ByNoa
{
    partial class CustomerCard
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
            this.lblTitleCustomerCard = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIDCustomer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNameCustomer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPhoneCustomer = new System.Windows.Forms.TextBox();
            this.dtpBirthCustomer = new System.Windows.Forms.DateTimePicker();
            this.dtpJoinCustomer = new System.Windows.Forms.DateTimePicker();
            this.cmbStatusCustomer = new System.Windows.Forms.ComboBox();
            this.btnDeleteCustomer = new System.Windows.Forms.Button();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBackToCustomers = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitleCustomerCard
            // 
            this.lblTitleCustomerCard.AutoSize = true;
            this.lblTitleCustomerCard.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleCustomerCard.ForeColor = System.Drawing.Color.DeepPink;
            this.lblTitleCustomerCard.Location = new System.Drawing.Point(89, 13);
            this.lblTitleCustomerCard.Name = "lblTitleCustomerCard";
            this.lblTitleCustomerCard.Size = new System.Drawing.Size(288, 43);
            this.lblTitleCustomerCard.TabIndex = 0;
            this.lblTitleCustomerCard.Text = "Customer Card";
            this.lblTitleCustomerCard.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(110, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID:";
            // 
            // txtIDCustomer
            // 
            this.txtIDCustomer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDCustomer.Location = new System.Drawing.Point(176, 73);
            this.txtIDCustomer.Name = "txtIDCustomer";
            this.txtIDCustomer.Size = new System.Drawing.Size(200, 32);
            this.txtIDCustomer.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Full Name:";
            // 
            // txtNameCustomer
            // 
            this.txtNameCustomer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameCustomer.Location = new System.Drawing.Point(176, 123);
            this.txtNameCustomer.Name = "txtNameCustomer";
            this.txtNameCustomer.Size = new System.Drawing.Size(200, 32);
            this.txtNameCustomer.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "Phone:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 30);
            this.label4.TabIndex = 6;
            this.label4.Text = "Birth Date:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 30);
            this.label5.TabIndex = 7;
            this.label5.Text = "Join Date:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(80, 348);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 30);
            this.label6.TabIndex = 8;
            this.label6.Text = "Status:";
            // 
            // txtPhoneCustomer
            // 
            this.txtPhoneCustomer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneCustomer.Location = new System.Drawing.Point(176, 176);
            this.txtPhoneCustomer.Name = "txtPhoneCustomer";
            this.txtPhoneCustomer.Size = new System.Drawing.Size(200, 32);
            this.txtPhoneCustomer.TabIndex = 9;
            // 
            // dtpBirthCustomer
            // 
            this.dtpBirthCustomer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBirthCustomer.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthCustomer.Location = new System.Drawing.Point(177, 232);
            this.dtpBirthCustomer.Name = "dtpBirthCustomer";
            this.dtpBirthCustomer.Size = new System.Drawing.Size(200, 32);
            this.dtpBirthCustomer.TabIndex = 10;
            // 
            // dtpJoinCustomer
            // 
            this.dtpJoinCustomer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpJoinCustomer.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpJoinCustomer.Location = new System.Drawing.Point(176, 294);
            this.dtpJoinCustomer.Name = "dtpJoinCustomer";
            this.dtpJoinCustomer.Size = new System.Drawing.Size(201, 32);
            this.dtpJoinCustomer.TabIndex = 11;
            // 
            // cmbStatusCustomer
            // 
            this.cmbStatusCustomer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatusCustomer.FormattingEnabled = true;
            this.cmbStatusCustomer.Location = new System.Drawing.Point(176, 348);
            this.cmbStatusCustomer.Name = "cmbStatusCustomer";
            this.cmbStatusCustomer.Size = new System.Drawing.Size(200, 31);
            this.cmbStatusCustomer.TabIndex = 12;
            // 
            // btnDeleteCustomer
            // 
            this.btnDeleteCustomer.BackgroundImage = global::ByNoa.Properties.Resources.DELETE_ICON1;
            this.btnDeleteCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDeleteCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCustomer.ForeColor = System.Drawing.Color.Transparent;
            this.btnDeleteCustomer.Location = new System.Drawing.Point(-28, 360);
            this.btnDeleteCustomer.Name = "btnDeleteCustomer";
            this.btnDeleteCustomer.Size = new System.Drawing.Size(152, 129);
            this.btnDeleteCustomer.TabIndex = 13;
            this.btnDeleteCustomer.UseVisualStyleBackColor = true;
            this.btnDeleteCustomer.Click += new System.EventHandler(this.btnDeleteCustomer_Click);
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.BackgroundImage = global::ByNoa.Properties.Resources.SAVEICON;
            this.btnSaveCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveCustomer.ForeColor = System.Drawing.Color.Transparent;
            this.btnSaveCustomer.Location = new System.Drawing.Point(361, 385);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(98, 79);
            this.btnSaveCustomer.TabIndex = 14;
            this.btnSaveCustomer.UseVisualStyleBackColor = true;
            this.btnSaveCustomer.Click += new System.EventHandler(this.btnSaveCustomer_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnBackToCustomers);
            this.panel1.Controls.Add(this.txtIDCustomer);
            this.panel1.Controls.Add(this.btnSaveCustomer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbStatusCustomer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTitleCustomerCard);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpJoinCustomer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtPhoneCustomer);
            this.panel1.Controls.Add(this.dtpBirthCustomer);
            this.panel1.Controls.Add(this.txtNameCustomer);
            this.panel1.Controls.Add(this.btnDeleteCustomer);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(71, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 469);
            this.panel1.TabIndex = 15;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnBackToCustomers
            // 
            this.btnBackToCustomers.BackgroundImage = global::ByNoa.Properties.Resources.ICON_BACK1;
            this.btnBackToCustomers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBackToCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToCustomers.ForeColor = System.Drawing.Color.Transparent;
            this.btnBackToCustomers.Location = new System.Drawing.Point(-1, -1);
            this.btnBackToCustomers.Name = "btnBackToCustomers";
            this.btnBackToCustomers.Size = new System.Drawing.Size(80, 57);
            this.btnBackToCustomers.TabIndex = 15;
            this.btnBackToCustomers.UseVisualStyleBackColor = true;
            this.btnBackToCustomers.Click += new System.EventHandler(this.btnBackToCustomers_Click);
            // 
            // CustomerCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ByNoa.Properties.Resources.רקע_ורוד_מתחלף1;
            this.ClientSize = new System.Drawing.Size(620, 582);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerCard";
            this.Text = "CustomerCard";
            this.Load += new System.EventHandler(this.CustomerCard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitleCustomerCard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIDCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNameCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPhoneCustomer;
        private System.Windows.Forms.DateTimePicker dtpBirthCustomer;
        private System.Windows.Forms.DateTimePicker dtpJoinCustomer;
        private System.Windows.Forms.ComboBox cmbStatusCustomer;
        private System.Windows.Forms.Button btnDeleteCustomer;
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBackToCustomers;
    }
}