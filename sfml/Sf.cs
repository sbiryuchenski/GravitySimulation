using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace sfml
{
    class Sf
    {
        public static readonly uint H = 700;
        public static readonly uint W = 1200;
        const uint FPS = 60;
        public static RenderWindow window;
        Clock clock = new Clock();
        float delta = 100f;
        VertexArray speedLine;
        int mass;

        bool isPaused = false;
        static public bool isPausedCreating = false;
        static public bool creatingSpeed = false;
        static public bool creatingPlanet = false;

        private MassInput field; // dont use it. It won't work
        private MassInput GetField { get; set; } = new MassInput();

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
                if (planets[i].Pos.X > W + 1000 || planets[i].Pos.X < -1000 || planets[i].Pos.Y > H + 1000 || planets[i].Pos.Y < -1000)
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
            planets[0].Pos = new Vector2f(0, 0);
        }
        void InitWindow()
        {
            VideoMode mode = new VideoMode(W, H);
            window = new RenderWindow(mode, "хуй");
            window.SetFramerateLimit(FPS);
            Planets.AddPlanet(Planets.EmptyPlanet);

            Planets.AddPlanet(1, new Vector2f(0.1f, 0.1f), new Vector2f(W / 2, H / 2), new Color(255, 0, 0), 5);
            Planets.AddPlanet(50, new Vector2f(0f, -0.1f), new Vector2f(W / 2 + 100, H / 2 + 100), new Color(0, 255, 0), 5);
            Planets.AddPlanet(50, new Vector2f(0.1f, -0.4f), new Vector2f(W / 2 - 100, H / 2 + 100), new Color(0, 255, 0), 5);

            field = new MassInput();
            field.Init();
        }


        public void Show()
        {
            InitWindow();

            #region events
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
                    Vector2i point = Mouse.GetPosition(window);
                    if (GetField.CheckIsMouseInRectangle())
                    {
                        GetField.InputMass();
                        if (GetField.isEditing)
                        {
                            window.TextEntered += (sender, e) =>
                            {
                                if (e.Unicode != "\b")
                                {
                                    if (GetField.isEditing)
                                    {
                                        GetField.enteredMass += e.Unicode;
                                        e.Unicode = string.Empty;
                                    }
                                }
                            };
                            window.KeyPressed += (sender, e) =>
                            {
                                if (GetField.isEditing)
                                {
                                    if (e.Code == Keyboard.Key.Backspace)
                                    {
                                        if (GetField.enteredMass.Length > 1)
                                        {
                                            GetField.enteredMass = string.Empty;
                                        }
                                    }
                                }
                            };
                        }
                    }
                    else
                    {
                        if (GetField.enteredMass == string.Empty)
                        {
                            GetField.enteredMass = "20";
                        }
                        if (int.TryParse(GetField.enteredMass, out mass))
                        {
                            Planets.ConstMass = int.Parse(GetField.enteredMass);
                        }
                        else
                        {
                            GetField.enteredMass = "20";
                            Planets.ConstMass = 20;
                        }

                        if (GetField.isEditing) GetField.isEditing = false;
                        else
                        {
                            Planets.CreatePlanet();

                            speedLine = new VertexArray(PrimitiveType.LineStrip, 2);
                            speedLine.Clear();
                            speedLine.Append(new Vertex((Vector2f)point));
                            speedLine.Append(new Vertex((Vector2f)point));
                        }
                    }
                }
            };
            window.MouseButtonReleased += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                {
                    Planets.CreateSpeed();
                }
            };
            window.KeyPressed += (sender, e) =>

            {
                if (e.Code == Keyboard.Key.P)
                {
                    isPaused = !isPaused;
                    creatingSpeed = false;
                    creatingPlanet = false;
                }
            };
            window.KeyPressed += (sender, e) =>
            {
                if (e.Code == Keyboard.Key.C)
                {
                    Planets.ClearPlanetList();
                }
            };
            #endregion

            #region drawing
            while (window.IsOpen)
            {
                delta = clock.Restart().AsSeconds();

                window.DispatchEvents();

                window.Clear();
                DrawPlanets(Planets.PlanetList);

                if (!isPaused && !isPausedCreating && !GetField.isEditing)
                {
                    CountNextState(Planets.PlanetList);
                }
                if (creatingPlanet)
                {
                    if (creatingSpeed)
                        speedLine[1] = new Vertex((Vector2f)Mouse.GetPosition(window));
                    window.Draw(speedLine);
                }
                GetField.DrawTextField(/*Planets.ConstMass.ToString()*/ GetField.enteredMass);
                window.Display();
            }
            #endregion
        }
    }
}
