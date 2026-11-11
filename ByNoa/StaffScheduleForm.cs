using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ByNoa
{
    public partial class StaffScheduleForm : Form
    {
        public StaffScheduleForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // מסך מלא
            this.BackColor = Color.White; // רקע נקי
        }

        private void StaffScheduleForm_Load(object sender, EventArgs e)
        {
            // 1. עיצוב ופריסה
            SetupGridStyle();

            // 2. לוגיקת הרשאות (RBAC) - כאן קורה הקסם!
            if (Program.CurrentWorker.type == WorkerType.cosmetician)
            {
                // --- מצב עובדת רגילה: רואה רק את עצמה ---
                List<Worker> selfList = new List<Worker>();
                selfList.Add(Program.CurrentWorker); // יוצרים רשימה שיש בה רק אותה

                cmbWorkerSelect.DataSource = selfList;
                cmbWorkerSelect.SelectedItem = Program.CurrentWorker; // בוחרים אותה אוטומטית
                cmbWorkerSelect.Enabled = false; // נועלים את האפשרות לשנות!

                this.Text = "My Schedule - " + Program.CurrentWorker.name;
            }
            else
            {
                // --- מצב מנהלת/רפרנטית: רואה את כולם ---
                if (Program.Workers != null)
                {
                    cmbWorkerSelect.DataSource = new List<Worker>(Program.Workers);
                }
                cmbWorkerSelect.Enabled = true; // משאירים פתוח לבחירה
            }

            // 3. הגדרות תאריך והפעלה ראשונית
            dtpScheduleDate.Value = DateTime.Today;
            RefreshWeeklySchedule();
        }

        private void SetupGridStyle()
        {
            // --- התיקון הגדול: מתיחת הגריד על כל המסך ---
            // זה אומר לגריד: "תיצמד לכל הקצוות של החלון ותגדל איתם"
            dgvWeeklySchedule.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // מיקום התחלתי וגודל (ליתר ביטחון)
            dgvWeeklySchedule.Location = new Point(20, 100);
            dgvWeeklySchedule.Width = this.ClientSize.Width - 40; // רוחב החלון פחות שוליים
            dgvWeeklySchedule.Height = this.ClientSize.Height - 120; // גובה החלון פחות שוליים

            // עיצוב יוקרתי (BYNOA Style)
            dgvWeeklySchedule.EnableHeadersVisualStyles = false;
            dgvWeeklySchedule.ColumnHeadersDefaultCellStyle.BackColor = Color.MediumVioletRed;
            dgvWeeklySchedule.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvWeeklySchedule.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 11); // גופן קצת יותר גדול
            dgvWeeklySchedule.ColumnHeadersHeight = 45; // כותרת גבוהה יותר

            dgvWeeklySchedule.RowTemplate.Height = 35; // שורות גבוהות ומרווחות יותר

            dgvWeeklySchedule.DefaultCellStyle.SelectionBackColor = Color.LavenderBlush;
            dgvWeeklySchedule.DefaultCellStyle.SelectionForeColor = Color.DarkRed;
            dgvWeeklySchedule.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvWeeklySchedule.RowHeadersVisible = false;
            dgvWeeklySchedule.GridColor = Color.Gainsboro;
            dgvWeeklySchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // מתיחת העמודות לרוחב
            dgvWeeklySchedule.BorderStyle = BorderStyle.None;
            dgvWeeklySchedule.BackgroundColor = Color.White; // רקע לבן לגריד עצמו
        }

        private void UpdateDynamicHeaders()
        {
            DateTime start = dtpScheduleDate.Value.Date;

            // עמודה 0 היא Time, עמודות 1-7 הן הימים
            for (int i = 1; i <= 7; i++)
            {
                DateTime currentDay = start.AddDays(i - 1);
                string colName = "colDay" + i;

                if (dgvWeeklySchedule.Columns[colName] != null)
                {
                    dgvWeeklySchedule.Columns[colName].HeaderText = currentDay.ToString("dddd\n dd/MM"); // שבירת שורה לשם יפה
                    dgvWeeklySchedule.Columns[colName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void RefreshWeeklySchedule()
        {
            UpdateDynamicHeaders();

            Worker selectedWorker = cmbWorkerSelect.SelectedItem as Worker;
            if (selectedWorker == null || selectedWorker.branchId == null) return;

            dgvWeeklySchedule.Rows.Clear();

            TimeSpan current = selectedWorker.branchId.openingHour;
            TimeSpan end = selectedWorker.branchId.closingHour;

            while (current < end)
            {
                int rowIndex = dgvWeeklySchedule.Rows.Add(current.ToString(@"hh\:mm"));
                DataGridViewRow row = dgvWeeklySchedule.Rows[rowIndex];

                // עיצוב עמודת השעה
                row.Cells[0].Style.BackColor = Color.WhiteSmoke;
                row.Cells[0].Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // מילוי ימי השבוע
                for (int dayIndex = 0; dayIndex < 7; dayIndex++)
                {
                    DateTime columnDate = dtpScheduleDate.Value.Date.AddDays(dayIndex);
                    int columnIndex = dayIndex + 1;

                    var app = selectedWorker.appointments.FirstOrDefault(a =>
                        a.date.Date == columnDate &&
                        a.status != AppointmentStatus.cancelled &&
                        current >= a.startTime && current < a.startTime.Add(TimeSpan.FromMinutes(a.treatment.durationInMinutes))
                    );

                    if (app != null)
                    {
                        row.Cells[columnIndex].Value = app.customer.name + "\n" + app.treatment.name;
                        row.Cells[columnIndex].Style.BackColor = Color.MistyRose;
                        row.Cells[columnIndex].Style.ForeColor = Color.MediumVioletRed;
                        row.Cells[columnIndex].Tag = app;
                        row.Cells[columnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                current = current.Add(TimeSpan.FromMinutes(30));
            }
        }

        // --- אירועים ---
        private void dtpScheduleDate_ValueChanged(object sender, EventArgs e)
        {
            RefreshWeeklySchedule();
        }

        private void cmbWorkerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshWeeklySchedule();
        }

    }
}