using ByNoa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ByNoa
{
    // The main dashboard for viewing and managing appointments
    public partial class AppointmentsForm : Form
    {
        public AppointmentsForm()
        {
            InitializeComponent();
        }
        // Event Handler: Form Load
        private void AppointmentsForm_Load(object sender, EventArgs e)
        {
            RunAutoReminders();

            SetupGridColumns();
            LoadWorkersToFilter();
            LoadStatusOptions();
            SetDefaultFilters();
            RefreshGrid(Program.Appointments);
            // If the user is a Cosmetician (not Manager), restrict permissions
            if (Program.CurrentWorker.type == WorkerType.cosmetician)
            {
                btnAddNewAppointment.Visible = false;

                foreach (DataGridViewColumn col in dgvAppointments.Columns)
                {
                    if (col.DataPropertyName == "status" || col.DataPropertyName == "notes")
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                }
            }
        }

        // Method: Automates status updates to 'Reminded' 24 hours before
        private void RunAutoReminders()
        {
            bool changesMade = false;
            DateTime tomorrow = DateTime.Today.AddDays(1);

            foreach (var app in Program.Appointments)
            {
                if (app.status == AppointmentStatus.scheduled && app.date.Date == tomorrow)
                {
                    app.status = AppointmentStatus.reminded;
                    app.update_appointment(app.date, app.startTime, app.actualStartTime, app.status, app.worker, app.customer, app.treatment, app.notes);
                    changesMade = true;
                }
            }

            if (changesMade)
            {
                RefreshGrid(Program.Appointments);
            }
        }
        // Method: Refreshes the DataGridView with a sorted list of appointments
        private void RefreshGrid(List<Appointment> listToShow)
        {
            if (listToShow == null) return;

            var sortedList = listToShow
                .OrderByDescending(a => a.date)
                .ThenByDescending(a => a.startTime)
                .ToList();

            dgvAppointments.DataSource = null;
            dgvAppointments.DataSource = sortedList;

            if (dgvAppointments.Columns["id"] != null) dgvAppointments.Columns["id"].HeaderText = "ID";
            if (dgvAppointments.Columns["date"] != null) dgvAppointments.Columns["date"].HeaderText = "Date";
            if (dgvAppointments.Columns["startTime"] != null) dgvAppointments.Columns["startTime"].HeaderText = "Time";
            if (dgvAppointments.Columns["status"] != null) dgvAppointments.Columns["status"].HeaderText = "Status";
            if (dgvAppointments.Columns["notes"] != null) dgvAppointments.Columns["notes"].HeaderText = "Notes";

            if (dgvAppointments.Columns["is_new"] != null) dgvAppointments.Columns["is_new"].Visible = false;
        }

        // Method: Populates the worker filter dropdown
        private void LoadWorkersToFilter()
        {
            cmbFilterWorker.Items.Clear();
            cmbFilterWorker.Items.Add("All Workers");

            if (Program.Workers != null)
            {
                foreach (var worker in Program.Workers)
                {
                    cmbFilterWorker.Items.Add(worker);
                }
            }
            cmbFilterWorker.SelectedIndex = 0;
        }

        // Method: Populates the status filter dropdown
        private void LoadStatusOptions()
        {
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.Add("All Statuses");
            foreach (var status in Enum.GetValues(typeof(AppointmentStatus)))
            {
                cmbFilterStatus.Items.Add(status);
            }
            cmbFilterStatus.SelectedIndex = 0;
        }
        // Event Handler: Apply Filter Button Click
        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            string searchName = txtSearchCustomer.Text.Trim().ToLower();
            var query = Program.Appointments.AsEnumerable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(a => a.customer.name.ToLower().Contains(searchName));
            }

            if (dtpFromDate.Checked)
            {
                query = query.Where(a => a.date.Date >= dtpFromDate.Value.Date);
            }

            if (dtpToDate.Checked)
            {
                query = query.Where(a => a.date.Date <= dtpToDate.Value.Date);
            }

            if (cmbFilterWorker.SelectedIndex > 0)
            {
                Worker selectedWorker = (Worker)cmbFilterWorker.SelectedItem;
                query = query.Where(a => a.worker.id == selectedWorker.id);
            }

            if (cmbFilterStatus.SelectedIndex > 0)
            {
                // תיקון קטן לסינון לפי ה-Enum החדש
                string selectedStatusString = cmbFilterStatus.SelectedItem.ToString();
                if (Enum.TryParse(selectedStatusString, out AppointmentStatus statusEnum))
                {
                    query = query.Where(a => a.status == statusEnum);
                }
            }

            var filtered = query.ToList();
            RefreshGrid(filtered);
        }
        // Event Handler: Double Click on Cell (Opens Edit Form)
        private void dgvAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Program.CurrentWorker.type == WorkerType.cosmetician) return;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvAppointments.Columns[e.ColumnIndex].ReadOnly == true)
                {
                    Appointment selected = dgvAppointments.Rows[e.RowIndex].DataBoundItem as Appointment;

                    if (selected != null)
                    {
                        AddEditAppointmentForm editForm = new AddEditAppointmentForm(selected);
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            RefreshGrid(Program.Appointments);
                        }
                    }
                }
            }
        }
        // Event Handler: Add New Appointment Button
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddEditAppointmentForm addForm = new AddEditAppointmentForm(null);

            if (addForm.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid(Program.Appointments);
            }
        }

        // --- Logic for Manual Status Updates in the Grid ---
        private void dgvAppointments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Appointment editedApp = (Appointment)dgvAppointments.Rows[e.RowIndex].DataBoundItem;
                bool statusChanged = false;

                if (dgvAppointments.Columns[e.ColumnIndex].DataPropertyName == "status")
                {
                    if (editedApp.status == AppointmentStatus.in_treatment)
                    {
                        bool hasHealthDeclaration = Program.PDFs.Any(p => p.appointment != null && p.appointment.id == editedApp.id);

                        if (!hasHealthDeclaration)
                        {
                            MessageBox.Show("Cannot start treatment! Health declaration missing.\nPlease upload PDF to switch status to 'Medical Review', then approve manually.",
                                            "Safety Check", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                            editedApp.status = AppointmentStatus.reminded;
                            this.BeginInvoke(new Action(() => RefreshGrid(Program.Appointments))); // רענון כדי לבטל את השינוי בגרפיקה
                            return;
                        }

                        if (editedApp.actualStartTime == TimeSpan.Zero)
                        {
                            editedApp.actualStartTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                            statusChanged = true;
                        }
                    }

                    if (editedApp.status == AppointmentStatus.completed)
                    {
                        if (editedApp.actualStartTime == TimeSpan.Zero)
                        {
                            MessageBox.Show("Cannot complete treatment! It hasn't started yet.\nPlease set status to 'In Treatment' first.",
                                            "Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            editedApp.status = AppointmentStatus.in_treatment; // מחזירים אחורה
                            this.BeginInvoke(new Action(() => RefreshGrid(Program.Appointments)));
                            return;
                        }
                    }
                }

                // Save changes to database
                editedApp.update_appointment(
                    editedApp.date,
                    editedApp.startTime,
                    editedApp.actualStartTime,
                    editedApp.status,
                    editedApp.worker,
                    editedApp.customer,
                    editedApp.treatment,
                    editedApp.notes
                );

                dgvAppointments.InvalidateRow(e.RowIndex);
                if (statusChanged) MessageBox.Show("Treatment Started! Time logged: " + editedApp.actualStartTime.ToString(@"hh\:mm"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new Action(() => RefreshGrid(Program.Appointments)));
            }
        }
        // Event Handler: Reset Filters
        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            txtSearchCustomer.Clear();
            dtpFromDate.Checked = false;
            dtpToDate.Checked = false;

            if (cmbFilterWorker.Items.Count > 0) cmbFilterWorker.SelectedIndex = 0;
            if (cmbFilterStatus.Items.Count > 0) cmbFilterStatus.SelectedIndex = 0;

            RefreshGrid(Program.Appointments);
        }

        // --- Logic for PDF Upload & Medical Review Transition ---
        private void dgvAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAppointments.Columns[e.ColumnIndex].Name == "colPDF")
            {
                Appointment selectedApp = (Appointment)dgvAppointments.Rows[e.RowIndex].DataBoundItem;
                var pdf = Program.PDFs.FirstOrDefault(p => p.appointment != null && p.appointment.id == selectedApp.id);

                if (pdf != null)
                {
                    pdf.viewPDF();
                }
                else
                {
                    if (Program.CurrentWorker.type == WorkerType.cosmetician)
                    {
                        MessageBox.Show("Cosmeticians cannot upload files.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    openFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FilePDF newFile = new FilePDF(openFileDialog1.FileName, "Health_" + selectedApp.id, FileType.healthDeclaration, selectedApp, null, true);
                        newFile.uploadPDF(openFileDialog1.FileName);

                        // --- השינוי החשוב: עדכון סטטוס אוטומטי ל-Medical Review ---
                        selectedApp.status = AppointmentStatus.medical_review;
                        selectedApp.update_appointment(selectedApp.date, selectedApp.startTime, selectedApp.actualStartTime, selectedApp.status, selectedApp.worker, selectedApp.customer, selectedApp.treatment, selectedApp.notes);
                        // -----------------------------------------------------------

                        RefreshGrid(Program.Appointments); // רענון כדי לראות את הסטטוס החדש בגריד
                        MessageBox.Show("Declaration uploaded! Status changed to 'Medical Review'.");
                    }
                }
            }
        }

        private void SetDefaultFilters()
        {
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now.AddYears(1);
            dtpFromDate.Checked = false;
            dtpToDate.Checked = false;
        }
        // Configures the DataGridView appearance and columns
        private void SetupGridColumns()
        {
            dgvAppointments.AutoGenerateColumns = false;
            dgvAppointments.Columns.Clear();

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "id", HeaderText = "ID", ReadOnly = true, Width = 50 });

            DataGridViewTextBoxColumn dateCol = new DataGridViewTextBoxColumn { DataPropertyName = "date", HeaderText = "Date", ReadOnly = true };
            dateCol.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvAppointments.Columns.Add(dateCol);

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "startTime", HeaderText = "Time", ReadOnly = true });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "actualStartTime", HeaderText = "Actual Start", Name = "actualStartTime", ReadOnly = false });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "worker", HeaderText = "Worker", ReadOnly = true });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "customer", HeaderText = "Customer", ReadOnly = true });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "treatment", HeaderText = "Treatment", ReadOnly = true });

            DataGridViewComboBoxColumn statusCol = new DataGridViewComboBoxColumn();
            statusCol.DataPropertyName = "status";
            statusCol.HeaderText = "Status";
            statusCol.DataSource = Enum.GetValues(typeof(AppointmentStatus));
            statusCol.ReadOnly = false;
            dgvAppointments.Columns.Add(statusCol);

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "notes", HeaderText = "Notes", ReadOnly = false });

            DataGridViewImageColumn pdfCol = new DataGridViewImageColumn { Name = "colPDF", HeaderText = "File", ImageLayout = DataGridViewImageCellLayout.Zoom, Width = 40 };
            dgvAppointments.Columns.Add(pdfCol);

            dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }
        // Event Handler: Cell Formatting (Colors and Icons)
        private void dgvAppointments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAppointments.Rows[e.RowIndex].DataBoundItem is Appointment app)
            {
                if (app.date.Date == DateTime.Today)
                {
                    dgvAppointments.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LavenderBlush;
                    dgvAppointments.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Pink;
                }
            }

            if (dgvAppointments.Columns[e.ColumnIndex].DataPropertyName == "date" && e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }

            if (dgvAppointments.Columns[e.ColumnIndex].DataPropertyName == "actualStartTime" && e.Value is TimeSpan ts && ts == TimeSpan.Zero)
            {
                e.Value = "";
                e.FormattingApplied = true;
            }

            if (dgvAppointments.Columns[e.ColumnIndex].Name == "colPDF" && e.RowIndex >= 0)
            {
                Appointment rowApp = (Appointment)dgvAppointments.Rows[e.RowIndex].DataBoundItem;
                bool hasPdf = Program.PDFs.Any(p => p.appointment != null && p.appointment.id == rowApp.id);
                e.Value = hasPdf ? Properties.Resources.pdf_icon : Properties.Resources.PlusIcon;
                e.FormattingApplied = true;
            }
        }

        // Quick Filter for Today's Appointments
        private void btnTodayOnly_Click(object sender, EventArgs e)
        {
            var todayApps = Program.Appointments.Where(a => a.date.Date == DateTime.Today).ToList();
            RefreshGrid(todayApps.Count > 0 ? todayApps : Program.Appointments);
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}
