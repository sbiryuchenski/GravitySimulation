﻿using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System;

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
                DrawPlanet(planet);
            }
            foreach (var planet in planets)
            {
                DrawOrbit(planet);
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
                view.Zoom(0.95f);
            }
            else
            {
                view.Zoom(1.05f);
            }
            window.SetView(view);
        }
        void InitWindow()
        {
            VideoMode mode = new VideoMode(W, H);
            window = new RenderWindow(mode, "хуй");
            window.SetFramerateLimit(FPS);
            view = new View(new FloatRect(0, 0, W, H));
            window.SetView(view);

            Planets.AddPlanet(Planets.EmptyPlanet);

            Planets.AddPlanet(1, new Vector2f(0.1f, 0.1f), new Vector2f(W / 2, H / 2), new Color(255, 0, 0), 5);
            Planets.AddPlanet(50, new Vector2f(0f, -0.1f), new Vector2f(W / 2 + 100, H / 2 + 100), new Color(0, 255, 0), 5);
            Planets.AddPlanet(50, new Vector2f(0.1f, -0.4f), new Vector2f(W / 2 - 100, H / 2 + 100), new Color(0, 255, 0), 5);
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
                if(e.Code == Keyboard.Key.F3)
                {
                    isSystemInfoDisplay = !isSystemInfoDisplay;
                }
            };
            window.MouseButtonPressed += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                {
                    Vector2i point = (Vector2i)(window.MapPixelToCoords(Mouse.GetPosition(window)));
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
                        if (ColorSelecter.isInColor())
                        {
                           ColorSelecter.SelectColor();
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
                }
                if(e.Button == Mouse.Button.Right)
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
                if(e.Button == Mouse.Button.Right)
                {
                    isWindowMoving = false;
                }
            };
            window.MouseMoved += (sender, e) =>
             {
                 newPosition = new Vector2f(e.X, e.Y);
                 if(isWindowMoving)
                    MoveWindow();
                 currentMousePosition = new Vector2f(e.X, e.Y);
             };
            window.MouseWheelScrolled += (sender, e) =>
            {
                ZoomWindow(e.Delta);
            };
            if(isWindowMoving)
                MoveWindow();
            #endregion

            #region drawing
            while (window.IsOpen)
            {
                
                delta = clock.Restart().AsSeconds();
                Framerate = (int)Math.Round(1/delta);

                window.DispatchEvents();
                window.Clear();

                if (!isPaused && !isPausedCreating && !GetField.isEditing)
                {
                    Planets.CountNextState();
                }

                DrawSpeedLine();

                DrawPlanets(Planets.PlanetList);

                var old = window.GetView();
                window.SetView(window.DefaultView);
                GetField.DrawTextField(GetField.enteredMass);
                AllText.DrawSystemInfoText(isSystemInfoDisplay);
                ColorSelecter.DrawColorSelecter();
                window.SetView(view);

                window.Display();
            }
            #endregion
        }
    }
}