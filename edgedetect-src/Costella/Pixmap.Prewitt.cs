// (c) 2011 John P. Costella.
using System;
namespace Costella
{
    public static class PixmapPrewitt
    {
        public static Pixmap<double> PrewittX(this Pixmap<double> p) {return p.BlurY().CentralDiffX();}
        public static Pixmap<double> PrewittY(this Pixmap<double> p) {return p.BlurX().CentralDiffY();}
        public static Tuple<Pixmap<double>, Pixmap<double>> PrewittXY(this Pixmap<double> p) {return new Tuple<Pixmap<double>, Pixmap<double>>(p.PrewittX(), p.PrewittY());}
        public static Pixmap<double> PrewittMagnitude(this Pixmap<double> p) {return p.PrewittXY().CartesianToPolarR();}
        public static Pixmap<double> PrewittAngle(this Pixmap<double> p) {return p.PrewittXY().CartesianToPolarTheta();}
        public static Tuple<Pixmap<double>, Pixmap<double>> PrewittMagnitudeAngle(this Pixmap<double> p) {return p.PrewittXY().CartesianToPolar();}
        static Pixmap<double> BlurX(this Pixmap<double> p) {return Blur(p, new Int2(1, 0));}
        static Pixmap<double> BlurY(this Pixmap<double> p) {return Blur(p, new Int2(0, 1));}
        static Pixmap<double> Blur(this Pixmap<double> p, Int2 d) {var r = new Pixmap<double>(p.Dimensions - 2 * d); r.ForAll(i => r[i] = (p[i + 2 * d] + p[i] + p[i + d]) / 3); return r;}
    }
}
