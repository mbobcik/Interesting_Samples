// (c) 2011 John P. Costella.
using System;
namespace Costella
{
    public static class PixmapGradient
    {
        public static Pixmap<double> GradientX(this Pixmap<double> p) {return p.DiffX().Upsample().TrimY();}
        public static Pixmap<double> GradientY(this Pixmap<double> p) {return p.DiffY().Upsample().TrimX();}
        public static Tuple<Pixmap<double>, Pixmap<double>> GradientXY(this Pixmap<double> p) {return new Tuple<Pixmap<double>, Pixmap<double>>(p.GradientX(), p.GradientY());}
        public static Pixmap<double> GradientMagnitude(this Pixmap<double> p) {return p.GradientXY().CartesianToPolarR();}
        public static Pixmap<double> GradientAngle(this Pixmap<double> p) {return p.GradientXY().CartesianToPolarTheta();}
        public static Tuple<Pixmap<double>, Pixmap<double>> GradientMagnitudeAngle(this Pixmap<double> p) {return p.GradientXY().CartesianToPolar();}
        public static Pixmap<double> Trim(this Pixmap<double> p) {return p.TrimX().TrimY();}
        public static Pixmap<double> TrimX(this Pixmap<double> p) {return Trim(p, new Int2(1, 0));}
        public static Pixmap<double> TrimY(this Pixmap<double> p) {return Trim(p, new Int2(0, 1));}
        public static Pixmap<double> Trim(this Pixmap<double> p, Int2 d) {var r = new Pixmap<double>(p.Dimensions - 2 * d); r.ForAll(i => r[i] = p[i + d]); return r;}
        static Pixmap<double> DiffX(this Pixmap<double> p) {return Diff(p, new Int2(1, 0));}
        static Pixmap<double> DiffY(this Pixmap<double> p) {return Diff(p, new Int2(0, 1));}
        static Pixmap<double> Diff(this Pixmap<double> p, Int2 d) {var r = new Pixmap<double>(p.Dimensions - d); r.ForAll(i => r[i] = p[i + d] - p[i]); return r;}
    }
}
