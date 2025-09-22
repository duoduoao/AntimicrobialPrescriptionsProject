using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntimicrobialPrescriptions.Domain.Exceptions
{
    public class InvalidPrescriptionStatusException : Exception
    {
        public InvalidPrescriptionStatusException(string message) : base(message) { }
    }
}
