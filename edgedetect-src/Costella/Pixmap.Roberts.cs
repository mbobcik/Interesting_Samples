// (c) 2011 John P. Costella.
using System;
namespace Costella
{
    public static class PixmapRoberts
    {
        public static Pixmap<double> RobertsX(this Pixmap<double> p) {var r = new Pixmap<double>(p.Width - 1, p.Height - 1); var s = Math.Sqrt(0.5); r.ForAll(i => r[i] = (p[i.I + 1, i.J + 1] - p[i]) * s); return r;}
        public static Pixmap<double> RobertsY(this Pixmap<double> p) {var r = new Pixmap<double>(p.Width - 1, p.Height - 1); var s = Math.Sqrt(0.5); r.ForAll(i => r[i] = (p[i.I, i.J + 1] - p[i.I + 1, i.J]) * s); return r;}
        public static Tuple<Pixmap<double>, Pixmap<double>> RobertsXY(this Pixmap<double> p) {return new Tuple<Pixmap<double>, Pixmap<double>>(p.RobertsX(), p.RobertsY());}
        public static Pixmap<double> RobertsMagnitude(this Pixmap<double> p) {return p.RobertsXY().CartesianToPolarR();}
        public static Pixmap<double> RobertsAngle(this Pixmap<double> p) {return p.RobertsXY().CartesianToPolarTheta().Rotate();}
        public static Tuple<Pixmap<double>, Pixmap<double>> RobertsMagnitudeAngle(this Pixmap<double> p) {return p.RobertsXY().CartesianToPolar();}
        static Pixmap<double> Rotate(this Pixmap<double> p) {var r = new Pixmap<double>(p.Dimensions); var c = Math.PI * 2; var d = c / 8; r.ForAll(i => {r[i] -= d; if (r[i] <= -Math.PI) r[i] += c;}); return r;}
    }
}
