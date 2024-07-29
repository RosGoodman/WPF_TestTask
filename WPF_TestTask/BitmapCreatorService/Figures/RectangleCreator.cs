using System.Drawing;

namespace BitmapCreatorService.Figures;

internal static class RectangleCreator
{
    internal static void DrowRectangle(Bitmap bitmap, float ox, float oy, float width, float height)
    {
        Graphics gfx = Graphics.FromImage(bitmap);
        var pen = new Pen(Color.Red, 3);
        gfx.DrawRectangle(pen, ox, oy, width, height);
    }
}
