using System;
using System.Collections.Generic;
using System.Text;

namespace sfml
{
    /// <summary>
    /// Global application and graphic settings
    /// </summary>
    class Settings
    {
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public int Framerate { get; set; }
        public int AntialiasingLevel { get; set; }
        public bool TextureSmoothing { get; set; }
        public bool UsePlanetTexture { get; set; }
        public bool ShowPlanetOrbit { get; set; }
        public bool UseFPSEnhancement { get; set; }
    }
}
