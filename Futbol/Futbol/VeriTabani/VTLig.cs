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
    public class VTLig : IEntity<Lig>
    {
        private static FutbolDbContext context = new FutbolDbContext();
        public Lig Ara(string ad)
        {
            return context.Ligler.Where(l => l.Ad == ad).FirstOrDefault();
        }

        public void Ekle(Lig entity)
        {
            context.Ligler.Add(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Guncelle(Lig entity)
        {
            context.Ligler.Update(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Sil(Lig entity)
        {
            context.Ligler.Remove(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void TumunuGetir()
        {
            Console.WriteLine("--------------------");
            foreach (var lig in context.Ligler)
            {
                Console.WriteLine(lig.Ad);
            }
        }

        public Lig LigGetir(Lig lig)
        {
            return context.Ligler.Include(l => l.Takimlar).Where(l => l.Ad == lig.Ad).FirstOrDefault();
        }

        public Lig Ara(string ad, string soyad)
        {
            throw new NotImplementedException();
        }

        public List<Lig> Ligler()
        {
            return context.Ligler.ToList();
        }
    }
}
