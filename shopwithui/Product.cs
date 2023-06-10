using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopwithui
{
    internal class Product
    {
        public string Product_name;
        public string Store; 
        public string Location;
        public Product(string product_name, string store, string location)
        {
            Product_name = product_name;
            Store = store;
            Location = location;
        }
    }
}
