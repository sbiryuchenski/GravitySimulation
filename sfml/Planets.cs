using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace sfml
{
    public static class Planets
    {
        /// <summary>
        /// List of bodies in sysmem
        /// </summary>
        public static List<PBody> PlanetList { get;  private set; } = new List<PBody>();

        /// <summary>
        /// Add a planet to planetList
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="Speed"></param>
        /// <param name="startPosition"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        public static void AddPlanet(float mass, Vector2f Speed, Vector2f startPosition, Color color, int size)
        {
            PBody planet = new PBody { Mass = mass, Speed = Speed, Pos = startPosition, Color = color, Size = size };
            planet.Init();
            PlanetList.Add(planet);
        }
    }
}
