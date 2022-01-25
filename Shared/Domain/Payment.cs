using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTech.Shared.Domain
{
    public class Payment
    {
        public int PayId { get; set; }
        public string PayMeth { get; set; }
        public DateTime DateTime { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
