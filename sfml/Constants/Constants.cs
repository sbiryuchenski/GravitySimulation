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
                    returnColor = new Color(59, 89, 220);
                    break;
                case Colors.Green:
                    returnColor = new Color(36, 139, 179);
                    break;
                case Colors.Red:
                    returnColor = new Color(244, 107, 59);
                    break;
                case Colors.Yellow:
                    returnColor = new Color(152, 122, 92);
                    break;
                case Colors.Orange:
                    returnColor = new Color(230, 144, 49);
                    break;
                case Colors.Violet:
                    returnColor = new Color(93, 82, 168);
                    break;
                case Colors.InLoveToad:
                    returnColor = new Color(60, 170, 60);
                    break;
                case Colors.SemiTranspetentBlack:
                    returnColor = new Color(0, 0, 0, 128);
                    break;
                case Colors.Grey:
                    returnColor = new Color(150, 150, 150);
                    break;
                default:
                    throw new Exception($"Color {color} not found");
            }
            return returnColor;
        }
    }
}
