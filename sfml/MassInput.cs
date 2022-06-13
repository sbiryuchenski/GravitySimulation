using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfml
{
    public class MassInput : IDrawableObject
    {
        RectangleShape field { get; set; } = new RectangleShape();
        Text text = new Text();
        Font font = new Font("font.ttf");
        public bool isEditing { get; set; } = false;
        public string enteredMass { get; set; } = Planets.ConstMass.ToString();
        public MassInput()
        {
            Init();
        }
        public void Init()
        {
            field.Size = new Vector2f(100, 20);
            field.FillColor = new Color(0, 0, 0);
            field.OutlineColor = new Color(255, 255, 255);
            field.OutlineThickness = 1;
            field.Position = new Vector2f(5, Sf.H - field.Size.Y - 10);

            text.Font = font;
            text.FillColor = new Color(255, 255, 255);
            text.CharacterSize = 15;
            text.DisplayedString = string.Empty;
            text.Position = new Vector2f(field.Position.X + 3, field.Position.Y);
        }
        public Drawable GetDrawable() => field;

        /// <summary>
        /// Draw rectangle with text
        /// </summary>
        public void DrawTextField(string mass)
        {
            text.DisplayedString = mass;
            if (isEditing) field.FillColor = new Color(255, 0, 0);
            else field.FillColor = new Color(0, 0, 0);
            field.Draw(Sf.window, RenderStates.Default);
            text.Draw(Sf.window, RenderStates.Default);
        }

        /// <summary>
        /// True if cursor in recrangle
        /// </summary>
        /// <param name="mouse"></param>
        /// <returns></returns>
        public bool CheckIsMouseInRectangle()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            return field.GetGlobalBounds().Contains(mouse.X, mouse.Y);
        }
        /// <summary>
        /// Use field to input mass for new planets
        /// </summary>
        public void InputMass()
        {
            isEditing = true;
            enteredMass = string.Empty;
        }
    }
}
