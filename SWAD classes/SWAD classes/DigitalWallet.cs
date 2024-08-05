using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class DigitalWallet : Payment
    {
        public string Type { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
