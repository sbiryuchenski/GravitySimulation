using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace sfml
{
    partial class Sf
    {
        private void DrawAll()
        {
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
                    Planets.CountNextState(speed);
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
