using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ByNoa
{
    // Form for Adding or Editing an Appointment
    public partial class AddEditAppointmentForm : Form
    {
        private Customer selectedCustomer = null;
        private Appointment editingAppointment = null;

        // Constructor: Initializes a new appointment form
        public AddEditAppointmentForm()
        {
            InitializeComponent();
            this.editingAppointment = null;
            this.WindowState = FormWindowState.Maximized;
        }

        // Constructor: Initializes the form for editing an existing appointment
        public AddEditAppointmentForm(Appointment app)
        {
            InitializeComponent();
            this.editingAppointment = app;
            this.WindowState = FormWindowState.Maximized;
        }

        // Event Handler: Form Load - Sets up layout, design, and loads data
        private void AddEditAppointmentForm_Load(object sender, EventArgs e)
        {
            // 1. Setup layout (30% Left, 70% Right)
            InitLayout30_70();

            // 2. Arrange Left Panel Inputs
            ArrangeLeftPanelInputs();

            // 3. Align Right Side Grids (Calendars)
            AlignRightSideGrids();

            // 4. Apply Visual Design
            ApplyDesign();

            // 5. Setup Grids for Schedule View
            SetupScheduleGrids(dvgScheduleWorker1);
            SetupScheduleGrids(dvgScheduleWorker2);
            SetupScheduleGrids(dvgScheduleWorker3);
            PopulateWorkerSelectors();

            // 6. Load Data into ComboBoxes
            cmbTreatment.DataSource = new List<Treatment>(Program.Treatments);
            cmbWorker.DataSource = new List<Worker>(Program.Workers);

            // Prevent free text input
            cmbTreatment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWorker.DropDownStyle = ComboBoxStyle.DropDownList;

            // Check if in Edit Mode or New Mode
            if (editingAppointment != null)
            {
                selectedCustomer = editingAppointment.customer;
                lblSelectedCustomer.Text = "EDITING: " + selectedCustomer.name.ToUpper();
                txtCustomerSearch.Visible = false;
                lstCustomerResults.Visible = false;
                txtCustomerSearch.Text = selectedCustomer.name;
                cmbTreatment.SelectedItem = editingAppointment.treatment;
                cmbWorker.SelectedItem = editingAppointment.worker;
                dtpDate.Value = editingAppointment.date;
                txtStartTime.Text = editingAppointment.startTime.ToString(@"hh\:mm");
            }
            else
            {
                lblSelectedCustomer.Text = "NEW APPOINTMENT";
                dtpDate.Value = DateTime.Today;
            }

            RefreshDailySchedules();
        }

        // Method: Arranges input controls on the left panel
        private void ArrangeLeftPanelInputs()
        {
            Control p1 = this.Controls.Find("panel1", true).FirstOrDefault();
            if (p1 == null) return;

            // Remove old auto-generated labels
            for (int i = p1.Controls.Count - 1; i >= 0; i--)
            {
                Control c = p1.Controls[i];
                if (c is Label && c.Name.StartsWith("lblAuto")) p1.Controls.RemoveAt(i);
            }

            int leftMargin = 30;
            int width = p1.Width - 60;
            int fieldHeight = 35;
            int currentY = 20;
            int gap = 85;

            // --- Customer Name Label ---
            lblSelectedCustomer.Location = new Point(leftMargin, currentY);
            lblSelectedCustomer.AutoSize = true;
            lblSelectedCustomer.MaximumSize = new Size(width, 0);
            p1.Controls.Add(lblSelectedCustomer);

            currentY += 60;

            // --- Customer Search Field ---
            if (editingAppointment == null)
            {
                CreateLabel("Search Customer:", leftMargin, currentY, p1);

                txtCustomerSearch.Visible = true;
                ResetControlState(txtCustomerSearch, leftMargin, currentY + 30, width, fieldHeight);

                // Setup result list
                lstCustomerResults.Location = new Point(leftMargin, currentY + 30 + fieldHeight);
                lstCustomerResults.Width = width;
                lstCustomerResults.Height = 120;
                lstCustomerResults.Visible = false;
                p1.Controls.Add(lstCustomerResults);
                lstCustomerResults.BringToFront();
            }

            currentY += gap;

            // --- Treatment Selection ---
            CreateLabel("Choose Treatment:", leftMargin, currentY, p1);
            ResetControlState(cmbTreatment, leftMargin, currentY + 30, width, fieldHeight);

            currentY += gap;

            // --- Worker Selection ---
            CreateLabel("Choose Worker:", leftMargin, currentY, p1);
            ResetControlState(cmbWorker, leftMargin, currentY + 30, width, fieldHeight);

            currentY += gap;

            // --- Date Selection ---
            CreateLabel("Choose Date:", leftMargin, currentY, p1);
            ResetControlState(dtpDate, leftMargin, currentY + 30, width, fieldHeight);

            currentY += gap;

            // --- Time Selection ---
            CreateLabel("Start Time:", leftMargin, currentY, p1);
            ResetControlState(txtStartTime, leftMargin, currentY + 30, width, fieldHeight);

            lblExpectedEndTime.Location = new Point(leftMargin, currentY + 65);
            lblExpectedEndTime.Visible = true;
            lblExpectedEndTime.Font = new Font("Century Gothic", 9F, FontStyle.Italic);

            // --- Save Button ---
            if (btnSave != null)
            {
                if (btnSave.Parent != p1) p1.Controls.Add(btnSave);
                btnSave.Dock = DockStyle.Bottom;
                btnSave.Height = 65;
                btnSave.BringToFront();
            }
        }

        // Helper Method: Resets control properties for consistent layout
        private void ResetControlState(Control c, int x, int y, int w, int h)
        {
            if (c == null) return;
            c.Dock = DockStyle.None;
            c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            c.Location = new Point(x, y);
            c.Width = w;
            c.Height = h;
            c.Font = new Font("Century Gothic", 11F, FontStyle.Regular);
            c.BringToFront();
        }

        // Helper Method: Creates dynamic labels
        private void CreateLabel(string text, int x, int y, Control parent)
        {
            Label l = new Label();
            l.Name = "lblAuto_" + Guid.NewGuid().ToString().Substring(0, 4);
            l.Text = text;
            l.Location = new Point(x, y);
            l.AutoSize = true;
            l.Font = new Font("Century Gothic", 11F, FontStyle.Regular);
            l.ForeColor = Color.MediumVioletRed;
            parent.Controls.Add(l);
        }

        // Method: Applies custom colors and fonts
        private void ApplyDesign()
        {
            Control p1 = this.Controls.Find("panel1", true).FirstOrDefault();
            if (p1 != null) p1.BackColor = Color.WhiteSmoke;

            Control p6 = this.Controls.Find("panel6", true).FirstOrDefault();
            if (p6 != null) p6.BackColor = Color.White;

            lblSelectedCustomer.ForeColor = Color.MediumVioletRed;
            lblSelectedCustomer.Font = new Font("Century Gothic", 18, FontStyle.Bold | FontStyle.Underline);

            if (btnSave != null)
            {
                btnSave.FlatStyle = FlatStyle.Flat;
                btnSave.FlatAppearance.BorderSize = 0;
                btnSave.BackColor = Color.MediumVioletRed;
                btnSave.ForeColor = Color.White;
                btnSave.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                btnSave.Text = "SAVE APPOINTMENT";
            }
        }

        // Method: Initializes the main TableLayoutPanel (30/70 split)
        private void InitLayout30_70()
        {
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Parent = this;
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.RowCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            Control p1 = this.Controls.Find("panel1", true).FirstOrDefault();
            if (p1 != null) { p1.Parent = mainLayout; mainLayout.Controls.Add(p1, 0, 0); p1.Dock = DockStyle.Fill; }

            Control p6 = this.Controls.Find("panel6", true).FirstOrDefault();
            if (p6 != null) { p6.Parent = mainLayout; mainLayout.Controls.Add(p6, 1, 0); p6.Dock = DockStyle.Fill; }

            mainLayout.BringToFront();
        }

        // Method: Aligns the grids on the right side
        private void AlignRightSideGrids()
        {
            Control p6 = this.Controls.Find("panel6", true).FirstOrDefault();
            if (p6 == null) return;
            p6.Controls.Clear();

            TableLayoutPanel gridLayout = new TableLayoutPanel();
            gridLayout.Parent = p6;
            gridLayout.Dock = DockStyle.Fill;
            gridLayout.ColumnCount = 3;
            gridLayout.RowCount = 2;
            gridLayout.Padding = new Padding(0);
            gridLayout.Margin = new Padding(0);

            gridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            gridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            gridLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            gridLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            gridLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            EmbedControlInTable(gridLayout, cmbWorkerSelect1, 0, 0);
            EmbedControlInTable(gridLayout, dvgScheduleWorker1, 0, 1);
            EmbedControlInTable(gridLayout, cmbWorkerSelect2, 1, 0);
            EmbedControlInTable(gridLayout, dvgScheduleWorker2, 1, 1);
            EmbedControlInTable(gridLayout, cmbWorkerSelect3, 2, 0);
            EmbedControlInTable(gridLayout, dvgScheduleWorker3, 2, 1);
        }

        // Helper Method: Embeds a control into the TableLayoutPanel
        private void EmbedControlInTable(TableLayoutPanel tlp, Control c, int col, int row)
        {
            if (c == null) return;
            if (c.Parent != null) c.Parent.Controls.Remove(c);
            c.Parent = tlp;
            tlp.Controls.Add(c, col, row);
            c.Dock = DockStyle.Fill;
            c.Margin = new Padding(1);
            if (c is DataGridView dgv) dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Method: Configures the DataGridView appearance
        private void SetupScheduleGrids(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.Columns.Add("Time", "Time");
            dgv.Columns.Add("Status", "Activity");
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowTemplate.Height = 35;
            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        // Method: Fills the worker's schedule grid based on availability
        private void FillWorkerGrid(DataGridView dgv, Worker worker)
        {
            if (worker == null || worker.branchId == null) return;
            dgv.Rows.Clear();
            Treatment selectedTreatment = (Treatment)cmbTreatment.SelectedItem;
            TimeSpan current = worker.branchId.openingHour;
            TimeSpan end = worker.branchId.closingHour;

            while (current < end)
            {
                int rowIndex = dgv.Rows.Add(current.ToString(@"hh\:mm"), "Free");
                DataGridViewRow row = dgv.Rows[rowIndex];

                // Check for existing appointment
                var existingApp = Program.Appointments.FirstOrDefault(a =>
                    a.worker.id == worker.id &&
                    a.date.Date == dtpDate.Value.Date &&
                    a.status != AppointmentStatus.cancelled &&
                    current >= a.startTime && current < a.startTime.Add(TimeSpan.FromMinutes(a.treatment.durationInMinutes))
                );

                if (existingApp != null)
                {
                    row.Cells[1].Value = existingApp.treatment.name + " (" + existingApp.customer.name + ")";
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (selectedTreatment != null && !worker.IsAvailable(dtpDate.Value, current, selectedTreatment))
                {
                    row.Cells[1].Value = "N/A";
                    row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
                current = current.Add(TimeSpan.FromMinutes(30));
            }
        }

        // Event Handler: Save Button Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveAppointment(sender, e);
        }

        // Method: Core logic to save the appointment
        private void saveAppointment(object sender, EventArgs e)
        {
            if (dtpDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Cannot schedule an appointment in the past!", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedCustomer == null) { MessageBox.Show("Please select a customer!"); return; }

            Worker w = (Worker)cmbWorker.SelectedItem;
            Treatment t = (Treatment)cmbTreatment.SelectedItem;
            TimeSpan start;

            if (!TimeSpan.TryParse(txtStartTime.Text, out start)) { MessageBox.Show("Invalid Time"); return; }

            if (dtpDate.Value.Date == DateTime.Today && start < DateTime.Now.TimeOfDay)
            {
                MessageBox.Show("Cannot schedule for a time that has already passed today!", "Time Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isSelf = (editingAppointment != null && editingAppointment.worker.id == w.id && editingAppointment.date.Date == dtpDate.Value.Date && editingAppointment.startTime == start);

            if (isSelf || w.IsAvailable(dtpDate.Value, start, t))
            {
                if (editingAppointment == null)
                {
                    new Appointment(0, dtpDate.Value, start, TimeSpan.Zero, "", AppointmentStatus.scheduled, w, selectedCustomer, t, true);
                }
                else
                {
                    editingAppointment.update_appointment(dtpDate.Value, start, editingAppointment.actualStartTime, editingAppointment.status, w, selectedCustomer, t, "");
                }

                MessageBox.Show("Saved!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                showErrorMassage();
            }
        }

        // Method: Displays error message
        private void showErrorMassage()
        {
            MessageBox.Show("Worker is busy!");
        }

        // Method: Handles clicking on a schedule cell to set time
        private void HandleGridClick(DataGridView dgv, ComboBox workerSelector, int rowIndex)
        {
            if (rowIndex >= 0 && dgv.Rows[rowIndex].Cells[0].Value != null)
            {
                if (dgv.Rows[rowIndex].DefaultCellStyle.BackColor == Color.MistyRose) return;
                txtStartTime.Text = dgv.Rows[rowIndex].Cells[0].Value.ToString();
                if (workerSelector.SelectedItem != null) cmbWorker.SelectedItem = workerSelector.SelectedItem;
                UpdateExpectedEndTime();
            }
        }

        // Method: Updates the expected end time label
        private void UpdateExpectedEndTime()
        {
            if (cmbTreatment.SelectedItem is Treatment t && TimeSpan.TryParse(txtStartTime.Text, out TimeSpan start))
            {
                lblExpectedEndTime.Text = "End: " + start.Add(TimeSpan.FromMinutes(t.durationInMinutes)).ToString(@"hh\:mm");
                lblExpectedEndTime.Visible = true;
            }
        }

        // Event Handlers for UI Interactions
        private void dtpDate_ValueChanged(object sender, EventArgs e) => selectDate(sender, e);
        private void selectDate(object sender, EventArgs e) { RefreshDailySchedules(); }
        private void cmbWorkerSelect1_SelectedIndexChanged(object sender, EventArgs e) => RefreshDailySchedules();
        private void cmbWorkerSelect2_SelectedIndexChanged(object sender, EventArgs e) => RefreshDailySchedules();
        private void cmbWorkerSelect3_SelectedIndexChanged(object sender, EventArgs e) => RefreshDailySchedules();
        private void cmbTreatment_SelectedIndexChanged(object sender, EventArgs e) { selectTreatment(sender, e); }
        private void selectTreatment(object sender, EventArgs e) { RefreshDailySchedules(); UpdateExpectedEndTime(); }
        private void txtStartTime_TextChanged(object sender, EventArgs e) => insertTime(sender, e);
        private void insertTime(object sender, EventArgs e) { UpdateExpectedEndTime(); }
        private void dvgScheduleWorker1_CellClick(object sender, DataGridViewCellEventArgs e) => HandleGridClick(dvgScheduleWorker1, cmbWorkerSelect1, e.RowIndex);
        private void dvgScheduleWorker2_CellClick(object sender, DataGridViewCellEventArgs e) => HandleGridClick(dvgScheduleWorker2, cmbWorkerSelect2, e.RowIndex);
        private void dvgScheduleWorker3_CellClick(object sender, DataGridViewCellEventArgs e) => HandleGridClick(dvgScheduleWorker3, cmbWorkerSelect3, e.RowIndex);

        // Event Handler: Customer Search Text Changed
        private void txtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            customerSearch(sender, e);

        }

        // Method: Performs customer search
        private void customerSearch(object sender, EventArgs e)
        {
            if (editingAppointment == null && txtCustomerSearch.Text.Length >= 2)
            {
                var f = Program.find_customers_by_id_name(txtCustomerSearch.Text);
                lstCustomerResults.DataSource = f;
                lstCustomerResults.Visible = f.Count > 0; // Show only if results exist
            }
            else
            {
                lstCustomerResults.Visible = false; // Hide if no input
            }
        }

        // Event Handler: Selection from Customer Search Results
        private void lstCustomerResults_MouseClick(object sender, MouseEventArgs e)
        {
            selectFormListCustomerReasult(sender, e);
        }

        // Method: Handles selection from customer list
        private void selectFormListCustomerReasult(object sender, EventArgs e)
        {
            if (lstCustomerResults.SelectedItem is Customer c)
            {
                selectedCustomer = c;
                lblSelectedCustomer.Text = "Client: " + c.name;
                txtCustomerSearch.Text = c.name;
                lstCustomerResults.Visible = false;
            }

        }

        // Method: Refreshes all worker schedule grids
        private void RefreshDailySchedules()
        {
            FillWorkerGrid(dvgScheduleWorker1, (Worker)cmbWorkerSelect1.SelectedItem);
            FillWorkerGrid(dvgScheduleWorker2, (Worker)cmbWorkerSelect2.SelectedItem);
            FillWorkerGrid(dvgScheduleWorker3, (Worker)cmbWorkerSelect3.SelectedItem);
        }

        // Method: Populates worker selectors
        private void PopulateWorkerSelectors()
        {
            cmbWorkerSelect1.DataSource = new List<Worker>(Program.Workers);
            cmbWorkerSelect2.DataSource = new List<Worker>(Program.Workers);
            cmbWorkerSelect3.DataSource = new List<Worker>(Program.Workers);
            if (Program.Workers.Count > 1) cmbWorkerSelect2.SelectedIndex = 1;
            if (Program.Workers.Count > 2) cmbWorkerSelect3.SelectedIndex = 2;
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        // Placeholder Event Handlers (Do not remove)
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint_1(object sender, PaintEventArgs e) { }
        private void panel6_Paint(object sender, PaintEventArgs e) { }

        // Event Handler: Worker Combobox Selection Changed
        private void cmbWorker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectWorker(sender, e);
        }

        // Method: Logic for worker selection
        private void selectWorker(object sender, EventArgs e)
        {
            Worker currentWorker = (Worker)cmbWorker.SelectedItem;
        }
    }
}