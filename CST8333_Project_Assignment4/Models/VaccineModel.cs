using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST8333_Project_Assignment4.Models
{
    // Aleksandr Anseev
    /// <summary>
    /// This is a Vaccine Model class.
    /// Getters and setters for all table columns
    /// are stored in this class.
    /// </summary>
    public class VaccineModel
    {
        public string Pruid { get; set; }
        public string Prename { get; set; }
        public string Prfrname { get; set; }
        public string Week_end { get; set; }
        public string Product_name { get; set; }
        public string Numtotal_atleast1dose { get; set; }
        public string Numtotal_partially { get; set; }
        public string Numtotal_fully { get; set; }
        public string Prop_atleast1dose { get; set; }
        public string Prop_partially { get; set; }
        public string Prop_fully { get; set; }
        public string Numweekdelta_atleast1dose { get; set; }
        public string Numweekdelta_fully { get; set; }
        public string Propweekdelta_partially { get; set; }
        public string Propweekdelta_fully { get; set; }
    }
}
