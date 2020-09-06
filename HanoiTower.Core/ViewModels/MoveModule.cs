using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HanoiTower.Core.Models;

namespace HanoiTower.Core.ViewModels
{
    public class MoveModule : ObservableObject
    {
        public MoveModule()
        {
            SliderBool = true;
            LimiterForAuto = true;
            pegname = "A";
            pegend = "C";
            NumberOfDisks = 3;
            DelayMilliSecond = 500;
        }
        private int _numberOfDisks;
        private int _delayMilliSecond;
        private int _delaySecond;
        private bool _stopper;
        private bool _step;
        private bool _onother;

        public int DelayMilliSecond
        {
            get { return _delayMilliSecond; }
            set
            {
                _delayMilliSecond = value;
                RaisePropertyChanged(nameof(DelayMilliSecond));
                DelaySecond = DelayMilliSecond / 1000;

            }
        }
        public int NumberOfDisks
        {
            get { return _numberOfDisks; }
            set
            {
                _numberOfDisks = value;
                RaisePropertyChanged(nameof(NumberOfDisks));
                Generate(NumberOfDisks);


            }
        }
        public int DelaySecond
        {
            get { return _delaySecond; }
            set
            {
                _delaySecond = value;
                RaisePropertyChanged(nameof(DelaySecond));
            }
        }
        public string pegname
        {
            get { return _pegname; }
            set
            {
                _pegname = value;
                RaisePropertyChanged(nameof(pegname));
            }
        }
        public string pegend
        {
            get { return _pegend; }
            set
            {
                _pegend = value;
                RaisePropertyChanged(nameof(pegend));
            }
        }
        public string Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                RaisePropertyChanged(nameof(Steps));
            }
        }
        public bool StepByStepFlag
        {
            get { return _stepByStepFlag; }
            set
            {
                _stepByStepFlag = value;
                RaisePropertyChanged(nameof(StepByStepFlag));
            }
        }
        public bool SliderBool
        {
            get { return _sliderBool; }
            set
            {
                _sliderBool = value;
                RaisePropertyChanged(nameof(SliderBool));
            }
        }
        public bool LimiterForAuto
        {
            get { return _limiterForAuto; }
            set
            {
                _limiterForAuto = value;
                RaisePropertyChanged(nameof(LimiterForAuto));
            }
        }

        private string z = "";
        private string y = "";
        private int asa = 1;
        public Instructions hoy { get; } = new Instructions();
        public MyObservableCollection first { get; } = new MyObservableCollection("Cột A");
        public MyObservableCollection second { get; } = new MyObservableCollection("Cột B");
        public MyObservableCollection third { get; } = new MyObservableCollection("Cột C");
        public MyObservableCollection ALPHAfirst { get; } = new MyObservableCollection("Cột A");
        public MyObservableCollection ALPHAsecond { get; } = new MyObservableCollection("Cột B");
        public MyObservableCollection ALPHAthird { get; } = new MyObservableCollection("Cột C");
        public MyObservableCollection BETAfirst { get; } = new MyObservableCollection("Cột A");
        public MyObservableCollection BETAsecond { get; } = new MyObservableCollection("Cột B");
        public MyObservableCollection BETAthird { get; } = new MyObservableCollection("Cột C");
        public ObservableCollection<String> Procedure { get; } = new ObservableCollection<String>();
        public ObservableCollection<String> BackProcedure { get; } = new ObservableCollection<String>();
        public void Generate(int number)
        {
            ObservableCollection<Sticks> tempo = new ObservableCollection<Sticks>();
            ObservableCollection<Sticks> alphatempo = new ObservableCollection<Sticks>();
            ObservableCollection<Sticks> betatempo = new ObservableCollection<Sticks>();
            first.Clear();
            third.Clear();
            second.Clear();
            ALPHAfirst.Clear();
            ALPHAsecond.Clear();
            ALPHAthird.Clear();
            BETAfirst.Clear();
            BETAsecond.Clear();
            BETAthird.Clear();
            Procedure.Clear();
            BackProcedure.Clear();
            Steps = "";
            if (pegname == "A" || pegname == "a")
            {
                pegname = "A";
                tempo = first;
                alphatempo = ALPHAfirst;
                betatempo = BETAfirst;

            }
            if (pegname == "B" || pegname == "b")
            {
                pegname = "B";

                tempo = second;
                alphatempo = ALPHAsecond;
                betatempo = BETAsecond;

            }
            if (pegname == "C" || pegname == "c")
            {
                pegname = "C";

                tempo = third;
                alphatempo = ALPHAthird;
                betatempo = BETAthird;

            }
            for (int i = number; i > 0; i--)
            {
                var b = new Sticks(i.ToString());
                tempo.Insert(0, new Sticks(i.ToString()));
                alphatempo.Insert(0, new Sticks(i.ToString()));
                betatempo.Insert(0, new Sticks(i.ToString()));
            }
            StepByStepFlag = false;

        }
        private Queue<String> hoqy = new Queue<string>();
        private string _pegname;
        private string _pegend;
        private bool _stepByStepFlag;
        private string _steps;
        private bool _sliderBool;
        private bool _limiterForAuto;

        public async Task Move(int n, MyObservableCollection beginning, MyObservableCollection end, MyObservableCollection aux)
        {

            if (n > 0)
            {
                await Move(n - 1, beginning, aux, end);
                var b = beginning.ElementAtOrDefault(0);
                end.Insert(0, b);
                beginning.RemoveAt(0);
                string z = "Chuyển đĩa " + n + " từ " + beginning.Name + " sang " + end.Name;
                Steps = z;
                await Task.Delay(DelayMilliSecond);
                await Move(n - 1, aux, end, beginning);
            }
        }
        public ICommand MoveCommand => new RelayCommand(MoveCommandProc, MoveCommandCondition);
        private async void MoveCommandProc()
        {
            LimiterForAuto = false;

            SliderBool = false;
            Queue<string> girl = new Queue<string>();
            if (pegend == "C" || pegend == "c")
            {
                pegend = "C";
            }
            if (pegend == "B" || pegend == "b")
            {
                pegend = "B";
            }
            if (pegend == "A" || pegend == "a")
            {
                pegend = "A";
            }
            Generate(NumberOfDisks);
            await Task.Delay(400);

            if (pegname == "A" && pegend == "B")
            {
                girl = hoy.MoveStep(NumberOfDisks, first, second, third);
                while (girl.Count != 0)
                {
                    Procedure.Add(girl.Dequeue());
                }
                await Move(NumberOfDisks, ALPHAfirst, ALPHAsecond, ALPHAthird);
            }
            if (pegname == "B" && pegend == "A")
            {
                girl = hoy.MoveStep(NumberOfDisks, second, first, third);
                while (girl.Count != 0)
                {
                    Procedure.Add(girl.Dequeue());
                }
                await Move(NumberOfDisks, ALPHAsecond, ALPHAfirst, ALPHAthird);
            }
            if (pegname == "A" && pegend == "C")
            {
                girl = hoy.MoveStep(NumberOfDisks, first, third, second);
                while (girl.Count != 0)
                {
                    Procedure.Add(girl.Dequeue());
                }
                await Move(NumberOfDisks, ALPHAfirst, ALPHAthird, ALPHAsecond);

            }
            if (pegname == "C" && pegend == "A")
            {
                girl = hoy.MoveStep(NumberOfDisks, third, first, second);
                while (girl.Count != 0)
                {
                    Procedure.Add(girl.Dequeue());
                }
                await Move(NumberOfDisks, ALPHAthird, ALPHAfirst, ALPHAsecond);
            }
            if (pegname == "B" && pegend == "C")
            {
                girl = hoy.MoveStep(NumberOfDisks, second, third, first);
                while (girl.Count != 0)
                {
                    Procedure.Add(girl.Dequeue());
                }
                await Move(NumberOfDisks, ALPHAsecond, ALPHAthird, ALPHAfirst);
            }
            if (pegname == "C" && pegend == "B")
            {
                girl = hoy.MoveStep(NumberOfDisks, third, second, first);
                while (girl.Count != 0)
                {
                    Procedure.Add(girl.Dequeue());
                }
                await Move(NumberOfDisks, ALPHAthird, ALPHAsecond, ALPHAfirst);
            }
            SliderBool = true;
            LimiterForAuto = true;

        }
        private bool MoveCommandCondition()
        {
            return NumberOfDisks != 0;
        }
        public ICommand StepByStepCommand => new RelayCommand(StepByStepCommandProc);


        private void StepByStepCommandProc()
        {
            if (StepByStepFlag == false)
            {
                Generate(NumberOfDisks);
                Queue<string> girl = new Queue<string>();
                if (pegend == "C" || pegend == "c")
                {
                    pegend = "C";
                }
                if (pegend == "B" || pegend == "b")
                {
                    pegend = "B";
                }
                if (pegend == "A" || pegend == "a")
                {
                    pegend = "A";
                }
                if (pegname == "A" && pegend == "B")
                {
                    girl = hoy.MoveStep(NumberOfDisks, BETAfirst, BETAsecond, BETAthird);
                    while (girl.Count != 0)
                    {
                        BackProcedure.Add(girl.Dequeue());
                    }
                }
                if (pegname == "B" && pegend == "A")
                {
                    girl = hoy.MoveStep(NumberOfDisks, BETAsecond, BETAfirst, BETAthird);
                    while (girl.Count != 0)
                    {
                        BackProcedure.Add(girl.Dequeue());
                    }
                }
                if (pegname == "A" && pegend == "C")
                {
                    girl = hoy.MoveStep(NumberOfDisks, BETAfirst, BETAthird, BETAsecond);
                    while (girl.Count != 0)
                    {
                        BackProcedure.Add(girl.Dequeue());
                    }

                }
                if (pegname == "C" && pegend == "A")
                {
                    girl = hoy.MoveStep(NumberOfDisks, BETAthird, BETAfirst, BETAsecond);
                    while (girl.Count != 0)
                    {
                        BackProcedure.Add(girl.Dequeue());
                    }
                }
                if (pegname == "B" && pegend == "C")
                {
                    girl = hoy.MoveStep(NumberOfDisks, BETAsecond, BETAthird, BETAfirst);
                    while (girl.Count != 0)
                    {
                        BackProcedure.Add(girl.Dequeue());
                    }
                }
                if (pegname == "C" && pegend == "B")
                {
                    girl = hoy.MoveStep(NumberOfDisks, BETAthird, BETAsecond, BETAfirst);
                    while (girl.Count != 0)
                    {
                        BackProcedure.Add(girl.Dequeue());
                    }
                }

                string zxc = BackProcedure.First();
                BackProcedure.Remove(BackProcedure.First());
                string[] checker = zxc.Split(' '); //641 2 + 53 *
                if (checker[5] == "A" && checker[8] == "C")
                {
                    var b = ALPHAfirst.ElementAtOrDefault(0);
                    ALPHAthird.Insert(0, b);
                    ALPHAfirst.RemoveAt(0);
                    Procedure.Add(zxc);
                    Steps = zxc;
                }
                if (checker[5] == "C" && checker[8] == "A")
                {
                    var b = ALPHAthird.ElementAtOrDefault(0);
                    ALPHAfirst.Insert(0, b);
                    ALPHAthird.RemoveAt(0);
                    Procedure.Add(zxc);
                    Steps = zxc;
                }
                if (checker[5] == "A" && checker[8] == "B")
                {
                    var b = ALPHAfirst.ElementAtOrDefault(0);
                    ALPHAsecond.Insert(0, b);
                    ALPHAfirst.RemoveAt(0);
                    Procedure.Add(zxc);
                    Steps = zxc;
                }
                if (checker[5] == "B" && checker[8] == "A")
                {
                    var b = ALPHAsecond.ElementAtOrDefault(0);
                    ALPHAfirst.Insert(0, b);
                    ALPHAsecond.RemoveAt(0);
                    Procedure.Add(zxc);
                    Steps = zxc;
                }
                if (checker[5] == "B" && checker[8] == "C")
                {
                    var b = ALPHAsecond.ElementAtOrDefault(0);
                    ALPHAthird.Insert(0, b);
                    ALPHAsecond.RemoveAt(0);
                    Procedure.Add(zxc);
                    Steps = zxc;
                }
                if (checker[5] == "C" && checker[8] == "B")
                {
                    var b = ALPHAthird.ElementAtOrDefault(0);
                    ALPHAsecond.Insert(0, b);
                    ALPHAthird.RemoveAt(0);
                    Procedure.Add(zxc);
                    Steps = zxc;
                }

                StepByStepFlag = true;
                return;
            }
            if (StepByStepFlag)
            {
                if (BackProcedure.Count != 0)
                {
                    string zxc = BackProcedure.First();
                    BackProcedure.Remove(BackProcedure.First());
                    string[] checker = zxc.Split(' '); //641 2 + 53 *
                    if (checker[5] == "A" && checker[8] == "C")
                    {
                        var b = ALPHAfirst.ElementAtOrDefault(0);
                        ALPHAthird.Insert(0, b);
                        ALPHAfirst.RemoveAt(0);
                        Procedure.Add(zxc);
                        Steps = zxc;
                    }
                    if (checker[5] == "C" && checker[8] == "A")
                    {
                        var b = ALPHAthird.ElementAtOrDefault(0);
                        ALPHAfirst.Insert(0, b);
                        ALPHAthird.RemoveAt(0);
                        Procedure.Add(zxc);
                        Steps = zxc;
                    }
                    if (checker[5] == "A" && checker[8] == "B")
                    {
                        var b = ALPHAfirst.ElementAtOrDefault(0);
                        ALPHAsecond.Insert(0, b);
                        ALPHAfirst.RemoveAt(0);
                        Procedure.Add(zxc);
                        Steps = zxc;
                    }
                    if (checker[5] == "B" && checker[8] == "A")
                    {
                        var b = ALPHAsecond.ElementAtOrDefault(0);
                        ALPHAfirst.Insert(0, b);
                        ALPHAsecond.RemoveAt(0);
                        Procedure.Add(zxc);
                        Steps = zxc;
                    }
                    if (checker[5] == "B" && checker[8] == "C")
                    {
                        var b = ALPHAsecond.ElementAtOrDefault(0);
                        ALPHAthird.Insert(0, b);
                        ALPHAsecond.RemoveAt(0);
                        Procedure.Add(zxc);
                        Steps = zxc;
                    }
                    if (checker[5] == "C" && checker[8] == "B")
                    {
                        var b = ALPHAthird.ElementAtOrDefault(0);
                        ALPHAsecond.Insert(0, b);
                        ALPHAthird.RemoveAt(0);
                        Procedure.Add(zxc);
                        Steps = zxc;
                    }

                }
                else return;
            }
        }
    }
}
