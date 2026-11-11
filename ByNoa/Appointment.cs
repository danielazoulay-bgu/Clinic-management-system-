using ByNoa;
using System;
using System.Data.SqlClient;
using static System.Windows.Forms.AxHost;

// Represents an Appointment entity in the system
public class Appointment
{
    // Static counter to generate unique IDs for new appointments
    public static int next_id = 0;
    // Properties corresponding to database columns
    public int id { get; set; }
    public DateTime date { get; set; }
    public TimeSpan startTime { get; set; }
    public TimeSpan actualStartTime { get; set; }
    public AppointmentStatus status { get; set; }
    public string notes { get; set; }
    public Worker worker { get; set; }
    public Customer customer { get; set; }
    public Treatment treatment { get; set; }
    public bool is_new { get; set; }

    // Constructor: Initializes an appointment object
    public Appointment(int id, DateTime date, TimeSpan startTime, TimeSpan actualStartTime, string notes, AppointmentStatus status, Worker worker, Customer customer, Treatment treatment, bool is_new)
    {
        this.id = id;
        this.date = date;
        this.startTime = startTime;
        this.actualStartTime = actualStartTime;
        this.notes = notes;
        this.status = status;
        this.worker = worker;
        this.customer = customer;
        this.treatment = treatment;
        this.is_new = is_new;
        // Add this appointment to related entities' internal lists
        worker.AddAppointment(this);
        customer.AddAppointment(this);
        treatment.AddAppointment(this);

        // Logic for creating a BRAND NEW appointment
        if (is_new)
        {
            this.id = next_id;

            next_id = next_id + 1;

            this.create_appointment();
            Program.Appointments.Add(this);
        }
        // If not new (loaded from DB), we skip ID generation to preserve original data
    }
    // Method: Inserts the appointment into the SQL database
    public void create_appointment()
    {
        SqlCommand c = new SqlCommand();
        c.CommandText = "EXECUTE dbo.SP_add_appointment @id, @date, @time, @status, @workerId, @customerId, @treatmentName, @notes";
        c.Parameters.AddWithValue("@id", this.id);
        c.Parameters.AddWithValue("@date", this.date);
        c.Parameters.AddWithValue("@time", this.startTime);
        c.Parameters.AddWithValue("@status", this.status.ToString());
        c.Parameters.AddWithValue("@workerId", this.worker.id);
        c.Parameters.AddWithValue("@customerId", this.customer.id);
        c.Parameters.AddWithValue("@treatmentName", this.treatment.name);
        // Handle nullable notes field
        if (string.IsNullOrEmpty(this.notes))
            c.Parameters.AddWithValue("@notes", DBNull.Value);
        else
            c.Parameters.AddWithValue("@notes", this.notes);

        SQL_CON SC = new SQL_CON();
        SC.execute_non_query(c);
    }
    // Method: Deletes the appointment from the database
    public void delete_appointment()
    {
        SqlCommand c = new SqlCommand();
        c.CommandText = "EXECUTE dbo.SP_delete_appointment @id";
        c.Parameters.AddWithValue("@id", this.id);

        SQL_CON SC = new SQL_CON();
        SC.execute_non_query(c);

        Program.Appointments.Remove(this);
    }
    // Method: Updates existing appointment details in the database and memory
    public void update_appointment(DateTime newDate, TimeSpan newStartTime, TimeSpan newActualStart, AppointmentStatus newStatus, Worker newWorker, Customer newCust, Treatment newTreat, string newNotes)
    {
        SqlCommand c = new SqlCommand();
        c.CommandText = "EXECUTE dbo.SP_Update_appointment @id, @date, @startTime, @actualStart, @status, @workerId, @customerId, @treatmentName, @notes";

        c.Parameters.AddWithValue("@id", this.id);
        c.Parameters.AddWithValue("@date", newDate);
        c.Parameters.AddWithValue("@startTime", newStartTime);
        c.Parameters.AddWithValue("@actualStart", newActualStart);
        c.Parameters.AddWithValue("@status", newStatus.ToString());
        c.Parameters.AddWithValue("@workerId", newWorker.id);
        c.Parameters.AddWithValue("@customerId", newCust.id);
        c.Parameters.AddWithValue("@treatmentName", newTreat.name);

        // טיפול בהערות (בדיקה אם ריק כפי שעשית ב-Create)
        if (string.IsNullOrEmpty(newNotes))
            c.Parameters.AddWithValue("@notes", DBNull.Value);
        else
            c.Parameters.AddWithValue("@notes", newNotes);

        SQL_CON SC = new SQL_CON();
        SC.execute_non_query(c);

        // Update the object's properties in memory to reflect changes        this.date = newDate;
        this.startTime = newStartTime;
        this.actualStartTime = newActualStart;
        this.status = newStatus;
        this.worker = newWorker;
        this.customer = newCust;
        this.treatment = newTreat;
        this.notes = newNotes;
    }

    // --- State Machine Support Methods(Implementation pending) ---

    // Sends an automatic reminder 24h before the appointment
    public void sendReminder()
    {
        // Logic to send SMS/Email notification to the customer
        // Transition: Scheduled -> Reminded
    }

    // Uploads the health declaration PDF file
    public void uploadPDF(string filePath)
    {
        // Logic to save file path to database
        // Transition: Reminded -> MedicalReview
    }

    // Adds clinical notes during or after treatment
    public void addNotes(string content)
    {
        this.notes = content;
        // Logic to save notes to database
        // Active State: InTreatment
    }

    // Moves the appointment to the archive table after 2 years
    public void sendToArchive()
    {
        // Logic to move record from Active DB to Archive DB
        // Transition: Completed/Cancelled -> Archived
    }
}