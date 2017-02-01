using kursMMHTP.Helpers;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kursMMHTP.ViewModel
{
    class MainWindowModel : INotifyPropertyChanged
    {
        #region singletone
        private static MainWindowModel instance;

        private MainWindowModel()
        {
            Vf = 0.001;
            x = 0.001;
            Mu = 0.001;
            S = 0.001;
            dP = 400;
            r0 = 0.001;
            Rfp = 0.001;
        }

        public static MainWindowModel getInsance()
        {
            if (instance == null)
            {
                instance = new MainWindowModel();
            }
            return instance;
        }
        #endregion

        #region ClickCalculate
        private ICommand _clickCalculateCommand;
        public ICommand ClickCalculateCommand
        {
            get
            {
                return _clickCalculateCommand ?? (_clickCalculateCommand = new CommandHandler(() => ClickCalculateAction(), true));
            }
        }

        public void ClickCalculateAction()
        {
            new kursMMHTP.Model.MathModel();
        }
        #endregion

        //названия переменных взяты из физических формул.
        #region data from user
        //объём суспензии в фильтре
        private Double _Vf;
        public Double Vf
        {
            get { return _Vf; }
            set
            {
                _Vf = value;
                RaisePropertyChanged("Vf");
            }
        }

        //объемная доля твердой фазы в суспензии (от 0 до 1)
        private Double _x;
        public Double x
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChanged("x");
            }
        }

        //вязкость жидкой фазы суспензии, н•сек• м-2;
        private Double _Mu;
        public Double Mu
        {
            get { return _Mu; }
            set
            {
                _Mu = value;
                RaisePropertyChanged("Mu");
            }
        }

        //поверхность фильтрования, м2;
        private Double _S;
        public Double S
        {
            get { return _S; }
            set
            {
                _S = value;
                RaisePropertyChanged("S");
            }
        }
        //разность давлений, н• м-2;
        private int _dP;
        public int dP
        {
            get { return _dP; }
            set
            {
                _dP = value;
                RaisePropertyChanged("dP");
            }
        }
        //удельное объёмное сопротивление осадка (сопротивление, оказываемое потоку фильтрата равномерным слоем осадка толщиной 1 м), м-2.
         private Double _r0;
        public Double r0
        {
            get { return _r0; }
            set
            {
                _r0 = value;
                RaisePropertyChanged("r0");
            }
        }
        //сопротивление фильтровальной перегородки, м-1
        private Double _Rfp;
        public Double Rfp
        {
            get { return _Rfp; }
            set
            {
                _Rfp = value;
                RaisePropertyChanged("Rfp");
            }
        }
        #endregion

        #region Data after calc
        
        //объём фильтрата в сборнике
        private Double _Vc;
        public Double Vc
        {
            get { return _Vc; }
            set
            {
                _Vc = value;
                RaisePropertyChanged("Vc");
            }
        }
        //высоту слоя осадка (см)
        private Double _hoc;
        public Double hoc
        {
            get { return _hoc; }
            set
            {
                _hoc = value;
                RaisePropertyChanged("hoc");
            }
        }
        //время фильтрования
        private Double _t;
        public Double t
        {
            get { return _t; }
            set
            {
                _t = value;
                RaisePropertyChanged("t");
            }
        }
        //точки для граффика зависимости Vf от t
        private List<DataPoint> _pointsVf;
        public List<DataPoint> PointsVf
        {
            get { return _pointsVf; }
            set
            {
                _pointsVf = value;
                RaisePropertyChanged("PointsVf");
            }
        }
        //точки для граффика зависимости Vc от t
        private List<DataPoint> _pointsVc;
        public List<DataPoint> PointsVc
        {
            get { return _pointsVc; }
            set
            {
                _pointsVc = value;
                RaisePropertyChanged("PointsVc");
            }
        }
        //точки для граффика зависимости hoc от t
        private List<DataPoint> _pointsHoc;
        public List<DataPoint> PointsHoc
        {
            get { return _pointsHoc; }
            set
            {
                _pointsHoc = value;
                RaisePropertyChanged("PointsHoc");
            }
        }
        //объем слоя осадка в фильтре
        private Double _Vfout;
        public Double Vfout
        {
            get { return _Vfout; }
            set {
                _Vfout = value;
                RaisePropertyChanged("Vfout");
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
