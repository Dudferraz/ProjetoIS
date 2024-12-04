using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Somiod.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Creation_datetime { get; set; }
        public int Parent { get; set; }
        public int Event_BD { get; set; }
        public string Endpoint { get; set; }

        public int Enabled { get; set; }

    }
}