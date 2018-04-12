// (c) 2006-2011 John P. Costella.
using System; using System.Threading.Tasks;
namespace Costella
{
    public partial class Pixmap<T>
    {
        public Pixmap<T> Upsample() {return UpsampleX().UpsampleY();}
        public Pixmap<T> Downsample() {return DownsampleX().DownsampleY();}
        public Pixmap<T> Resample(double f) {return ResampleX(f).ResampleY(f);}
        public Pixmap<T> Resample(double xf, double yf) {return ResampleX(xf).ResampleY(yf);}
        public Pixmap<T> UpsampleX() {var r = new Pixmap<T>(2 * Width, Height); Parallel.For(0, Height, j => new UpDownX(this, r, j, true)); return r;}
        public Pixmap<T> UpsampleY() {var r = new Pixmap<T>(Width, 2 * Height); Parallel.For(0, Width, i => new UpDownY(this, r, i, true)); return r;}
        public Pixmap<T> DownsampleX() {var r = new Pixmap<T>((Width + 1) / 2, Height); Parallel.For(0, Height, j => new UpDownX(this, r, j, false)); return r;}
        public Pixmap<T> DownsampleY() {var r = new Pixmap<T>(Width, (Height + 1) / 2); Parallel.For(0, Width, i => new UpDownY(this, r, i, false)); return r;}
        public Pixmap<T> ResampleX(double f) {var r = new Pixmap<T>((int)Math.Ceiling(f * Width), Height); Parallel.For(0, Height, j => new ResampleableX(this, r, f, j)); return r;}
        public Pixmap<T> ResampleY(double f) {var r = new Pixmap<T>(Width, (int)Math.Ceiling(f * Height)); Parallel.For(0, Width, i => new ResampleableY(this, r, f, i)); return r;}
        abstract class Base: Magic.IUpDown
        {
            public abstract int DestLength {get;}
            public Pixmap<T> src, dest; public int max;
            public int Limit(int n) {return n < 0 ? 0 : n > max ? max : n;}
        }
        abstract class UpDownBase: Base, Magic<T>.IAccessible
        {
            public abstract T this[int n] {get; set;}
        }
        abstract class Resampleable: Base, Magic<T>.IFull
        {
            public abstract T this[int n] {get; set;}
            public double SrcOrigin {get {return 0;}} public double SrcSpacing {get {return f;}} protected double f;
            public double DestOrigin {get {return 0;}} public double DestSpacing {get {return 1;}}
        }
        class UpDownX: UpDownBase, Magic<T>.IUpDown
        {
            public UpDownX(Pixmap<T> src, Pixmap<T> dest, int j, bool up) {this.src = src; this.dest = dest; this.j = j; max = src.Width - 1; if (up) this.Upsample<T>(); else this.Downsample<T>();} int j;
            public override int DestLength {get {return dest.Width;}}
            public override T this[int i] {get {return src[Limit(i), j];} set {dest[i, j] = value;}}
        }
        class UpDownY: UpDownBase, Magic<T>.IUpDown
        {
            public UpDownY(Pixmap<T> p, Pixmap<T> d, int i, bool up) {this.src = p; this.dest = d; this.i = i; max = p.Height - 1; if (up) this.Upsample<T>(); else this.Downsample<T>();} int i;
            public override int DestLength {get {return dest.Height;}}
            public override T this[int j] {get {return src[i, Limit(j)];} set {dest[i, j] = value;}}
        }
        class ResampleableX: Resampleable
        {
            public ResampleableX(Pixmap<T> p, Pixmap<T> d, double f, int j) {this.src = p; this.dest = d; this.f = f; this.j = j; max = p.Width - 1; this.Resample<T>();} int j;
            public override int DestLength {get {return dest.Width;}}
            public override T this[int i] {get {return src[Limit(i), j];} set {dest[i, j] = value;}}
        }
        class ResampleableY: Resampleable
        {
            public ResampleableY(Pixmap<T> p, Pixmap<T> d, double f, int i) {this.src = p; this.dest = d; this.f = f; this.i = i; max = p.Height - 1; this.Resample<T>();} int i;
            public override int DestLength {get {return dest.Height;}}
            public override T this[int j] {get {return src[i, Limit(j)];} set {dest[i, j] = value;}}
        }
    }
}
