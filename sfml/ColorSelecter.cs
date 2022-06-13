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
        RectangleShape red, orange, yellow, green, blue, violet, inLoveToad;
        List<RectangleShape> colorList;
        private void Init()
        {
            
            red = new RectangleShape();
            red.FillColor = Constants.GetColor(Colors.Red);
            red.Position = new Vector2f(140, Sf.H - 25);

            orange = new RectangleShape();
            orange.FillColor = Constants.GetColor(Colors.Orange);
            orange.Position = new Vector2f(170, Sf.H - 25);

            yellow = new RectangleShape();
            yellow.FillColor = Constants.GetColor(Colors.Yellow);
            yellow.Position = new Vector2f(200, Sf.H - 25);

            green = new RectangleShape();
            green.FillColor = Constants.GetColor(Colors.Green);
            green.Position = new Vector2f(230, Sf.H - 25);

            blue = new RectangleShape();
            blue.FillColor = Constants.GetColor(Colors.Blue);
            blue.Position = new Vector2f(260, Sf.H - 25);

            violet = new RectangleShape();
            violet.FillColor = Constants.GetColor(Colors.Violet);
            violet.Position = new Vector2f(290, Sf.H - 25);

            inLoveToad = new RectangleShape();
            inLoveToad.FillColor = Constants.GetColor(Colors.InLoveToad);
            inLoveToad.Position = new Vector2f(320, Sf.H - 25);

            colorList = new List<RectangleShape>() { red, orange, yellow, green, blue, violet, inLoveToad };

            foreach (var clr in colorList)
            {
                clr.Size = new Vector2f(20, 20);
                clr.OutlineThickness = 1;
            }
        }
        /// <summary>
        /// Check is cursor in color and retur that color
        /// </summary>
        /// <returns></returns>
        private RectangleShape SelecterThickness()
        {
            Color color = Color.Transparent;
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var clr in colorList)
            {
                clr.OutlineColor = Color.Black;
            }
            foreach (var clr in colorList)
            {
                if(clr.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    clr.OutlineThickness = 1;
                    clr.OutlineColor = Color.White;
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
                clr.Draw(Sf.window, RenderStates.Default);
            }
        }
        /// <summary>
        /// Select which color planets will create
        /// </summary>
        public void SelectColor()
        {
            var selectedColor = SelecterThickness();
            Planets.ConstColor = selectedColor.FillColor;
            foreach (var clr in colorList)
            {
                clr.Position = new Vector2f(clr.Position.X, Sf.H - 25);
            }
            selectedColor.Position = new Vector2f(selectedColor.Position.X, Sf.H - 35);
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
                if (clr.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
