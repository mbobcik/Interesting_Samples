// (c) 2011 John P. Costella.
using System; using System.Drawing; using System.IO; using Costella;
class ImageGradientMagnitude
{
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length != 2) throw new Exception("Syntax: ImageGradientMagnitude <infile> <outfile>");
            if (!File.Exists(args[0])) throw new Exception("File '" + args[0] + "' does not exist");
            new Bitmap(args[0]).ToPixmap().ToAycc()[1].ToPixmap<double>(z => z).GradientMagnitude().ToPixmap<byte>(z => 2 * z > 255 ? (byte)255 : (byte)Math.Round(2 * z)).ToBitmap().Save(args[1]);
        }
        catch (Exception e) {Console.WriteLine(e);}
    }
}
