using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.Models
{
    public class Takim
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public Lig Lig { get; set; }
        public int LigId { get; set; }
        public TeknikDirektor TeknikDirektor { get; set; }
        public int TeknikDirektorId { get; set; }
        public List<Oyuncu> Oyuncular { get; set; }

    }
}
