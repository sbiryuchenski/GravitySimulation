using SFML.Graphics;
using SFML.System;

namespace sfml
{
    partial class Sf
    {


        public static readonly uint H = 700;
        public static readonly uint W = 1200;
        const uint FPS = 60;
        public static int Framerate = 0;
        public static RenderWindow window;
        public static View view;

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
    }
}
