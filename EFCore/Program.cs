﻿using EFCore;
using EFCore.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();
using (var _context = new AppDbContext())
{
    var category = _context.Categories.First();
    _context.Categories.Remove(category);
    //Cascade => ilişkili olduğu bütün Product'lar silinir.
    //Restrict => ilişkili olduğu bütün Product'ların foreign key'leri null olarak atanır.
    //NoAction => ilişkili olduğu bütün Product'ların foreign key'leri null olarak atanır.
    //SetNull => ilişkili olduğu bütün Product'ların foreign key'leri null olarak atanır.
    _context.SaveChanges(); 
}
//var category = new Category() { Name="Kalemler",Products = new List<Product>() 
//{
//    new(){Name="kalem 1",Price=100,Stock=200,Barcode=123},
//    new(){Name="kalem 2",Price=100,Stock=200,Barcode=123},
//    new(){Name="kalem 3",Price=100,Stock=200,Barcode=123}
//} };
//_context.Add(category);
//_context.SaveChanges();




//var teacher = _context.Teachers.First(x=>x.Name=="Hasan Öğretmen");
//teacher.Students.AddRange(new List<Student>{
//    new Student { Name = "Hasan 100", Age=20},
//    new Student { Name = "Hasan 200", Age = 30 } });
//_context.SaveChanges();




//var teacher = new Teacher() { Name="Hasan Öğretmen",Students = new List<Student>() 
//{
//    new Student(){Name="Hasan1",Age=22},
//    new Student(){Name="Hasan2",Age=22}
//}
//};
//_context.Add(teacher);
//_context.SaveChanges();




//var student = new Student() { Name="Ahmet", Age=23};
//student.Teachers.Add(new Teacher() { Name = "Ahmet Öğretmen" });
//student.Teachers.Add(new Teacher() { Name = "Mehmet Öğretmen" });
//_context.Add(student);
//_context.SaveChanges();




//Product -> Parent
//ProductFeature -> Childed

//var category = _context.Categories.First(x => x.Name == "Silgiler");
//var product = new Product { Name = "Silgi", Price = 200, Stock = 200, Barcode = 123, Category = category, ProductFeature = new ProductFeature { Color="Red",Height=100,Width=200 } };
//ProductFeature productFeature = new ProductFeature() { Color="Blue",Width=200,Height=300,Product=product};
//_context.ProductFeatures.Add(productFeature);
//_context.Products.Add(product);
//_context.Add(product);
//_context.SaveChanges();




//var category = _context.Categories.First(x=>x.Name == "Defterler");
//var product = new Product() { Name = "defter 1", Price = 100, Stock = 200, Barcode = 123, CategoryId = category.Id };
//_context.Add(category);
//_context.SaveChanges();




//var category = new Category() { Name = "Defterler" };
//category.Products.Add(new Product() { Name = "defter 1", Price = 100, Stock = 200, Barcode = 123 });
//category.Products.Add(new Product() { Name = "defter 2", Price = 100, Stock = 200, Barcode = 123 });
//_context.Add(category);
//_context.SaveChanges(); //DB'ye sadece Category'i ekleyerek ataması yapılmış olduğu için aynı anda Product'ları da eklemiş olduk.




//var category = new Category(){ Name="Kalemler" };
//var product1 = new Product() { Name = "kalem 1", Price = 100, Stock = 200, Barcode = 123, Category = category };
//_context.Products.Add(product1);
//_context.SaveChanges(); //DB'ye sadece Product'u ekleyerek ataması yapılmış olduğu için aynı anda Category'i de eklemiş olduk.




//var category = new Category() { Name = "Kalemler" };
//var product1 = new Product() { Name = "kalem 1", Price = 100, Stock = 200, Barcode = 123, Category = category };
//_context.Add(product1);
//_context.SaveChanges(); //Üstteki ile aynı işlemi yapmamızı sağlar.




//_context.Products.Add(new() { Name="Kalem 1", Price=200, Stock=100, Barcode=123});
//_context.Products.Add(new() { Name = "Kalem 1", Price = 200, Stock = 100, Barcode = 123 });
//_context.Products.Add(new() { Name = "Kalem 1", Price = 200, Stock = 100, Barcode = 123 });
//_context.SaveChanges();


//AsNoTracking() metodunu kullanarak DB'de istediğimiz tablonun takibini sonlandırarak bu tablonun Entityleri ile herhangi bir işlem yapılmasına engel oluruz. Performans açısından bu metodun kullanımı önemlidir. ReadOnly işlemler için kullanılabilir

//_context.Update(new Product() { Id = 5, Name = "Defter", Price = 500, Stock = 500, Barcode = 500 }); //Update metodu, eğer hedef Entity DB'de yoksa ekleme fonksiyonuna da sahiptir.
//await _context.SaveChangesAsync();





//var product = await _context.Products.FirstAsync();

//Console.WriteLine($"ilk state:{_context.Entry(product).State}"); //Product DB'de olduğu fakat değiştirilmediği için Unchanged durumdadır.

//_context.Entry(product).State = EntityState.Detached;

//Console.WriteLine($"son state:{_context.Entry(product).State}"); //Product state ataması sonrası Detached durumdadır ve Track'ten çıkarılmıştır.

//product.Name = "Kalem 2000";

//await _context.SaveChangesAsync(); //Name niteliği aynı kalmıştır.

//Console.WriteLine($"save changes state:{_context.Entry(product).State}");





//var product = await _context.Products.FirstAsync();

//Console.WriteLine($"ilk state:{_context.Entry(product).State}"); //Product DB'de olduğu fakat değiştirilmediği için Unchanged durumdadır.

//_context.Remove(product);

//Console.WriteLine($"son state:{_context.Entry(product).State}"); //Product silindiği için Deleted durumdadır.

//await _context.SaveChangesAsync();

//Console.WriteLine($"save changes state:{_context.Entry(product).State}"); //Product, DB son halini aldıktan sonra DB'de olmadığı için Detached durumdadır.




//var product = await _context.Products.FirstAsync();

//Console.WriteLine($"ilk state:{_context.Entry(product).State}"); //Product DB'de olduğu fakat değiştirilmediği için Unchanged durumdadır.

//product.Price = 1000;

//Console.WriteLine($"son state:{_context.Entry(product).State}"); //Product'ın özelliği değiştirildiği için Modified durumdadır.

//await _context.SaveChangesAsync();

//Console.WriteLine($"save changes state:{_context.Entry(product).State}"); //Product DB'ye kaydedildiği için artık Unchanged durumdadır.




//var newProduct = new Product { Name="Kalem 200", Price=200,Stock=100,Barcode=333 };

//Console.WriteLine($"ilk state:{_context.Entry(newProduct).State}"); //Product henüz DB'ye eklenmediği için Detached durumdadır

//await _context.AddAsync(newProduct);

//Console.WriteLine($"son state:{_context.Entry(newProduct).State}"); //Product DB'ye taslak olarak eklendiği için Added durumdadır.

//await _context.SaveChangesAsync();

//Console.WriteLine($"save changes state:{_context.Entry(newProduct).State}"); //Product DB'ye kaydedildiği için artık Unchanged durumdadır.




//var products = await _context.Products.ToListAsync();

//products.ForEach(p => {
//    var state = _context.Entry(p).State;
//    Console.WriteLine($"{p.Id} :{p.Name} - {p.Price} - {p.Stock} state: {state}"); //Entityler DB'de yer aldığı fakat değiştirilmediği için hepsi Unchanged durumdadır
//});