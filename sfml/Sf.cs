using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.Json;

namespace sfml
{
    partial class Sf
    {
        void DrawPlanet(PBody obj) => window.Draw(obj.GetDrawable());
        void DrawOrbit(PBody obj) => window.Draw(obj.GetLine());
        void DrawPlanets(List<PBody> planets)
        {
            foreach (var planet in planets)
            {
                DrawOrbit(planet);
            }
            foreach (var planet in planets)
            {
                DrawPlanet(planet);
            }
            for (int i = 0; i < planets.Count; i++)
            {
                if (planets[i].Pos.X > W + 10000 || planets[i].Pos.X < -10000 || planets[i].Pos.Y > H + 10000 || planets[i].Pos.Y < -10000)
                {
                    planets.Remove(planets[i]);
                }
            }
        }
        void DrawSpeedLine()
        {
            if (creatingPlanet)
            {
                if (creatingSpeed)
                    speedLine[1] = new Vertex(window.MapPixelToCoords(Mouse.GetPosition(window)));
                window.Draw(speedLine);
            }
        }

        void MoveWindow()
        {
            var offset = window.MapPixelToCoords((Vector2i)currentMousePosition) - window.MapPixelToCoords((Vector2i)newPosition);
            view.Move(offset);
            window.SetView(view);
        }
        void ZoomWindow(float delta)
        {
            if (delta > 0)
            {
                if (zoomFactor > 0.05f)
                {
                    zoomFactor *= 0.93f;
                    view.Zoom(0.93f);
                }
            }
            else
            {
                if (zoomFactor < 4)
                {
                    zoomFactor *= 1.07f;
                    view.Zoom(1.07f);
                }
            }
            window.SetView(view);
        }
        void InitWindow()
        {
            if (File.Exists("Settings.json")) // Config load
            {
                string jsn = File.ReadAllText("Settings.json");
                Settings = JsonSerializer.Deserialize<Settings>(jsn);
            }

            VideoMode mode = new VideoMode(W, H);
            settings.AntialiasingLevel = 8;
            window = new RenderWindow(mode, "Gravity simulation",Styles.None, settings);
            window.SetFramerateLimit(FPS);
            view = new View(new FloatRect(0, 0, W, H));
            window.SetView(view);
            
            Planets.AddPlanet(Planets.EmptyPlanet);

            //Planets.AddPlanet(1, new Vector2f(0.1f, 0.1f), new Vector2f(W / 2, H / 2), new Color(255, 0, 0), 5);
            //Planets.AddPlanet(50, new Vector2f(0f, -0.1f), new Vector2f(W / 2 + 100, H / 2 + 100), new Color(0, 255, 0), 5);
            //Planets.AddPlanet(50, new Vector2f(0.1f, -0.4f), new Vector2f(W / 2 - 100, H / 2 + 100), new Color(0, 255, 0), 5);
        }

        public void Show()
        {
            InitWindow();

            HandleInput();

            DrawAll();
        }
    }
}
