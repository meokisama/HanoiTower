using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HanoiTower.Core.Models
{
    public class Instructions : ObservableObject
    {
        public ObservableCollection<Sticks> firstpeg { get; } = new ObservableCollection<Sticks>();
        public ObservableCollection<Sticks> secondpeg { get; } = new ObservableCollection<Sticks>();
        public ObservableCollection<Sticks> thirdpeg { get; } = new ObservableCollection<Sticks>();
        public string Disknumber
        {
            get { return _disknumber; }
            set
            {
                _disknumber = value;
                RaisePropertyChanged(nameof(Disknumber));
            }
        }
        private Queue<Sticks> Disk = new Queue<Sticks>();
        private Queue<string> words = new Queue<string>();
        private Queue<ObservableCollection<Sticks>> firstCollection = new Queue<ObservableCollection<Sticks>>();
        private Queue<ObservableCollection<Sticks>> secondCollection = new Queue<ObservableCollection<Sticks>>();
        private string _disknumber;
        private bool _test;
        public bool Test
        {
            get { return _test; }
            set
            {
                _test = value;
                RaisePropertyChanged(nameof(Test));
            }
        }

        private int necker = 0;
        public Queue<String> MoveStep(int n, MyObservableCollection beginning, MyObservableCollection end, MyObservableCollection aux)
        {
            necker = 0;
            if (n > 0)
            {
                MoveStep(n - 1, beginning, aux, end);
                var b = beginning.ElementAtOrDefault(0);
                end.Insert(0, b);
                beginning.RemoveAt(0);
                string z = "Chuyển đĩa " + n + " từ " + beginning.Name + " sang " + end.Name;
                words.Enqueue(z);
                MoveStep(n - 1, aux, end, beginning);
            }

            necker = (int)(Math.Pow(2, n) - 1);
            return words;
        }

        public void Step(int n, ObservableCollection<Sticks> beginning, ObservableCollection<Sticks> end,
            ObservableCollection<Sticks> aux)
        {
            if (necker != 0)
            {
                var firstColl = firstCollection.Dequeue();
                var secondColl = secondCollection.Dequeue();
                var theDisk = Disk.Dequeue();

                if (secondColl.GetHashCode() == beginning.GetHashCode() && firstColl.GetHashCode() == end.GetHashCode())
                {
                    var b = beginning.ElementAtOrDefault(0);
                    end.Insert(0, theDisk);
                    beginning.RemoveAt(0);
                }
                if (secondColl.GetType().Name == end.GetType().Name && firstColl.GetType().Name == beginning.GetType().Name)
                {
                    var b = end.ElementAtOrDefault(0);
                    beginning.Insert(0, theDisk);
                    end.RemoveAt(0);
                }
                if (secondColl == beginning && firstColl == aux)
                {
                    var b = beginning.ElementAtOrDefault(0);
                    aux.Insert(0, theDisk);
                    beginning.RemoveAt(0);
                }
                if (secondColl == aux && firstColl == beginning)
                {
                    var b = aux.ElementAtOrDefault(0);
                    end.Insert(0, theDisk);
                    aux.RemoveAt(0);
                }
                if (secondColl == aux && firstColl == end)
                {
                    var b = aux.ElementAtOrDefault(0);
                    end.Insert(0, theDisk);
                    aux.RemoveAt(0);
                }
                if (secondColl == end && firstColl == aux)
                {
                    var b = end.ElementAtOrDefault(0);
                    aux.Insert(0, theDisk);
                    end.RemoveAt(0);
                }
                necker--;
            }
        }

        //public void TowersIterative(int number, ObservableCollection<Sticks> beginning, ObservableCollection<Sticks> end,
        //    ObservableCollection<Sticks> aux, int index, int i)
        //{
        //    var a = ngek(number, beginning, end, aux, index, i);
        //    beginning = a.Item1;
        //    end = a.Item2;
        //    aux = a.Item3;
        //}
        //public static Tuple<ObservableCollection<Sticks>,ObservableCollection<Sticks>,ObservableCollection<Sticks>> ngek(int number, ObservableCollection<Sticks> beginning, ObservableCollection<Sticks> end, ObservableCollection<Sticks> aux,int index,int i )
        //{
        //    int aa = (int)Math.Pow(2, number) - 1;
        //    if(index != 0)
        //    { 
        //        if (i % number == 1)
        //        {
        //            var b = beginning.ElementAtOrDefault(0);
        //            end.Insert(0, b);
        //            beginning.RemoveAt(0);
        //        }
        //        if (i % number == 2)
        //        {
        //            var b = beginning.ElementAtOrDefault(0);
        //            aux.Insert(0, b);
        //            beginning.RemoveAt(0);
        //        }
        //        if (i % number == 0)
        //        {
        //            var b = aux.ElementAtOrDefault(0);
        //            end.Insert(0, b);
        //            beginning.RemoveAt(0);
        //        }
        //    }
        //    return new Tuple<ObservableCollection<Sticks>, ObservableCollection<Sticks>, ObservableCollection<Sticks>>(beginning, end, aux);

        //}
    }
}
