using Futbol.Data;
using Futbol.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Futbol
{
    internal class Program
    {
        //Oyuncu listeleme gözden geçirilecek.
        //Program.cs temizlenecek.
        static FutbolDbContext context = new FutbolDbContext();
        static void Main(string[] args)
        {
            string islem = string.Empty;
            AnaEkran();
            do
            {
                islem = Console.ReadLine();
                Islemler(islem);

            } while (islem != "0");
        }

        public static void AnaEkran()
        {
            Console.WriteLine("(1) Takım Menüsü");
            Console.WriteLine("(2) Lig Menüsü");
            Console.WriteLine("(3) Oyuncu Menüsü");
            Console.WriteLine("(4) Teknik Direktör Menüsü");
            Console.WriteLine("(0) ÇIKIŞ");
        }

        public static void Islemler(string islem)
        {
            Console.Clear();
            if (islem == "1")
            {
                TakimMenusu();
            }
            else if (islem == "2")
            {
                LigMenusu();
            }
            else if (islem == "3")
            {
                OyuncuMenusu();
            }
            else if (islem == "4")
            {
                TeknikDirektorMenusu();
            }
        }

        private static void TeknikDirektorMenusu()
        {
            Console.WriteLine("(1) T. Direktör Ekle");
            Console.WriteLine("(2) T. Direktör Sil");
            Console.WriteLine("(3) T. Direktör Ara");
            Console.WriteLine("(0) Ana Menü");
            string islem = Console.ReadLine();
            TeknikDrektorIslemleri(islem);
        }

        private static void TeknikDrektorIslemleri(string islem)
        {
            Console.Clear();
            if (islem == "1")
            {
                Console.Write("T. Direktör Adı: ");
                string ad = Console.ReadLine();
                Console.Write("T. Direktör Soyadı: ");
                string soyad = Console.ReadLine();
                Console.Write("T. Direktör Bilgileri: ");
                string bilgi = Console.ReadLine();
                TeknikDirektor direktor = new TeknikDirektor()
                {
                    Ad = ad,
                    Soyad = soyad,
                    Bilgi = bilgi
                };
                TeknikDirektorEkle(direktor);
            }
            else if (islem == "2")
            {
                var td = TeknikDirektorAra();
                context.TeknikDirektorler.Remove(td);
                int result = context.SaveChanges();
                string message = result > 0 ? "Başarılı" : "Başarısız";
                Console.WriteLine("--------------------");
                Console.WriteLine(message);
            }
            else if (islem == "3")
            {
                var td = TeknikDirektorAra();
                Console.Clear();
                if (td != null)
                {
                    var takim = context.Takimlar.Where(t => t.TeknikDirektorId == td.Id).FirstOrDefault();
                    Console.WriteLine("T. Direktör Adı: {0}", td.Ad);
                    Console.WriteLine("T. Direktör Soyadı: {0}", td.Soyad);
                    Console.WriteLine("T. Direktör Bilgileri: {0}", td.Bilgi);
                    Console.WriteLine("T. Direktör Takımı: {0}", takim.Ad);
                }
                else
                {
                    Console.WriteLine("T. Direktör Bulunamadı.");
                }
            }
            else if (islem == "0")
            {
                AnaEkran();
            }
            else
            {
                Console.WriteLine("GEÇERLİ OLMAYAN SEÇİM!");
            }
            TeknikDirektorMenusu();
        }

        private static void OyuncuMenusu()
        {
            Console.WriteLine("(1) Oyuncu Ekle");
            Console.WriteLine("(2) Oyuncu Güncelle");
            Console.WriteLine("(3) Oyuncu Sil");
            Console.WriteLine("(4) Oyuncu Ara");
            Console.WriteLine("(0) Ana Menü");
            string islem = Console.ReadLine();
            OyuncuIslemleri(islem);
        }

        private static void OyuncuIslemleri(string islem)
        {
            Console.Clear();
            if (islem == "1")
            {
                Console.Write("Oyuncu Adı: ");
                string ad = Console.ReadLine();
                Console.Write("Oyuncu Soyadı: ");
                string soyad = Console.ReadLine();
                Console.WriteLine("Takımı: ");
                foreach (var t in context.Takimlar)
                {
                    Console.WriteLine("({0}) {1}", t.Id, t.Ad);
                }
                Console.Write("Seçiminiz: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Oyuncu Bilgileri: ");
                string bilgi = Console.ReadLine();
                Oyuncu oyuncu = new Oyuncu()
                {
                    Ad = ad,
                    Soyad = soyad,
                    TakimId = id,
                    Bilgi = bilgi
                };
                OyuncuEkle(oyuncu);
            }
            else if (islem == "2")
            {
                var oyuncu = OyuncuAra();
                Console.Clear();
                if (oyuncu != null)
                {
                    Console.WriteLine("Oyuncunun Yeni Takımı: ");
                    foreach (var t in context.Takimlar)
                    {
                        Console.WriteLine("({0}) {1}", t.Id, t.Ad);
                    }
                    Console.Write("Seçiniz: ");
                    int takimId = Convert.ToInt32(Console.ReadLine());
                    oyuncu.TakimId = takimId;
                    context.Oyuncular.Update(oyuncu);
                    int result = context.SaveChanges();
                    string message = result > 0 ? "Başarılı" : "Başarısız";
                    Console.WriteLine("--------------------");
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("OYUNCU BULUNAMADI!");
                }
            }
            else if (islem == "3")
            {
                var oyuncu = OyuncuAra();
                Console.Clear();
                if (oyuncu != null)
                {
                    context.Oyuncular.Remove(oyuncu);
                    int result = context.SaveChanges();
                    string message = result > 0 ? "Başarılı" : "Başarısız";
                    Console.WriteLine("--------------------");
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("OYUNCU BULUNAMADI!");
                }
            }
            else if (islem == "4")
            {
                var oyuncu = OyuncuAra();
                Console.Clear();
                if (oyuncu != null)
                {
                    var oyuncuContext = context.Oyuncular.Include(z => z.Takim).Where(o => o.Id == oyuncu.Id).FirstOrDefault();
                    Console.WriteLine("Oyuncunun Adı: {0}",oyuncuContext.Ad);
                    Console.WriteLine("Oyuncunun Soyadı: {0}", oyuncuContext.Soyad);
                    Console.WriteLine("Oyuncunun Takımı: {0}", oyuncuContext.Takim.Ad);
                    Console.WriteLine("Oyuncu Bilgileri: {0}", oyuncuContext.Bilgi);
                }
            }
            else if (islem == "0")
            {
                AnaEkran();
            }
            else
            {
                Console.WriteLine("GEÇERLİ OLMAYAN SEÇİM!");
            }
            OyuncuMenusu();
        }

        private static void LigMenusu()
        {
            Console.WriteLine("(1) Lig Ekle");
            Console.WriteLine("(2) Tüm Ligler");
            Console.WriteLine("(3) Lig Ara");
            Console.WriteLine("(0) Ana Menü");
            string islem = Console.ReadLine();
            LigIslemleri(islem);
        }

        private static void LigIslemleri(string islem)
        {
            Console.Clear();
            if (islem == "1")
            {
                Console.WriteLine("Lig Adı: ");
                string ad = Console.ReadLine();
                Lig lig = new Lig()
                {
                    Ad = ad,
                };
                LigEkle(lig);
            }
            else if (islem == "2")
            {
                LigListele();
            }
            else if (islem == "3")
            {
                var lig = LigAra();
                Console.Clear();
                if (lig != null)
                {
                    var ligContext = context.Ligler.Include(l => l.Takimlar).Where(l => l.Ad == lig.Ad).FirstOrDefault();
                    Console.WriteLine("Ligin Adı: {0}", ligContext.Ad);
                    Console.WriteLine("TAKIMLAR: ");
                    foreach(var t in ligContext.Takimlar)
                    {
                        Console.WriteLine(t.Ad);
                    }
                }
                else
                {
                    Console.WriteLine("LİG BULUNAMADI!");
                }
            }
            else if (islem == "0")
            {
                AnaEkran();
            }
            else
            {
                Console.WriteLine("GEÇERLİ OLMAYAN SEÇİM!");
            }
            LigMenusu();
        }

        private static void TakimMenusu()
        {
            Console.WriteLine("(1) Takım Ekle");
            Console.WriteLine("(2) Takım Güncelle");
            Console.WriteLine("(3) Takım Sil");
            Console.WriteLine("(4) Tüm Takımlar");
            Console.WriteLine("(5) Takım Ara");
            Console.WriteLine("(0) Ana Menü");
            string islem = Console.ReadLine();
            TakimIslemleri(islem);
        }

        public static void TakimIslemleri(string islem)
        {
            Console.Clear();
            if (islem == "1")
            {
                Console.Write("Takım Adı: ");
                string ad = Console.ReadLine();
                Console.WriteLine("T. Direktörü: ");
                foreach(var td in context.TeknikDirektorler)
                {
                    Console.WriteLine("({0}) {1} {2}", td.Id, td.Ad, td.Soyad);
                }
                Console.Write("Seçiminiz: ");
                int tdId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ligi: ");
                foreach(var l in context.Ligler)
                {
                    Console.WriteLine("{0} {1}", l.Id, l.Ad);
                }
                Console.Write("Seçiminiz: ");
                int ligId = Convert.ToInt32(Console.ReadLine());
                Takim takim = new Takim()
                {
                    Ad = ad,
                    LigId = ligId,
                    TeknikDirektorId = tdId
                };
                TakimEkle(takim);
            }
            else if (islem == "2")
            {
                var takim = TakimAra();
                Console.Clear();
                if (takim != null)
                {
                    Console.WriteLine("Yeni T. Direktörü Seçiniz: ");
                    TeknikDirektorListele();
                    int tdId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Yeni Ligi Seçiniz: ");
                    LigListele();
                    int ligId = Convert.ToInt32(Console.ReadLine());
                    takim.TeknikDirektorId = tdId;
                    takim.LigId = ligId;
                    context.Takimlar.Update(takim);
                    int result = context.SaveChanges();
                    string message = result > 0 ? "Başarılı" : "Başarısız";
                    Console.WriteLine("--------------------");
                    Console.WriteLine(message);
                }
            }
            else if (islem == "3")
            {
                var takim = TakimAra();
                if (takim != null)
                {
                    context.Takimlar.Remove(takim);
                    int result = context.SaveChanges();
                    string message = result > 0 ? "Başarılı" : "Başarısız";
                    Console.WriteLine("--------------------");
                    Console.WriteLine(message);
                }
            }
            else if (islem == "4")
            {
                TakimListele();
            }
            else if (islem == "5")
            {
                var takim = TakimAra();
                Console.Clear();
                if (takim != null)
                {
                    var takimContext = context.Takimlar.Include(t => t.TeknikDirektor).Include(t => t.Lig).Where(t => t.Id == takim.Id).FirstOrDefault();
                    Console.WriteLine("Takım Adı: {0}",takimContext.Ad);
                    Console.WriteLine("T. Direktörü: {0} {1}", takimContext.TeknikDirektor.Ad, takimContext.TeknikDirektor.Soyad);
                    Console.WriteLine("Ligi: {0}", takimContext.Lig.Ad);
                }
            }
            else if (islem == "0")
            {
                AnaEkran();
            }
            else
            {
                Console.WriteLine("GEÇERLİ OLMAYAN SEÇİM!");
            }
            TakimMenusu();
        }

        public static void LigEkle(Lig lig)
        {
            context.Ligler.Add(lig);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }
        
        public static void LigListele()
        {
            Console.WriteLine("--------------------");
            foreach (var lig in context.Ligler)
            {
                Console.WriteLine(lig.Ad);
            }
        }

        public static Lig LigAra()
        {
            Console.Write("Ligin Adını Giriniz: ");
            string ad = Console.ReadLine();
            var lig = context.Ligler.Where(l => l.Ad == ad).FirstOrDefault();
            return lig;
        }

        public static void OyuncuEkle(Oyuncu oyuncu)
        {
            context.Oyuncular.Add(oyuncu);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public static void OyuncuListele()
        {
            Console.WriteLine("--------------------");
            OyuncuYazdir(context.Oyuncular.ToList());
        }

        public static Oyuncu OyuncuAra()
        {
            Console.Write("Oyuncu Adı: ");
            string ad = Console.ReadLine();
            Console.Write("Oyuncu Soyadı: ");
            string soyad = Console.ReadLine();
            var oyuncu = context.Oyuncular.Where(o => o.Ad == ad).Where(o => o.Soyad == soyad).FirstOrDefault();
            return oyuncu;
        }

        public static void TeknikDirektorEkle(TeknikDirektor teknikDirektor)
        {
            context.TeknikDirektorler.Add(teknikDirektor);
            int result = context.SaveChanges();
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine("--------------------");
            Console.WriteLine(message);
        }

        public static void TeknikDirektorListele()
        {
            Console.WriteLine("--------------------");
            foreach (var td in context.TeknikDirektorler)
            {
                Console.WriteLine(td.Ad + " " + td.Soyad + " " + td.Bilgi);
            }
        }

        public static TeknikDirektor TeknikDirektorAra()
        {
            Console.Write("T. Direktör Adını Giriniz: ");
            string ad = Console.ReadLine();
            Console.Write("T. Direktör Soyadını Giriniz: ");
            string soyad = Console.ReadLine();
            var td = context.TeknikDirektorler.Where(p => p.Ad == ad).Where(t => t.Soyad == soyad).FirstOrDefault();
            return td;
        }

        public static void TakimEkle(Takim takim)
        {
            context.Takimlar.Add(takim);
            int result = context.SaveChanges();
            Console.WriteLine("--------------------");
            string message = result > 0 ? "Başarılı" : "Başarısız";
            Console.WriteLine(message);
        }

        public static void TakimListele()
        {
            var allContext = context.Takimlar.Include(x => x.TeknikDirektor).Include(y => y.Lig).ToList();
            var oyuncuContext = context.Oyuncular.Include(z => z.Takim).ToList();
            foreach (var takim in allContext)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Takım Adı: {0}", takim.Ad);
                Console.WriteLine("Teknik Direktörü: {0} {1}", takim.TeknikDirektor.Ad, takim.TeknikDirektor.Soyad);
                Console.WriteLine("Bulunduğu Lig: {0}", takim.Lig.Ad);
                Console.WriteLine("Oyuncular: ");
                OyuncuYazdir(oyuncuContext);
            }
        }

        public static Takim TakimAra()
        {
            Console.Write("Takımın Adını Giriniz: ");
            string ad = Console.ReadLine();
            var takim = context.Takimlar.Where(t => t.Ad == ad).FirstOrDefault();
            return takim;
        }

        public static void OyuncuYazdir(List<Oyuncu> oyuncular)
        {
            foreach (var oyuncu in oyuncular)
            {
                Console.WriteLine(oyuncu.Ad + " " + oyuncu.Soyad);
            }
        }
    }
}
