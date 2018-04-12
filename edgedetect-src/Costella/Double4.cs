// (c) 2010-2011 John P. Costella.
using System;
namespace Costella
{
    public struct Double4
    {
        public Double4(double x, double y, double z, double t) {this.x = x; this.y = y; this.z = z; this.t = t;}
        public Double4(Double4 other): this(other.X, other.Y, other.Z, other.T) {}
        public double X {get {return x;}} double x;
        public double Y {get {return y;}} double y;
        public double Z {get {return z;}} double z;
        public double T {get {return t;}} double t;
        public double this[int d] {get {return d == 0 ? X : d == 1 ? Y : d == 2 ? Z : T;}}
        public override bool Equals(object o) {var other = (Double4)o; return this.x == other.x && this.y == other.y && this.z == other.z && this.t == other.t;}
        public override int GetHashCode() {return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ t.GetHashCode();}
        public static bool operator==(Double4 a, Double4 b) {return a.Equals(b);}
        public static bool operator!=(Double4 a, Double4 b) {return !(a == b);}
        public static Double4 operator+(Double4 a, Double4 b) {return new Double4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.T + b.T);}
        public static Double4 operator-(Double4 a, Double4 b) {return new Double4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.T - b.T);}
        public static Double4 operator*(Double4 a, double b) {return new Double4(a.X * b, a.Y * b, a.Z * b, a.T * b);}
        public static Double4 operator*(double a, Double4 b) {return b * a;}
        public static Double4 operator/(Double4 a, double b) {return new Double4(a.X / b, a.Y / b, a.Z / b, a.T / b);}
        public override string ToString() {return "(" + x + ", " + y + ", " + z + ", " + t + ")";}
    }
}
