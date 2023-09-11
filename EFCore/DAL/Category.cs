using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    public class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();//bu parametre ile Product modelinde bir notasyon belirtmesek bile foreign key olarak algılanıp ilişki kuruluyor. Foreign key olarak Product modelinde yeni sütun oluşturuluyor
    }
}
