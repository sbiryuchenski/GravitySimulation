using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace sfml
{
    class Sf
    {
        public static readonly uint W = 900;
        public static readonly uint H = 900;
        const uint FPS = 60;
        public static RenderWindow window;
        Clock clock = new Clock();
        float delta = 100f;

        PBody planetCandidate = null;

        bool isPaused = false;
        bool creatingSpeed = false;
        bool creatingPlanet = false;

        VertexArray speedLine;


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
            for (int i = 0; i < planets.Count; i++)
            {
                if (planets[i].Pos.X > 1000 || planets[i].Pos.Y > 1000)
                {
                    planets.Remove(planets[i]);
                }
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

            Planets.AddPlanet(1, new Vector2f(0.1f, 0.1f), new Vector2f(W/2, H/2), new Color(255, 0, 0), 5);
            Planets.AddPlanet(50, new Vector2f(0f, -0.1f), new Vector2f(W / 2+100, H / 2+100), new Color(0, 255, 0), 5);
            Planets.AddPlanet(50, new Vector2f(0.1f, -0.4f), new Vector2f(W / 2 -100, H / 2 + 100), new Color(0, 255, 0), 5);
        }

        public void CreatePlanet()
        {
            creatingPlanet = true;
            creatingSpeed = true;

            isPaused = true;
            speedLine = new VertexArray(PrimitiveType.LineStrip, 2);
            speedLine.Clear();
            Vector2i point = Mouse.GetPosition(window);
            speedLine.Append(new Vertex((Vector2f)point));
            speedLine.Append(new Vertex((Vector2f)point));

            planetCandidate = new PBody { Mass = 20, Speed = new Vector2f(0, 0), Pos = (Vector2f)point, Color = new Color(0, 0, 255), Size = 5 };
        }
        public void CreateSpeed()
        {
            Vector2i point = Mouse.GetPosition(window);
            if (planetCandidate != null)
                planetCandidate.Speed = new Vector2f((point.X - planetCandidate.Pos.X) / 100, (point.Y - planetCandidate.Pos.Y) /100);
            creatingSpeed = false;
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
                    CreatePlanet();
                }
            };
            window.MouseButtonReleased += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                {
                    CreateSpeed();
                }
            };

            window.KeyPressed += (sender, e) =>

            {
                if (e.Code == Keyboard.Key.P)
                {
                    if(planetCandidate != null)
                    {
                        Planets.AddPlanet(planetCandidate);
                        planetCandidate = null;
                    }
                    isPaused = !isPaused;
                    creatingSpeed = false;
                    creatingPlanet = false;
                }
            };



            while (window.IsOpen)
            {
                delta = clock.Restart().AsSeconds();

                window.DispatchEvents();

                window.Clear();
                DrawPlanets(Planets.PlanetList);

                if (!isPaused)
                {
                    CountNextState(Planets.PlanetList);
                }
                if(creatingPlanet)
                {
                    if(creatingSpeed)
                        speedLine[1] = new Vertex((Vector2f)Mouse.GetPosition(window));
                    window.Draw(speedLine);
                }

                window.Display();
            }
        }
    }
}






