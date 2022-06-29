using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace sfml
{
    static class Constants
    {
        public static Color GetColor(Colors color)
        {
            Color returnColor = new Color();
            switch (color)
            {
                case Colors.White:
                    returnColor = Color.White;
                    break;
                case Colors.Black:
                    returnColor = Color.Black;
                    break;
                case Colors.Blue:
                    returnColor = Color.Cyan;
                    break;
                case Colors.Green:
                    returnColor = Color.Green;
                    break;
                case Colors.Red:
                    returnColor = Color.Red;
                    break;
                case Colors.Yellow:
                    returnColor = Color.Yellow;
                    break;
                case Colors.Orange:
                    returnColor = new Color(255, 102, 0);
                    break;
                case Colors.Violet:
                    returnColor = new Color(128, 0, 255);
                    break;
                case Colors.InLoveToad:
                    returnColor = new Color(60, 170, 60);
                    break;
                default:
                    throw new Exception($"Color {color} not found");
            }
            return returnColor;
        }
    }
}
