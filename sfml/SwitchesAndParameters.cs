﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfml
{
    partial class Sf
    {

        public static readonly uint W = 1280; // use 16:9 plz
        public static readonly uint H = 720;

        const uint FPS = 60;
        public static int Framerate = 8; // idk why, maybe should delete
        public static RenderWindow window;
        public static View view;
        public static ContextSettings settings = new ContextSettings();

        Clock clock = new Clock();
        float delta = 100f;
        VertexArray speedLine;
        int mass;
        Vector2f currentMousePosition;
        Vector2f newPosition;

        bool isPaused = false;
        static public bool isPausedCreating = false;
        static public bool creatingSpeed = false;
        static public bool creatingPlanet = false;
        bool isWindowMoving = false;
        bool isSystemInfoDisplay = false;

        // private MassInput field; // dont use it. It won't work
        private MassInput GetField { get; set; } = new MassInput();
        private TextElements AllText { get; set; } = new TextElements();
        private ColorSelecter ColorSelecter { get; set; } = new ColorSelecter();
        private SizeSelecter SizeSelecter { get; set; } = new SizeSelecter();
        private OrbitPreweiw OrbitPreweiw { get; set; }
        private Fone Fone { get; set; } = new Fone();
    }
}
