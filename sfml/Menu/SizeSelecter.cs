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
        int position = 70;
        Size xs, s, m, l, xl, xxl;
        List<Size> sizeList;
        private void Init()
        {
            xs = new Size();
            xs.Circle = new CircleShape();
            xs.Circle.Radius = 3;
            xs.Circle.Position = new Vector2f(360, Sf.H - 30 - xs.Circle.Radius);

            s = new Size();
            s.Circle = new CircleShape();
            s.Circle.Radius = 5;
            s.Circle.Position = new Vector2f(370 + xs.Circle.Radius * 2, Sf.H - 30 - s.Circle.Radius);

            m = new Size();
            m.Circle = new CircleShape();
            m.Circle.Radius = 10;
            m.Circle.Position = new Vector2f(385 + s.Circle.Radius * 2, Sf.H - 30 - m.Circle.Radius);

            l = new Size();
            l.Circle = new CircleShape();
            l.Circle.Radius = 15;
            l.Circle.Position = new Vector2f(405 + m.Circle.Radius * 2, Sf.H - 30 - l.Circle.Radius);

            xl = new Size();
            xl.Circle = new CircleShape();
            xl.Circle.Radius = 20;
            xl.Circle.Position = new Vector2f(435 + l.Circle.Radius * 2, Sf.H - 30 - xl.Circle.Radius);

            xxl = new Size();
            xxl.Circle = new CircleShape();
            xxl.Circle.Radius = 30;
            xxl.Circle.Position = new Vector2f(475 + xl.Circle.Radius * 2, Sf.H - 30 - xxl.Circle.Radius);

            sizeList = new List<Size>() { xs, s, m, l, xl, xxl };

            foreach (var sz in sizeList)
            {
                sz.Circle.OutlineThickness = 1;
                sz.Circle.FillColor = Color.Transparent;
                sz.Circle.OutlineColor = Color.White;
                sz.Circle.OutlineThickness = 1;
            }
        }
        /// <summary>
        /// Check is cursor in size and retur that size
        /// </summary>
        /// <returns></returns>
        private Size SelecterThickness()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var sz in sizeList)
            {
                if (sz.Circle.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    sz.Circle.OutlineColor = Color.Red;
                    return sz;
                }
            }
            return m;
        }
        public void Draw(int getpos)
        {
            position = getpos;
            foreach (var sz in sizeList)
            {
                sz.Circle.Position = new Vector2f(sz.Circle.Position.X, Sf.H - position - sz.Circle.Radius + 37);
                if (sz.IsSelected)
                    sz.Circle.OutlineColor = Color.Red;
                else
                    sz.Circle.OutlineColor = Color.White;
            }
            SelecterThickness();
            foreach (var sz in sizeList)
            {
                sz.Circle.Draw(Sf.window, RenderStates.Default);
            }
        }
        /// <summary>
        /// Select which color planets will create
        /// </summary>
        public void SelectSize()
        {
            Size selectedSize = SelecterThickness();
            Planets.ConstSize = (int)selectedSize.Circle.Radius;
            foreach (var sz in sizeList)
            {
                sz.IsSelected = false;
            }
            selectedSize.IsSelected = true;
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
                if (sz.Circle.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    return true;
                }
            }
            return false;
        }
    }
    class Size
    {
        public bool IsSelected = false;
        public CircleShape Circle { get; set; }
    }
}
