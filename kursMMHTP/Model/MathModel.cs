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
        /*
         * названия переменных взяты из физических формул их изменение было бы не рентабельным.
         * Vf - объём суспензии в фильтре
         * S - поверхность фильтрования, м2;
         * x - объемная доля твердой фазы в суспензии (от 0 до 1)
         * hoc - высоту слоя осадка (см)
         * dP - разность давлений, н• м-2;
         * mu - вязкость жидкой фазы суспензии, н•сек• м-2;
         * Rfp - сопротивление фильтровальной перегородки, м-1
         * Roc - сопротивление слоя осадка, м-1
         * r0 - удельное объёмное сопротивление осадка (сопротивление, оказываемое потоку фильтрата равномерным слоем осадка толщиной 1 м), м-2.
         * Gout - объёмный расход суспензии через фильтрующую перегородку, м3/c; 
         * t - время фильтрования
         * Vc - объём фильтрата в сборнике
         * PointsVf - точки для граффика Vf
         * PointsVc - точки для граффика Vc
         * PointsHoc - точки для граффика Hoc
         * Vfout - объем суспензии(остатка) в фильтре после фильтрации
         */
        public double Vf;
        public double S;
        public double x;
        public double hoc;
        public double dP;
        public double mu;
        public double Rfp;
        public double Roc;
        public double r0;
        public double Gout;
        public double t;
        public double Vc;
        public List<DataPoint> PointsVf = new List<DataPoint>();
        public List<DataPoint> PointsVc = new List<DataPoint>();
        public List<DataPoint> PointsHoc = new List<DataPoint>();
        public double Vfout;
        #endregion

        public MathModel(double Vf, double S, double x, double dP, double mu, double Rfp, double r0)
        {
            this.Vf = Vf;
            this.S = S;
            this.x = x;
            this.dP = dP;
            this.mu = mu;
            this.Rfp = Rfp * 10000000000;
            this.r0 = r0 * 1000000000000;
            Calculation();
        }

        //основные вычисления
        private void Calculation()
        {

            hoc = HocCalc();
            Roc = RocCalc();
            Gout = CalcGout();
            t = CalcT();

            Vc = CalcVc();

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

            //перевод высоты остатка в метрическую систему
            hoc = hoc * 100;

            Vfout = CalcVf();

        }
        
        //рассчет объема осадка
        private Double CalcVc()
        {
            return Gout * S * t;
        }

        //рассчет объема фильтрата
        private Double CalcVf()
        {
            return hoc * S;
        }

        //рассчет температуры
        private Double CalcT()
        {
            return Vf / (S * Gout);
        }

        //рассчет высоты осадка
        private Double HocCalc()
        {
            return (x * Vf) / S;
        }

        //рассчет сопротивление слоя осадка
        private Double RocCalc()
        {
            return r0 * hoc;
        }

        //рассчет расхода суспензии
        private Double CalcGout()
        {
            return dP / (mu * (Rfp + Roc));
        }
    }
}
