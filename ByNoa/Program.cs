using ByNoa;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByNoa
{
    // The main entry class for the application.
    public static class Program
    {
        public static System.Collections.Generic.List<Worker> Workers;
        public static System.Collections.Generic.List<ByNoaBranch> Branches;
        public static System.Collections.Generic.List<Customer> Customers;
        public static System.Collections.Generic.List<Supplier> Suppliers;
        public static System.Collections.Generic.List<Product> Products;
        public static System.Collections.Generic.List<Treatment> Treatments;
        public static System.Collections.Generic.List<FinancialTransaction> Transactions;
        public static System.Collections.Generic.List<Appointment> Appointments;
        public static System.Collections.Generic.List<FilePDF> PDFs;
        // Stores the currently logged-in worker
        public static Worker CurrentWorker;



        [STAThread]
        // Master initialization method: Calls all specific load methods
        public static void initLists()
        {
            init_branch();
            init_workers();
            init_customers();
            init_suppliers();
            init_products();
            initTreatments();
            initTransactions();
            init_appointment();
            init_pdfs();

        }

        // --- Helper Methods: Search functions to find objects in memory ---
        public static Customer find_customer_by_id(int id)
        {
            for (int i = 0; i < Customers.Count; i++)
            {
                if (Customers[i].id == id)
                    return Customers[i];
            }
            return null;
        }

        // Performs a case-insensitive search for customers by Name or ID
        public static List<Customer> find_customers_by_id_name(string query)
        {
         
            string q = query.ToLower();

            return Customers.Where(c =>
                   c.name.ToLower().Contains(q) ||
                   c.id.ToString().Contains(q)
            ).ToList();
        }

        public static Appointment find_appointment_by_id(int id)
        {
            for (int i = 0; i < Appointments.Count; i++)
            {
                if (Appointments[i].id == id)
                    return Appointments[i];
            }
            return null;
        }

        public static FinancialTransaction find_transaction_by_id(int id)
        {
            for (int i = 0; i < Transactions.Count; i++)
            {
                if (Transactions[i].id == id)
                    return Transactions[i];
            }
            return null;
        }

        public static Treatment find_treatment_by_name(string name)
        {
            for (int i = 0; i < Treatments.Count; i++)
            {
                if (Treatments[i].name == name)
                    return Treatments[i];
            }
            return null;
        }

        public static Worker find_worker_by_id(int id)
        {
            for (int i = 0; i < Workers.Count; i++)
            {
                if (Workers[i].id == id)
                    return Workers[i];
            }
            return null;
        }

        public static ByNoaBranch find_branch_by_id(int id)
        {
            for (int i = 0; i < Branches.Count; i++)
            {
                if (Branches[i].id == id)
                    return Branches[i];
            }
            return null;
        }


        public static Supplier find_supplier_by_name(string supplierName)
        {
            for (int i = 0; i < Suppliers.Count; i++)
            {
                if (Suppliers[i].name == supplierName)
                    return Suppliers[i];
            }
            return null;
        }
        // --- Data Loading Methods (Fetching from DB via Stored Procedures) ---

        // Loads all Branches
        public static void init_branch()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_Branches";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Branches = new List<ByNoaBranch>();

            while (rdr.Read())
            {
                ByNoaBranch b = new ByNoaBranch(int.Parse(rdr.GetValue(0).ToString()), rdr.GetValue(1).ToString(), rdr.GetValue(2).ToString(), rdr.GetValue(3).ToString(), rdr.GetValue(4).ToString(), TimeSpan.Parse(rdr.GetValue(5).ToString()), TimeSpan.Parse(rdr.GetValue(6).ToString()));
                Branches.Add(b);
            }

        }
        // Loads all Workers and links them to their Branch
        public static void init_workers()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_Workers";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Workers = new List<Worker>();

            while (rdr.Read())
            {
                WorkerType T = (WorkerType)Enum.Parse(typeof(WorkerType), rdr.GetValue(5).ToString());
                WorkerStatusType S = (WorkerStatusType)Enum.Parse(typeof(WorkerStatusType), rdr.GetValue(6).ToString());
                ByNoaBranch B = find_branch_by_id(int.Parse(rdr.GetValue(7).ToString()));
                Worker w = new Worker(int.Parse(rdr.GetValue(0).ToString()), rdr.GetValue(1).ToString(), DateTime.Parse(rdr.GetValue(2).ToString()), DateTime.Parse(rdr.GetValue(3).ToString()), rdr.GetValue(4).ToString(), T, S, B, rdr.GetValue(8).ToString(), rdr.GetValue(9).ToString());
                Workers.Add(w);
     
            }

        }
        // Loads all Customers
        public static void init_customers()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_customers";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Customers = new List<Customer>();

            while (rdr.Read())
            {
                CustomerStatusType S = (CustomerStatusType)Enum.Parse(typeof(CustomerStatusType), rdr.GetValue(5).ToString());
                Customer C = new Customer(int.Parse(rdr.GetValue(0).ToString()), rdr.GetValue(1).ToString(), DateTime.Parse(rdr.GetValue(2).ToString()), rdr.GetValue(3).ToString(), DateTime.Parse(rdr.GetValue(4).ToString()), S, false);
                Customers.Add(C);
            }

        }


        // Loads all Suppliers
        public static void init_suppliers()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_suppliers";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Suppliers = new List<Supplier>();

            while (rdr.Read())
            {
                SupplierType S = (SupplierType)Enum.Parse(typeof(SupplierType), rdr.GetValue(1).ToString());
                Supplier C = new Supplier(rdr.GetValue(0).ToString(), S, rdr.GetValue(2).ToString(), rdr.GetValue(3).ToString(), rdr.GetValue(4).ToString(), int.Parse(rdr.GetValue(5).ToString()));
                Suppliers.Add(C);
            }

    
        }
        // Loads all Products and links them to Suppliers
        public static void init_products()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_products";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Products = new List<Product>();

            while (rdr.Read())
            {
                Supplier s = find_supplier_by_name(rdr.GetValue(4).ToString());
                Product p = new Product(rdr.GetValue(0).ToString(), rdr.GetValue(1).ToString(), int.Parse(rdr.GetValue(2).ToString()), int.Parse(rdr.GetValue(3).ToString()), s, false);
                Products.Add(p);
            }
        }

        // Loads all available Treatment types
        public static void initTreatments()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_treatments";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Treatments = new List<Treatment>();

            while (rdr.Read())
            {
                Treatment T = new Treatment(rdr.GetValue(0).ToString(), int.Parse(rdr.GetValue(1).ToString()), decimal.Parse(rdr.GetValue(2).ToString()));
                Treatments.Add(T);
            }
        }
        // Loads Financial Transactions (Income/Expense)
        public static void initTransactions()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_transactions";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Transactions = new List<FinancialTransaction>();

            while (rdr.Read())
            {
                PaymentType P = (PaymentType)Enum.Parse(typeof(PaymentType), rdr.GetValue(3).ToString());
                TransactionType T = (TransactionType)Enum.Parse(typeof(TransactionType), rdr.GetValue(5).ToString());
                Supplier S = null;
                if (!rdr.IsDBNull(7))
                {
                    S = find_supplier_by_name(rdr.GetValue(7).ToString());
                }
                Customer C = null;
                if (!rdr.IsDBNull(6))
                {
                    C = find_customer_by_id(int.Parse(rdr.GetValue(6).ToString()));
                }

                FinancialTransaction FT = new FinancialTransaction(int.Parse(rdr.GetValue(0).ToString()), decimal.Parse(rdr.GetValue(1).ToString()), DateTime.Parse(rdr.GetValue(2).ToString()), P, rdr.GetValue(4).ToString(), T, C, S, false);
                Transactions.Add(FT);
            }

        }
        // Loads Appointments and calculates the next available ID
        private static void init_appointment()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_appointments";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            Appointments = new List<Appointment>();

            while (rdr.Read())
            {
                TimeSpan actualTime = TimeSpan.Zero;
                if (!rdr.IsDBNull(3))
                {
                    actualTime = TimeSpan.Parse(rdr.GetValue(3).ToString());
                }

                string notes = "";
                if (!rdr.IsDBNull(4))
                {
                    notes = rdr.GetValue(4).ToString();
                }

                Worker W = find_worker_by_id(int.Parse(rdr.GetValue(6).ToString()));
                Customer C = find_customer_by_id(int.Parse(rdr.GetValue(7).ToString()));
                Treatment T = find_treatment_by_name(rdr.GetValue(8).ToString());

                AppointmentStatus AS = (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), rdr.GetValue(5).ToString());

                Appointment A = new Appointment(int.Parse(rdr.GetValue(0).ToString()), DateTime.Parse(rdr.GetValue(1).ToString()), TimeSpan.Parse(rdr.GetValue(2).ToString()), actualTime, notes, AS, W, C, T, false);
                Appointments.Add(A);
            }

            if (Appointments.Count > 0)
            {
                Appointment.next_id = Appointments.Max(a => a.id) + 1;
            }
            else
            {
                Appointment.next_id = 0; 
            }
        }

        // Loads File Paths (PDFs) related to appointments or transactions
        public static void init_pdfs()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.Get_all_Files";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(c);

            PDFs = new List<FilePDF>();
            while (rdr.Read())
            {
                Appointment A = null;
                if (!rdr.IsDBNull(3))
                {
                    int appId = int.Parse(rdr.GetValue(3).ToString());
                    A = find_appointment_by_id(appId); 
                }

                FinancialTransaction FT = null;
                if (!rdr.IsDBNull(4))
                {
                    int transId = int.Parse(rdr.GetValue(4).ToString());
                    FT = find_transaction_by_id(transId); 
                }
                FileType F = (FileType)Enum.Parse(typeof(FileType), rdr.GetValue(2).ToString());
                FilePDF Pdf = new FilePDF(rdr.GetValue(0).ToString(), rdr.GetValue(1).ToString(), F, A, FT, false);
                PDFs.Add(Pdf);
            }
          
        }
        // Filters PDF list to find Health Declarations for a specific customer
        public static List<FilePDF> GetCustomerDeclarations(int customerId)
        {
            return PDFs.Where(p => p.appointment != null &&
                                   p.appointment.customer.id == customerId &&
                                   p.type == FileType.healthDeclaration)
                       .ToList();
        }


        [STAThread]
        // Main Application Entry Point
        public static void Main()
            {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                initLists();// Load all data from DB before showing UI

            Application.Run(new LoginForm());// Start with Login

        }
    }

    }



