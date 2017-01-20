using kursMMHTP.Helpers;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kursMMHTP.ViewModel
{
    class MainWindowModel : DependencyObject
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

        #region data from user
        //объём суспензии в фильтре
        public Double Vf
        {
            get { return (Double)GetValue(VfProperty); }
            set { SetValue(VfProperty, value); }
        }

        //объемная доля твердой фазы в суспензии (от 0 до 1)
        public Double x
        {
            get { return (Double)GetValue(xProperty); }
            set { SetValue(xProperty, value); }
        }

        //вязкость жидкой фазы суспензии, н•сек• м-2;
        public Double Mu
        {
            get { return (Double)GetValue(MuProperty); }
            set { SetValue(MuProperty, value); }
        }

        //поверхность фильтрования, м2;
        public Double S
        {
            get { return (Double)GetValue(SProperty); }
            set { SetValue(SProperty, value); }
        }
        //разность давлений, н• м-2;
        public int dP
        {
            get { return (int)GetValue(dPProperty); }
            set { SetValue(dPProperty, value); }
        }
        //удельное объёмное сопротивление осадка (сопротивление, оказываемое потоку фильтрата равномерным слоем осадка толщиной 1 м), м-2.
        public Double r0
        {
            get { return (Double)GetValue(r0Property); }
            set { SetValue(r0Property, value); }
        }
        //сопротивление фильтровальной перегородки, м-1
        public Double Rfp
        {
            get { return (Double)GetValue(RfpProperty); }
            set { SetValue(RfpProperty, value); }
        }
        #endregion

        #region Data after calc
        //объём фильтрата в сборнике
        public Double Vc
        {
            get { return (Double)GetValue(VcProperty); }
            set { SetValue(VcProperty, value); }
        }
        //высоту слоя осадка (см)
        public Double hoc
        {
            get { return (Double)GetValue(hocProperty); }
            set { SetValue(hocProperty, value); }
        }
        //время фильтрования
        public Double t
        {
            get { return (Double)GetValue(tProperty); }
            set { SetValue(tProperty, value); }
        }
        //точки для граффика зависимости Vf от t
        public List<DataPoint> PointsVf
        {
            get { return (List<DataPoint>)GetValue(PointsVfProperty); }
            set { SetValue(PointsVfProperty, value); }
        }
        //точки для граффика зависимости Vc от t
        public List<DataPoint> PointsVc
        {
            get { return (List<DataPoint>)GetValue(PointsVcProperty); }
            set { SetValue(PointsVcProperty, value); }
        }
        //точки для граффика зависимости hoc от t
        public List<DataPoint> PointsHoc
        {
            get { return (List<DataPoint>)GetValue(PointsHocProperty); }
            set { SetValue(PointsHocProperty, value); }
        }
        //объем слоя осадка в фильтре
        public Double Vfout
        {
            get { return (Double)GetValue(VfoutProperty); }
            set { SetValue(VfoutProperty, value); }
        }

        #endregion

        #region propdp

        // Using a DependencyProperty as the backing store for Vf.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VfoutProperty =
            DependencyProperty.Register("Vfout", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for PointsVf.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsHocProperty =
            DependencyProperty.Register("PointsHoc", typeof(List<DataPoint>), typeof(MainWindowModel));


        // Using a DependencyProperty as the backing store for PointsVf.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsVcProperty =
            DependencyProperty.Register("PointsVc", typeof(List<DataPoint>), typeof(MainWindowModel));


        // Using a DependencyProperty as the backing store for PointsVf.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsVfProperty =
            DependencyProperty.Register("PointsVf", typeof(List<DataPoint>), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for t.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty tProperty =
            DependencyProperty.Register("t", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for hoc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty hocProperty =
            DependencyProperty.Register("hoc", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for Vc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VcProperty =
            DependencyProperty.Register("Vc", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for Rfp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RfpProperty =
            DependencyProperty.Register("Rfp", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for r0.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty r0Property =
            DependencyProperty.Register("r0", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for dP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty dPProperty =
            DependencyProperty.Register("dP", typeof(int), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for S.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SProperty =
            DependencyProperty.Register("S", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for Mu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MuProperty =
            DependencyProperty.Register("Mu", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for x.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xProperty =
            DependencyProperty.Register("x", typeof(Double), typeof(MainWindowModel));

        // Using a DependencyProperty as the backing store for Vf.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VfProperty =
            DependencyProperty.Register("Vf", typeof(Double), typeof(MainWindowModel));

        #endregion 
 

         
    }
}
