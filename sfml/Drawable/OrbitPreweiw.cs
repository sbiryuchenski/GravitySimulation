using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
            Color pointColor = Constants.GetColor(Colors.Grey);
            while (creatingBody.line.Line.VertexCount < 500)
            {
                foreach (var planet in Planets.PlanetList)
                {
                    creatingBody.CountOffset(planet);
                    creatingBody.SetVelocity();
                }
                if (creatingBody.line.Line.VertexCount > 250)
                {
                    pointColor = new Color(pointColor.R, pointColor.G, pointColor.B, (byte)(500 - creatingBody.line.Line.VertexCount));
                }
                creatingBody.line.Color = pointColor;
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
