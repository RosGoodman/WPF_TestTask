using System.Drawing;
using System.Drawing.Drawing2D;

namespace BitmapCreatorService;

internal class AxisCreator
{
    internal void DrawAxis(Bitmap bitmap, int divisionsOX, int divisionsOY)
    {
        Graphics gfx = Graphics.FromImage(bitmap);
        var pen = new Pen(Color.Black, 3);

        DrawOX(gfx, pen, divisionsOX);
        DrawOY(gfx, pen, divisionsOY);
        

        RectangleF rectf = new RectangleF(70, 90, 90, 50);

        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
        gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
        gfx.DrawString("yourText", new Font("Tahoma", 8), Brushes.Black, rectf);

        gfx.Flush();
    }

    private void DrawOX(Graphics gfx, Pen pen, int divisionsOX)
    {
        //draw OX
        gfx.DrawLine(pen, BitmapCreator.startOX, BitmapCreator.bitmapSize, BitmapCreator.bitmapSize, BitmapCreator.bitmapSize);

        int step = BitmapCreator.bitmapSize / divisionsOX;
        int distance = 0;

        for (int i = 1; i < divisionsOX + 2; i++)
        {
            gfx.DrawLine(pen, distance, BitmapCreator.bitmapSize, distance, BitmapCreator.bitmapSize - 10);
            distance += step;
        }
    }

    private void DrawOY(Graphics gfx, Pen pen, int divisionsOY)
    {
        //draw OY
        gfx.DrawLine(pen, BitmapCreator.startOX, BitmapCreator.startOY, 10, 10);

        int step = BitmapCreator.bitmapSize / divisionsOY;
        int distance = BitmapCreator.bitmapSize;

        for (int i = 1; i < divisionsOY + 2; i++)
        {
            gfx.DrawLine(pen, 0, distance, 10, distance);
            distance -= step;
        }
    }
}
