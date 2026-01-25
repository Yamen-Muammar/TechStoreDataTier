using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreApp_Core_.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }

        public Category(int? id , string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }
    }
}
