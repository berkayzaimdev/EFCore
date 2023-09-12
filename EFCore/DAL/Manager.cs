using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    public class Manager //: Person //Owns Entity tanımı kalıtımı kaldırmamız gerekiyor
    {
        public int Id { get; set; } //Owns Entity tanımı gereği kendi Id alanını oluşturduk
        public Person Person { get; set; }
        public int Grade { get; set; }
    }
}
