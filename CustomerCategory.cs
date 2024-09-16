using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_007_09_09_2024
{
    public class CustomerCategory
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
    }
}
