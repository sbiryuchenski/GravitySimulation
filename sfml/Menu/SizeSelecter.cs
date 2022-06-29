using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace sfml
{
    class SizeSelecter
    {
        public SizeSelecter()
        {
            Init();
        }
        CircleShape xs, s, m, l, xl, xxl;
        List<CircleShape> sizeList;
        private void Init()
        {

            xs = new CircleShape();
            xs.Radius = 3;
            xs.FillColor = Planets.ConstColor;
            xs.Position = new Vector2f(360, Sf.H - 30 - xs.Radius);

            s = new CircleShape();
            s.Radius = 5;
            s.FillColor = Constants.GetColor(Colors.Orange);
            s.Position = new Vector2f(370 + xs.Radius*2, Sf.H - 30 - s.Radius);

            m = new CircleShape();
            m.Radius = 10;
            m.FillColor = Constants.GetColor(Colors.Yellow);
            m.Position = new Vector2f(385 + s.Radius * 2, Sf.H - 30 - m.Radius);

            l = new CircleShape();
            l.Radius = 15;
            l.FillColor = Constants.GetColor(Colors.Green);
            l.Position = new Vector2f(405 + m.Radius * 2, Sf.H - 30 - l.Radius);

            xl = new CircleShape();
            xl.Radius = 20;
            xl.FillColor = Constants.GetColor(Colors.Blue);
            xl.Position = new Vector2f(435 + l.Radius * 2, Sf.H - 30 - xl.Radius);

            xxl = new CircleShape();
            xxl.Radius = 30;
            xxl.FillColor = Constants.GetColor(Colors.Violet);
            xxl.Position = new Vector2f(475 + xl.Radius * 2, Sf.H - 30 - xxl.Radius);

            sizeList = new List<CircleShape>() { xs, s, m, l, xl, xxl };

            foreach (var sz in sizeList)
            {
                sz.OutlineThickness = 1;
                sz.FillColor = Planets.ConstColor;
            }
        }
        /// <summary>
        /// Check is cursor in size and retur that size
        /// </summary>
        /// <returns></returns>
        private CircleShape SelecterThickness()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var sz in sizeList)
            {
                sz.OutlineColor = Color.Transparent;
            }
            foreach (var sz in sizeList)
            {
                if (sz.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    sz.OutlineThickness = 1;
                    sz.OutlineColor = Color.White;
                    return sz;
                }
            }
            return m;
        }
        public void DrawSizeSelecter()
        {
            SelecterThickness();
            foreach (var sz in sizeList)
            {
                sz.Draw(Sf.window, RenderStates.Default);
            }
        }
        /// <summary>
        /// Select which color planets will create
        /// </summary>
        public void SelectSize()
        {
            var selectedSize = SelecterThickness();
            Planets.ConstSize = (int)selectedSize.Radius;
            foreach (var sz in sizeList)
            {
                sz.Position = new Vector2f(sz.Position.X, Sf.H - 30 - sz.Radius);
            }
            selectedSize.Position = new Vector2f(selectedSize.Position.X, Sf.H - 45 - selectedSize.Radius);
        }
        /// <summary>
        /// Check cursor in any size
        /// </summary>
        /// <returns></returns>
        public bool isInSize()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var sz in sizeList)
            {
                if (sz.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
