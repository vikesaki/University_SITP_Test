using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopwithui
{
    internal class Hash_Table
    {
        //Инициализация массива
        static int Size = 10;
        Hash_Node[] Nodes = new Hash_Node[Size];
        int Hashsize = 8;
        Hash_Node[] Hash = new Hash_Node[8];
        int[] Status = new int[8];
        int Count = 0;

        //чтение колличества строк с файла input.txt
        /*static int GetSize()
        {
            using (StreamReader input = new StreamReader(@"..\..\..\User_data.txt", System.Text.Encoding.Default))
            {
                return Convert.ToInt32(input.ReadLine());
            }
        }*/

        public void Read()
        {
            string fullline;
            string newlogin;
            string newlocation;
            string newage;
            string separator = ";";
            using (StreamReader input = new StreamReader(@"..\..\..\User_data.txt", System.Text.Encoding.Default))
            {
                Size = Convert.ToInt32(input.ReadLine());
                for (int i = 0; i < Size; i++)
                {
                    newlogin = " ";
                    newage = " ";
                    newlocation = " ";
                    fullline = " ";
                    string[] words;


                    fullline = input.ReadLine();
                    words = fullline.Split(separator);

                    newlogin = words[0];
                    newlocation = words[1];
                    newage = words[2];

                    ConsoleAdd(newlogin, newlocation, Convert.ToInt32(newage));
                }
            }
        }

        public void ReadManual(string filelocation)
        {
            //Hash = new Hash_Node[Hashsize];
            string fullline;
            string newlogin;
            string newlocation;
            string newage;
            string separator = ";";
            using (StreamReader input = new StreamReader(filelocation, System.Text.Encoding.Default))
            {
                string firstline = input.ReadLine();
                bool itsint1 = int.TryParse(firstline, out int filesize);
                if (itsint1)
                {
                    Size = filesize;
                    for (int i = 0; i < Size; i++)
                    {
                        newlogin = " ";
                        newage = " ";
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
                                newlogin = words[0];
                                newlocation = words[1];
                                newage = words[2];

                                bool itsint = int.TryParse(words[2], out int age);
                                if (itsint)
                                {
                                    ConsoleAdd(newlogin, newlocation, Convert.ToInt32(newage));
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

        public void Save(string filelocation)
        {
            int actualsize = 0;
            for (int i = 0; i < Hashsize; i++)
            {
                if (Hash[i] != null)
                {
                    if (Hash[i].Login != " " && Hash[i].Age != 0 && Hash[i].Location != " ")
                        actualsize++;
                }

            }
            FileStream file = new FileStream(filelocation, FileMode.Create, FileAccess.Write);
            using (StreamWriter filewriter = new StreamWriter(file))
            {
                filewriter.WriteLine(actualsize);
                for (int i = 0; i < Hashsize; i++)
                {
                    if (Hash[i] != null)
                    {
                        if (Hash[i].Login != " " && Hash[i].Age != 0 && Hash[i].Location != " ")
                            filewriter.WriteLine(Hash[i].Login + ";" + Hash[i].Location + ";" + Hash[i].Age);
                    }
                        
                }
            }
        }

        private void Upsize()
        {
            Hash_Node[] n = new Hash_Node[Hashsize];
            int[] Status_2 = new int[Hashsize];
            for (int i = 0; i < Hashsize; i++)
            {
                n[i] = new Hash_Node();
                n[i].Login = Hash[i].Login;
                n[i].Location = Hash[i].Location;
                n[i].Age = Hash[i].Age;
                Status_2[i] = Status[i];
            }
            Hashsize *= 2;
            Array.Resize(ref Hash, Hashsize);
            Array.Resize(ref Status, Hashsize);
            for (int i = 0; i < Hashsize / 2; i++)
            {
                Hash[i].Login = " ";
                Hash[i].Location = " ";
                Hash[i].Age = 0;
                Status[i] = 0;
            }
            for (int i = Hashsize / 2; i < Hashsize; i++)
                Hash[i] = new Hash_Node();
            Count = 0;
            for (int i = 0; i < Hashsize / 2; i++)
            {
                if (Status_2[i] == 1)
                    Add(n[i]);
            }

        }
        private void Downsize()
        {
            Hash_Node[] n = new Hash_Node[Hashsize];
            int[] Status_2 = new int[Hashsize];
            for (int i = 0; i < Hashsize; i++)
            {
                n[i] = new Hash_Node();
                if (Hash[i] == null)
                    continue;
                n[i].Login = Hash[i].Login;
                n[i].Location = Hash[i].Location;
                n[i].Age = Hash[i].Age;
                Status_2[i] = Status[i];
            }
            Hashsize /= 2;
            Array.Resize(ref Hash, Hashsize);
            Array.Resize(ref Status, Hashsize);
            for (int i = 0; i < Hashsize; i++)
            {
                if (Hash[i] == null)
                    continue;
                Hash[i].Login = " ";
                Hash[i].Location = " ";
                Hash[i].Age = 0;
                Status[i] = 0;
            }
            for (int i = 0; i < Hashsize * 2; i++)
            {
                if (Status_2[i] == 1)
                    Add(n[i]);
            }

        }
        private int Hash1(string n, int m)
        {
            int k = 0;
            for (int i = 0; i < n.Length; i++)
            {
                k += Convert.ToInt32(n[i]);
            }
            int x;
            while (m > 0)
            {
                x = m % 100;
                k += x;
                m = m / 100;
            }
            /*int s = 0;
            while (k > 0)
            {
                s += k & 1000;
                k /= 1000;
            }*/
            return k % Hashsize;
        }
        private int Hash2(string n, int m)
        {
            int k = 0;
            for (int i = 0; i < n.Length; i++)
            {
                k += Convert.ToInt32(n[i]);
            }
            int x;
            while (m > 0)
            {
                x = m % 100;
                k += x;
                m = m / 100;
            }
            return ((k * 2) + 1) % Hashsize;
        }
        private int Collision(int x, string n, int m)
        {
            int y = Hash2(n, m);
            return (x + y) % Hashsize;
        }
        public void Add(Hash_Node n)
        {
            int x = Hash1(n.Login, n.Age);
            for (int i = 0; i < Hashsize; i++)
            {
                if (Status[x] == 1)
                {
                    if (Hash[x].Login == n.Login && Hash[x].Age == n.Age)
                    {
                        return;
                    }
                }
                else if (Status[x] == 0 | Status[x] == 2)
                {
                    Hash[x] = n;
                    Status[x] = 1;
                    Count++;
                    return;
                }
                x = Collision(x, n.Login, n.Age);
            }
            if ((Hashsize * 0.75) < Count)
                Upsize();
            Add(n);
        }
        public void ConsoleAdd(string login, string ingridient, int condition)
        {
            Hash_Node n = new Hash_Node(login, ingridient, condition);
            Add(n);
        }
        public Hash_Node Search(string n, int m)
        {
            int x = Hash1(n, m);
            for (int i = 0; i < Hashsize; i++)
            {
                if (Status[x] == 1)
                {
                    if (Hash[x].Login == n)
                        return Hash[x];
                }
                else
                    return null;
                x = Collision(x, n, m);
                //x = (x + y) % Hashsize;
            }
            return null;
        }
        public void SearchAndPrint(string n, int m, ListBox list1, ListBox list2, ListBox list3)
        {
            list1.Items.Clear();
            list2.Items.Clear();
            list3.Items.Clear();
            int x = Hash1(n, m);
            for (int i = 0; i < Hashsize; i++)
            {
                if (Status[x] == 1)
                {
                    if (Hash[x].Login == n)
                    {
                        list1.Items.Add(Hash[x].Login);
                        list2.Items.Add(Hash[x].Location);
                        list3.Items.Add(Hash[x].Age);
                    }
                }
                x = Collision(x, n, m);
                //x = (x + y) % Hashsize;
            }
        }
        public void SearchPrint(string n, int m)
        {
            Hash_Node k = Search(n, m);
            if (k != null)
                Console.WriteLine(k.Login + " " + k.Location + " " + k.Age);
        }
        public void Delite(string n, int m)
        {
            int x = Hash1(n, m);
            for (int i = 0; i < Hashsize; i++)
            {
                if (Status[x] == 1)
                {
                    if (Hash[x].Login == n)
                    {
                        Status[x] = 2;
                        Hash[x].Login = "";
                        Hash[x].Location = "";
                        Hash[x].Age = 0;
                        //Hash[x] = null;
                        Count--;
                    }
                    if (Count <= Hashsize / 2)
                        Downsize();
                }
                else
                    return;
                x = Collision(x, n, m);
                //x = (x + y) % Hashsize;               
            }

        }
        public void HashPrint()
        {
            string hashout;
            int prehash, posthash;
            for (int i = 0; i < Hashsize; i++)
                if (Hash[i] != null)
                {
                    prehash = 0;
                    posthash = 0;
                    hashout = "";
                    if (Hash[i].Login != "" && Hash[i].Location != "" && Hash[i].Age != 0)
                    {
                        prehash = Hash1(Hash[i].Login, Hash[i].Age);
                        posthash = prehash;
                        hashout += prehash;
                        while (posthash != i)
                        {
                            posthash = Collision(posthash, Hash[i].Login, Hash[i].Age);
                            hashout += ", " + posthash;
                        }
                        Console.WriteLine(i + ". " + Hash[i].Login + " " + Hash[i].Location + " " + Hash[i].Age + " " + Status[i] + " (" + hashout + ")");
                    }
                        
                    else
                        Console.WriteLine(i + ".");
                }
                else
                    Console.WriteLine(i + ".");
        }
        public String ReturnLogin(int x)
        {
            if (x < Hashsize)
            {
                if (Hash[x] != null)
                    return Hash[x].Login;
                else
                    return "hashempty";
            }
            return "hashended";
        }
        public String ReturnLocation(int x)
        {
            if (x < Hashsize)
            {
                if (Hash[x] != null)
                    return Hash[x].Location;
                else
                    return "hashempty";
            }
            return "hashended";
        }
        public int ReturnAge(int x)
        {
            if (x < Hashsize)
            {
                if (Hash[x] != null)
                    return Hash[x].Age;
                else
                    return -2;
            }
            return -1;
        }
        public void HashDelite()
        {
            Hashsize = 8;
            Array.Resize(ref Hash, Hashsize);
            Array.Resize(ref Status, Hashsize);
            for (int i = 0; i < Hashsize; i++)
            {
                Hash[i].Login = "";
                Hash[i].Location = "";
                Hash[i].Age = 0;
                Status[i] = 0;
            }
        }
    }
}
