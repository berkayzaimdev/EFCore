using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    public class Employee //: Person //Owns Entity tanımı kalıtımı kaldırmamız gerekiyor
    {
        public int Id { get; set; } //Owns Entity tanımı gereği kendi Id alanını oluşturduk
        public Person Person { get; set; }
        [Precision(18,2)]
        public decimal Salary { get; set; }
    }
}
