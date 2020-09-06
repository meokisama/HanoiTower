using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower.Core.Models
{
    public interface IStack<T>
    {
        T Peek();
        T Pop();
        void Push(T item);
        void Clear();
        bool IsEmpty(); //cant make it property because idk the shortcut =>
        int Count { get; } //property
        //exercise
        void Print();
    }
}
