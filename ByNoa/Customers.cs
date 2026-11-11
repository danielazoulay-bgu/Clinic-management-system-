using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByNoa
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }

        private void Customers_Load(object sender, EventArgs e)
        {
            LoadCustomers();

            // חסימת הוספת לקוחה לקוסמטיקאית
            if (Program.CurrentWorker.type == WorkerType.cosmetician)
            {
                btnAddCustomer.Visible = false;
            }
        }

        private void LoadCustomers()
        {
            RefreshGrid(Program.Customers);
        }

        private void RefreshGrid(List<Customer> listToDisplay)
        {
            dvgCustomers.DataSource = null;
            dvgCustomers.DataSource = listToDisplay;

            if (dvgCustomers.Columns["is_new"] != null) dvgCustomers.Columns["is_new"].Visible = false;

            if (dvgCustomers.Columns["birthDate"] != null)
            {
                dvgCustomers.Columns["birthDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dvgCustomers.Columns["birthDate"].HeaderText = "Birth Date";
            }

            if (dvgCustomers.Columns["joinDate"] != null)
            {
                dvgCustomers.Columns["joinDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dvgCustomers.Columns["joinDate"].HeaderText = "Join Date";
            }

            if (dvgCustomers.Columns["id"] != null) dvgCustomers.Columns["id"].HeaderText = "ID";
            if (dvgCustomers.Columns["name"] != null) dvgCustomers.Columns["name"].HeaderText = "Full Name";
            if (dvgCustomers.Columns["phone"] != null) dvgCustomers.Columns["phone"].HeaderText = "Phone";
            if (dvgCustomers.Columns["status"] != null) dvgCustomers.Columns["status"].HeaderText = "Status";
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string term = txtSearchCustomer.Text.ToLower();
            var filteredList = Program.Customers.Where(c =>
                c.name.ToLower().Contains(term) ||
                c.id.ToString().Contains(term) ||
                c.phone.Contains(term)
            ).ToList();

            RefreshGrid(filteredList);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerCard card = new CustomerCard(null);
            card.ShowDialog();
            RefreshGrid(Program.Customers);
        }

        private void dvgCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Customer selected = (Customer)dvgCustomers.Rows[e.RowIndex].DataBoundItem;
                ClientHistoryForm historyPage = new ClientHistoryForm(selected);
                historyPage.ShowDialog();
                RefreshGrid(Program.Customers);
            }
        }
    }
}