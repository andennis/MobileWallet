using System;
using System.Collections.Generic;

namespace Common.Web.Controls.ColorPicker
{
    public class ColorPickerBuilder : WidgetBuilderBase<ColorPicker, ColorPickerBuilder>
    {
        private ColorPickerEventBuilder _eventBuilder;
        private PaletteSizeBuilder _paletteSizeBuilder;
        private ColorPaletteTileSize _paletteTileSize;

        public ColorPickerBuilder(ColorPicker component)
            :base(component)
        {
        }

        private ColorPickerEventBuilder EventBuilder
        {
            get { return _eventBuilder ?? (_eventBuilder = new ColorPickerEventBuilder(_component.Events)); }
        }

        public PaletteSizeBuilder PaletteSizeBuilderType
        {
            get { return _paletteSizeBuilder ?? (_paletteSizeBuilder = new PaletteSizeBuilder(_paletteTileSize = new ColorPaletteTileSize())); }
        }

        public ColorPickerBuilder Events(Action<ColorPickerEventBuilder> clientEventsAction)
        {
            clientEventsAction(EventBuilder);
            return this;
            
        }

        public ColorPickerBuilder Value(string color)
        {
            _component.Value = color;
            return this;
        }

        public ColorPickerBuilder Opacity(bool allowOpacity)
        {
            _component.Opacity = allowOpacity;
            return this;
        }
        public ColorPickerBuilder Palette(IEnumerable<string> palette)
        {
            _component.PaletteColors = palette;
            return this;
        }
        public ColorPickerBuilder Palette(ColorPickerPalette palette)
        {
            _component.Palette = palette;
            return this;
        }
        public ColorPickerBuilder Enable(bool value)
        {
            _component.Enabled = value;
            return this;
        }
        public ColorPickerBuilder Buttons(bool value)
        {
            _component.Buttons = value;
            return this;
        }
        public ColorPickerBuilder ToolIcon(string cssClass)
        {
            _component.ToolIcon = cssClass;
            return this;
        }
        public ColorPickerBuilder TileSize(int tileSize)
        {
            _component.TileSize = tileSize;
            return this;
        }
        public ColorPickerBuilder TileSize(Action<PaletteSizeBuilder> sizeAction)
        {
            sizeAction(_paletteSizeBuilder);
            _component.TileSize = _paletteTileSize;
            return this;
        }

    }
}
