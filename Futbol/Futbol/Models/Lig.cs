using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.Models
{
    public class Lig
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public List<Takim> Takimlar { get; set; }
    }
}
