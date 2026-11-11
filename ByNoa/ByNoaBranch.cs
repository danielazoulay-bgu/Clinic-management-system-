using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{
    // Represents a physical branch of the salon chain
    public class ByNoaBranch
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string adress { get; set; }
        public TimeSpan openingHour { get; set; }
        public TimeSpan closingHour { get; set; }

        // List of workers assigned to this specific branch
        public List<Worker> workers { get; set; }


        // Constructor: Initializes a branch entity with details and hours
        public ByNoaBranch(int id, string name, string phone, string email, string adress, TimeSpan openingHour, TimeSpan closingHour)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.email = email;
            this.adress = adress;
            this.openingHour = openingHour;
            this.closingHour = closingHour;
            this.workers = new List<Worker>();


        }
        // Method: Checks if the branch is open at a specific time
        public bool isOpen(TimeSpan currentTime)
        {
            if (currentTime >= openingHour && currentTime < closingHour)
                return true;
            return false;

        }

        // Method: Links a worker to this branch's internal list
        public void AddWorker(Worker w)
        {
            if (w != null)
            {
                workers.Add(w);
            }

        }
    }


}
