using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopwithui
{
    internal class Search
    {
        public void Category_Search(Tree tree, Hash_Table hash, string product_name, string login, int age, ListBox list1, ListBox list2)
        {
            List<string> list = new List<string>();
            Hash_Node a = hash.Search(login, age);
            Console.WriteLine("\noutput result ------------------------------------------------------");
            list = tree.findstore(product_name, a.Location);
            for (int i = 0; i < list.Count; i++)
            {
                //Console.WriteLine(list[i]);
                list1.Items.Insert(i,list[i]);
                list2.Items.Insert(i,a.Location);
            }
        }
    }
}
