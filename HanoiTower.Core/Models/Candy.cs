using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HanoiTower.Core.Models
{
    public class Candy : ObservableObject
    {
        private int _number;
        private ObservableCollection<Sticks> _start;
        private ObservableCollection<Sticks> _middle;
        private ObservableCollection<Sticks> _end;

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                RaisePropertyChanged(nameof(Number));
            }
        }

        public ObservableCollection<Sticks> start
        {
            get { return _start; }
            set
            {
                _start = value;
                RaisePropertyChanged(nameof(start));

            }
        }

        public ObservableCollection<Sticks> middle
        {
            get { return _middle; }
            set
            {
                _middle = value;
                RaisePropertyChanged(nameof(middle));

            }
        }

        public ObservableCollection<Sticks> end
        {
            get { return _end; }
            set
            {
                _end = value;
                RaisePropertyChanged(nameof(end));

            }
        }

        public Candy(int number, ObservableCollection<Sticks> start, ObservableCollection<Sticks> middle, ObservableCollection<Sticks> end)
        {
            _number = number;
            _start = start;
            _middle = middle;
            _end = end;
        }
    }
}
