using System;
using System.Collections.Generic;

namespace HoangHuyHieu_211243214.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? NameVn { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
