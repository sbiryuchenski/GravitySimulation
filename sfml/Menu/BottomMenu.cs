using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace sfml
{
    /// <summary>
    /// Bottom menu
    /// </summary>
    class BottomMenu
    {
        int position = 0;
        bool isHided = true;
        public BottomMenu()
        {
            Init();
        }
        RectangleShape menuRect;
        public ColorSelecter ColorSelecter { get; set; } = new ColorSelecter();
        public SizeSelecter SizeSelecter { get; set; } = new SizeSelecter();
        private void Init()
        {
            menuRect = new RectangleShape(new Vector2f(Sf.W, 70));
            menuRect.FillColor = Constants.GetColor(Colors.SemiTranspetentBlack);
            menuRect.Position = new Vector2f(0, Sf.H - position);
        }
        public void Draw(bool isMove)
        {
            if (isMove)
                 ShowMenu();
            else
                 HideMenu();
            if (!isHided)
            {
                menuRect.Draw(Sf.window, RenderStates.Default);
                ColorSelecter.Draw(position);
                SizeSelecter.Draw(position);
            }
        }
        private void ShowMenu()
        {
            isHided = false;
            if (position < 70)
            {
                menuRect.Position = new Vector2f(0, Sf.H - position);
                position += 3;
            }
        }
        private void HideMenu()
        {
            if (position > 0)
            {
                menuRect.Position = new Vector2f(0, Sf.H - position);
                position -= 3;
            }
            else isHided = true;
        }
    }
}
