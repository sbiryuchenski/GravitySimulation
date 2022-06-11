using SFML.Graphics;
using SFML.System;
using System;

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
            if (position.X < Sf.W + 1 && position.Y < Sf.H + 1)
            {
                if (Line.VertexCount == n)
                {
                    //Line.Resize(n * 2);
                    //n *= 2;
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
            }
        }
    }
}
