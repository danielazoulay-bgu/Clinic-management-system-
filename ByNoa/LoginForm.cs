using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByNoa
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // איסוף הנתונים מהתיבות שעיצבת
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            // בדיקת התחברות מול ה-SQL
            Worker authenticatedWorker = Worker.Login(user, pass);

            if (authenticatedWorker != null)
            {
                // שמירת העובדת המחוברת בזיכרון לשימוש עתידי
                Program.CurrentWorker = authenticatedWorker;

                MessageBox.Show("Login Successful! Welcome " + authenticatedWorker.name);


                MainDashboard mainSystem = new MainDashboard();
                mainSystem.Show();

                this.Hide(); // הסתרת מסך הלוגין
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // שמירה על הפונקציה שה-Designer יצר כדי שלא יקרוס
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}