using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{
    public class Worker
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime startWorkDate { get; set; }
        public string phone { get; set; }
        public WorkerType type { get; set; }
        public WorkerStatusType status { get; set; }
        public ByNoaBranch branchId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<Appointment> appointments { get; set; }






        public Worker(int id, string name, DateTime birthDate, DateTime startWorkDate, string phone, WorkerType type, WorkerStatusType status, ByNoaBranch branchId, string userName, string password)
        {
            this.id = id;
            this.name = name;
            this.birthDate = birthDate;
            this.startWorkDate = startWorkDate;
            this.phone = phone;
            this.type = type;
            this.status = status;
            this.branchId = branchId;
            this.userName = userName;
            this.password = password;
            this.appointments = new List<Appointment>();
            this.assignToBranch(branchId);

        }

        public void create_worker()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.SP_add_worker @id, @name, @birthDate, @startDate, @phone, @type, @status, @branchId";
            c.Parameters.AddWithValue("@id", this.id);
            c.Parameters.AddWithValue("@name", this.name);
            c.Parameters.AddWithValue("@birthDate", this.birthDate);
            c.Parameters.AddWithValue("@startDate", this.startWorkDate);
            c.Parameters.AddWithValue("@phone", this.phone);
            c.Parameters.AddWithValue("@type", this.type.ToString());
            c.Parameters.AddWithValue("@status", this.status.ToString());
            c.Parameters.AddWithValue("@branchId", this.branchId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(c);
        }
        // --- פונקציה חדשה: אימות והחייאת אובייקט מה-SQL ---
        public static Worker Login(string user, string pass)
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.SP_login_worker @user, @pass";
            c.Parameters.AddWithValue("@user", user);
            c.Parameters.AddWithValue("@pass", pass);

            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            if (rdr.Read())
            {
                // בדיוק כמו שעשית ב-init_workers:
                WorkerType T = (WorkerType)Enum.Parse(typeof(WorkerType), rdr.GetValue(5).ToString());
                WorkerStatusType S = (WorkerStatusType)Enum.Parse(typeof(WorkerStatusType), rdr.GetValue(6).ToString());

                // כאן עשיתי המרה ישירה ל-Enum של הסניף (כי המספר נמצא בעמודה 7)
                ByNoaBranch B = Program.find_branch_by_id(int.Parse(rdr.GetValue(7).ToString()));

                // יצירת האובייקט לפי סדר העמודות ב-SQL (0 עד 9)
                return new Worker(
                    int.Parse(rdr.GetValue(0).ToString()),
                    rdr.GetValue(1).ToString(),
                    DateTime.Parse(rdr.GetValue(2).ToString()),
                    DateTime.Parse(rdr.GetValue(3).ToString()),
                    rdr.GetValue(4).ToString(),
                    T, S, B,
                    rdr.GetValue(8).ToString(),
                    rdr.GetValue(9).ToString()
                );
            }
            return null; // אם לא מצאה כלום
        }

        public void assignToBranch(ByNoaBranch b)
        {
            // זה מוסיף את העובד הזה לרשימת העובדים של הסניף
            if (b != null)
            {
                b.AddWorker(this);
            }
        }

        public bool IsAvailable(DateTime date, TimeSpan startTime, Treatment treatment)
        {
            // רצים על רשימת התורים הפנימית של העובד (במקום לחפש ב-Program)
            foreach (Appointment app in this.appointments)
            {
                // בדיקה: האם התור הוא באותו תאריך והוא לא מבוטל?
                if (app.date.Date == date.Date && app.status != AppointmentStatus.cancelled)
                {
                    // חישוב זמני סיום
                    TimeSpan existingEnd = app.startTime.Add(TimeSpan.FromMinutes(app.treatment.durationInMinutes));
                    TimeSpan newEnd = startTime.Add(TimeSpan.FromMinutes(treatment.durationInMinutes));

                    // בדיקת חפיפה: (התחלה חדשה < סיום קיים) וגם (סיום חדש > התחלה קיימת)
                    if (startTime < existingEnd && newEnd > app.startTime)
                    {
                        return false; // חפיפה! אני תפוסה בזמן הזה
                    }
                }
            }
            return true; // עברנו על כל התורים ולא הייתה התנגשות -> אני פנויה
        }

        public void AddAppointment(Appointment app)
        {
            if (app != null)
            {
                this.appointments.Add(app);
            }
        }




        public override string ToString()
        {
            return this.name;
        }

    }
}
