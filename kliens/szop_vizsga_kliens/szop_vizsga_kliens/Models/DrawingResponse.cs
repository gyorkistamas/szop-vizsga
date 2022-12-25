using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szop_vizsga_kliens.Models
{
    internal class DrawingResponse
    {
        public int Error { get; set; }

        public string Message { get; set; }

        public Drawing Drawing_data { get; set; }
    }
}
