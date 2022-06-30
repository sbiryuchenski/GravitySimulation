using SFML.Graphics;
using SFML.System;
using System;

namespace sfml
{
    class Fone
    {
        Image galaxy;
        Sprite sprite;
        Texture texture;
        private void Init()
        {
            galaxy = new Image($"{Environment.CurrentDirectory}\\Recources\\fone.png");
            texture = new Texture(galaxy);
            sprite = new Sprite(texture);
            float scaleX = (float)Sf.W / texture.Size.X;
            float scaleY = (float)Sf.H / texture.Size.Y;
            sprite.Scale = new Vector2f(scaleX, scaleY);
        }
        public Fone()
        {
            Init();
        }
        /// <summary>
        /// Draw fone image
        /// </summary>
        public void Draw()
        {
            sprite.Draw(Sf.window, RenderStates.Default);
        }
    }
}
