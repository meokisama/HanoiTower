using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace HanoiTower.Core.Models
{
    public class Sticks : ObservableObject
    {
        private string _name;
        private int _size;
        private SolidColorBrush _color;
        private int _sizeborder;
        private static readonly Random getrandom = new Random();

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public int size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged(nameof(size));
            }
        }

        public int sizeborder
        {
            get { return _sizeborder; }
            set
            {
                _sizeborder = value;
                RaisePropertyChanged(nameof(sizeborder));
            }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged(nameof(Color));
            }
        }

        public Sticks(string name)
        {
            var number = int.Parse(name);
            var oi = GetUniqueRandomColor(number);
            _name = name;
            _size = int.Parse(name) * 40;
            _sizeborder = _size + 1;
            _color = new SolidColorBrush(oi);
        }
        public static Color GetUniqueRandomColor(int count)
        {
            HashSet<Color> hs = new HashSet<Color>();
            Color color;
            //while (!hs.Add(color = System.Windows.Media.Color.FromRgb((byte)getrandom.Next(70, 200), (byte)getrandom.Next(100, 225), (byte)getrandom.Next(100, 230)))) ;
           
            color = Colors.SpringGreen;
                
            return color;
        }

    }
}
