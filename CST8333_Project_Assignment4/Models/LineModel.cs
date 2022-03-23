using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST8333_Project_Assignment4.Models
{
    // Aleksandr Anseev
    /// <summary>
    /// This is a Line Model class.
    /// Getters and setters for all neccessary 
    /// line properties are stored in this class.
    /// </summary>
    public class LineModel
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public string Color { get; set; }
    }
}
