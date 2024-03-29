﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace sfml
{
    public static class Planets
    {
        /// <summary>
        /// List of bodies in sysmem
        /// </summary>
        public static List<PBody> PlanetList { get; private set; } = new List<PBody>();
        public static PBody planetCandidate = null; // New planet when creating
        public static int ConstMass { get; set; } = 20; // Mass of planet when it's creating
        public static Color ConstColor { get; set; } = ColorConstants.GetColor(Colors.Blue);
        public static int ConstSize { get; set; } = 5;
        public static readonly PBody EmptyPlanet = new PBody(0, new Vector2f(0, 0), new Vector2f(0, 0), new Color(), 0); // Need to work counting algoritm

        /// <summary>
        /// Add a planet to planetList
        public static void AddPlanet(float mass, Vector2f Speed, Vector2f startPosition, Color color, int size)
        {
            PBody planet = new PBody { Mass = mass, Speed = Speed, Pos = startPosition, Color = color, Size = size };
            planet.Init();
            PlanetList.Add(planet);
        }
        public static void AddPlanet(PBody planet) => AddPlanet(planet.Mass, planet.Speed, planet.Pos, planet.Color, planet.Size);

        /// <summary>
        /// Create new planet and add it to planet list
        /// </summary>
        public static void CreatePlanet()
        {
            Sf.creatingPlanet = true;
            Sf.creatingSpeed = true;
            Sf.isPausedCreating = true;

            Vector2f point = Sf.window.MapPixelToCoords(Mouse.GetPosition(Sf.window));

            planetCandidate = new PBody { Mass = ConstMass, Speed = new Vector2f(0, 0), Pos = (Vector2f)point, Color = ConstColor, Size = ConstSize };
        }
        public static void CreateSpeed()
        {
            Vector2f point = Sf.window.MapPixelToCoords(Mouse.GetPosition(Sf.window));
            if (planetCandidate != null)
                planetCandidate.Speed = new Vector2f((point.X - planetCandidate.Pos.X) / 100, (point.Y - planetCandidate.Pos.Y) / 100);
            Sf.creatingSpeed = false;
            Sf.creatingPlanet = false;
            Sf.isPausedCreating = false;
            if (planetCandidate != null) AddPlanet(planetCandidate);
            planetCandidate = null;
        }

        /// <summary>
        /// Delet all planets from list
        /// </summary>
        public static void ClearPlanetList()
        {
            PlanetList.Clear();
            AddPlanet(EmptyPlanet);
        }
        public static void CountNextState(int speed = 1)
        {
            for (int i = 0; i < speed; i++)
            {
                foreach (var planet in PlanetList)
                {
                    foreach (var planet2 in PlanetList)
                    {
                        if (planet != planet2)
                        {
                            planet.CountOffset(planet2);
                        }
                    }
                }
                foreach (var planet in PlanetList)
                {
                    planet.SetVelocity();
                }
            }
        }
    }
}
