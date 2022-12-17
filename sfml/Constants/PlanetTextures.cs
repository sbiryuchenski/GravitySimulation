using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sfml
{
    public static class PlanetTextures
    {
        public static Dictionary<Color, Texture> textureList = new Dictionary<Color, Texture>()
        {
            {ColorConstants.GetColor(Colors.Red), new Texture($"{Environment.CurrentDirectory}\\Recources\\redPlanet.png")},
            {ColorConstants.GetColor(Colors.InLoveToad), new Texture($"{Environment.CurrentDirectory}\\Recources\\jaba.png")},
            {ColorConstants.GetColor(Colors.Orange), new Texture($"{Environment.CurrentDirectory}\\Recources\\orangePlanet.png")},
            {ColorConstants.GetColor(Colors.Yellow), new Texture($"{Environment.CurrentDirectory}\\Recources\\yellowPlanet.png")},
            {ColorConstants.GetColor(Colors.Blue), new Texture($"{Environment.CurrentDirectory}\\Recources\\bluePlanet.png")},
            {ColorConstants.GetColor(Colors.Green), new Texture($"{Environment.CurrentDirectory}\\Recources\\greenPlanet.png")},
            {ColorConstants.GetColor(Colors.Violet), new Texture($"{Environment.CurrentDirectory}\\Recources\\violetPlanet.png")},
        };
        public static Dictionary<Color, Texture> squareTextureList = new Dictionary<Color, Texture>()
        {
            {ColorConstants.GetColor(Colors.Red), new Texture($"{Environment.CurrentDirectory}\\Recources\\redPlanetSq.png")},
            {ColorConstants.GetColor(Colors.InLoveToad), new Texture($"{Environment.CurrentDirectory}\\Recources\\jabaSq.png")},
            {ColorConstants.GetColor(Colors.Orange), new Texture($"{Environment.CurrentDirectory}\\Recources\\orangePlanetSq.png")},
            {ColorConstants.GetColor(Colors.Yellow), new Texture($"{Environment.CurrentDirectory}\\Recources\\yellowPlanetSq.png")},
            {ColorConstants.GetColor(Colors.Blue), new Texture($"{Environment.CurrentDirectory}\\Recources\\bluePlanetSq.png")},
            {ColorConstants.GetColor(Colors.Green), new Texture($"{Environment.CurrentDirectory}\\Recources\\greenPlanetSq.png")},
            {ColorConstants.GetColor(Colors.Violet), new Texture($"{Environment.CurrentDirectory}\\Recources\\violetPlanetSq.png")},
        };

        public static Texture GetTextureByColor(Color color)
        {
            var texture = textureList.Where(_ => _.Key == color).Select(_ => _.Value).FirstOrDefault();
            if (texture == null)
                texture = GetTextureByColor(ColorConstants.GetColor(Colors.Blue));
            texture.Smooth = true;
            return texture;
        }
        public static Texture GetSquareTextureByColor(Color color)
        {
            var texture = squareTextureList.Where(_ => _.Key == color).Select(_ => _.Value).FirstOrDefault();
            texture.Smooth = true;
            return texture;
        }
    }
}
