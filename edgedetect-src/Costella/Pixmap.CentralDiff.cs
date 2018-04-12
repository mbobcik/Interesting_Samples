// (c) 2011 John P. Costella.
using System;
namespace Costella
{
    public static class PixmapCentralDiff
    {
        public static Pixmap<double> CentralDiffX(this Pixmap<double> p) {return CentralDiff(p, new Int2(1, 0));}
        public static Pixmap<double> CentralDiffY(this Pixmap<double> p) {return CentralDiff(p, new Int2(0, 1));}
        public static Pixmap<double> CentralDiff(this Pixmap<double> p, Int2 d) {var r = new Pixmap<double>(p.Dimensions - 2 * d); r.ForAll(i => r[i] = 0.5 * (p[i + 2 * d] - p[i])); return r;}
    }
}
