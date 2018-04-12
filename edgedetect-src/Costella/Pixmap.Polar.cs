// (c) 2011 John P. Costella.
using System;
namespace Costella
{
    public static class PixmapPolar
    {
        public static Pixmap<double> CartesianToPolarR(Pixmap<double> x, Pixmap<double> y) {var m = new Pixmap<double>(x.Dimensions); m.ForAll(i => m[i] = R(x[i], y[i])); return m;}
        public static Pixmap<double> CartesianToPolarR(this Tuple<Pixmap<double>, Pixmap<double>> xy) {return CartesianToPolarR(xy.Item1, xy.Item2);}
        public static Pixmap<double> CartesianToPolarTheta(Pixmap<double> x, Pixmap<double> y) {var a = new Pixmap<double>(x.Dimensions); a.ForAll(i => a[i] = Theta(x[i], y[i])); return a;}
        public static Pixmap<double> CartesianToPolarTheta(this Tuple<Pixmap<double>, Pixmap<double>> xy) {return CartesianToPolarTheta(xy.Item1, xy.Item2);}
        public static Tuple<Pixmap<double>, Pixmap<double>> CartesianToPolar(Pixmap<double> x, Pixmap<double> y) {return new Tuple<Pixmap<double>, Pixmap<double>>(CartesianToPolarR(x, y), CartesianToPolarTheta(x, y));}
        public static Tuple<Pixmap<double>, Pixmap<double>> CartesianToPolar(this Tuple<Pixmap<double>, Pixmap<double>> xy) {return CartesianToPolar(xy.Item1, xy.Item2);}
        static double R(double x, double y) {return Math.Sqrt(x * x + y * y);}
        static double Theta(double x, double y) {return Math.Atan2(y, x);}
    }
}
