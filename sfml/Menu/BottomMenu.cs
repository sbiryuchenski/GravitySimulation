using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace sfml
{
    /// <summary>
    /// Bottom menu
    /// </summary>
    class BottomMenu
    {
        public bool IsDrawIcons { get; set; } = false;

        int position = 0;
        bool isHided = true;
        public BottomMenu()
        {
            Init();
        }
        RectangleShape menuRect;
        public ColorSelecter ColorSelecter { get; set; } = new ColorSelecter();
        public SizeSelecter SizeSelecter { get; set; } = new SizeSelecter();
        public MassInput MassInput { get; set; } = new MassInput();
        public MenuButton MenuButton { get; set; } = new MenuButton();
        public UselessDrawableShit UselessDrawableShit { get; set; } = new();
        private void Init()
        {
            menuRect = new RectangleShape(new Vector2f(Sf.W, 70));
            menuRect.FillColor = ColorConstants.GetColor(Colors.SemiTranspetentBlack);
            menuRect.Position = new Vector2f(0, Sf.H - position);
        }
        public void Draw(bool isMove, string fieldText)
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
                MassInput.Draw(fieldText, position);
                UselessDrawableShit.DrawAll(position, IsDrawIcons);
            }
            MenuButton.Draw(position, isMove);
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
            MassInput.isEditing = false;
        }
        public bool isMouseInMenu()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            return menuRect.GetGlobalBounds().Contains(mouse.X, mouse.Y);
        }
        public Buttons GetPressedButton() => UselessDrawableShit.GetPressedButton();
    }
    class MenuButton
    {
        RectangleShape menuButton;
        Texture menuButtonTextureUp;
        Texture menuButtonTextureDown;
        bool isUp = true;
        public MenuButton()
        {
            Init();
        }
        private void Init()
        {
            menuButtonTextureUp = new Texture($"{Environment.CurrentDirectory}\\Recources\\menuButtonUp.png");
            menuButtonTextureDown = new Texture($"{Environment.CurrentDirectory}\\Recources\\menuButtonDown.png");
            menuButtonTextureUp.Smooth = true;
            menuButtonTextureDown.Smooth = true;
            menuButton = new RectangleShape(new Vector2f(20, 20));
            menuButton.FillColor = Color.White;
            menuButton.Position = new Vector2f(5, Sf.H - 95);
            menuButton.Texture = menuButtonTextureUp;
        }
        /// <summary>
        /// Check is cursor in color and retur that color
        /// </summary>
        /// <returns></returns>
        private void SelecterButtonThickness()
        {
            if (IsInButton())
            {
                menuButton.OutlineThickness = 1;
                menuButton.OutlineColor = Color.White;
            }
            else
            {
                menuButton.OutlineThickness = 0;
            }
        }
        public bool IsInButton()
        {
            var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
            return menuButton.GetGlobalBounds().Contains(mouse.X, mouse.Y);
        }
        public void Draw(int position, bool isUp)
        {
            if (!isUp)
                menuButton.Texture = menuButtonTextureUp;
            else
                menuButton.Texture = menuButtonTextureDown;
            SelecterButtonThickness();
            menuButton.Position = new Vector2f(10, Sf.H - position - 30);
            menuButton.Draw(Sf.window, RenderStates.Default);
        }
    }
}
