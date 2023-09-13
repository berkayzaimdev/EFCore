using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductFull> ProductFulls { get; set; }
        public DbSet<ProductEssential> ProductEssentials { get; set; }
        public DbSet<ProductWithFeature> ProductWithFeatures { get; set; }

        public AppDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region LogLevel
            //Önem sırasına göre LogLevel'lar aşağıdaki gibidir,

            //Trace: En ayrıntılı iletileri içerir. Bu iletiler hassas uygulama verileri içerebilir. Bu iletiler varsayılan olarak devre dışı bırakılmıştır ve üretimde etkinleştirilmemesi gerekir.
            //Debug: Hata ayıklama ve geliştirme için. Yüksek hacim nedeniyle üretimde dikkatli kullanılmalıdır.
            //Information: Uygulamanın genel akışını izler. Uzun süreli bir değeri olabilir.
            //Warning: Anormal veya beklenmeyen olaylar için. Normalde uygulamanın başarısız olmasına neden olmayan hataları veya koşulları içerir.
            //Error: İşlenemeyen hatalar ve özel durumlar için. Bu iletiler uygulama genelindeki bir hatayı değil geçerli işlem veya istekteki hatayı gösterir.
            //Critical: Hemen ilgilenilmesi gereken hatalar için. Örnekler: veri kaybı senaryoları, yetersiz disk alanı.
            #endregion

            Initializer.Build();
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Fluent API

            #region Modify Table and Columns
            //modelBuilder.Entity<Product>().ToTable("ProductTBB","productsbb"); bu şekilde tablo oluşturabiliyoruz
            //modelBuilder.Entity<Product>().HasKey(x=>x.Product_Id);
            //modelBuilder.Entity<Product>().Property(x=>x.Name).IsRequired().HasMaxLength(100).IsFixedLength(); //Model sınıfımızda nullable açık olmasına rağmen Fluent API bu durumun önüne IsRequired metodu sayesinde geçti ve sütun not nullable oldu. FixedLength ile de sütuna sabit uzunluk ataması yapıldı.
            #endregion



            #region One-to-Many
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x=>x.Category_Id); //Her kategorinin birden fazla ürünü olabilir. Fakat bir ürün aynı anda sadece bir kategoriye ait olabilir.
            #endregion



            #region One-to-One
            //modelBuilder.Entity<Product>().HasOne(x=>x.ProductFeature).WithOne(x=>x.Product).HasForeignKey<ProductFeature>(x=>x.Id); //Her ürünün sadece bir özelliği olabilir. Her özellik de aynı anda sadece bir ürüne ait olabilir.
            #endregion



            #region Many-to-Many
            //modelBuilder.Entity<Student>()
            //    .HasMany(x => x.Teachers)
            //    .WithMany(x => x.Students)
            //    .UsingEntity<Dictionary<string, object>>(
            //    "StudentTeacherManyToMany",
            //    x => x.HasOne<Teacher>().WithMany().HasForeignKey("Teacher_Id").HasConstraintName("FK__TeacherId"),
            //    x => x.HasOne<Student>().WithMany().HasForeignKey("Student_Id").HasConstraintName("FK__StudentId")
            //    );//Bu noktade FluentAPI kullanmak işimizi daha çok kolaylaştırır, daha anlamlı bir parametrizasyon sağlar.
            #endregion



            #region DeleteBehavior
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).HasComputedColumnSql("[Price]*[Kdv]");
            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).ValueGeneratedOnAdd();//Identity
            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).ValueGeneratedOnAddOrUpdate();//Computed
            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).ValueGeneratedNever();//None
            #endregion



            #region Keyless Entity
            //modelBuilder.Entity<ProductFull>().HasNoKey();
            #endregion



            #region Entity Properties
            //modelBuilder.Entity<Product>().Ignore(x => x.Barcode); //söz konusu parametrenin DB'ye işlenmemesini sağlar.
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsUnicode(false).HasMaxLength(500); //söz konusu parametrenin unicode olmamasını sağlar ve maksimum uzunluğu ayarlar. Unicode false olarak ayarlandığı için artık Japonca, Korece vb. karakterleri kullanamayız ve sütun tipi varchar olur.
            //modelBuilder.Entity<Product>().Property(x => x.Url).HasColumnType("varchar(500)").HasColumnName("ProductUrl"); //söz konusu parametrenin sütun tipini ve ismini değiştiririz.
            #endregion



            #region Inheritence

            #region Table-per-Hiearchy
            //public DbSet<BasePerson> Persons { get; set; }
            //public DbSet<Manager> Managers { get; set; }
            //public DbSet<Employee> Employees { get; set; }

            //Bu şekilde bir parametrizasyon yaptığımızda, ayrı ayrı Manager ve Employee tabloları yerine tek bir Persons tablosu oluşur.
            //Bu tabloda ortak parametrelerin yanı sıra, kalıtım alan sınıfların kendi parametreleri de bulunur.
            //Buna ek olarak, Discriminator adında bir sütun daha oluşur. Bu sütun nvarchar(max) tipinde olup kalıtım alan sınıfın adını temsil eder.
            //Nesne oluştururken kalıtım alan sınıfın dahil etmediği sütunlar NULL olarak atanır.
            //Nesneleri döngü ile dönerken switch case yapısını kullanabiliriz. Manager ve Employee olarak tanımladığımızda derleyici bunu algılayacaktır.
            //e.g: switch(p) case Manager manager
            #endregion



            #region Table-per-Type
            //modelBuilder.Entity<BasePerson>().ToTable("Persons");
            //modelBuilder.Entity<Employee>().ToTable("Employees");
            //modelBuilder.Entity<Manager>().ToTable("Managers");

            //Bu şekilde FluentAPI metodları ile kurulum yaptığımızda, üç tablo da oluşur
            //Üç tablonun da Entity'lerine göre parametreleri oluşur
            //Add işlemi yaptığımızda hedef Entity'nin yanı sıra Persons tablosunda veri eklemesi otomatik yapılır.
            //Nesneleri döngü ile dönerken switch case yapısını kullanabiliriz. Manager ve Employee olarak tanımladığımızda derleyici bunu algılayacaktır.
            //e.g: switch(p) case Manager manager
            #endregion



            #region Owned & Owns Entities
            //modelBuilder.Entity<Manager>().OwnsOne(x => x.Person, p =>
            //{
            //    p.Property(x => x.FirstName).HasColumnName("FirstName");
            //    p.Property(x => x.LastName).HasColumnName("LastName");
            //    p.Property(x => x.Age).HasColumnName("Age");
            //});

            //modelBuilder.Entity<Employee>().OwnsOne(x => x.Person, p =>
            //{
            //    p.Property(x => x.FirstName).HasColumnName("FirstName");
            //    p.Property(x => x.LastName).HasColumnName("LastName");
            //    p.Property(x => x.Age).HasColumnName("Age");
            //});
            //Bu şekilde kullanım yaptığımız zaman bize 2 tane tablo oluşacaktır; Employees ve Managers.
            //FluentAPI kullanmadığımız durumda sütun isimleri Person_FirstName şeklinde olacaktır. Bu sebeple Best Practice'i sağlayan yöntem FluentAPI'dır.
            #endregion

            #endregion



            #region Indexing
            //modelBuilder.Entity<Product>().HasIndex(x => x.Name).IncludeProperties(x => new { x.Price, x.Stock }); //Name alanına göre indexleme yaparız ve dönüş değerlerini belirttiğimiz gibi alırız
            #endregion



            #region Check Constraint
            //modelBuilder.Entity<Product>().HasCheckConstraint("PriceDiscountCheck", "[Price]>[DiscountPrice]"); //Tabloya koşul ekler
            #endregion



            #region ToSqlQuery
            //modelBuilder.Entity<ProductEssential>().HasNoKey().ToSqlQuery("select Name,Price from Products");
            
            //Bu metot ile tabloları kendimiz SQL sorgusu yazarak oluşturabiliriz, diğer tablolardan veri çekebiliriz.
            #endregion

            #endregion
            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    ChangeTracker.Entries().ToList().ForEach(e =>
        //    {
        //        if (e.Entity is Product p)
        //        {
        //            if (e.State == EntityState.Added)
        //            {
        //                p.CreatedDate = DateTime.Now;
        //            }
        //        }
        //    });
        //    return base.SaveChanges();
        //}
    }
}