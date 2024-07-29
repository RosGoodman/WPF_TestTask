using BitmapCreatorService.Figures;
using System.Drawing;

namespace BitmapCreatorService;

public class BitmapCreator : IBitmapCreator
{
    internal static readonly int startOX = 20;
    internal static readonly int endOX = 520;

    internal static readonly int offsetOX = 20;
    internal static readonly int offsetOY = 20;

    internal static readonly int startOY = 500 + offsetOY;
    internal static readonly int endOY = 20 + offsetOY;
    internal static readonly int bitmapSize = 540;

    private Bitmap _bitmap = null!;
    private int _divisionsOX = 1;
    private int _divisionsOY = 1;

    public Bitmap GetBitmap() => _bitmap;

    /// <inheritdoc/>
    public IBitmapCreator CreateAxis(int divisionsOX, int divisionsOY)
    {
        _divisionsOX = divisionsOX;
        _divisionsOY = divisionsOY;

        _bitmap = new(bitmapSize, bitmapSize);
        Graphics gfx = Graphics.FromImage(_bitmap);

        AxisCreator.DrawAxis(_bitmap, divisionsOX, divisionsOY);

        return this;
    }

    /// <inheritdoc/>
    public IBitmapCreator DrawFigure(FiguresEnum figure, float ox, float oy, float width, float height)
    {
        var stepOX = GetStepOX();
        var stepOY = GetStepOY();

        var ox1 = ox * stepOX + startOX;
        var oy1 = startOY - (oy + height) * stepOY;
        var ox2 = width * stepOX;
        var oy2 = height * stepOY;

        if (height == 0)
            oy2 = 1;

        if (width == 0)
            ox2 = 1;

        //тут можно будет, при необходимости добавить другие фигуры
        if (figure == FiguresEnum.Rectangle)
            RectangleCreator.DrowRectangle(_bitmap, ox1, oy1, ox2, oy2);

        return this;
    }

    private float GetStepOX() => (endOX - startOX) / _divisionsOX;
    private float GetStepOY() => (startOY - offsetOY) / _divisionsOY;
}
