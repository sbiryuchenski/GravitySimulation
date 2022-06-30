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
        PlanetColor red, orange, yellow, green, blue, violet, inLoveToad;
        List<PlanetColor> colorList;
        //private void Init()
        //{
            
        //    red = new RectangleShape();
        //    red.FillColor = Constants.GetColor(Colors.Red);
        //    red.Position = new Vector2f(140, Sf.H - 30);

        //    orange = new RectangleShape();
        //    orange.FillColor = Constants.GetColor(Colors.Orange);
        //    orange.Position = new Vector2f(170, Sf.H - 30);

        //    yellow = new RectangleShape();
        //    yellow.FillColor = Constants.GetColor(Colors.Yellow);
        //    yellow.Position = new Vector2f(200, Sf.H - 30);

        //    green = new RectangleShape();
        //    green.FillColor = Constants.GetColor(Colors.Green);
        //    green.Position = new Vector2f(230, Sf.H - 30);

        //    blue = new RectangleShape();
        //    blue.FillColor = Constants.GetColor(Colors.Blue);
        //    blue.Position = new Vector2f(260, Sf.H - 30);

        //    violet = new RectangleShape();
        //    violet.FillColor = Constants.GetColor(Colors.Violet);
        //    violet.Position = new Vector2f(290, Sf.H - 30);

        //    inLoveToad = new RectangleShape();
        //    inLoveToad.FillColor = Constants.GetColor(Colors.InLoveToad);
        //    inLoveToad.Position = new Vector2f(320, Sf.H - 30);

        //    colorList = new List<RectangleShape>() { red, orange, yellow, green, blue, violet, inLoveToad };

        //    foreach (var clr in colorList)
        //    {
        //        clr.Size = new Vector2f(20, 20);
        //        clr.OutlineThickness = 1;
        //    }
        //}

        private void Init()
        {
            red = new PlanetColor();
            red.Rectangle = new RectangleShape();
            red.Color = Constants.GetColor(Colors.Red);
            red.Rectangle.Texture = PlanetTextures.GetTextureByColor(Constants.GetColor(Colors.Red));
            red.Rectangle.Position = new Vector2f(140, Sf.H - 30);

            orange = new PlanetColor();
            orange.Rectangle = new RectangleShape();
            orange.Color = Constants.GetColor(Colors.Orange);
            orange.Rectangle.Texture = PlanetTextures.GetTextureByColor(Constants.GetColor(Colors.Orange));
            orange.Rectangle.Position = new Vector2f(170, Sf.H - 30);

            yellow = new PlanetColor();
            yellow.Rectangle = new RectangleShape();
            yellow.Color = Constants.GetColor(Colors.Yellow);
            yellow.Rectangle.Texture = PlanetTextures.GetTextureByColor(Constants.GetColor(Colors.Yellow));
            yellow.Rectangle.Position = new Vector2f(200, Sf.H - 30);

            green = new PlanetColor();
            green.Rectangle = new RectangleShape();
            green.Color = Constants.GetColor(Colors.Green);
            green.Rectangle.Texture = PlanetTextures.GetTextureByColor(Constants.GetColor(Colors.Green));
            green.Rectangle.Position = new Vector2f(230, Sf.H - 30);

            blue = new PlanetColor();
            blue.Rectangle = new RectangleShape();
            blue.Color = Constants.GetColor(Colors.Blue);
            blue.Rectangle.Texture = PlanetTextures.GetTextureByColor(Constants.GetColor(Colors.Blue));
            blue.Rectangle.Position = new Vector2f(260, Sf.H - 30);

            violet = new PlanetColor();
            violet.Rectangle = new RectangleShape();
            violet.Color = Constants.GetColor(Colors.Violet);
            violet.Rectangle.Texture = PlanetTextures.GetTextureByColor(Constants.GetColor(Colors.Violet));
            violet.Rectangle.Position = new Vector2f(290, Sf.H - 30);

            inLoveToad = new PlanetColor();
            inLoveToad.Rectangle = new RectangleShape();
            inLoveToad.Color = Constants.GetColor(Colors.InLoveToad);
            inLoveToad.Rectangle.Texture = PlanetTextures.GetTextureByColor(Constants.GetColor(Colors.InLoveToad));
            inLoveToad.Rectangle.Position = new Vector2f(320, Sf.H - 30);

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
            Color color = Color.Transparent;
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var clr in colorList)
            {
                clr.Rectangle.OutlineColor = Color.Transparent;
            }
            foreach (var clr in colorList)
            {
                if(clr.Rectangle.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    clr.Rectangle.OutlineThickness = 1;
                    clr.Rectangle.OutlineColor = Color.White;
                    return clr;
                }
            }
            return blue;
        }
        public void DrawColorSelecter()
        {
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
            var selectedColor = SelecterThickness();
            Planets.ConstColor = selectedColor.Color;
            foreach (var clr in colorList)
            {
                clr.Rectangle.Position = new Vector2f(clr.Rectangle.Position.X, Sf.H - 30);
            }
            selectedColor.Rectangle.Position = new Vector2f(selectedColor.Rectangle.Position.X, Sf.H - 35);
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
        public RectangleShape Rectangle { get; set; }
        public Color Color { get; set; }
    }
}
