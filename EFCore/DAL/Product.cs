using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    //[Table("ProductTb",Schema = "products")] //notasyon ile tablo adı ve ön eki belirleme
    public class Product
    {
        //[Key] id belirleme
        public int Id { get; set; }

        //[Required] //konfigürasyon ayarlarında nullable özelliği açık ise bu notasyonu null olmasını istemediğimiz sütunlar için kullanabiliriz.
        //[MaxLength(100)] //sütun için üst uzunluk limiti belirlememize yardımcı olur.
        public string Name { get; set; }

        //[Column("price2", Order = 2,TypeName = "decimal(18,2)")]//Order niteliğini sadece tablo ilk kez oluştururken değiştirebiliyoruz.

        public decimal Price { get; set; }

        public int Kdv { get; set; }

        public int Stock { get; set; }

        public int Barcode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal PriceKdv { get; set; }

        //public int CategoryId { get; set; }

        //Navigation Property
        //[ForeignKey(nameof(Category_Id))]
        //public Category Category { get; set; }

        //public ProductFeature ProductFeature { get; set; }

        //Computed = EFCore add ve update işlemlerinde bu alanı sorgulara dahil etmez.
        //Identity = EFCore sadece update işlemlerinde bu alanı sorgulara dahil etmez.
        //None = Veritabanı tarafından otomatik değer üretmeyi kapatır.
        public DateTime? LastAccessDate { get; set; }
    }
}
