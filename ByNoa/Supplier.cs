using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{
    public class Supplier
    {
        public string name {  get; set; }
        public SupplierType type { get; set; }
        public string phone {  get; set; }
        public string email { get; set; }
        public string contantPerson { get; set; }
        public int leadTimeInDays { get; set; }

        public List<Product> products { get; set; }

        public List<FinancialTransaction> trans { get; set; }



        public Supplier(string name, SupplierType type, string phone, string email, string contantPerson, int leadTimeInDays) {
            this.name = name;   
            this.type = type;
            this.phone = phone; 
            this.email = email;
            this.contantPerson = contantPerson; 
            this.leadTimeInDays = leadTimeInDays;
            this.products = new List<Product>();
            this.trans = new List<FinancialTransaction>();   
        }
        public bool canDeliverByDate(DateTime date)
        {
            return true;
        }

        public void AddProduct(Product p)
        {
            if (p != null)
            {
                this.products.Add(p);
            }
        }

        public void AddTransaction(FinancialTransaction ft)
        {
            if (ft != null)
            {
                this.trans.Add(ft);
            }
        }

    }
}
