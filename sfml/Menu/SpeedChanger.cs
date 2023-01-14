using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace sfml.Menu
{
    class SpeedChanger
    {
        RectangleShape scroll;
        Texture scrollTexture;
        RectangleShape scrollLine;
        public bool isChanging = false;
        float H = Sf.H + 28;
        public SpeedChanger()
        {
            Init();
        }

        void Init()
        {
            scrollTexture = new Texture($"{Environment.CurrentDirectory}\\Recources\\Scroll.png");
            scrollTexture.Smooth = true;

            scroll = new RectangleShape();
            scroll.FillColor = Color.White;
            scroll.Size = new SFML.System.Vector2f(8, 20);
            scroll.Texture = scrollTexture;
            scroll.Position = new SFML.System.Vector2f(Sf.W - 300, H);

            scrollLine = new RectangleShape();
            scrollLine.FillColor = Color.White;
            scrollLine.Size = new SFML.System.Vector2f(200, 2);
            scrollLine.Position = new SFML.System.Vector2f(Sf.W - 300, H);
        }

        public void ChangeSpeed()
        {
            if (isChanging)
            {
                var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
                uint mouseX = (uint)mouse.X;
                if (mouse.X < Sf.W - 300 || mouse.X > Sf.W - 100)
                {
                    if (mouse.X < Sf.W - 300) mouseX = Sf.W - 300;
                    else if (mouse.X > Sf.W - 100) mouseX = Sf.W - 100;
                }

                scroll.Position = new Vector2f((float)mouseX, scroll.Position.Y);
            }
        }

        public bool IsInScroll()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            if (scroll.GetGlobalBounds().Contains(mouse.X, mouse.Y))
            {
                return true;
            }
            else return false;
        }

        public void Draw(int getpos)
        {
            scroll.Position = new Vector2f(scroll.Position.X, H - getpos);
            scrollLine.Position = new Vector2f(scrollLine.Position.X, H - getpos+9);
            ChangeSpeed();
            scrollLine.Draw(Sf.window, RenderStates.Default);
            scroll.Draw(Sf.window, RenderStates.Default);
            Sf.Speed = GetSpeed();
        }

        public int GetSpeed()
        {
            float speed = (scroll.Position.X - (Sf.W - 300)) / 10;
            speed = (float)Math.Round(speed);
            if (speed < 1) speed = 1;
            if (speed > 20) speed = 20;
            return (int)speed;
        }
    }
}
