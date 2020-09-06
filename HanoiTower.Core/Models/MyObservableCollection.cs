using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower.Core.Models
{
    public class MyObservableCollection : ObservableCollection<Sticks>
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

            }
        }
        public MyObservableCollection(string name)
        {
            _name = name;
        }

    }
}
