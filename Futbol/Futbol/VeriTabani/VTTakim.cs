
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
    public class VTTakim : IEntity<Takim>
    {
        private static FutbolDbContext context = new FutbolDbContext();
        public Takim Ara(string ad)
        {
            return context.Takimlar.Where(l => l.Ad == ad).FirstOrDefault();
        }

        public Takim Ara(string ad, string soyad)
        {
            throw new NotImplementedException();
        }

        public void Ekle(Takim entity)
        {
            context.Takimlar.Add(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Guncelle(Takim entity)
        {
            context.Takimlar.Update(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void Sil(Takim entity)
        {
            context.Takimlar.Remove(entity);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public void TumunuGetir()
        {
            foreach (var t in context.Takimlar)
            {
                var takim = TakimGetir(t);
                Console.WriteLine("--------------------");
                Console.WriteLine("Takım Adı: {0}", takim.Ad);
                Console.WriteLine("Teknik Direktörü: {0} {1}", takim.TeknikDirektor.Ad, takim.TeknikDirektor.Soyad);
                Console.WriteLine("Bulunduğu Lig: {0}", takim.Lig.Ad);
            }
        }

        public Takim TakimGetir(Takim takim)
        {
            return context.Takimlar.Include(x => x.TeknikDirektor).Include(y => y.Lig).Where(t => t.Id == takim.Id).FirstOrDefault();
        }

        public Takim TakimGetir(int id)
        {
            return context.Takimlar.Where(t => t.TeknikDirektorId == id).FirstOrDefault();
        }

        public List<Takim> Takimlar()
        {
            return context.Takimlar.ToList();
        }
    }
}
