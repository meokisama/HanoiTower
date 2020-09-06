using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower.Core.Models
{
    public class StackArray<T> : IStack<T>
    {
        //property
        public int Newsize { get; set; }
        public string boy1 { get; set; }


        //first thing to do, create the field(private);
        private T[] Arey = new T[0];

        public StackArray()
        {
            Newsize = 0;
        }

        public void Push(T item)
        {
            Newsize++;
            //arrray are immutable, can't resize, ∴ bakwit
            //Array.Resize(ref Arey, Newsize);
            //Arey[Newsize - 1] = item;
            Arey = new List<T>(Arey) { item }.ToArray();
        }
        public T Pop()
        {
            //problematic child
            if (IsEmpty()) throw new InvalidOperationException("Stack is Empty");
            else
            {
                var tempint = Arey.Length;
                var element = Arey.Last();
                Array.Resize(ref Arey, tempint - 1);
                //Arey = Arey.Where(o => !Equals(o, Arey.Length)).ToArray();
                return element;
            }
        }
        public void Clear()
        {
            Array.Clear(Arey, 0, Arey.Length);
            Array.Resize(ref Arey, 0);
            Newsize = 0;
        }
        public T Peek()
        {
            //L I F O
            if (IsEmpty())
            {
                throw new InvalidOperationException("Stack is Empty");
            }
            else
            {
                var tmp = Arey.Length;
                return Arey[tmp - 1];
            }
        }
        public bool IsEmpty()
        {
            //my array will not have negative indexer
            if (Arey.Length > 0)
            {
                return false;
            }
            else
            {
                var value = default(T);
                int pos = Array.IndexOf(Arey, value);
                if (pos > -1)
                {
                    // meaning the array has a value, but is @ index 0
                    return false;
                }
                return true;
            }
        }
        public int Count => Arey.Length;    //**MAKE SURE THAT THE MAXIMUM INDEX ON AN ARRAY LIST IS LESS THAN THE LIST SIZE**

        public void Print()
        {
            foreach (var VARIABLE in Arey)
            {
                Console.Write("" + VARIABLE);
            }
        }

        public string strength()
        {
            foreach (var c in Arey)
            {
                boy1 += c.ToString();
            }
            return boy1;
        }
    }
}
