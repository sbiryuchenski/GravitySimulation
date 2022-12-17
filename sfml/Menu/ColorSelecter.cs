using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace sfml
{
    /// <summary>
    /// Color selecter in bottom menu
    /// </summary>
    class ColorSelecter
    {
        public ColorSelecter()
        {
            Init();
        }
        int position = 70;
        PlanetColor red, orange, yellow, green, blue, violet, inLoveToad;
        List<PlanetColor> colorList;
        private void Init()
        {
            red = new PlanetColor();
            red.Rectangle = new RectangleShape();
            red.Color = ColorConstants.GetColor(Colors.Red);
            red.Rectangle.Texture = PlanetTextures.GetSquareTextureByColor(ColorConstants.GetColor(Colors.Red));
            red.Rectangle.Position = new Vector2f(140, Sf.H + position);

            orange = new PlanetColor();
            orange.Rectangle = new RectangleShape();
            orange.Color = ColorConstants.GetColor(Colors.Orange);
            orange.Rectangle.Texture = PlanetTextures.GetSquareTextureByColor(ColorConstants.GetColor(Colors.Orange));
            orange.Rectangle.Position = new Vector2f(170, Sf.H + position);

            yellow = new PlanetColor();
            yellow.Rectangle = new RectangleShape();
            yellow.Color = ColorConstants.GetColor(Colors.Yellow);
            yellow.Rectangle.Texture = PlanetTextures.GetSquareTextureByColor(ColorConstants.GetColor(Colors.Yellow));
            yellow.Rectangle.Position = new Vector2f(200, Sf.H + position);

            green = new PlanetColor();
            green.Rectangle = new RectangleShape();
            green.Color = ColorConstants.GetColor(Colors.Green);
            green.Rectangle.Texture = PlanetTextures.GetSquareTextureByColor(ColorConstants.GetColor(Colors.Green));
            green.Rectangle.Position = new Vector2f(230, Sf.H + position);

            blue = new PlanetColor();
            blue.Rectangle = new RectangleShape();
            blue.Color = ColorConstants.GetColor(Colors.Blue);
            blue.Rectangle.Texture = PlanetTextures.GetSquareTextureByColor(ColorConstants.GetColor(Colors.Blue));
            blue.Rectangle.Position = new Vector2f(260, Sf.H + position);

            violet = new PlanetColor();
            violet.Rectangle = new RectangleShape();
            violet.Color = ColorConstants.GetColor(Colors.Violet);
            violet.Rectangle.Texture = PlanetTextures.GetSquareTextureByColor(ColorConstants.GetColor(Colors.Violet));
            violet.Rectangle.Position = new Vector2f(290, Sf.H + position);

            inLoveToad = new PlanetColor();
            inLoveToad.Rectangle = new RectangleShape();
            inLoveToad.Color = ColorConstants.GetColor(Colors.InLoveToad);
            inLoveToad.Rectangle.Texture = PlanetTextures.GetSquareTextureByColor(ColorConstants.GetColor(Colors.InLoveToad));
            inLoveToad.Rectangle.Position = new Vector2f(320, Sf.H + position);

            colorList = new List<PlanetColor>() { red, orange, yellow, green, blue, violet, inLoveToad };

            foreach (var clr in colorList)
            {
                clr.Rectangle.Size = new Vector2f(20, 20);
                clr.Rectangle.OutlineThickness = 1;
                clr.Rectangle.FillColor = Color.White;
            }
        }

        /// <summary>
        /// Check is cursor in color and retur that color
        /// </summary>
        /// <returns></returns>
        private PlanetColor SelecterThickness()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var clr in colorList)
            {
                clr.Rectangle.OutlineColor = Color.Transparent;
            }
            foreach (var clr in colorList)
            {
                if (clr.Rectangle.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    clr.Rectangle.OutlineThickness = 1;
                    clr.Rectangle.OutlineColor = Color.White;
                    return clr;
                }
            }
            return blue;
        }
        public void Draw(int getpos)
        {
            position = getpos;
            foreach (var clr in colorList)
            {
                if (clr.IsSelected)
                    clr.Rectangle.Position = new Vector2f(clr.Rectangle.Position.X, Sf.H - position + 35);
                else
                    clr.Rectangle.Position = new Vector2f(clr.Rectangle.Position.X, Sf.H - position + 40);
            }
            SelecterThickness();
            foreach (var clr in colorList)
            {
                clr.Rectangle.Draw(Sf.window, RenderStates.Default);
            }
        }
        /// <summary>
        /// Select which color planets will create
        /// </summary>
        public void SelectColor()
        {
            PlanetColor selectedColor = SelecterThickness();
            Planets.ConstColor = selectedColor.Color;
            foreach (var clr in colorList)
            {
                clr.IsSelected = false;
            }
            selectedColor.IsSelected = true;
        }
        /// <summary>
        /// Check cursor in any color
        /// </summary>
        /// <returns></returns>
        public bool isInColor()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var clr in colorList)
            {
                if (clr.Rectangle.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    return true;
                }
            }
            return false;
        }
    }
    class PlanetColor
    {
        public bool IsSelected = false;
        public RectangleShape Rectangle { get; set; }
        public Color Color { get; set; }
    }
}
