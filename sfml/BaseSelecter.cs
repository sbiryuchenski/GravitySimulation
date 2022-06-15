using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace sfml
{
    /// <summary>
    /// Base class for parameter selectors. DOESN'T WORK
    /// </summary>
    public abstract class BaseSelecter
    {
        public BaseSelecter()
        {
            Init();
        }
        protected List<Shape> objectList;
        private void Init()
        {

        }
        /// <summary>
        /// Check is cursor in color and retur that color
        /// </summary>
        /// <returns></returns>
        private Shape SelecterThickness()
        {
            Color color = Color.Transparent;
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var clr in objectList)
            {
                clr.OutlineColor = Color.Transparent;
            }
            foreach (var clr in objectList)
            {
                if (clr.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    clr.OutlineThickness = 1;
                    clr.OutlineColor = Color.White;
                    return clr;
                }
            }
            return new RectangleShape();
        }
        public void DrawSelecter()
        {
            SelecterThickness();
            foreach (var clr in objectList)
            {
                clr.Draw(Sf.window, RenderStates.Default);
            }
        }
        /// <summary>
        /// Select which parameter planets will create
        /// </summary>
        public void SelectParameter()
        {

        }
        /// <summary>
        /// Check cursor in any list object 
        /// </summary>
        /// <returns></returns>
        public bool isInColor()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            foreach (var clr in objectList)
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
