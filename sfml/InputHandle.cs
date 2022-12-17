using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace sfml
{
    partial class Sf
    {
        private void Pause()
        {
            isPaused = !isPaused;
            creatingSpeed = false;
            creatingPlanet = false;
        }

        private void HandleInput()
        {
            #region events
            Buttons pressedButton = Buttons.NotAButton;

            window.Closed += (obj, e) => { window.Close(); };
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
                        else if (isMenuShowing && Menu.UselessDrawableShit.IsMouseInAnyButton())
                        {
                                pressedButton = Menu.GetPressedButton();
                            switch (pressedButton)
                            {
                                case Buttons.CW:
                                    Planets.ClearPlanetList();
                                    break;
                                case Buttons.MW:
                                    isMenuShowing = !isMenuShowing;
                                    break;
                                case Buttons.PW:
                                    Pause();
                                    break;
                                case Buttons.F3W:
                                    isSystemInfoDisplay = !isSystemInfoDisplay;
                                    break;
                                case Buttons.InfoW:
                                    Menu.IsDrawIcons = !Menu.IsDrawIcons;
                                    break;
                            }
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
                                if (!Menu.isMouseInMenu())
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
                }
                if (e.Button == Mouse.Button.Right)
                {
                    currentMousePosition = (Vector2f)Mouse.GetPosition(window);
                    isWindowMoving = true;
                }
            };
            window.KeyPressed += (sender, e) =>
            {
                if (e.Code == Keyboard.Key.Escape)
                {
                    window.Close();
                }
                if (e.Code == Keyboard.Key.P)
                {
                    Pause();
                    Menu.UselessDrawableShit.MakeButtonRed(Buttons.PW);
                }
                if (e.Code == Keyboard.Key.C)
                {
                    Planets.ClearPlanetList();
                    Menu.UselessDrawableShit.MakeButtonRed(Buttons.CW);
                }
                if (e.Code == Keyboard.Key.F3)
                {
                    isSystemInfoDisplay = !isSystemInfoDisplay;
                    Menu.UselessDrawableShit.MakeButtonRed(Buttons.F3W);
                }
                if (e.Code == Keyboard.Key.M)
                {
                    isMenuShowing = !isMenuShowing;
                    Menu.UselessDrawableShit.MakeButtonRed(Buttons.MW);
                }
            };
            window.KeyReleased += (sender, e) =>
            {
                if (e.Code == Keyboard.Key.P)
                {
                    Menu.UselessDrawableShit.MakeButtonWhite(Buttons.PW);
                }
                if (e.Code == Keyboard.Key.C)
                {
                    Menu.UselessDrawableShit.MakeButtonWhite(Buttons.CW);
                }
                if (e.Code == Keyboard.Key.F3)
                {
                    Menu.UselessDrawableShit.MakeButtonWhite(Buttons.F3W);
                }
                if (e.Code == Keyboard.Key.M)
                {
                    Menu.UselessDrawableShit.MakeButtonWhite(Buttons.MW);
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
        }
    }
}
