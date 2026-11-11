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
    public partial class MainDashboard : Form
    {
        // משתנה לבקרה על מצב התפריט (פתוח/סגור)
        bool sidebarExpand = false;

        public MainDashboard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized; // פתיחה על מסך מלא
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            // 1. ברגע שהמסך עולה, נטען את מסך התורים כברירת מחדל
            loadFormIntoPanel(new AppointmentsForm());

            // 2. כוונון כותרת החלון העליונה
            if (Program.CurrentWorker != null)
            {
                this.Text = "BYNOA Management System - Logged in as: " + Program.CurrentWorker.name;

                // --- השינוי החדש: עדכון השם בתפריט הצדדי ---
                lblName.Text = "Hi, " + Program.CurrentWorker.name;

                // מרכוז השם מתחת לתמונה (שיהיה יפה באמצע)
                // אנחנו מניחים ש-lblName נמצא בתוך pnlSidebar או פאנל עליון בתוכו
                if (lblName.Parent != null)
                {
                    CenterControlInParent(lblName);
                }
            }

            // 3. הרשאות (אופציונלי): הסתרת כפתורים לקוסמטיקאית
            // if (Program.CurrentWorker.type == WorkerType.cosmetician) { ... }
        }

        // --- פונקציית עזר חדשה למרכוז אלמנטים ---
        private void CenterControlInParent(Control ctrl)
        {
            if (ctrl.Parent == null) return;

            // חישוב האמצע: (רוחב האבא - רוחב האלמנט) חלקי 2
            int newX = (ctrl.Parent.Width - ctrl.Width) / 2;
            ctrl.Location = new Point(newX, ctrl.Location.Y);
        }

        // --- פונקציית עזר להצגת מסכים בתוך הפאנל המרכזי ---
        private void loadFormIntoPanel(Form childForm)
        {
            // ניקוי תוכן קודם מהפאנל המרכזי
            if (this.pnlMainContent.Controls.Count > 0)
                this.pnlMainContent.Controls.RemoveAt(0);

            // הגדרת הפורם כפקד פנימי
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill; // מתיחה על כל הפאנל

            this.pnlMainContent.Controls.Add(childForm);
            this.pnlMainContent.Tag = childForm;
            childForm.Show();
        }

        // --- לוגיקת האנימציה של התפריט (Timer) ---
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                // קיפול התפריט
                pnlSidebar.Width -= 20;
                if (pnlSidebar.Width <= 60) // רוחב האייקונים בלבד
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                // פתיחת התפריט
                pnlSidebar.Width += 20;
                if (pnlSidebar.Width >= 150) // רוחב הטקסט המלא
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                    // אופציונלי: למרכז שוב את השם אחרי הפתיחה
                    CenterControlInParent(lblName);
                }
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            // הפעלת הטיימר לקיפול/פתיחה
            sidebarTimer.Start();
        }

        // --- ניווט בין מסכים ---
        private void btnAppointments_Click(object sender, EventArgs e)
        {
            loadFormIntoPanel(new AppointmentsForm());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            loadFormIntoPanel(new Customers());
        }

        private void btnViewSchdule_Click(object sender, EventArgs e)
        {
            // יצירת מופע חדש של מסך הלוזים וטעינתו
            StaffScheduleForm scheduleView = new StaffScheduleForm();
            loadFormIntoPanel(scheduleView);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // יציאה מסודרת וחזרה למסך הלוגין
            Program.CurrentWorker = null;
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        // --- אירועים ריקים (אפשר להשאיר או למחוק אם לא בשימוש) ---
        private void pnlMainContent_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void pnlSidebar_Paint(object sender, PaintEventArgs e) { }

        private void MainDashboard_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pnlMainContent_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}