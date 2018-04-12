// (c) 2010-2011 John P. Costella.
using System;
namespace Costella
{
    public partial struct Int2
    {
        public Int2(int i, int j) {this.i = i; this.j = j;}
        public Int2(Int2 other) : this(other.I, other.J) {}
        public int I {get {return i;}} int i;
        public int J {get {return j;}} int j;
        public int this[int d] {get {return d == 0 ? I : J;}}
        public Int2 Transpose() {return new Int2(J, I);}
        public override bool Equals(object o) {var other = (Int2)o; return this.i == other.i && this.j == other.j;}
        public override int GetHashCode() {return i.GetHashCode() ^ j.GetHashCode();}
        public static bool operator==(Int2 a, Int2 b) {return a.Equals(b);}
        public static bool operator!=(Int2 a, Int2 b) {return !(a == b);}
        public static Int2 operator+(Int2 a, Int2 b) {return new Int2(a.I + b.I, a.J + b.J);}
        public static Int2 operator+(Int2 a, int b) {return new Int2(a.I + b, a.J + b);}
        public static Int2 operator+(int a, Int2 b) {return b + a;}
        public static Int2 operator-(Int2 a, Int2 b) {return new Int2(a.I - b.I, a.J - b.J);}
        public static Int2 operator*(Int2 a, int b) {return new Int2(a.I * b, a.J * b);}
        public static Int2 operator*(int a, Int2 b) {return b * a;}
        public static Int2 operator/(Int2 a, int b) {return new Int2(a.I / b, a.J / b);}
        public static Int2 operator>>(Int2 a, int b) {return new Int2(a.I >> b, a.J >> b);}
        public int Dot(Int2 o) {return I * o.I + J * o.J;}
        public override string ToString() {return "(" + i + ", " + j + ")";}
    }
}
