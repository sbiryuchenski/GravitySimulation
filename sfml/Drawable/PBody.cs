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
        public CircleShape circle { get; private set; }
        public Drawable GetDrawable()
        {
            if (circle == null)
            {
                circle = new CircleShape();
            }
            return circle;
        }
        public Drawable GetLine()
            {
            if (line == null)
            {
                line = new PlanetLine(Pos, Color);
            }
            return line.GetDrawable();
            }
        public PlanetLine line { get; private set; }

        public PBody()
        {
            circle = new CircleShape();
        }
        public PBody(float mass, Vector2f speed, Vector2f startPosition, Color color, int size)
        {
            Mass = mass;
            Speed = speed;
            Pos = startPosition;
            Color = color;
            Size = size;
        }
        public PBody(PBody copy) 
        {
            Mass = copy.Mass;
            Speed = copy.Speed;
            Pos = copy.Pos;
            Color = copy.Color;
            Size = copy.Size;
            circle = new CircleShape();
        }
        public void Init()
        {
            circle.Position = Pos;
            circle.Radius = Size;
            circle.FillColor = Color;
            line = new PlanetLine(Pos, Color);
            Texture texture;
            if (Color != Color.Transparent)
            {
                texture = PlanetTextures.GetTextureByColor(Color);
                circle.FillColor = Color.White;
                circle.Texture = texture;
            }
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
        /// Visible size of body
        /// </summary>
        public int Size { get; set; }

        private float ax, ay;

        public void SetOffset(bool isPreviewLine = false)
        {
            circle.Position = Pos - new Vector2f(circle.Radius / 2, circle.Radius / 2);
            if(!isPreviewLine) line.Add(Pos);
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
            if (range < 5000)
            {
                ax = (float)(bodyStable.Mass * (bodyStable.Pos.X - this.Pos.X) / Math.Pow(range, 3));
                ay = (float)(bodyStable.Mass * (bodyStable.Pos.Y - this.Pos.Y) / Math.Pow(range, 3));
                Speed = new Vector2f(Speed.X + ax, Speed.Y + ay);
            }
        }
        public void SetVelocity()
        {
            Pos = new Vector2f(Pos.X + Speed.X, Pos.Y + Speed.Y);
            SetOffset();
        }

        private float Range(PBody bodyStable)
        {
            float range = (float)Math.Sqrt(Math.Pow((this.Pos.X - bodyStable.Pos.X), 2) + Math.Pow((this.Pos.Y - bodyStable.Pos.Y), 2)); // Pythagorian theorem
            if (range < 5) range = 5;
            return range;
        }

    }
}
