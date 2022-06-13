using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace sfml
{
    /// <summary>
    /// Hope this will not crush. Line of planet to see a body path
    /// </summary>
    public class PlanetLine : IDrawableObject
    {
        public PlanetLine (Vector2f startPosition, Color color)
        {
            this.Color = color;
            Init(startPosition, color);
        }

        static uint n = 2000;
        public VertexArray Line { get; private set; } = new VertexArray(PrimitiveType.LineStrip, n);
        private void Init(Vector2f startPosition, Color color)
        {
            Line.Clear();
            Line[0] = new Vertex(startPosition, color);
        }
        public Drawable GetDrawable() => Line;
        public Color Color { get; set; }

        /// <summary>
        /// Add a point
        /// </summary>
        /// <param name="position"></param>
        public void Add(Vector2f position)
        {
            //VertexBuffer
            //if (position.X < Sf.W && position.Y < Sf.H)
            //{
            List<Vertex> ver = new List<Vertex>();
                if (Line.VertexCount == n)
                {
                for (uint i = 0; i < n - 1; i++)
                {
                    Line[i] = Line[i + 1];
                }
                Line[n - 1] = new Vertex(position, Color);
            }
                else
                {
                    Line.Append(new Vertex(position, Color));
                }
            //}
        }
    }
}
