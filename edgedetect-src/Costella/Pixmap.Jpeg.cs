// (c) 2005-2011 John P. Costella.
using System; using System.Drawing; 
namespace Costella
{
    public static class PixmapJpeg
    {
        public static Pixmap<byte>[] ToAycc(this Pixmap<Color> p)
        {
            var r = new Pixmap<byte>[4]; var d = p.Dimensions; var e = (d + 1) >> 1;
            var a = r[0] = new Pixmap<byte>(d); var y = r[1] = new Pixmap<byte>(d); var cb = r[2] = new Pixmap<byte>(e); var cr = r[3] = new Pixmap<byte>(e);
            p.ForAll(i => {var c = p[i].ToAycc(); a[i] = c.A; y[i] = c.R; if ((i.I & 1) == 0 && (i.J & 1) == 0) {var j = i / 2; cb[j] = c.G; cr[j] = c.B;}});
            return r;
        }
        public static Pixmap<Color> ToArgb(this Pixmap<byte>[] ps)
        {
            var a = ps[0]; var y = ps[1]; var cb = ps[2].Upsample(); var cr = ps[3].Upsample(); var r = new Pixmap<Color>(a.Dimensions);
            a.ForAll(i => r[i] = Color.FromArgb(a[i], y[i], cb[i], cr[i]).FromAycc());
            return r;
        }
        public static Pixmap<Color> FixChrominance(this Pixmap<Color> p) {return p.ToAycc().ToArgb();}
        public static Bitmap FixChrominance(this Bitmap b) {return b.ToPixmap().FixChrominance().ToBitmap();}
        public static Color ToAycc(this Color c) {return Color.FromArgb(c.A, (19595 * c.R + 38470 * c.G + 7471 * c.B + 32768) >> 16, (-11058 * c.R - 21709 * c.G+ 32767 * c.B + 8421376) >> 16, (32767 * c.R - 27438 * c.G - 5329 * c.B + 8421376) >> 16);}
        public static Color FromAycc(this Color c) {return Color.FromArgb(c.A, L(c.R + ((91885 * c.B - 11728512) >> 16)), L(c.R + ((8910464 - 22554 * c.G - 46803 * c.B) >> 16)), L(c.R + ((116134 * c.G - 14832384) >> 16)));}
        static int L(int i) {return i < 0 ? 0 : i > 255 ? 255 : i;}
    }
}
