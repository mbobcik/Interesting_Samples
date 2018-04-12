// (c) 2011 John P. Costella.
using System; using System.Threading.Tasks;
namespace Costella
{
    public static partial class Extensions
    {
        public static void ForAll(this Array l, Action<int> a) {Parallel.For(0, l.Length, i => a(i));}
        public static void SerialForAll(this Array l, Action<int> a) {for (var i = 0; i < l.Length; i++) a(i);}
        public static T ForAll<T>(this Array l, Func<T> n, Func<int, T> m, Func<T, T, T> r) {var a = n(); var b = new object(); Parallel.For<T>(0, l.Length, n, (i, z, v) => r(m(i), v), w => {lock (b) a = r(a, w);}); return a;}
        public static T SerialForAll<T>(this Array l, Func<T> n, Func<int, T> m, Func<T, T, T> r) {var a = n(); for (var i = 0; i < l.Length; i++) a = r(m(i), a); return a;}
    }
}
