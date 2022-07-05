using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DogrularlaDans
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rdn = new Random();
            double[,] locations = new double[2, 100];
            //TODO:1 ile 1000 arasında random sayılar iki boyutlu diziye atandı
            for (int i = 0; i <= locations.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= locations.GetUpperBound(1); j++)
                {
                    locations[i, j] = rdn.Next(1, 1000);
                }
            }
            for (int t = 2; t < locations.GetUpperBound(1) - 1; t++)
            {
                double Y1 = locations[1, 0];
                double Y2 = locations[1, 1];

                double Y3 = locations[1, t];
                double Y4 = locations[1, t + 1];

                double X1 = locations[0, 0];
                double X2 = locations[0, 1];

                double X3 = locations[0, t];
                double X4 = locations[0, t + 1];

                //TODO:Doğrular paralel yada çakışık değilse çakışma noktası koordinat hesabı
                double[] koor = DogrununKesisimNoktasıHesabi(Y1, Y2, Y3, Y4, X1, X2, X3, X4);

                double[] Yler1 = { Y1, Y2 };
                double[] Yler2 = { Y3, Y4 };

                double[] Xler1 = { X1, X2 };
                double[] Xler2 = { X3, X4 };

                if (((Yler1.Max() > koor[1] && Yler1.Min() < koor[1]) && (Yler2.Max() > koor[1] && Yler2.Min() < koor[1]))
                    && ((Xler1.Max() > koor[0] && Xler1.Min() < koor[0]) && (Xler2.Max() > koor[0] && Xler2.Min() < koor[0])))
                {
                    string text = string.Format("Y1={0} Y2={1} Y3={2} Y4={3} X1={4} X2={5} X3={6} X4={7} Y_Keşim={8} X_Keşim={9}\n", Y1, Y2, Y3, Y4, X1, X2, X3, X4, koor[1], koor[0]);
                    File.AppendAllText("D:\\kessimvar.txt", text);
                }
                else//sağlaması dosyaya yazılan koordinatlar üzeridne yapılabilir.
                {
                    string text = string.Format("Y1={0} Y2={1} Y3={2} Y4={3} X1={4} X2={5} X3={6} X4={7} Y_Keşim={8} X_Keşim={9}\n", Y1, Y2, Y3, Y4, X1, X2, X3, X4, koor[1], koor[0]);
                    File.AppendAllText("D:\\kessimyok.txt", text);
                }

            }

            //Thread.Sleep(3000);
        }

        public static double[] DogrununKesisimNoktasıHesabi(double Y1, double Y2, double Y3, double Y4, double X1, double X2, double X3, double X4)
        {
            double[] koor = new double[2];
            double Yfark = (Y2 - Y1);
            double Xfark = (X2 - X1);
            double egim = Yfark / Xfark;


            double Yfark2 = (Y4 - Y3);
            double Xfark2 = (X4 - X3);
            double egim2 = Yfark2 / Xfark2;


            if (egim == egim2)
            {
                koor[0] = 0;  //Paralel Doğrulardır Kesim Noktası Yoktur.
                koor[1] = 0;
                string text = string.Format("Y1={0} Y2={1} Y3={2} Y4={3} X1={4} X2={5} X3={6} X4={7} Y_Keşim={8} X_Keşim={9}\n", Y1, Y2, Y3, Y4, X1, X2, X3, X4, koor[1], koor[0]);
                File.AppendAllText("D:\\ParalelDogrular.txt", text);
                return koor;

            }
            else
            {
                double X = (Y1 - Y3 - (egim * X1) + (egim2 * X3)) / (egim2 - egim);
                double Y = (egim * X - egim * X1) + Y1;

                koor[0] = X;
                koor[1] = Y;

                return koor;
            }



        }
    }
}
