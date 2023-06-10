using System;
using System.Collections.Generic;
using System.Text;

namespace shopwithui
{
    class Tree
    {
        public Node _nil = new Node();
        public Node _root;
        static int Size = 0;

        public Node Root
        {
            get
            {
                return _root;
            }
        }
        public Tree()
        {

        }
        public void NilMaker()
        {
            _nil.Left = _nil;
            _nil.Parent = _nil;
            _nil.Right = _nil;
            _root = _nil;
        }
        public void Insert(string product_name, string store, string location)
        {
            Product k = new Product(product_name, store, location);
            Node n = new Node(_nil, k, false, _nil, _nil, 1);
            InsertNode(n);
        }
        /*static int GetSize()
        {
            using (StreamReader input = new StreamReader(@"..\..\..\Shop_data.txt", System.Text.Encoding.Default))
            {
                return Convert.ToInt32(input.ReadLine());
            }
        }*/

        public void Read()
        {
            string fullline;
            string newproduct;
            string newstore;
            string newlocation;
            string separator = ";";
            using (StreamReader input = new StreamReader(@"..\..\..\Shop_data.txt", System.Text.Encoding.Default))
            {
                Size = Convert.ToInt32(input.ReadLine());
                for (int i = 0; i < Size; i++)
                {
                    newproduct = " ";
                    newstore = " ";
                    newlocation = " ";
                    fullline = " ";
                    string[] words;


                    fullline = input.ReadLine();
                    words = fullline.Split(separator);

                    newproduct = words[0];
                    newstore = words[1];
                    newlocation = words[2];

                    Insert(newproduct, newstore, newlocation);
                }
            }
        }

        public void ReadManual(string filelocation)
        {
            Clean();
            string fullline;
            string newproduct;
            string newstore;
            string newlocation;
            string separator = ";";
            string firstline;
            using (StreamReader input = new StreamReader(filelocation, System.Text.Encoding.Default))
            {
                firstline = input.ReadLine();
                bool itsint = int.TryParse(firstline, out int filesize);
                if (itsint)
                {
                    Size = filesize;
                    for (int i = 0; i < Size; i++)
                    {
                        newproduct = " ";
                        newstore = " ";
                        newlocation = " ";
                        fullline = " ";
                        string[] words;


                        fullline = input.ReadLine();
                        words = fullline.Split(separator);
                        if (words.Length >= 3)
                        {
                            bool itsnotstring = string.IsNullOrEmpty(words[0]);
                            bool itsnotstring1 = string.IsNullOrEmpty(words[1]);
                            bool itsnotstring2 = string.IsNullOrEmpty(words[2]);
                            if (!itsnotstring && !itsnotstring1 && !itsnotstring2)
                            {
                                newproduct = words[0];
                                newstore = words[1];
                                newlocation = words[2];
                                Insert(newproduct, newstore, newlocation);
                            }
                            else
                            {
                                MessageBox.Show("Input Is Incorrect", "Error");
                                break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Input Is Incorrect", "Error");
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Input Is Incorrect", "Error");
                }
            }
        }
        public void Save(Node n, string filelocation)
        {
            FileStream file = new FileStream(filelocation, FileMode.Create, FileAccess.Write);
            using (StreamWriter filewriter = new StreamWriter(file))
            {
                filewriter.WriteLine(Size);
                savehelper(_root, filewriter);
            } 
        }

        public void savehelper(Node n, StreamWriter name)
        {
            if (n != _nil)
            {
                Product p = n.Data;
                name.WriteLine(n.Data.Product_name + ";" + n.Data.Store + ";" + n.Data.Location);
                savehelper(n.Left, name);
                savehelper(n.Right, name);
            }
        }

        public void Delete(string product_name, string store, string location)
        {
            Product k = new Product(product_name, store, location);
            Node n = FindNode(k);
            if (n.Data != null && (n.Data.Product_name == product_name && n.Data.Store == store && n.Data.Location == location))
                DeleteNode(n);
        }
        public bool Find(string product_name, string store, string location)
        {
            Product k = new Product(product_name, store, location);
            Node n = FindNode(k);
            if (n.Data != default)
                return true;
            return false;
        }
        public void LRPrint()
        {
            LRPrintHelp(_root);
        }
        public void RLPrint()
        {
            RLPrintHelp(_root);
        }
        public void NLRPrint()
        {
            NLRPrintHelp(_root);
        }
        public void Draw()
        {
            DrawHelp(_root, 0);
        }
        public void Clean()
        {
            while (_root != _nil)
            {
                DeleteNode(_root);
            }
        }
        private void LeftRotate(Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;

            if (y.Left != _nil)
                y.Left.Parent = x;
            y.Parent = x.Parent;

            if (x.Parent == _nil)
                _root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }
        private void RightRotate(Node y)
        {
            Node x = y.Left;
            y.Left = x.Right;

            if (x.Right != _nil)
                x.Right.Parent = y;

            x.Parent = y.Parent;

            if (y.Parent == _nil)
                _root = x;
            else if (y == y.Parent.Right)
                y.Parent.Right = x;
            else
                y.Parent.Left = x;

            x.Right = y;
            y.Parent = x;
        }
        private bool NodeCompare(Product node1, Product node2)
        {
            if (node1 != null && node2 != null)
            {
                if (node1.Location == node2.Location && node1.Product_name == node2.Product_name
                    && node1.Store == node2.Store)
                    return true;
            }
            return false;
        }
        private void InsertNode(Node z)
        {
            if (_root != null)
            {
                Node y = _nil;
                Node x = _root;
                while (x != _nil)
                {
                    if (NodeCompare(z.Data, y.Data))
                    {
                        y.Count += 1;
                        return;
                    }
                    y = x;
                    if (ProductBigger(x.Data, z.Data))
                        x = x.Left;
                    else if (ProductBigger(z.Data, x.Data))
                        x = x.Right;
                }
                z.Parent = y;
                if (y == _nil)
                    _root = z;
                else if (ProductBigger(y.Data, z.Data))  
                    y.Left = z;
                else
                    y.Right = z;
                z.Left = _nil;
                z.Right = _nil;
                z.Color = false;
                InsertFixup(z);
            }
            else
            {
                _root = z;
                _root.Color = true;
            }
        }
        private void InsertFixup(Node z)
        {
            while (z != _root && z.Parent.Color == false)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    Node y = z.Parent.Parent.Right;
                    if (y != null && y.Color == false)
                    {
                        z.Parent.Color = true;
                        y.Color = true;
                        z.Parent.Parent.Color = false;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            LeftRotate(z);
                        }
                        z.Parent.Color = true;
                        z.Parent.Parent.Color = false;
                        RightRotate(z.Parent.Parent);
                    }
                }
                else
                {
                    Node y = z.Parent.Parent.Left;
                    if (y != null && y.Color == false)
                    {
                        z.Parent.Color = true;
                        y.Color = true;
                        z.Parent.Parent.Color = false;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RightRotate(z);
                        }
                        z.Parent.Color = true;
                        z.Parent.Parent.Color = false;
                        LeftRotate(z.Parent.Parent);
                    }
                }
            }
            _root.Color = true;
        }
        private void Transplant(Node u, Node v)
        {
            if (u.Parent == _nil)
                _root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            v.Parent = u.Parent;
        }
        private void DeleteNode(Node z)
        {
            if (z.Count > 1)
            {
                z.Count -= 1;
            }
            else
            {
                Node y = z;
                bool yColor = y.Color;
                Node x;
                if (z.Left == _nil)
                {
                    x = z.Right;
                    Transplant(z, z.Right);
                }
                else if (z.Right == _nil)
                {
                    x = z.Left;
                    Transplant(z, z.Left);
                }
                else
                {
                    y = Minimum(z.Right);
                    yColor = y.Color;
                    x = y.Right;
                    if (y.Parent == z)
                    {
                        x.Parent = y;
                    }
                    else
                    {
                        Transplant(y, y.Right);
                        y.Right = z.Right;
                        y.Right.Parent = y;
                    }
                    Transplant(z, y);
                    y.Left = z.Left;
                    y.Left.Parent = y;
                    y.Color = z.Color;
                }
                if (yColor == true)
                    DeleteFixup(x);
            }
        }
        private void DeleteFixup(Node x)
        {
            while (x != _root && x.Color == true)
            {
                if (x == x.Parent.Left)
                {
                    Node w = x.Parent.Right;
                    if (w.Color == false)
                    {
                        w.Color = true;
                        x.Parent.Color = false;
                        LeftRotate(x.Parent);
                        w = x.Parent.Right;
                    }
                    if (w.Left.Color == true && w.Right.Color == true)
                    {
                        w.Color = false;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Right.Color == true)
                        {
                            w.Left.Color = true;
                            w.Color = false;
                            RightRotate(w);
                            w = x.Parent.Right;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = true;
                        w.Right.Color = true;
                        LeftRotate(x.Parent);
                        x = _root;
                    }
                }
                else
                {
                    Node w = x.Parent.Left;
                    if (w.Color == false)
                    {
                        w.Color = true;
                        x.Parent.Color = false;
                        RightRotate(x.Parent);
                        w = x.Parent.Left;
                    }
                    if (w.Left.Color == true && w.Right.Color == true)
                    {
                        w.Color = false;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Left.Color == true)
                        {
                            w.Right.Color = true;
                            w.Color = false;
                            LeftRotate(w);
                            w = x.Parent.Left;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = true;
                        w.Left.Color = true;
                        RightRotate(x.Parent);
                        x = _root;
                    }
                }
            }
            x.Color = true;
        }
        public Node FindNode(Product z)
        {
            Node n = _root;
            while ( n.Data != null && (n.Data.Product_name != z.Product_name || n.Data.Location != z.Location || n.Data.Store != z.Store))
            {
                if (ProductBigger(n.Data, z))                         //(n.Data > z)
                    n = n.Left;
                else
                    n = n.Right;
            }
            return n;
        }
        private Product FindMinimum()
        {
            Node z = Minimum(_root);
            return z.Data;
        }
        private Node Minimum(Node z)
        {
            while (z.Left != _nil)
                z = z.Left;

            return z;
        }
        private void LRPrintHelp(Node n)
        {
            if (n != _nil)
            {
                Product p = n.Data;
                LRPrintHelp(n.Left);
                Console.WriteLine(p + ", quantity: " + n.Count);
                LRPrintHelp(n.Right);
            }
        }
        private void RLPrintHelp(Node n)
        {
            if (n != _nil)
            {
                Product p = n.Data;
                RLPrintHelp(n.Right);
                Console.WriteLine(p + ", quantity: " + n.Count);
                RLPrintHelp(n.Left);
            }
        }
        private void NLRPrintHelp(Node n)
        {
            if (n != _nil)
            {
                Product p = n.Data;
                Console.WriteLine(p + ", quantity: " + n.Count);
                NLRPrintHelp(n.Left);
                NLRPrintHelp(n.Right);
            }
        }

        public void ProductListOut(Node n,ListBox list)
        {
            int x = 0;
            if (n != _nil)
            {
                Product p = n.Data;
                list.Items.Insert(x, p.Product_name);
                ProductListOut(n.Left, list);
                ProductListOut(n.Right, list);
            }
        }

        public void LocationListOut(Node n, ListBox list)
        {
            int x = 0;
            if (n != _nil)
            {
                Product p = n.Data;
                list.Items.Insert(x, p.Location);
                LocationListOut(n.Left, list);
                LocationListOut(n.Right, list);
            }
        }
        public void NameListOut(Node n, ListBox list)
        {
            int x = 0;
            if (n != _nil)
            {
                Product p = n.Data;
                list.Items.Insert(x, p.Store);
                NameListOut(n.Left, list);
                NameListOut(n.Right, list);
            }
        }

        private void DrawHelp(Node root, int h)
        {
            int level = 0;
            if (root != _nil)
            {
                string colorout = "";
                Product p = root.Data;
                DrawHelp(root.Right, h + 8);
                for (int i = 0; i < h; i++)
                    Console.Write(" ");
                
                if (!root.Color)
                    colorout = "Red";
                else
                    colorout = "Black";
                Console.WriteLine(p.Product_name + " " + p.Store + " " + p.Location + ", count: " + root.Count + " " + colorout);
                DrawHelp(root.Left, h + 8);
            }
        }
        private bool ProductBigger(Product node1, Product node2)
        {
            if (node1.Product_name != node2.Product_name)
            {
                if (StringBigger(node1.Product_name, node2.Product_name))
                    return true;
                else
                    return false;
            }
            else if (node1.Store != node2.Store)
            {
                if (StringBigger(node1.Store, node2.Store))
                    return true;
                else
                    return false;
            }
            else if (node1.Location != node2.Location)
            {
                if (StringBigger(node1.Location, node2.Location))
                    return true;
                else
                    return false;
            }
            else
                return false;

            /*if (node1.Product_name != node2.Product_name || node1.Store != node2.Store || node1.Location != node2.Location)
            {
                if (NodeBigger(node1, node2))
                    return true;
                else
                    return false;
            }
            return false;*/
        }
        private bool NodeBigger(Product Node1, Product Node2)
        {
            int inttostring = 0;
            int inttostring2 = 0;
            for (int i = 0; i < Node1.Product_name.Length; i++)
                inttostring += Convert.ToInt32(Node1.Product_name[i]);
            for (int i = 0; i < Node2.Product_name.Length; i++)
                inttostring2 += Convert.ToInt32(Node2.Product_name[i]);

            for (int i = 0; i < Node1.Store.Length; i++)
                inttostring += Convert.ToInt32(Node1.Store[i]);
            for (int i = 0; i < Node2.Store.Length; i++)
                inttostring2 += Convert.ToInt32(Node2.Store[i]);

            for (int i = 0; i < Node1.Location.Length; i++)
                inttostring += Convert.ToInt32(Node1.Location[i]);
            for (int i = 0; i < Node2.Location.Length; i++)
                inttostring2 += Convert.ToInt32(Node2.Location[i]);

            if (inttostring != inttostring2)
            {
                if (inttostring > inttostring2)
                    return true;
                if (inttostring < inttostring2)
                    return false;
            }
            return false;
        }
        private bool StringBigger(string str1, string str2)
        {
            int intstring1 = 0;
            int intstring2 = 0;
            for (int i = 0; i < str1.Length; i++)
                intstring1 +=  Convert.ToInt32(str1[i]);
            for (int i = 0; i < str2.Length; i++)
                intstring2 += Convert.ToInt32(str2[i]);
            if (str1 != str2)
            {
                if (intstring1 > intstring2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
                return false;
        }

        public void findstorehelp(string product_name, string location, Node n, List<string> output)
        {
            if (n != _nil)
            {
                Product item = n.Data;
                findstorehelp(product_name, location, n.Left, output);
                if (n.Data.Product_name == product_name && n.Data.Location == location && n != _nil)
                    output.Add(n.Data.Store);
                findstorehelp(product_name, location, n.Right, output);
            }
        }

        public List<string> findstore(string product_name, string location)
        {
            List<string> list = new List<string>();
            findstorehelp(product_name, location, _root, list);
            return list;
        }

        public void findstoreprint(string product_name, string location, ListBox list1, ListBox list2, ListBox list3,int x)
        {
            fsphelper(product_name,location,_root,list1,list2,list3,x);
        }
        public void fsphelper(string product_name, string location, Node n, ListBox list1, ListBox list2, ListBox list3,int x)
        {
            if (n != _nil)
            {
                Product item = n.Data;
                fsphelper(product_name, location, n.Left, list1, list2, list3, x);
                if (n.Data.Product_name == product_name && n.Data.Location == location && n != _nil)
                {
                    list1.Items.Insert(x, n.Data.Store);
                    list2.Items.Insert(x, n.Data.Location);
                    list3.Items.Insert(x, n.Data.Product_name);
                    x++;
                }
                fsphelper(product_name, location, n.Right, list1, list2, list3, x);
            }
        }
    }
}

