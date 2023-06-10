using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopwithui
{
    internal class Hash_Node
    {
        public string Login;  
        public string Location; 
        public int Age;
        public Hash_Node(string login, string location, int age)
        {
            Login = login;
            Location = location;
            Age = age;
        }
        public Hash_Node()
        {
            Login = "";
            Location = "";
            Age = 0;
        }
    }
}
