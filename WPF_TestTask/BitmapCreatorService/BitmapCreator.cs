using System.Drawing;

namespace BitmapCreatorService;

public class BitmapCreator
{
    internal static readonly int startOX = 20;
    internal static readonly int endOX = 520;

    internal static readonly int offsetOX = 20;
    internal static readonly int offsetOY = 20;

    internal static readonly int startOY = 500 + offsetOY;
    internal static readonly int endOY = 20 + offsetOY;
    internal static readonly int bitmapSize = 540;
    

    public Bitmap Create(int divisionsOX, int divisionsOY)
    {
        Bitmap bitmap = new(bitmapSize, bitmapSize);
        Graphics gfx = Graphics.FromImage(bitmap);

        var pen = new Pen(Color.Black, 3);
        gfx.DrawRectangle(pen, 200, 220, 200, 200);

        AxisCreator.DrawAxis(bitmap, divisionsOX, divisionsOY);

        return bitmap;
    }
}
