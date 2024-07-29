using BitmapCreatorService.Figures;
using System.Drawing;

namespace BitmapCreatorService
{
    public interface IBitmapCreator
    {
        public Bitmap GetBitmap();

        /// <summary>
        /// Нарисовать оси координат с указанным кол-вом делений.
        /// </summary>
        /// <param name="divisionsOX"> Кол-во делений по оси ОХ. </param>
        /// <param name="divisionsOY"> Кол-во делений по оси ОУ. </param>
        /// <returns> Bitmap. </returns>
        IBitmapCreator CreateAxis(int divisionsOX, int divisionsOY);

        /// <summary>
        /// Добавить фигуру указанных размеров на битмап.
        /// </summary>
        /// <param name="figure"> Выбранная фигура. </param>
        /// <param name="ox"> Начальная точка фигуры по оси ОХ. </param>
        /// <param name="oy"> Начальная точка фигуры по оси ОУ. </param>
        /// <param name="width"> Ширина фигуры. </param>
        /// <param name="height"> Высота фигуры. </param>
        IBitmapCreator DrawFigure(FiguresEnum figure, float ox, float oy, float width, float height);
    }
}