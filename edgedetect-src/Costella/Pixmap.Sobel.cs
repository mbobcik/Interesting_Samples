// (c) 2011 John P. Costella.
using System;
namespace Costella
{
    public static class PixmapSobel
    {
        public static Pixmap<double> SobelX(this Pixmap<double> p) {return p.BlurY().CentralDiffX();}
        public static Pixmap<double> SobelY(this Pixmap<double> p) {return p.BlurX().CentralDiffY();}
        public static Tuple<Pixmap<double>, Pixmap<double>> SobelXY(this Pixmap<double> p) {return new Tuple<Pixmap<double>, Pixmap<double>>(p.SobelX(), p.SobelY());}
        public static Pixmap<double> SobelMagnitude(this Pixmap<double> p) {return p.SobelXY().CartesianToPolarR();}
        public static Pixmap<double> SobelAngle(this Pixmap<double> p) {return p.SobelXY().CartesianToPolarTheta();}
        public static Tuple<Pixmap<double>, Pixmap<double>> SobelMagnitudeAngle(this Pixmap<double> p) {return p.SobelXY().CartesianToPolar();}
        static Pixmap<double> BlurX(this Pixmap<double> p) {return Blur(p, new Int2(1, 0));}
        static Pixmap<double> BlurY(this Pixmap<double> p) {return Blur(p, new Int2(0, 1));}
        static Pixmap<double> Blur(this Pixmap<double> p, Int2 d) {var r = new Pixmap<double>(p.Dimensions - 2 * d); r.ForAll(i => r[i] = (p[i + 2 * d] + p[i] + 2 * p[i + d]) / 4); return r;}
    }
}
