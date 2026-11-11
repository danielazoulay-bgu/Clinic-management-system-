using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{// Represents a Customer entity in the system
    public class Customer
    {
        // Basic customer details corresponding to DB columns
        public int id { get; set; }
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public string phone { get; set; }
        public DateTime joinDate { get; set; }
        public CustomerStatusType status { get; set; }
        public List<Appointment> appointments { get; set; }

        public List<FinancialTransaction> trans { get; set; }

        public bool is_new { get; set; }
        public Customer(int id, String name, DateTime birthDate, string phone, DateTime joinDate, CustomerStatusType status, bool is_new)
        {
            this.id = id;
            this.name = name;
            this.birthDate = birthDate;
            this.phone = phone;
            this.joinDate = joinDate;
            this.status = status;
            this.is_new = is_new;
            this.appointments = new List<Appointment>();
            this.trans = new List<FinancialTransaction>();

            // If it's a new customer, save immediately to DB and global list
            if (is_new)
            {
                this.create_customer();
                Program.Customers.Add(this);
            }

        }

        // Method: Adds an appointment to the customer's history list
        public void AddAppointment(Appointment app)
        {
            if (app != null)
            {
                this.appointments.Add(app);
            }
        }

        // Method: Adds an appointment to the customer's history list
        public void AddTransaction(FinancialTransaction ft)
        {
            if (ft != null)
            {
                this.trans.Add(ft);
            }
        }
        // Method: Inserts the new customer into the SQL database
        public void create_customer()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.SP_add_customer @id, @name, @birthDate, @phone, @joinDate, @status";
            c.Parameters.AddWithValue("@id", this.id);
            c.Parameters.AddWithValue("@name", this.name);
            c.Parameters.AddWithValue("@birthDate", this.birthDate);
            c.Parameters.AddWithValue("@phone", this.phone);
            c.Parameters.AddWithValue("@joinDate", this.joinDate);
            c.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(c);
        }

        // Method: Updates existing customer details in memory and database
        public void update_customer(string name, string phone, CustomerStatusType status, DateTime birthDate)
        {
            this.name = name;
            this.phone = phone;
            this.status = status;
            this.birthDate = birthDate;

            try
            {
                SQL_CON sc = new SQL_CON();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "EXECUTE dbo.SP_Update_customer @id, @name, @phone, @birthDate, @status";

                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@name", this.name);
                cmd.Parameters.AddWithValue("@phone", this.phone);
                cmd.Parameters.AddWithValue("@birthDate", this.birthDate);
                cmd.Parameters.AddWithValue("@status", this.status.ToString());

                sc.execute_non_query(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer in SQL: " + ex.Message);
            }
        }

        // Method: Deletes the customer from the database and memory
        public void delete_customer()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.SP_delete_customer @id";
            cmd.Parameters.AddWithValue("@id", this.id);

            SQL_CON sc = new SQL_CON();
            sc.execute_non_query(cmd);

            Program.Customers.Remove(this);
        }

        // Placeholder for viewing history logic 
        public List<Appointment> viewTretmentHistory()
        {
            return this.appointments
                           .Where(a => a.status == AppointmentStatus.completed)
                           .ToList();
        }


        // Override ToString to display Customer Name in UI controls (e.g., ComboBox)
        public override string ToString()
        {
            return this.name;
        }

    }

    }

