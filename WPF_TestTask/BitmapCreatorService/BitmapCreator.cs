using System.Drawing;

namespace BitmapCreatorService;

public class BitmapCreator
{
    internal static readonly int startOX = 10;
    internal static readonly int startOY = 510;
    internal static readonly int bitmapSize = 510;

    public Bitmap Create(int divisionsOX, int divisionsOY)
    {
        Bitmap bitmap = new(bitmapSize, bitmapSize);
        Graphics gfx = Graphics.FromImage(bitmap);

        var pen = new Pen(Color.Black, 3);
        gfx.DrawRectangle(pen, 200, 220, 200, 200);

        var axisCreator = new AxisCreator();
        axisCreator.DrawAxis(bitmap, divisionsOX, divisionsOY);

        return bitmap;
    }
}
