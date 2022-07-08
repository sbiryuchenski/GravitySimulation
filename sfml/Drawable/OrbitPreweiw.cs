using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace sfml
{
    class OrbitPreweiw
    {
        public OrbitPreweiw(PBody creatingBody)
        {
            Init(creatingBody);
        }
        uint n = 100;
        PBody creatingBody;
        private void Init(PBody creatingBody)
        {

            this.creatingBody = creatingBody;
        }

        /// <summary>
        /// Count full orbit preweiw
        /// </summary>
        /// <param name="planets"></param>
        void CountPreweiw()
        {
            creatingBody = new PBody(Planets.planetCandidate);
            Vector2f point = Sf.window.MapPixelToCoords(Mouse.GetPosition(Sf.window));
            creatingBody.Init();
            creatingBody.Speed = new Vector2f((point.X - creatingBody.Pos.X) / 100, (point.Y - creatingBody.Pos.Y) / 100);
           // creatingBody.line.Line;
           // creatingBody.line.Line.Clear();
            while (creatingBody.line.Line.VertexCount < 400)
            {
                foreach (var planet in Planets.PlanetList)
                {
                    creatingBody.CountOffset(planet);
                    creatingBody.SetVelocity();
                }
                creatingBody.SetOffset();
            }
        }
        public void DrawPreweiwLine()
        {
            CountPreweiw();
            creatingBody.line.Line.Draw(Sf.window, RenderStates.Default);
        }
    }
}
