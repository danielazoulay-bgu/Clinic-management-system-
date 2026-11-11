using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace ByNoa
{
    public partial class CustomerCard : Form
    {
        private Customer selectedCustomer; 

        public CustomerCard(Customer customer)
        {
            InitializeComponent();
            this.selectedCustomer = customer; 
        }

        private void CustomerCard_Load(object sender, EventArgs e)
        {
            // כפייה של אנגלית על התאריכים בחלון זה
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            btnBackToCustomers.Visible = true;

            // מילוי התיבה של הסטטוס מה-Enum
            cmbStatusCustomer.DataSource = Enum.GetValues(typeof(CustomerStatusType));

            if (selectedCustomer != null)
            {
                // --- מצב עריכה (הגענו מדאבל קליק) ---
                FillDetails();
                txtIDCustomer.Enabled = false;
                btnDeleteCustomer.Visible = true;
                this.Text = "Edit Customer: " + selectedCustomer.name;
            }
            else
            {
                // --- מצב הוספה (הגענו מה-+) ---
                txtIDCustomer.Enabled = true;
                btnDeleteCustomer.Visible = false;
                this.Text = "Add New Customer";
            }
        }

        private void FillDetails()
        {
            txtIDCustomer.Text = selectedCustomer.id.ToString();
            txtNameCustomer.Text = selectedCustomer.name;
            txtPhoneCustomer.Text = selectedCustomer.phone;
            dtpBirthCustomer.Value = selectedCustomer.birthDate;
            dtpJoinCustomer.Value = selectedCustomer.joinDate;
            cmbStatusCustomer.SelectedItem = selectedCustomer.status;
        }

    
           private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            // קריאה לפונקציית הולידציה המרכזית
            if (!IsValidForm())
            {
                return; // אם הטופס לא תקין, עוצרים כאן
            }

            try
            {
                SaveData(); // פונקציה נפרדת לביצוע השמירה בפועל
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // פונקציית ה"מאסטר" שבודקת את כל הטופס
        private bool IsValidForm()
        {
            // בדיקת שדות ריקים
            if (string.IsNullOrWhiteSpace(txtIDCustomer.Text) ||
                string.IsNullOrWhiteSpace(txtNameCustomer.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneCustomer.Text) ||
                cmbStatusCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("All fields are mandatory.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // הפעלת בדיקות ספציפיות
            if (!ValidateID(txtIDCustomer.Text.Trim())) return false;
            if (!ValidateName(txtNameCustomer.Text.Trim())) return false;
            if (!ValidatePhone(txtPhoneCustomer.Text.Trim())) return false;
            if (!ValidateDates(dtpBirthCustomer.Value.Date, dtpJoinCustomer.Value.Date)) return false;

            return true; // הכל תקין!
        }

        private bool ValidateID(string id)
        {
            // 1. בדיקת תקינות מבנית (9 ספרות)
            if (!System.Text.RegularExpressions.Regex.IsMatch(id, @"^\d{9}$"))
            {
                MessageBox.Show("ID must be exactly 9 digits.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIDCustomer.Focus();
                return false;
            }

            // 2. --- התיקון: בדיקה אם הלקוחה כבר קיימת (רק במצב של לקוחה חדשה) ---
            if (selectedCustomer == null) // רק אם אנחנו יוצרים חדשה
            {
                // נניח שיש לך פונקציית עזר כזו ב-Program, או שתעשי LINQ פשוט
                Customer existing = Program.Customers.FirstOrDefault(c => c.id == int.Parse(id));

                if (existing != null)
                {
                    MessageBox.Show("This ID already exists in the system!", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIDCustomer.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool ValidateName(string name)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z\u0590-\u05FF\s]+$"))
            {
                MessageBox.Show("Name must contain only letters and spaces.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNameCustomer.Focus();
                return false;
            }
            return true;
        }

        private bool ValidatePhone(string phone)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^05\d-?\d{7}$"))
            {
                MessageBox.Show("Invalid Phone format (05X-XXXXXXX).", "Invalid Phone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneCustomer.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateDates(DateTime birth, DateTime join)
        {
            DateTime today = DateTime.Now.Date;

            if (birth > today || join > today)
            {
                MessageBox.Show("Dates cannot be in the future.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (join <= birth)
            {
                MessageBox.Show("Join Date must be after Birth Date.", "Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        
        private void SaveData()
        {
            if (selectedCustomer == null)
            {
                // יצירת לקוחה חדשה (מפעיל את ה-Constructor)
                new Customer(
                    int.Parse(txtIDCustomer.Text),
                    txtNameCustomer.Text.Trim(),
                    dtpBirthCustomer.Value.Date,
                    txtPhoneCustomer.Text.Trim(),
                    dtpJoinCustomer.Value.Date,
                    (CustomerStatusType)cmbStatusCustomer.SelectedItem,
                    true);

                MessageBox.Show("New Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // עדכון לקוחה קיימת (מפעיל את הפונקציה במחלקה)
                selectedCustomer.update_customer(
                    txtNameCustomer.Text.Trim(),
                    txtPhoneCustomer.Text.Trim(),
                    (CustomerStatusType)cmbStatusCustomer.SelectedItem,
                    dtpBirthCustomer.Value.Date
                );

                MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                // --- מחיקה: כל הלוגיקה (SQL ורשימה) עברה למחלקה! ---
                selectedCustomer.delete_customer();

                MessageBox.Show("Customer Deleted Successfully!");
                this.Close();
            }
        }

        // --- פונקציות ריקות שקיימות ב-Designer שלך (שמרתי עליהן לבקשתך) ---
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void btnBackToCustomers_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
