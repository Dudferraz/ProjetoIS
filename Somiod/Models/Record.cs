using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Somiod.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Creation_datetime { get; set; }
        public int Parent { get; set; }
        public string Content { get; set; }

    }
}