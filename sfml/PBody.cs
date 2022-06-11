﻿using SFML.Graphics;
using SFML.System;
using System;

namespace sfml
{
    /// <summary>
    /// Physical body
    /// </summary>
    public class PBody : IDrawableObject
    {
        // float G = 1;
        public CircleShape circle { get; private set; }
        public Drawable GetDrawable() => circle;
        public Drawable GetLine() => line.GetDrawable();
        private PlanetLine line;

        public PBody()
        {
            circle = new CircleShape();
        }
        public void Init()
        {
            circle.Position = Pos;
            circle.Radius = Size;
            circle.FillColor = Color;
            line = new PlanetLine(Color);
        }

        /// <summary>
        /// Color of body
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Linear speed of body
        /// </summary>
        public Vector2f Speed { get; set; }
        /// <summary>
        /// Mass of body
        /// </summary>
        public float Mass { get; set; }
        /// <summary>
        /// Current position of body
        /// </summary>
        public Vector2f Pos { get; set; } // Current position of body
        /// <summary>
        /// Velocity of body
        /// </summary>
        public Vector2f Vel { get; set; } = new Vector2f(0, 0);
        /// <summary>
        /// Visible size of body
        /// </summary>
        public int Size { get; set; }

        public void SetOffset()
        {
            circle.Position = Pos - new Vector2f(circle.Radius / 2, circle.Radius / 2);
            line.Add(Pos);
            return;
        }

        /// <summary>
        /// Counts offset of body because gravity
        /// </summary>
        /// <param name="bodyStable"></param>
        /// <returns></returns>
        public void CountOffset(PBody bodyStable)
        {
            float range = Range(bodyStable);
            float ax = (float)(bodyStable.Mass * (bodyStable.Pos.X - this.Pos.X) / Math.Pow(range, 3));
            float ay = (float)(bodyStable.Mass * (bodyStable.Pos.Y - this.Pos.Y) / Math.Pow(range, 3));
            Speed = new Vector2f(Speed.X + ax, Speed.Y + ay);
            Pos = new Vector2f(Pos.X + Speed.X, Pos.Y + Speed.Y);
            SetOffset();
        }

        private float Range(PBody bodyStable)
        {
            float range = (float)Math.Sqrt(Math.Pow((this.Pos.X - bodyStable.Pos.X), 2) + Math.Pow((this.Pos.Y - bodyStable.Pos.Y), 2)); // Pythagorian theorem
            return range;
        }
    }
}
