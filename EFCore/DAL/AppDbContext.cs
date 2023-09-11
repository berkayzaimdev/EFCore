using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlCon"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("ProductTBB","productsbb"); bu şekilde tablo oluşturabiliyoruz
            //modelBuilder.Entity<Product>().HasKey(x=>x.Product_Id);
            //modelBuilder.Entity<Product>().Property(x=>x.Name).IsRequired().HasMaxLength(100).IsFixedLength(); //Model sınıfımızda nullable açık olmasına rağmen Fluent API bu durumun önüne IsRequired metodu sayesinde geçti ve sütun not nullable oldu. FixedLength ile de sütuna sabit uzunluk ataması yapıldı.


            //One-to-Many
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x=>x.Category_Id); //Her kategorinin birden fazla ürünü olabilir. Fakat bir ürün aynı anda sadece bir kategoriye ait olabilir.


            //One-to-One
            //modelBuilder.Entity<Product>().HasOne(x=>x.ProductFeature).WithOne(x=>x.Product).HasForeignKey<ProductFeature>(x=>x.Id); //Her ürünün sadece bir özelliği olabilir. Her özellik de aynı anda sadece bir ürüne ait olabilir.


            //Many-to-Many
            //modelBuilder.Entity<Student>()
            //    .HasMany(x => x.Teachers)
            //    .WithMany(x => x.Students)
            //    .UsingEntity<Dictionary<string, object>>(
            //    "StudentTeacherManyToMany",
            //    x => x.HasOne<Teacher>().WithMany().HasForeignKey("Teacher_Id").HasConstraintName("FK__TeacherId"),
            //    x => x.HasOne<Student>().WithMany().HasForeignKey("Student_Id").HasConstraintName("FK__StudentId")
            //    );//Bu noktade FluentAPI kullanmak işimizi daha çok kolaylaştırır, daha anlamlı bir parametrizasyon sağlar.


            //DeleteBehavior ataması
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).HasComputedColumnSql("[Price]*[Kdv]");
            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).ValueGeneratedOnAdd();//Identity
            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).ValueGeneratedOnAddOrUpdate();//Computed
            //modelBuilder.Entity<Product>().Property(x => x.PriceKdv).ValueGeneratedNever();//None

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