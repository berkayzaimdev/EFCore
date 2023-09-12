using Microsoft.EntityFrameworkCore;
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
    //[Index(nameof(Name),nameof(Url))]
    //[Index(nameof(Url))]
    //[Index(nameof(Name))] Index belirleme ile DB'de ayrı bir alan açılır. Bu alan sayesinde sorguları daha pratik ve hızlı bir şekilde getirebiliriz. Indexlemeye verebileceğimiz iki örnek; Telefon Rehberi ve İçindekiler Listesi
    public class Product
    {
        //[Key] id belirleme
        public int Id { get; set; }

        //[Required] //konfigürasyon ayarlarında nullable özelliği açık ise bu notasyonu null olmasını istemediğimiz sütunlar için kullanabiliriz.
        //[MaxLength(100)] //sütun için üst uzunluk limiti belirlememize yardımcı olur.
        public string Name { get; set; }
        public string Url { get; set; }

        //[Column("price2", Order = 2,TypeName = "decimal(18,2)")]//Order niteliğini sadece tablo ilk kez oluştururken değiştirebiliyoruz.
        [Precision(9,2)] //Bu notasyon ile decimal özelliklerin sınırını belirleyebiliriz. İlk rakam ile hane sayısını, ikinci rakam ile de ondalık kısmın hane sayısını değiştirebiliriz.
        public decimal Price { get; set; }

        [Precision(9,2)]
        public decimal DiscountPrice { get; set; }

        public int Kdv { get; set; }

        public int Stock { get; set; }

        public int Barcode { get; set; }

        //Computed = EFCore add ve update işlemlerinde bu alanı sorgulara dahil etmez.
        //Identity = EFCore sadece update işlemlerinde bu alanı sorgulara dahil etmez.
        //None = Veritabanı tarafından otomatik değer üretmeyi kapatır.
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal PriceKdv { get; set; }

        //public int CategoryId { get; set; }

        //Navigation Property
        //[ForeignKey(nameof(Category_Id))]
        //public Category Category { get; set; }

        public ProductFeature ProductFeature { get; set; }

        public DateTime? LastAccessDate { get; set; }
    }
}
