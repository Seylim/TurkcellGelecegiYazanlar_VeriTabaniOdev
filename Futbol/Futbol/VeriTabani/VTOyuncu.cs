using Futbol.Data;
using Futbol.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.VeriTabani
{
    public class VTOyuncu : IEntity<Oyuncu>
    {
        private static FutbolDbContext context = new FutbolDbContext();
        public Oyuncu Ara(string ad)
        {
            throw new NotImplementedException();
        }

        public Oyuncu Ara(string ad, string soyad)
        {
            return context.Oyuncular.Where(o => o.Ad == ad).Where(o => o.Soyad == soyad).FirstOrDefault();
        }

        public void Ekle(Oyuncu entity)
        {
            context.Oyuncular.Add(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Guncelle(Oyuncu entity)
        {
            context.Oyuncular.Update(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Sil(Oyuncu entity)
        {
            context.Oyuncular.Remove(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void TumunuGetir()
        {
            Console.Clear();
            Console.WriteLine("----- OYUNCULAR -----");
            foreach(var o in context.Oyuncular)
            {
                Console.WriteLine("{0} {1} {2}", o.Id, o.Ad, o.Soyad);
                var takim = OyuncuGetir(o).Takim.Ad;
                Console.WriteLine("Takımı: {0}", takim);
                Console.WriteLine("--------------------");
            }
        }

        public Oyuncu OyuncuGetir(Oyuncu oyuncu)
        {
            return context.Oyuncular.Include(z => z.Takim).Where(o => o.Id == oyuncu.Id).FirstOrDefault();
        }
    }
}
