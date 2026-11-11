using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByNoa
{
    public enum AppointmentStatus
    {
        scheduled, 
        reminded,
        medical_review,
        in_treatment,
        completed,
        cancelled,
        archived
    }
}
