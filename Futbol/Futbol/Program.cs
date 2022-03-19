using Futbol.Data;
using Futbol.Models;
using Futbol.VeriTabani;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Futbol
{
    internal class Program
    {
        static FutbolDbContext context = new FutbolDbContext();
        static VTLig vtLig = new VTLig();
        static VTOyuncu vtOyuncu = new VTOyuncu();
        static VTTeknikDirektor vtTeknikDirektor = new VTTeknikDirektor();
        static VTTakim vtTakim = new VTTakim();
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
                TeknikDirektorEkle();
            }
            else if (islem == "2")
            {
                TeknikDirektorSil();
            }
            else if (islem == "3")
            {
                TeknikDirektorGoster();
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
                OyuncuEkle();
            }
            else if (islem == "2")
            {
                OyuncuGuncelle();
            }
            else if (islem == "3")
            {
                OyuncuSil();
            }
            else if (islem == "4")
            {
                OyuncuGoster();
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
                vtLig.Ekle(lig);
            }
            else if (islem == "2")
            {
                vtLig.TumunuGetir();
            }
            else if (islem == "3")
            {
                LigAra();
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
                TakimEkle();
            }
            else if (islem == "2")
            {
                TakimGuncelle();
            }
            else if (islem == "3")
            {
                TakimSil();
            }
            else if (islem == "4")
            {
                TakimListele();
            }
            else if (islem == "5")
            {
                TakimGoster();
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

        public static void LigAra()
        {
            Console.Write("Ligin Adını Giriniz: ");
            string ad = Console.ReadLine();
            var lig = vtLig.Ara(ad);
            
            Console.Clear();
            if (lig != null)
            {
                var ligContext = vtLig.LigGetir(lig);
                Console.WriteLine("Ligin Adı: {0}", ligContext.Ad);
                Console.WriteLine("TAKIMLAR: ");
                foreach (var t in ligContext.Takimlar)
                {
                    Console.WriteLine(t.Ad);
                }
            }
            else
            {
                Console.WriteLine("LİG BULUNAMADI!");
            }
        }

        public static void OyuncuEkle()
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
            vtOyuncu.Ekle(oyuncu);
        }
        public static Oyuncu OyuncuAra()
        {
            Console.Write("Oyuncu Adı: ");
            string ad = Console.ReadLine();
            Console.Write("Oyuncu Soyadı: ");
            string soyad = Console.ReadLine();
            var oyuncu = vtOyuncu.Ara(ad, soyad);
            return oyuncu;
        }

        public static void OyuncuGuncelle()
        {
            var oyuncu = OyuncuAra();
            Console.Clear();
            if (oyuncu != null)
            {
                Console.WriteLine("Oyuncunun Yeni Takımı: ");
                foreach (var t in vtTakim.Takimlar())
                {
                    Console.WriteLine("({0}) {1}", t.Id, t.Ad);
                }
                Console.Write("Seçiniz: ");
                int takimId = Convert.ToInt32(Console.ReadLine());
                oyuncu.TakimId = takimId;
                vtOyuncu.Guncelle(oyuncu);
            }
            else
            {
                Console.WriteLine("OYUNCU BULUNAMADI!");
            }
        }

        public static void OyuncuSil()
        {
            var oyuncu = OyuncuAra();
            Console.Clear();
            if (oyuncu != null)
            {
                vtOyuncu.Sil(oyuncu);
            }
            else
            {
                Console.WriteLine("OYUNCU BULUNAMADI!");
            }
        }
        public static void OyuncuGoster()
        {
            var oyuncu = OyuncuAra();
            Console.Clear();
            if (oyuncu != null)
            {
                var oyuncuContext = vtOyuncu.OyuncuGetir(oyuncu);
                Console.WriteLine("Oyuncunun Adı: {0}", oyuncuContext.Ad);
                Console.WriteLine("Oyuncunun Soyadı: {0}", oyuncuContext.Soyad);
                Console.WriteLine("Oyuncunun Takımı: {0}", oyuncuContext.Takim.Ad);
                Console.WriteLine("Oyuncu Bilgileri: {0}", oyuncuContext.Bilgi);
            }
        }

        public static void TeknikDirektorEkle()
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
            vtTeknikDirektor.Ekle(direktor);
        }
        public static void TeknikDirektorSil()
        {
            var td = TeknikDirektorAra();
            vtTeknikDirektor.Sil(td);
        }
        public static void TeknikDirektorGoster()
        {
            var td = TeknikDirektorAra();
            Console.Clear();
            if (td != null)
            {
                var takim = vtTakim.TakimGetir(td.Id);
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

        public static void TeknikDirektorListele()
        {
            Console.WriteLine("--------------------");
            foreach (var td in vtTeknikDirektor.TeknikDirektorler())
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
            var td = vtTeknikDirektor.Ara(ad, soyad);
            return td;
        }

        public static void TakimEkle()
        {
            Console.Write("Takım Adı: ");
            string ad = Console.ReadLine();
            Console.WriteLine("T. Direktörü: ");
            foreach (var td in vtTeknikDirektor.TeknikDirektorler())
            {
                Console.WriteLine("({0}) {1} {2}", td.Id, td.Ad, td.Soyad);
            }
            Console.Write("Seçiminiz: ");
            int tdId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ligi: ");
            foreach (var l in vtLig.Ligler())
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
            vtTakim.Ekle(takim);
        }
        public static void TakimGuncelle()
        {
            var takim = TakimAra();
            Console.Clear();
            if (takim != null)
            {
                Console.WriteLine("Yeni T. Direktörü Seçiniz: ");
                TeknikDirektorListele();
                int tdId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Yeni Ligi Seçiniz: ");
                vtLig.TumunuGetir();
                int ligId = Convert.ToInt32(Console.ReadLine());
                takim.TeknikDirektorId = tdId;
                takim.LigId = ligId;
                vtTakim.Guncelle(takim);
            }
        }
        public static void TakimSil()
        {
            var takim = TakimAra();
            if (takim != null)
            {
                vtTakim.Sil(takim);
            }
        }
        public static void TakimGoster()
        {
            var takim = TakimAra();
            Console.Clear();
            if (takim != null)
            {
                var takimContext = vtTakim.TakimGetir(takim);
                Console.WriteLine("Takım Adı: {0}", takimContext.Ad);
                Console.WriteLine("T. Direktörü: {0} {1}", takimContext.TeknikDirektor.Ad, takimContext.TeknikDirektor.Soyad);
                Console.WriteLine("Ligi: {0}", takimContext.Lig.Ad);
            }
        }

        public static void TakimListele()
        {
            vtTakim.TumunuGetir();
        }

        public static Takim TakimAra()
        {
            Console.Write("Takımın Adını Giriniz: ");
            string ad = Console.ReadLine();
            var takim = vtTakim.Ara(ad);
            return takim;
        }

    }
}
