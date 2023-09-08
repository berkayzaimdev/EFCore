using EFCore;
using EFCore.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();
using (var _context = new AppDbContext())
{
    _context.Products.Add(new() { Name="Kalem 1", Price=200, Stock=100, Barcode=123});
    _context.Products.Add(new() { Name = "Kalem 1", Price = 200, Stock = 100, Barcode = 123 });
    _context.Products.Add(new() { Name = "Kalem 1", Price = 200, Stock = 100, Barcode = 123 });
    _context.SaveChanges();


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
}