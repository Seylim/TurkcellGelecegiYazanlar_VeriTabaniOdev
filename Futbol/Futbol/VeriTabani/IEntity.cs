using Futbol.Data;
using Futbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.VeriTabani
{
    public interface IEntity<T>
    {
        void Ekle(T entity);
        void Guncelle(T entity);
        void Sil(T entity);
        void TumunuGetir();
        T Ara(string ad);
        T Ara(string ad, string soyad);
    }
}
