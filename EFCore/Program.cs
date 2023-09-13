using EFCore;
using EFCore.DAL;
using Microsoft.EntityFrameworkCore;

Initializer.Build();
using (var _context = new AppDbContext())
{
}

#region Pagination
//GetProducts(1, 6).ForEach(x =>
//{
//    Console.WriteLine($"{x.Id} {x.Name} {x.Price}");
//});

//static List<Product> GetProducts(int page, int pageSize)
//{
//    using (var _context = new AppDbContext())
//    {
//        //page=1 pageSize=3 => ilk 3 data => skip:0 take:3 (page-1)*pageSize => (1-1)*3 = 0
//        //page=2 pageSize=3 => ilk 3 data => skip:3 take:3 (page-1)*pageSize => (2-1)*3 = 3
//        //page=3 pageSize=3 => ilk 3 data => skip:6 take:3 (page-1)*pageSize => (3-1)*3 = 6
//        return _context.Products.Where(x=>x.Price>300).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList(); //Skip ile hedef sayfa*sayfadaki nesne sayısı kadar nesne atlarız
// Take ile de sayfadaki nesne sayısı kadar nesneyi alırız ve sorgu tamamlanmış olur.
//    }
//}
#endregion



#region Queries

#region FromSqlRaw
//var id = 5;
//decimal price = 100;
//var products = await _context.Products.FromSqlRaw("select * from products").ToListAsync();
//var product1 = await _context.Products.FromSqlRaw("select * from products where id={0}", id).FirstAsync();
//var product2 = await _context.Products.FromSqlRaw("select * from products where price>{0}", price).ToListAsync();

//var products2 = await _context.ProductEssentials.FromSqlRaw("select Name,Price from products").ToListAsync();
//var productsWithFeature = await _context.ProductWithFeatures.FromSqlRaw("select p.Id,p.Name,p.Price,pf.Color,pf.Height from Products p inner join ProductFeatures pf on p.Id = pf.Id").ToListAsync();


//FromSqlRaw metodu sayesinde tablolara SQL sorgusunu istediğimiz gibi yazarak uygulayabiliriz.
#endregion



#region FromSqlInterpolated
//var id = 5;
//decimal price = 100;
//var product3 = await _context.Products.FromSqlInterpolated($"select * from products where price>{price}").ToListAsync();

//FromSqlRaw ile arasındaki fark, yazım formatında '$' işareti kullanarak değişkenleri indexleme yöntemi yerine direkt yerine yazma yöntemi ile belirlememizdir.
#endregion

#endregion



#region Joins

#region Full Outer Join
//var leftJoin = await (from p in _context.Products
//                      join pf in _context.ProductFeatures on p.Id equals pf.Id into pfList
//                      from pf in pfList.DefaultIfEmpty()
//                      select new
//                      {
//                          ProductName = p.Name,
//                          ProductColor = pf.Color,
//                          ProductWidth = (int?)pf.Width ?? 5
//                      }).ToListAsync();

//var rightJoin = await (from pf in _context.ProductFeatures
//                       join p in _context.Products on pf.Id equals p.Id into pList
//                       from p in pList.DefaultIfEmpty()
//                       select new
//                       {
//                           ProductName = p.Name,
//                           ProductColor = pf.Color,
//                           ProductWidth = (int?)pf.Width ?? 5
//                       }).ToListAsync();

//var outerJoin = leftJoin.Union(rightJoin);

//Left Join ve Right Join sonucu oluşan sorguları, Union metoduyla birleştirerek Full Outer Join'i gerçekleyebiliriz. Bu sorgu tipinde iki tabloda da eşleşen eşleşmeyen, tüm sonuçlar elde edilir.
#endregion



#region Left/Right Join
//var leftJoin = await (from p in _context.Products
//                            join pf in _context.ProductFeatures on p.Id equals pf.Id into pfList
//                            from pf in pfList.DefaultIfEmpty()
//                            select new
//                            {
//                                ProductName = p.Name,
//                                ProductColor = pf.Color,
//                                ProductWidth = (int?)pf.Width ?? 5
//                            }).ToListAsync();

//var rightJoin = await (from pf in _context.ProductFeatures
//                             join p in _context.Products on pf.Id equals p.Id into pList
//                             from p in pList.DefaultIfEmpty()
//                             select new
//                             {
//                                 ProductName = p.Name,
//                                 ProductColor = pf.Color,
//                                 ProductWidth = (int?)pf.Width ?? 5
//                             }).ToListAsync();


//DefaultIfEmpty() metodunu sağdaki ya da soldaki, hangi tabloya yönlü Join uygulamak istiyorsak o nesneye uygulayarak Join uygulaması yapabiliriz.
//Yönlü Join sonucunda ortak olan sorguların yanı sıra, eşleşmeyen değere sahip sorgular da yer alacaktır. NULL olarak gözükeceklerdir.
#endregion



#region Inner Join

#region Inner Join I
//var result = _context.Categories.Join(_context.Products, x => x.Id, y => y.CategoryId, (c, p) => new
//{
//    CategoryName = c.Name,
//    ProductName = p.Name,
//    ProductPrice = p.Price
//}).ToList();
#endregion



#region Inner Join II
//var result = _context.Categories.Join(_context.Products,x=>x.Id,y=>y.CategoryId,(c,p) => p).ToList();

//var result2 = (from c in _context.Categories
//               join p in _context.Products on c.Id equals p.CategoryId
//               select p).ToList();


//2'li Join
//İkisi de eşit sonuçlar türetir
#endregion



#region Inner Join III
//var result1 = _context.Categories.Join(_context.Products, c => c.Id, p => p.CategoryId, (c, p) => new {c,p})
//.Join(_context.ProductFeatures, x => x.p.Id, y => y.Id, (c, pf) => new 
//{
//    CategoryName=c.c.Name,
//    ProductName=c.p.Name,
//    ProductFeatureColor=pf.Color
//});

//var result2 = (from c in _context.Categories
//               join p in _context.Products on c.Id equals p.CategoryId
//               join pf in _context.ProductFeatures on p.Id equals pf.Id
//               select new
//               {
//                   CategoryName = c.Name,
//                   ProductName = c.Name,
//                   ProductFeatureColor = pf.Color
//               }).ToList();


//3'lü Join
//ikisi de eşit sonuçlar türetir
#endregion

#endregion

#endregion



#region Using Methods with Where
//var persons = _context.People.ToList().Where(x => FormatPhone(x.Phone) == "5554443322"); 
//Burada ToList() metodunu kullanmasaydık, FormatPhone kullanımı bize hata fırlatacaktı. Çünkü verileri henüz DB'den çekmeden harici bir metot uygulayamayız. Bunun önüne de ToList kullanımı ile geçtik

//string FormatPhone(string phone)
//{
//    return phone.Substring(0, phone.Length - 1);
//}
#endregion



#region Keyless Entity ataması
//var productFulls = _context.ProductFulls.FromSqlRaw(@"select p.Id, c.Name 'CategoryName', p.Name, p.Price, pf.Height from 
//Products p
//join ProductFeatures pf on p.Id=pf.Id
//join Categories c on p.CategoryId=c.Id").ToList();
#endregion



#region Related Data Load

#region Eager Loading
//var categoryWithProducts = _context.Categories.Include(x => x.Products).ThenInclude(x=>x.ProductFeature).First();


//Bir nesneyi çağırırken Include metodu kullanarak o nesnenin bağıntılı nesnelerini sorguya dahil edebiliriz.
//Nesnelerin, varsa Child nesnelerini de ThenInclude metoduyla dahil edebiliriz.
//Dezavantajı; tüm nesneleri çekerken en baştan Child'ları dahil etmemiz gerekir. Bu problem hafıza açısından bir dezavantaj olarak karşımıza çıkar.
#endregion



#region Explicit Loading
//var category = _context.Categories.First();
//_context.Entry(category).Collection(x => x.Products).Load();

//var product = _context.Products.First();
//_context.Entry(product).Reference(x => x.ProductFeature).Load();


//Bir nesneyi çağırdıktan sonra, Load metodunu kullanarak o nesnenin bağıntılı nesnelerine erişim sağlayabiliriz
//Bağıntılı nesnelerini almak istediğimiz nesnenin bağıntılı nesnesi List şeklindeyse Collection, class şeklindeyse Reference metodu ile nesne ataması yapılır.
#endregion



#region Lazy Loading
//Microsoft.EntityFrameworkCore.Proxies kütüphanesinin bize sunduğu UseLazyLoadingProxies() metodu ile AppDbContext tarafında konfigürasyon, Lazy Loading olarak ayarlanabilir. Default olarak Lazy Loading kapalıdır.
//Lazy Loading kullanarak bütün nesnelerin bağıntılı nesneleri hazır olarak atanır.
//Kullanımı için bağıntılı nesne ister tek bir class olsun, ister List şeklinde olsun, virtual keyword'ü ile işaretlenmesi gerekir. Aksi takdirde hata alırız.


//Bu durum bize hafıza probleminin yanı sıra, "N+1 Problem" adı verilen bir problemi de doğurur.
//N+1 Problem, SQL tarafında SELECT işleminin gereksiz olarak 1 kere daha fazla çalıştırılması durumudur. Nicelik olarak küçük görünse de, nitelik olarak büyük projelerde önemli bir hafıza sorunu yaratır.
#endregion

#endregion



#region Delete Behaviors
//var category = _context.Categories.First();
//_context.Categories.Remove(category);
//Cascade => ilişkili olduğu bütün Product'lar silinir.
//Restrict => ilişkili olduğu bütün Product'ların foreign key'leri null olarak atanır.
//NoAction => ilişkili olduğu bütün Product'ların foreign key'leri null olarak atanır.
//SetNull => ilişkili olduğu bütün Product'ların foreign key'leri null olarak atanır.
//_context.SaveChanges(); 
#endregion



#region Related Data Adding

#region One-to-Many Data Adding
//var category = new Category() { Name="Kalemler",Products = new List<Product>() 
//{
//    new(){Name="kalem 1",Price=100,Stock=200,Barcode=123},
//    new(){Name="kalem 2",Price=100,Stock=200,Barcode=123},
//    new(){Name="kalem 3",Price=100,Stock=200,Barcode=123}
//} };
//_context.Add(category);
//_context.SaveChanges();
#endregion



#region Many-to-Many Data Adding 1
//var teacher = _context.Teachers.First(x=>x.Name=="Hasan Öğretmen");
//teacher.Students.AddRange(new List<Student>{
//    new Student { Name = "Hasan 100", Age=20},
//    new Student { Name = "Hasan 200", Age = 30 } });
//_context.SaveChanges();
#endregion



#region Many-to-Many Data Adding 2
//var teacher = new Teacher() { Name="Hasan Öğretmen",Students = new List<Student>() 
//{
//    new Student(){Name="Hasan1",Age=22},
//    new Student(){Name="Hasan2",Age=22}
//}
//};
//_context.Add(teacher);
//_context.SaveChanges();
#endregion



#region Many-to-Many Data Adding 3
//var student = new Student() { Name="Ahmet", Age=23};
//student.Teachers.Add(new Teacher() { Name = "Ahmet Öğretmen" });
//student.Teachers.Add(new Teacher() { Name = "Mehmet Öğretmen" });
//_context.Add(student);
//_context.SaveChanges();
#endregion



#region One-to-One Data Adding
//Product -> Parent
//ProductFeature -> Childed

//var category = _context.Categories.First(x => x.Name == "Silgiler");
//var product = new Product { Name = "Silgi", Price = 200, Stock = 200, Barcode = 123, Category = category, ProductFeature = new ProductFeature { Color="Red",Height=100,Width=200 } };
//ProductFeature productFeature = new ProductFeature() { Color="Blue",Width=200,Height=300,Product=product};
//_context.ProductFeatures.Add(productFeature);
//_context.Products.Add(product);
//_context.Add(product);
//_context.SaveChanges();
#endregion

#endregion



#region Context Data Adding

#region Context Data Adding 1
//var category = _context.Categories.First(x=>x.Name == "Defterler");
//var product = new Product() { Name = "defter 1", Price = 100, Stock = 200, Barcode = 123, CategoryId = category.Id };
//_context.Add(category);
//_context.SaveChanges();
#endregion



#region Context Data Adding 2
//var category = new Category() { Name = "Defterler" };
//category.Products.Add(new Product() { Name = "defter 1", Price = 100, Stock = 200, Barcode = 123 });
//category.Products.Add(new Product() { Name = "defter 2", Price = 100, Stock = 200, Barcode = 123 });
//_context.Add(category);
//_context.SaveChanges(); //DB'ye sadece Category'i ekleyerek ataması yapılmış olduğu için aynı anda Product'ları da eklemiş olduk.
#endregion



#region Context Data Adding 3
//var category = new Category(){ Name="Kalemler" };
//var product1 = new Product() { Name = "kalem 1", Price = 100, Stock = 200, Barcode = 123, Category = category };
//_context.Products.Add(product1);
//_context.SaveChanges(); //DB'ye sadece Product'u ekleyerek ataması yapılmış olduğu için aynı anda Category'i de eklemiş olduk.
#endregion



#region Context Data Adding 4
//var category = new Category() { Name = "Kalemler" };
//var product1 = new Product() { Name = "kalem 1", Price = 100, Stock = 200, Barcode = 123, Category = category };
//_context.Add(product1);
//_context.SaveChanges(); //Üstteki ile aynı işlemi yapmamızı sağlar.
#endregion



#region Context Data Adding 5
//_context.Products.Add(new() { Name="Kalem 1", Price=200, Stock=100, Barcode=123});
//_context.Products.Add(new() { Name = "Kalem 1", Price = 200, Stock = 100, Barcode = 123 });
//_context.Products.Add(new() { Name = "Kalem 1", Price = 200, Stock = 100, Barcode = 123 });
//_context.SaveChanges();
#endregion

#endregion



#region Update Method
//AsNoTracking() metodunu kullanarak DB'de istediğimiz tablonun takibini sonlandırarak bu tablonun Entityleri ile herhangi bir işlem yapılmasına engel oluruz. Performans açısından bu metodun kullanımı önemlidir. ReadOnly işlemler için kullanılabilir

//_context.Update(new Product() { Id = 5, Name = "Defter", Price = 500, Stock = 500, Barcode = 500 }); //Update metodu, eğer hedef Entity DB'de yoksa ekleme fonksiyonuna da sahiptir.
//await _context.SaveChangesAsync();
#endregion



#region Entity States

#region Entity States 1
//var product = await _context.Products.FirstAsync();
//Console.WriteLine($"ilk state:{_context.Entry(product).State}"); //Product DB'de olduğu fakat değiştirilmediği için Unchanged durumdadır.
//_context.Entry(product).State = EntityState.Detached;
//Console.WriteLine($"son state:{_context.Entry(product).State}"); //Product state ataması sonrası Detached durumdadır ve Track'ten çıkarılmıştır.
//product.Name = "Kalem 2000";
//await _context.SaveChangesAsync(); //Name niteliği aynı kalmıştır.
//Console.WriteLine($"save changes state:{_context.Entry(product).State}");
#endregion



#region Entity States 2
//var product = await _context.Products.FirstAsync();
//Console.WriteLine($"ilk state:{_context.Entry(product).State}"); //Product DB'de olduğu fakat değiştirilmediği için Unchanged durumdadır.
//_context.Remove(product);
//Console.WriteLine($"son state:{_context.Entry(product).State}"); //Product silindiği için Deleted durumdadır.
//await _context.SaveChangesAsync();
//Console.WriteLine($"save changes state:{_context.Entry(product).State}"); //Product, DB son halini aldıktan sonra DB'de olmadığı için Detached durumdadır.
#endregion



#region Entity States 3
//var product = await _context.Products.FirstAsync();
//Console.WriteLine($"ilk state:{_context.Entry(product).State}"); //Product DB'de olduğu fakat değiştirilmediği için Unchanged durumdadır.
//product.Price = 1000;
//Console.WriteLine($"son state:{_context.Entry(product).State}"); //Product'ın özelliği değiştirildiği için Modified durumdadır.
//await _context.SaveChangesAsync();
//Console.WriteLine($"save changes state:{_context.Entry(product).State}"); //Product DB'ye kaydedildiği için artık Unchanged durumdadır.
#endregion



#region Entity States 4
//var newProduct = new Product { Name="Kalem 200", Price=200,Stock=100,Barcode=333 };
//Console.WriteLine($"ilk state:{_context.Entry(newProduct).State}"); //Product henüz DB'ye eklenmediği için Detached durumdadır
//await _context.AddAsync(newProduct);
//Console.WriteLine($"son state:{_context.Entry(newProduct).State}"); //Product DB'ye taslak olarak eklendiği için Added durumdadır.
//await _context.SaveChangesAsync();
//Console.WriteLine($"save changes state:{_context.Entry(newProduct).State}"); //Product DB'ye kaydedildiği için artık Unchanged durumdadır.
#endregion



#region Entity States 5
//var products = await _context.Products.ToListAsync();

//products.ForEach(p => {
//    var state = _context.Entry(p).State;
//    Console.WriteLine($"{p.Id} :{p.Name} - {p.Price} - {p.Stock} state: {state}"); //Entityler DB'de yer aldığı fakat değiştirilmediği için hepsi Unchanged durumdadır
//});
#endregion

#endregion