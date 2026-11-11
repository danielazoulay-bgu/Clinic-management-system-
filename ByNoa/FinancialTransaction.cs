using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{
    public class FinancialTransaction
    {
        public int id { get; set; }
        public TransactionType type { get; set; }
        public decimal amount { get; set; }
        public DateTime date { get; set; }
        public PaymentType paymentMethod { get; set; }
        public string notes { get; set; }
        public Customer customer { get; set; }
        public Supplier supplier { get; set; }

        public List<FilePDF>pdfs { get; set; }

        public bool is_new { get; set; }

        public FinancialTransaction(int id, decimal amount, DateTime date, PaymentType paymentMethod, string notes, TransactionType type, Customer customer, Supplier supplier, bool is_new)
        {
            this.id = id;
            this.type = type;
            this.amount = amount;
            this.date = date;
            this.paymentMethod = paymentMethod;
            this.notes = notes;
            this.customer = customer; 
            this.supplier= supplier;
            this.is_new = is_new;
            this.pdfs = new List<FilePDF>();

            if (supplier!=null ) { supplier.AddTransaction(this); }
            if (customer != null) { customer.AddTransaction(this); }

            if (is_new)
            {
                this.create_transaction();
                Program.Transactions.Add(this);

            }
        }

        public void AddPdf(FilePDF f)
        {
            if (f != null)
            {
                this.pdfs.Add(f);
            }
        }

        public void create_transaction()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.SP_add_transaction @id, @amount, @date, @method, @notes, @type, @custId, @supName";
            c.Parameters.AddWithValue("@id", this.id);
            c.Parameters.AddWithValue("@amount", this.amount);
            c.Parameters.AddWithValue("@date", this.date);
            c.Parameters.AddWithValue("@method", this.paymentMethod.ToString());
            c.Parameters.AddWithValue("@notes", this.notes);
            c.Parameters.AddWithValue("@type", this.type.ToString());
            if (this.customer != null)
                c.Parameters.AddWithValue("@custId", this.customer.id);
            else
                c.Parameters.AddWithValue("@custId", DBNull.Value);
            if (this.supplier != null)
                c.Parameters.AddWithValue("@supName", this.supplier.name); // שימי לב לוודא שקוראים לזה name או supplierName במחלקה שלך
            else
                c.Parameters.AddWithValue("@supName", DBNull.Value);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(c);
        }

        public void searchTransactionByDate(DateTime date)
        {
        }
    }
}
