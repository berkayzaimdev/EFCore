using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DAL
{
    [Owned] //bu notasyon ile bu sınıfın bir kapsayıcı sınıf olduğunu belirtebiliriz. Manager ve Employee sınıfları için bir temel oluşturduğu için bu notasyona ihtiyaç duyduk
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
