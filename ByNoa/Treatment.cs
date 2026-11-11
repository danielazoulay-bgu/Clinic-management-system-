using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{
    public class Treatment
    {
        public string name { get; set; }
        public int durationInMinutes { get; set; }
        public decimal price { get; set; }

        public List<Appointment> appointments { get; set; }

        public Treatment(string name, int durationInMinutes, decimal price) {
            this.name = name;
            this.durationInMinutes = durationInMinutes;
            this.price = price;
            this.appointments = new List<Appointment>();

        
        }

        public void AddAppointment(Appointment app)
        {
            if (app != null)
            {
                this.appointments.Add(app);
            }
        }


        public override string ToString() { return this.name; }
    }

}
