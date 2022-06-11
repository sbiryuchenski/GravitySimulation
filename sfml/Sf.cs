using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace sfml
{
    class Sf
    {
        const uint W = 900;
        const uint H = 900;
        const uint FPS = 60;
        RenderWindow window;

        bool isPaused = false;
        void DrawPlanet(PBody obj) => window.Draw(obj.GetDrawable());
        void DrawOrbit(PBody obj) => window.Draw(obj.GetLine());
        void DrawPlanets(List<PBody> planets)
        {
            foreach (var planet in planets)
            {
                DrawPlanet(planet);
            }
            foreach (var planet in planets)
            {
                DrawOrbit(planet);
            }
        }
        void CountNextState(List<PBody> planets)
        {
            foreach (var planet in planets)
            {
                foreach (var planet2 in planets)
                {
                    if (planet != planet2)
                    {
                        planet.CountOffset(planet2);
                    }
                }
            }
        }
            void InitWindow()
            {
                VideoMode mode = new VideoMode(W, H);
                window = new RenderWindow(mode, "хуй");
                window.SetFramerateLimit(FPS);

                Planets.AddPlanet(1, new Vector2f(0.4f, 0.1f), new Vector2f(W/2, H/2), new Color(255, 0, 0), 5);
                Planets.AddPlanet(50, new Vector2f(-0.1f, -0.1f), new Vector2f(W / 2+100, H / 2+100), new Color(0, 255, 0), 5);
            }

            public void Show()
            {
                InitWindow();
                window.Closed += (obj, e) => { window.Close(); };
                window.KeyPressed += (sender, e) =>
                {
                    if (e.Code == Keyboard.Key.Escape)
                    {
                        window.Close();
                    }
                };
                window.MouseButtonPressed += (sender, e) =>
                {
                    if (e.Button == Mouse.Button.Left)
                    {
                        Mouse.GetPosition();
                    }
                };

                window.KeyPressed += (sender, e) =>

                {
                    if (e.Code == Keyboard.Key.P)
                    {
                        isPaused = !isPaused;
                    }
                };

                Clock clock = new Clock();
                float delta = 100f;



                while (window.IsOpen)
                {

                    delta = clock.Restart().AsSeconds();

                    window.DispatchEvents();


                    if (!isPaused)
                    {
                        window.Clear();
                        CountNextState(Planets.PlanetList);
                        DrawPlanets(Planets.PlanetList);
                    }

                    window.Display();
                }
            }
    }
}






