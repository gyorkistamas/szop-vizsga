using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szop_vizsga_kliens.Models
{
    internal class ListOfDrawingsResponse
    {
        public int Error { get; set; }

        public string Message { get; set; }

        public List<Drawing> Data { get; set; }
    }
}
