using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    //[Keyless] //Bu notasyon ile Entity'nin bir anahtar özelliği olmamasını istiyoruz.
    //Keyless Entity için DB'ye insert, update ve delete işlemleri gerçekleştirilemez.
    //Raw SQL cümleciklerinden dönen veriyi map'lemek istediğimizde bu tip Entity'i kullanabiliriz.
    //Primary Key içermeyen tablo'larımızı map'lemek istediğimizde bu tip Entity'i kullanabiliriz.
    public class ProductFull
    {
        public int Product_Id { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Height { get; set; }
    }
}
