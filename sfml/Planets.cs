using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace sfml
{
    public static class Planets
    {
        /// <summary>
        /// List of bodies in sysmem
        /// </summary>
        public static List<PBody> PlanetList { get; private set; } = new List<PBody>();
        static PBody planetCandidate = null;
        public static int ConstMass { get; set; } = 20;
        public static readonly PBody EmptyPlanet = new PBody(0.00000000001f, new Vector2f(0, 0), new Vector2f(0, 0), new Color(), 0);

        /// <summary>
        /// Add a planet to planetListз
        public static void AddPlanet(float mass, Vector2f Speed, Vector2f startPosition, Color color, int size)
        {
            PBody planet = new PBody { Mass = mass, Speed = Speed, Pos = startPosition, Color = color, Size = size };
            planet.Init();
            PlanetList.Add(planet);
        }
        public static void AddPlanet(PBody planet) => AddPlanet(planet.Mass, planet.Speed, planet.Pos, planet.Color, planet.Size);

        public static void CreatePlanet()
        {
            Sf.creatingPlanet = true;
            Sf.creatingSpeed = true;

            Sf.isPausedCreating = true;

            Vector2i point = Mouse.GetPosition(Sf.window);
            planetCandidate = new PBody { Mass = ConstMass, Speed = new Vector2f(0, 0), Pos = (Vector2f)point, Color = new Color(0, 0, 255), Size = 5 };
        }
        public static void CreateSpeed()
        {
            Vector2i point = Mouse.GetPosition(Sf.window);
            if (planetCandidate != null)
                planetCandidate.Speed = new Vector2f((point.X - planetCandidate.Pos.X) / 100, (point.Y - planetCandidate.Pos.Y) / 100);
            Sf.creatingSpeed = false;
            Sf.creatingPlanet = false;
            Sf.isPausedCreating = false;
            if (planetCandidate != null) Planets.AddPlanet(planetCandidate);
            planetCandidate = null;
        }

        /// <summary>
        /// Delet all planets from list
        /// </summary>
        public static void ClearPlanetList()
        {
            PlanetList.Clear();
            PlanetList.Add(EmptyPlanet);
        }
    }
}
