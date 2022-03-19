using Futbol.Data;
using Futbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.VeriTabani
{
    public class VTTeknikDirektor : IEntity<TeknikDirektor>
    {
        private static FutbolDbContext context = new FutbolDbContext();
        public TeknikDirektor Ara(string ad)
        {
            throw new NotImplementedException();
        }

        public TeknikDirektor Ara(string ad, string soyad)
        {
            return context.TeknikDirektorler.Where(o => o.Ad == ad).Where(o => o.Soyad == soyad).FirstOrDefault();
        }

        public void Ekle(TeknikDirektor entity)
        {
            context.TeknikDirektorler.Add(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Guncelle(TeknikDirektor entity)
        {
            context.TeknikDirektorler.Update(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Sil(TeknikDirektor entity)
        {
            context.TeknikDirektorler.Remove(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void TumunuGetir()
        {
            Console.Clear();
            Console.WriteLine("----- TEKNİK DİREKTÖRLER -----");
            foreach (var td in context.TeknikDirektorler)
            {
                Console.WriteLine("{0} {1} {2}", td.Id, td.Ad, td.Soyad);
                Console.WriteLine("--------------------");
            }
        }

        public List<TeknikDirektor> TeknikDirektorler()
        {
            return context.TeknikDirektorler.ToList();
        }
    }
}
