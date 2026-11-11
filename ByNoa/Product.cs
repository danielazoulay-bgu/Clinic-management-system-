using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{
    public class Product
    {
        public string productName {  get; set; }
        public string company {  get; set; }
        public int quantity { get; set; }
        public int minimumQuantity { get; set; }

        public Supplier supplier { get; set; }

        public bool is_new { get; set; }



        public Product(string productName, string company, int quantity, int minimumQuantity, Supplier supplier, bool is_new) {
            this.productName = productName;
            this.company = company;
            this.quantity = quantity;   
            this.minimumQuantity = minimumQuantity;
            this.supplier = supplier;
            this.is_new = is_new;
            supplier.AddProduct(this);

            if (is_new)
            {
                this.create_product();
                Program.Products.Add(this);
            }

        }

        public void create_product()
        {
            SqlCommand c = new SqlCommand();
            c.CommandText = "EXECUTE dbo.SP_add_product @name, @company, @qty, @minQty, @supplier";
            c.Parameters.AddWithValue("@name", this.productName);
            c.Parameters.AddWithValue("@company", this.company);
            c.Parameters.AddWithValue("@qty", this.quantity);
            c.Parameters.AddWithValue("@minQty", this.minimumQuantity);
            c.Parameters.AddWithValue("@supplier", this.supplier.name);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(c);
        }

        public bool isBelowMininum()
        {
            return quantity < minimumQuantity;
        }
    }

}
