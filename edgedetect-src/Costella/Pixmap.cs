// (c) 2006-2011 John P. Costella.
using System; using System.Threading.Tasks;
namespace Costella
{
    public partial class Pixmap<T>
    {
        public Pixmap(int w, int h) {this.w = w; this.h = h; d = new T[h][]; Parallel.For(0, h, j => d[j] = new T[w]);} T[][] d;
        public Pixmap(Int2 d): this(d.I, d.J) {}
        public Pixmap(Pixmap<T> other): this(other.Dimensions) {ForAll(i => this[i] = other[i]);}
        public T this[int i, int j] {get {return d[j][i];} set {d[j][i] = value;}}
        public T this[Int2 i] {get {return this[i.I, i.J];} set {this[i.I, i.J] = value;}}
        public int Width {get {return w;}} int w;
        public int Height {get {return h;}} int h;
        public Int2 Dimensions {get {return new Int2(Width, Height);}}
        public Pixmap<T> Transpose() {var r = new Pixmap<T>(Dimensions.Transpose()); ForAll(i => r[i.Transpose()] = this[i]); return r;}
        public Pixmap<U> ToPixmap<U>(Func<T, U> c) {var r = new Pixmap<U>(Dimensions); ForAll(i => r[i] = c(this[i])); return r;}
        public void ForAll(Action<Int2> a) {d.ForAll(j => d[j].SerialForAll(i => a(new Int2(i, j))));}
        public void SerialForAll(Action<Int2> a) {d.SerialForAll(j => d[j].SerialForAll(i => a(new Int2(i, j))));}
        public U ForAll<U>(Func<U> n, Func<Int2, U> m, Func<U, U, U> r) {return d.ForAll<U>(n, j => d[j].SerialForAll<U>(n, i => m(new Int2(i, j)), r), r);}
        public U SerialForAll<U>(Func<U> n, Func<Int2, U> m, Func<U, U, U> r) {return d.SerialForAll<U>(n, j => d[j].SerialForAll<U>(n, i => m(new Int2(i, j)), r), r);}
    }
}
