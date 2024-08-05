using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class DebitCard : Payment
    {
        public string CardNumber { get; set; }
        public string Type { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
