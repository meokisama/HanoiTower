using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower.Core.Models
{
    public class ObservableStack<T> : IEnumerable<T>, ICollection, INotifyCollectionChanged, INotifyPropertyChanged
    {
        T[] _array;
        const int _defaultCapacity = 4;
        static T[] _emptyArray;
        int _size;
        object _syncRoot;
        int _version;

        static ObservableStack()
        {
            _emptyArray = new T[0];
        }

        public ObservableStack()
        {
            _array = _emptyArray;
            _size = 0;
            _version = 0;
            _syncRoot = new object();
        }

        public ObservableStack(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            var is2 = collection as ICollection<T>;

            if (is2 != null)
            {
                int count = is2.Count;
                _array = new T[count];
                is2.CopyTo(_array, 0);
                _size = count;
            }
            else
            {
                _size = 0;
                _array = new T[4];
                using (var enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        this.Push(enumerator.Current);
                    }
                }
            }
        }

        public ObservableStack(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Invalid capacity.");

            _array = new T[capacity];
            _size = 0;
            _version = 0;
        }

        public void Push(T item)
        {
            if (_size == _array.Length)
            {
                T[] dest = new T[(_array.Length == 0) ? 4 : (2 * _array.Length)];
                Array.Copy(_array, 0, dest, 0, _size);
                _array = dest;
            }

            _array[_size++] = item;
            _version++;

            OnCollectionAdded(item);
            OnPropertyChanged("Count");
        }

        public T Pop()
        {
            if (_size == 0)
                throw new InvalidOperationException("Empty stack, can not pop.");

            _version++;
            T local = _array[--_size];
            _array[_size] = default(T);

            OnCollectionRemoved(local, _size);
            OnPropertyChanged("Count");

            return local;
        }

        public T Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException("Empty stack, can not peek.");

            return _array[_size - 1];
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnCollectionReset()
        {
            var c = CollectionChanged;

            if (c != null)
                c(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        void OnCollectionAdded(object item)
        {
            var c = CollectionChanged;

            if (c != null)
                c(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        void OnCollectionRemoved(object item, int index)
        {
            var c = CollectionChanged;

            if (c != null)
                c(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        void OnPropertyChanged(string propertyName)
        {
            var p = PropertyChanged;

            if (p != null)
                p(this, new PropertyChangedEventArgs(propertyName));
        }



        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Clear()
        {
            Array.Clear(_array, 0, _size);
            _size = 0;
            _version++;

            OnCollectionReset();
            OnPropertyChanged("Count");
        }

        public bool Contains(T item)
        {
            int index = _size;

            var comparer = EqualityComparer<T>.Default;

            while (index-- > 0)
            {
                if (item == null)
                {
                    if (_array[index] == null)
                        return true;
                }
                else if (_array[index] != null && comparer.Equals(_array[index], item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");

            if ((array.Length - arrayIndex) < _size)
                throw new ArgumentException();

            Array.Copy(_array, 0, array, arrayIndex, _size);
            Array.Reverse(array, arrayIndex, _size);
        }

        public int Count
        {
            get { return _size; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            Array.Copy(this._array, 0, array, index, this._size);
            Array.Reverse(array, index, this._size);
        }

        public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            ObservableStack<T> _stack;
            int _index;
            int _version;
            T currentElement;

            internal Enumerator(ObservableStack<T> stack)
            {
                _stack = stack;
                _version = _stack._version;
                _index = -2;
                currentElement = default(T);
            }

            public T Current
            {
                get
                {
                    if (_index < 0)
                        throw new InvalidOperationException();

                    return currentElement;
                }
            }

            public void Dispose()
            {
                _index = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_index < 0)
                        throw new InvalidOperationException();

                    return currentElement;
                }
            }

            public bool MoveNext()
            {
                bool flag;

                if (_version != _stack._version)
                    throw new InvalidOperationException("Version mismatch.");

                if (_index == -2)
                {
                    _index = _stack._size - 1;
                    flag = _index >= 0;

                    if (flag)
                        currentElement = _stack._array[_index];

                    return flag;
                }

                if (_index == -1)
                    return false;

                flag = --_index >= 0;

                if (flag)
                {
                    currentElement = _stack._array[_index];
                    return flag;
                }

                currentElement = default(T);
                return flag;
            }

            public void Reset()
            {
                if (_version != _stack._version)
                    throw new InvalidOperationException("Version mismatch.");

                _index = -2;
                currentElement = default(T);
            }
        }
    }
}
