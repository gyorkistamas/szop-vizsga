using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szop_vizsga_kliens.Models
{
    internal class GradeModel
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}
