// (c) 2011 John P. Costella.
using System;
namespace Costella
{
    public static class PixmapScharr
    {
        public static Pixmap<double> ScharrX(this Pixmap<double> p) {return p.BlurY().CentralDiffX();}
        public static Pixmap<double> ScharrY(this Pixmap<double> p) {return p.BlurX().CentralDiffY();}
        public static Tuple<Pixmap<double>, Pixmap<double>> ScharrXY(this Pixmap<double> p) {return new Tuple<Pixmap<double>, Pixmap<double>>(p.ScharrX(), p.ScharrY());}
        public static Pixmap<double> ScharrMagnitude(this Pixmap<double> p) {return p.ScharrXY().CartesianToPolarR();}
        public static Pixmap<double> ScharrAngle(this Pixmap<double> p) {return p.ScharrXY().CartesianToPolarTheta();}
        public static Tuple<Pixmap<double>, Pixmap<double>> ScharrMagnitudeAngle(this Pixmap<double> p) {return p.ScharrXY().CartesianToPolar();}
        static Pixmap<double> BlurX(this Pixmap<double> p) {return Blur(p, new Int2(1, 0));}
        static Pixmap<double> BlurY(this Pixmap<double> p) {return Blur(p, new Int2(0, 1));}
        static Pixmap<double> Blur(this Pixmap<double> p, Int2 d) {var r = new Pixmap<double>(p.Dimensions - 2 * d); r.ForAll(i => r[i] = (3 * (p[i + 2 * d] + p[i]) + 10 * p[i + d]) / 16); return r;}
    }
}
