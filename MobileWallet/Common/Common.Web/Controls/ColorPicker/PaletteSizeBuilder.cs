
namespace Common.Web.Controls.ColorPicker
{
    public class PaletteSizeBuilder
    {
        private readonly ColorPaletteTileSize _tileSize;

        public PaletteSizeBuilder(ColorPaletteTileSize tileSize)
        {
            _tileSize = tileSize;
        }

        public PaletteSizeBuilder Width(int width)
        {
            _tileSize.Width = width;
            return this;
        }
        public PaletteSizeBuilder Height(int height)
        {
            _tileSize.Height = height;
            return this;
        }

    }
}
