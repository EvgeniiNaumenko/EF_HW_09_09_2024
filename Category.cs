﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_007_09_09_2024
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerCategory> CustomerCategories { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
    }
}
