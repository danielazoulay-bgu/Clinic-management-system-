using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ByNoa
{
    // Form for viewing the treatment history of a specific customer
    public partial class ClientHistoryForm : Form
    {
        private Customer selectedCustomer;

        // Constructor: Receives a Customer object to display history for

        public ClientHistoryForm(Customer customer)
        {
            InitializeComponent();
            this.selectedCustomer = customer;
            this.lblHeader.Text = "Treatment History: " + customer.name.ToUpper();

            this.dvgHistory.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dvgHistory_CellFormatting);
        }

        // Event Handler: Form Load
        private void ClientHistoryForm_Load(object sender, EventArgs e)
        {
            if (dvgHistory.Columns["colPDF"] == null)
            {
                DataGridViewImageColumn pdfCol = new DataGridViewImageColumn();
                pdfCol.Name = "colPDF";
                pdfCol.HeaderText = "";
                pdfCol.Width = 40;
                pdfCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dvgHistory.Columns.Add(pdfCol);
            }

            dvgHistory.Columns["colPDF"].DisplayIndex = 0;

            rbCompleted.Checked = true;
            LoadHistoryData();

            if (Program.CurrentWorker.type == WorkerType.cosmetician)
            {
                btnEditCustomer.Visible = false;
            }
        }

        // Method: Loads appointment history based on selected filter
        private void LoadHistoryData()
        {
            var history = selectedCustomer.appointments;

            if (rbCompleted.Checked)
            {
                history = history.Where(a => a.status == AppointmentStatus.completed).ToList();
            }

            var pdfCol = dvgHistory.Columns["colPDF"];

            dvgHistory.DataSource = null;
            dvgHistory.DataSource = history.OrderByDescending(a => a.date).ToList();

            if (dvgHistory.Columns["colPDF"] == null && pdfCol != null)
            {
                dvgHistory.Columns.Add(pdfCol);
            }

            if (dvgHistory.Columns["id"] != null) dvgHistory.Columns["id"].Visible = false;
            if (dvgHistory.Columns["is_new"] != null) dvgHistory.Columns["is_new"].Visible = false;
            if (dvgHistory.Columns["customer"] != null) dvgHistory.Columns["customer"].Visible = false;

            if (dvgHistory.Columns["worker"] != null) dvgHistory.Columns["worker"].Visible = false;
            if (dvgHistory.Columns["treatment"] != null) dvgHistory.Columns["treatment"].Visible = false;

            if (dvgHistory.Columns["colPDF"] != null)
            {
                dvgHistory.Columns["colPDF"].Width = 40;
                dvgHistory.Columns["colPDF"].HeaderText = "";
                dvgHistory.Columns["colPDF"].DisplayIndex = 0; // מוודא שזה בצד שמאל
            }
        }

        // Event Handler: Formats cell content (Logic for Icons and Empty Times)
        private void dvgHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dvgHistory.Columns[e.ColumnIndex].DataPropertyName == "actualStartTime")
            {
                if (e.Value is TimeSpan ts && ts == TimeSpan.Zero)
                {
                    e.Value = ""; // משאיר את התא ריק
                    e.FormattingApplied = true;
                }
            }

            if (dvgHistory.Columns[e.ColumnIndex].Name == "colPDF" && e.RowIndex >= 0)
            {
                Appointment rowApp = (Appointment)dvgHistory.Rows[e.RowIndex].DataBoundItem;

                bool hasPdf = Program.PDFs.Any(p => p.appointment != null && p.appointment.id == rowApp.id);

                if (hasPdf)
                {
                    e.Value = Properties.Resources.pdf_icon; // מציג את התמונה
                }
                else
                {
                    e.Value = new Bitmap(1, 1); // תמונה ריקה (שקוף)
                }
                e.FormattingApplied = true;
            }

            if (dvgHistory.Columns[e.ColumnIndex].DataPropertyName == "date" && e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }
        }

        // Event Handler: Click on PDF Icon to view file
        private void dvgHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dvgHistory.Columns[e.ColumnIndex].Name == "colPDF")
            {
                Appointment selectedApp = (Appointment)dvgHistory.Rows[e.RowIndex].DataBoundItem;
                var pdf = Program.PDFs.FirstOrDefault(p => p.appointment != null && p.appointment.id == selectedApp.id);

                if (selectedApp.date > DateTime.Now)
                {
                    MessageBox.Show("This is a future appointment. The health declaration will be uploaded on the day of the treatment.", "Future Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (pdf == null)
                {
                    MessageBox.Show("No health declaration was found for this past appointment.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    pdf.viewPDF();
                }
            }
        }
        // Event Handlers: Filter Toggles
        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAll.Checked) LoadHistoryData();
        }

        private void rbCompleted_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCompleted.Checked) LoadHistoryData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Event Handler: Edit Customer Details
        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            CustomerCard editCard = new CustomerCard(this.selectedCustomer);
            editCard.ShowDialog();
            this.lblHeader.Text = "Treatment History: " + selectedCustomer.name.ToUpper();
            LoadHistoryData(); // רענון אחרי עריכה
        }
    }
}