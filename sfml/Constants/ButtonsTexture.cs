using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sfml
{
    public static class ButtonsTexture
    {
        public static Dictionary<Buttons, Texture> textureList = new()
        {
            {Buttons.PW, new Texture($"{Environment.CurrentDirectory}\\Recources\\PButtonW.png")},
            {Buttons.PR, new Texture($"{Environment.CurrentDirectory}\\Recources\\PButtonR.png")},
            {Buttons.CW, new Texture($"{Environment.CurrentDirectory}\\Recources\\CButtonW.png")},
            {Buttons.CR, new Texture($"{Environment.CurrentDirectory}\\Recources\\CButtonR.png")},
            {Buttons.MW, new Texture($"{Environment.CurrentDirectory}\\Recources\\MButtonW.png")},
            {Buttons.MR, new Texture($"{Environment.CurrentDirectory}\\Recources\\MButtonR.png")},
            {Buttons.F3W, new Texture($"{Environment.CurrentDirectory}\\Recources\\F3ButtonW.png")},
            {Buttons.F3R, new Texture($"{Environment.CurrentDirectory}\\Recources\\F3ButtonR.png")},
            {Buttons.LMB, new Texture($"{Environment.CurrentDirectory}\\Recources\\LMB.png")},
            {Buttons.RMB, new Texture($"{Environment.CurrentDirectory}\\Recources\\RMB.png")},
            {Buttons.Scroll, new Texture($"{Environment.CurrentDirectory}\\Recources\\ScrollButton.png")},
            {Buttons.InfoW, new Texture($"{Environment.CurrentDirectory}\\Recources\\InfoButtonW.png")},
            {Buttons.InfoR, new Texture($"{Environment.CurrentDirectory}\\Recources\\InfoButtonR.png")},
        };

        public static Texture GetTexture(Buttons btn)
        {
            var texture = textureList.Where(_ => _.Key == btn).Select(_ => _.Value).FirstOrDefault();
            if (texture == null)
                texture = GetTexture(Buttons.CR);
            texture.Smooth = true;
            return texture;
        }
    }
}
