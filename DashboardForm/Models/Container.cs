using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardForm.Models
{

    public class Container
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime creation_datetime { get; set; }
        public int parent { get; set; }
        public string res_type { get; set; }
    }
}
