using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sfml
{
    public static class PlanetTextures
    {
        public static Dictionary<Color, Texture> textureList = new Dictionary<Color, Texture>()
        {
            {Constants.GetColor(Colors.Red), new Texture($"{Environment.CurrentDirectory}\\Recources\\redPlanet.png")},
            {Constants.GetColor(Colors.InLoveToad), new Texture($"{Environment.CurrentDirectory}\\Recources\\jaba.png")},
            {Constants.GetColor(Colors.Orange), new Texture($"{Environment.CurrentDirectory}\\Recources\\orangePlanet.png")},
            {Constants.GetColor(Colors.Yellow), new Texture($"{Environment.CurrentDirectory}\\Recources\\yellowPlanet.png")},
            {Constants.GetColor(Colors.Blue), new Texture($"{Environment.CurrentDirectory}\\Recources\\bluePlanet.png")},
            {Constants.GetColor(Colors.Green), new Texture($"{Environment.CurrentDirectory}\\Recources\\greenPlanet.png")},
            {Constants.GetColor(Colors.Violet), new Texture($"{Environment.CurrentDirectory}\\Recources\\violetPlanet.png")},
        };

        public static Texture GetTextureByColor(Color color)
        {
            var texture = textureList.Where(_ => _.Key == color).Select(_ => _.Value).FirstOrDefault();
            texture.Smooth = true;
            return texture;
        }
    }
}
