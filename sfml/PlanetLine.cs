using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace sfml
{
    /// <summary>
    /// Hope this will not crush. Line of planet to see a body path
    /// </summary>
    public class PlanetLine : IDrawableObject
    {
        public PlanetLine(Vector2f startPosition, Color color)
        {
            this.Color = color;
            Init(startPosition, color);
        }

        static readonly uint n = 2000;
        uint reducedN = 500;
        byte lagReduceCoefficient = 4, iterator = 0;
        bool reduceLags = false;
        public VertexArray Line { get; private set; }
        private void Init(Vector2f startPosition, Color color)
        {
            Line = new VertexArray(PrimitiveType.LineStrip, reduceLags ? reducedN:n);
            Line.Clear();
            Line[0] = new Vertex(startPosition, color);
        }
        public Drawable GetDrawable() => Line;

        public Color Color { get; set; }


        public void Add(Vector2f position)
        {
            if (reduceLags)
                AddOptimized(position);
            else
                JustAdd(position, n);
        }
        private void JustAdd(Vector2f position, uint iterator)
        {
            if (Line.VertexCount == iterator)
            {
                for (uint i = 0; i < iterator - 1; i++)
                {
                    Line[i] = Line[i + 1];
                }
                Line[iterator - 1] = new Vertex(position, Color);
            }
            else
            {
                Line.Append(new Vertex(position, Color));
            }
        }
        private void AddOptimized(Vector2f position)
        {
            if (iterator == lagReduceCoefficient)
            {
                JustAdd(position, reducedN);
                iterator = 0;
            }
            iterator++;
        }
    }
}
