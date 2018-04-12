// (c) 2006-2011 John P. Costella.
using System; using System.Drawing; 
namespace Costella
{
    public static partial class Pixmap
    {
        public static Pixmap<Color> ToPixmap(this Bitmap b) {var p = new Pixmap<Color>(b.Dimensions()); b.ForAll(i => p[i] = b.Get(i)); return p;}
        public static Pixmap<Double4> ToDPixmap(this Bitmap b) {var p = new Pixmap<Double4>(b.Dimensions()); b.ForAll(i => p[i] = CD(b.Get(i))); return p;}
        public static Bitmap ToBitmap(this Pixmap<byte> p) {var b = new Bitmap(p.Width, p.Height); b.ForAll(i => b.Set(i, G(p[i]))); return b;}
        public static Bitmap ToBitmap(this Pixmap<double> p) {return p.ToBitmap(p.Min(), p.Max());}
        public static Bitmap ToBitmap(this Pixmap<double> p, double min, double max) {var s = 255.0 / (max - min); var b = new Bitmap(p.Width, p.Height); b.ForAll(i => b.Set(i, G((byte)DI((p[i] - min) * s)))); return b;} 
        public static Bitmap ToBitmap(this Pixmap<Color> p) {var b = new Bitmap(p.Width, p.Height); b.ForAll(i => b.Set(i, p[i])); return b;}
        public static Bitmap ToBitmap(this Pixmap<Double4> p) {var b = new Bitmap(p.Width, p.Height); b.ForAll(i => b.Set(i, DC(p[i]))); return b;}
        public static double Min(this Pixmap<double> p) {return p.ForAll(() => Double.MaxValue, i => p[i], (a, b) => Math.Min(a, b));}
        public static double Max(this Pixmap<double> p) {return p.ForAll(() => Double.MinValue, i => p[i], (a, b) => Math.Max(a, b));}
        static Color G(byte b) {return Color.FromArgb(b, b, b);}
        static Color DC(Double4 d) {return Color.FromArgb(DI(d.X), DI(d.Y), DI(d.Z), DI(d.T));}
        static Double4 CD(Color c) {return new Double4(c.A, c.R, c.G, c.B);}
        static int DI(double d) {return d < 0 ? 0 : d > 255 ? 255 : (int)Math.Round(d);}
    }
}
