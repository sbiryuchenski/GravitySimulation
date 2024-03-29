﻿using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfml
{
    class TextElements
    {
        Text fps = new Text();
        Text objectsCounter = new Text();
        Text speed = new Text();
        Font font = new Font($"{Environment.CurrentDirectory}\\Recources\\font.ttf");
        public TextElements()
        {
            Init();
        }
        private void Init()
        {
            fps.Font = font;
            fps.FillColor = new Color(255, 255, 255);
            fps.CharacterSize = 15;
            fps.DisplayedString = string.Empty;
            fps.Position = new Vector2f(10, 10);

            objectsCounter.Font = font;
            objectsCounter.FillColor = new Color(255, 255, 255);
            objectsCounter.CharacterSize = 15;
            objectsCounter.DisplayedString = string.Empty;
            objectsCounter.Position = new Vector2f(10, 30);

            speed.Font = font;
            speed.FillColor = new Color(255, 255, 255);
            speed.CharacterSize = 15;
            speed.DisplayedString = string.Empty;
            speed.Position = new Vector2f(Sf.W-80, 10);
        }
        private void DrawFPSCounter()
        {
            fps.DisplayedString = $"FPS: {Sf.Framerate}";
            fps.Draw(Sf.window, RenderStates.Default);
        }
        private void DrawObjectsCount()
        {
            objectsCounter.DisplayedString = $"Total objects: {Planets.PlanetList.Count-1}";
            objectsCounter.Draw(Sf.window, RenderStates.Default);
            
        }
        public void DrawSystemInfoText(bool isDraw)
        {
            if (isDraw)
            {
                DrawFPSCounter();
                DrawObjectsCount();
                DrawSpeed();
            }

        }
        public void DrawSpeed()
        {
            speed.DisplayedString = $"Speed: {Sf.Speed}";
            speed.Draw(Sf.window, RenderStates.Default);
        }
    }
}
