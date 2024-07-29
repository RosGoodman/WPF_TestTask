using System.Drawing;
using System.Drawing.Drawing2D;

namespace BitmapCreatorService;

internal static class AxisCreator
{
    internal static void DrawAxis(Bitmap bitmap, int divisionsOX, int divisionsOY)
    {
        Graphics gfx = Graphics.FromImage(bitmap);
        var pen = new Pen(Color.Black, 3);

        DrawOX(gfx, pen, divisionsOX);
        DrawOY(gfx, pen, divisionsOY);
    }

    private static void DrawOX(Graphics gfx, Pen pen, int divisionsOX)
    {
        //draw OX
        gfx.DrawLine(pen, BitmapCreator.startOX, BitmapCreator.startOY, BitmapCreator.endOX, BitmapCreator.startOY);

        int step = (BitmapCreator.endOX - BitmapCreator.startOX) / divisionsOX;
        int distance = BitmapCreator.offsetOX + step;

        for (int i = 1; i < divisionsOX + 1; i++)
        {
            gfx.DrawLine(pen, distance, BitmapCreator.startOY, distance, BitmapCreator.startOY - 10);

            DrawNumb(gfx, i.ToString(), distance - 10, BitmapCreator.bitmapSize - 15);

            distance += step;
        }
    }

    private static void DrawOY(Graphics gfx, Pen pen, int divisionsOY)
    {
        //draw OY
        gfx.DrawLine(pen, BitmapCreator.startOX, BitmapCreator.startOY, BitmapCreator.offsetOX, BitmapCreator.offsetOY);

        int step = (BitmapCreator.startOY - BitmapCreator.offsetOY) / divisionsOY;
        int distance = BitmapCreator.startOY - step;

        for (int i = 1; i < divisionsOY + 1; i++)
        {
            gfx.DrawLine(pen, BitmapCreator.offsetOX, distance, BitmapCreator.offsetOX + 10, distance);

            DrawNumb(gfx, i.ToString(), 0, distance - 5);

            distance -= step;
        }
    }

    private static void DrawNumb(Graphics gfx, string text, float ox, float oy)
    {
        RectangleF rectf = new RectangleF(ox, oy, 20, 20);

        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
        gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
        gfx.DrawString(text, new Font("Verdana", 8), Brushes.Black, rectf);

        gfx.Flush();
    }
}
