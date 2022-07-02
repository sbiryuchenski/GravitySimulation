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
            #region events

            window.Closed += (obj, e) => { window.Close(); };
            window.KeyPressed += (sender, e) =>
            {
                if (e.Code == Keyboard.Key.Escape)
                {
                    window.Close();
                }
                if (e.Code == Keyboard.Key.P)
                {
                    isPaused = !isPaused;
                    creatingSpeed = false;
                    creatingPlanet = false;
                }
                if (e.Code == Keyboard.Key.C)
                {
                    Planets.ClearPlanetList();
                }
                if (e.Code == Keyboard.Key.F3)
                {
                    isSystemInfoDisplay = !isSystemInfoDisplay;
                }
                if(e.Code == Keyboard.Key.M)
                {
                    isMenuShowing = !isMenuShowing;
                }
            };
            window.MouseButtonPressed += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                {
                    Vector2i point = (Vector2i)(window.MapPixelToCoords(Mouse.GetPosition(window)));
                    if (Menu.MassInput.CheckIsMouseInRectangle())
                    {
                        Menu.MassInput.InputMass();
                        if (Menu.MassInput.isEditing)
                        {
                            window.TextEntered += (sender, e) =>
                            {
                                if (e.Unicode != "\b")
                                {
                                    if (Menu.MassInput.isEditing)
                                    {
                                        if (Menu.MassInput.enteredMass.Length < 9)
                                        {
                                            if (Int32.TryParse(e.Unicode, out mass))
                                            {
                                                Menu.MassInput.enteredMass += e.Unicode;
                                                e.Unicode = string.Empty;
                                            }
                                        }
                                    }
                                }
                            };
                            window.KeyPressed += (sender, e) =>
                            {
                                if (Menu.MassInput.isEditing)
                                {
                                    if (e.Code == Keyboard.Key.Backspace)
                                    {
                                        if (Menu.MassInput.enteredMass.Length > 1)
                                        {
                                            Menu.MassInput.enteredMass = string.Empty;
                                        }
                                    }
                                }
                            };
                        }
                    }
                    else
                    {
                        if (Menu.ColorSelecter.isInColor())
                        {
                            Menu.ColorSelecter.SelectColor();
                        }
                        else if (Menu.SizeSelecter.isInSize())
                        {
                            Menu.SizeSelecter.SelectSize();
                        }
                        else if (Menu.MenuButton.IsInButton())
                        {
                            isMenuShowing = !isMenuShowing;
                        }
                        else
                        {
                            if (Menu.MassInput.enteredMass == string.Empty)
                            {
                                Menu.MassInput.enteredMass = "20";
                            }
                            if (int.TryParse(Menu.MassInput.enteredMass, out mass))
                            {
                                Planets.ConstMass = int.Parse(Menu.MassInput.enteredMass);
                            }
                            else
                            {
                                Menu.MassInput.enteredMass = "20";
                                Planets.ConstMass = 20;
                            }

                            if (Menu.MassInput.isEditing) Menu.MassInput.isEditing = false;
                            else
                            {
                                Planets.CreatePlanet();
                                OrbitPreweiw = new OrbitPreweiw(Planets.planetCandidate);
                                speedLine = new VertexArray(PrimitiveType.LineStrip, 2);
                                speedLine.Clear();
                                speedLine.Append(new Vertex((Vector2f)point));
                                speedLine.Append(new Vertex((Vector2f)point));
                            }
                        }
                    }
                }
                if (e.Button == Mouse.Button.Right)
                {
                    currentMousePosition = (Vector2f)Mouse.GetPosition(window);
                    isWindowMoving = true;
                }
            };
            window.MouseButtonReleased += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                {
                    Planets.CreateSpeed();
                }
                if (e.Button == Mouse.Button.Right)
                {
                    isWindowMoving = false;
                }
            };
            window.MouseMoved += (sender, e) =>
             {
                 newPosition = new Vector2f(e.X, e.Y);
                 if (isWindowMoving)
                     MoveWindow();
                 currentMousePosition = new Vector2f(e.X, e.Y);
             };
            window.MouseWheelScrolled += (sender, e) =>
            {
                ZoomWindow(e.Delta);
            };
            if (isWindowMoving)
                MoveWindow();
            #endregion

            #region drawing
            while (window.IsOpen)
            {
                delta = clock.Restart().AsSeconds();
                Framerate = (int)Math.Round(1 / delta);

                window.DispatchEvents();
                window.Clear();

                var old = window.GetView();
                window.SetView(window.DefaultView);
                Fone.Draw();
                Menu.Draw(isMenuShowing, Menu.MassInput.enteredMass);
               // GetField.DrawTextField(GetField.enteredMass);
                AllText.DrawSystemInfoText(isSystemInfoDisplay);
                window.SetView(view);

                if (!isPaused && !isPausedCreating && !Menu.MassInput.isEditing)
                {
                    Planets.CountNextState();
                }

                DrawSpeedLine(); // Draw speed vector of creating planet
                DrawPlanets(Planets.PlanetList);

                if (isPausedCreating)
                {
                    this.OrbitPreweiw.DrawPreweiwLine();
                }

                window.Display();
            }
            #endregion
        }
    }
}
