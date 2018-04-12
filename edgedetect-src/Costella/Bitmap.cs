// (c) 2011 John P. Costella.
using System; using System.Drawing; using System.Drawing.Imaging;
namespace Costella
{
    public static class BitmapUtils
    {
        public static Int2 Dimensions(this Bitmap b) {return new Int2(b.Width, b.Height);}
        public static void ForAll(this Bitmap b, Action<Int2> a) {for (var j = 0; j < b.Height; j++) for (var i = 0; i < b.Width; i++) a(new Int2(i, j));}
        public static Color Get(this Bitmap b, int i, int j) {return b.GetPixel(i, j);}
        public static Color Get(this Bitmap b, Int2 i) {return b.Get(i.I, i.J);}
        public static void Set(this Bitmap b, int i, int j, Color c) {b.SetPixel(i, j, c);}
        public static void Set(this Bitmap b, Int2 i, Color c) {b.Set(i.I, i.J, c);}
        public static void SmartSave(this Bitmap b, string f) {var g = f.ToLower(); b.Save(f, g.EndsWith(".jpg") ? ImageFormat.Jpeg : g.EndsWith(".bmp") ? ImageFormat.Bmp : ImageFormat.Png);}
    }
}
