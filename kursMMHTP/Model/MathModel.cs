using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursMMHTP.Model
{
    class MathModel
    {
        #region var
        ViewModel.MainWindowModel MainViewModel = ViewModel.MainWindowModel.getInsance();

        //объём суспензии в фильтре
        private double Vf;
        //поверхность фильтрования, м2;
        private double S;
        //объемная доля твердой фазы в суспензии (от 0 до 1)
        private double x;
        //высоту слоя осадка (см)
        private double hoc;
        //разность давлений, н• м-2;
        private double dP;
        //вязкость жидкой фазы суспензии, н•сек• м-2;
        private double mu;
        //сопротивление фильтровальной перегородки, м-1
        private double Rfp;
        //сопротивление слоя осадка, м-1
        private double Roc;
        //удельное объёмное сопротивление осадка (сопротивление, оказываемое потоку фильтрата равномерным слоем осадка толщиной 1 м), м-2.
        private double r0;
        //объёмный расход суспензии через фильтрующую перегородку, м3/c; 
        private double Gout;
        //время фильтрования
        private double t;
        //объём фильтрата в сборнике
        private double Vc;
        //точки для граффика Vf
        private List<DataPoint> PointsVf = new List<DataPoint>();
        //точки для граффика Vf
        private List<DataPoint> PointsVc = new List<DataPoint>();
        //точки для граффика Vf
        private List<DataPoint> PointsHoc = new List<DataPoint>();
        #endregion

        public MathModel()
        {
            Vf = MainViewModel.Vf;
            S = MainViewModel.S;
            x = MainViewModel.x;
            dP = MainViewModel.dP;
            mu = MainViewModel.Mu;
            Rfp = MainViewModel.Rfp * 10000000000;
            r0 = MainViewModel.r0 * 1000000000000;
            Calculation();
        }

        private void Calculation()
        {

            hoc = HocCalc();
            Roc = RocCalc();
            Gout = CalcGout();
            t = CalcT();

            Vc = CalcVc();
            MainViewModel.Vc = Vc;
            MainViewModel.t = t;

            //рассчет данных для графика
            for (double i = 0.1; i < t && i < 10000; i++)
            {
                double graphVc = Gout * i * S;
                double graphVf = Vf - graphVc;
                double graphHoc = (x * graphVc) / S;
                PointsVf.Add(new DataPoint(i, graphVf));
                PointsVc.Add(new DataPoint(i, graphVc));
                PointsHoc.Add(new DataPoint(i, graphHoc));
            }

            MainViewModel.PointsVf = PointsVf;
            MainViewModel.PointsVc = PointsVc;
            MainViewModel.PointsHoc = PointsHoc;
            MainViewModel.hoc = hoc * 100;
            MainViewModel.Vfout= CalcVf();

        }

        private Double CalcVc()
        {
            return Gout * S * t;
        }

        private Double CalcVf()
        {
            return hoc * S;
        }

        private Double CalcT()
        {
            return Vf / (S * Gout);
        }

        private Double HocCalc()
        {
            return (x * Vf) / S;
        }

        private Double RocCalc()
        {
            return r0 * hoc;
        }

        private Double CalcGout()
        {
            return dP / (mu * (Rfp + Roc));
        }
    }
}
