using sfml;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace sfml
{
    class UselessDrawableShit
    {
        ButtonShape PButton, MButtonW, CButtonW, F3ButtonW, LMB, RMB;
        List<ButtonShape> buttonsList = new();

        public UselessDrawableShit()
        {
            Init();
        }

        private void Init()
        {
            #region ButtonShape

            PButton = new ButtonShape(
                ButtonsTexture.GetTexture(Buttons.PW),
                ButtonsTexture.GetTexture(Buttons.PR),
                new Vector2f(650, Sf.H-35),
                Buttons.PW);

            MButtonW = new ButtonShape(
              ButtonsTexture.GetTexture(Buttons.MW),
              ButtonsTexture.GetTexture(Buttons.MR),
              new Vector2f(650, Sf.H-70),
              Buttons.MW);

            CButtonW = new ButtonShape(
                ButtonsTexture.GetTexture(Buttons.CW),
                ButtonsTexture.GetTexture(Buttons.CR),
                new Vector2f(750, Sf.H-35),
                Buttons.CW);

            F3ButtonW = new ButtonShape(
                ButtonsTexture.GetTexture(Buttons.F3W),
                ButtonsTexture.GetTexture(Buttons.F3R),
                new Vector2f(750, Sf.H-70),
                Buttons.F3W);

            LMB = new ButtonShape(
              ButtonsTexture.GetTexture(Buttons.LMB),
              ButtonsTexture.GetTexture(Buttons.LMB),
              new Vector2f(850, Sf.H-35),
              Buttons.NotAButton);

            RMB = new ButtonShape(
              ButtonsTexture.GetTexture(Buttons.RMB),
              ButtonsTexture.GetTexture(Buttons.RMB),
              new Vector2f(850, Sf.H-70),
              Buttons.NotAButton);

            buttonsList = new()
            { PButton, MButtonW, CButtonW, F3ButtonW, LMB, RMB };

            #endregion
        }
        private void DrawButtons(int getpos)
        {
            foreach (var btn in buttonsList)
            {
                if (btn.isInButton())
                {
                    btn.SetRedTexture();
                }
                else
                {
                    btn.SetWhiteTexture();
                }
            }
            foreach (var btn in buttonsList)
            {
                btn.Draw(getpos);
            }
        }
        public void DrawAll(int getpos)
        {
            DrawButtons(getpos);
        }
        public Buttons GetPressedButton()
        {
            Buttons localBtn = Buttons.NotAButton;
            foreach(var btn in buttonsList)
            {
                if(btn.isInButton())
                {
                    localBtn = btn.btn;
                }
            }
            return localBtn;
        }
        public bool IsMouseInAnyButton()
        {
            bool isInButton = false;
            foreach(var btn in buttonsList)
            {
                if (btn.isInButton())
                    isInButton = true;
            }
            return isInButton;
        }
    }
}

class ButtonShape : BaseShape
{
    public ButtonShape(Texture texture, Texture textureR, Vector2f position, Buttons btn)
    {
        Init(texture, textureR, position, btn);
    }

    Texture textureR;

    private void Init(Texture texture, Texture textureR, Vector2f position, Buttons btn)
    {
        this.texture = texture;
        this.textureR = textureR;
        Rectangle.Size = new Vector2f(30, 30);
        Rectangle.Texture = this.texture;
        Rectangle.Position = position;
        relativePos = (int)(Sf.H - position.Y);
        this.btn = btn;
    }

    public void SetRedTexture()
    {
        Rectangle.Texture = textureR;
    }

    public void SetWhiteTexture()
    {
        Rectangle.Texture = texture;
    }
    public Buttons btn { get; private set; }
}

class BaseShape
{
    protected RectangleShape Rectangle { get; set; } = new();
    protected Texture texture;

    public bool isInButton()
    {
        var mouse = (Vector2f)Mouse.GetPosition(Sf.window);
        if (Rectangle.GetGlobalBounds().Contains(mouse.X, mouse.Y))
        {
            return true;
        }
        return false;
    }
    public void Draw(int pos)
    {
        Rectangle.Position = new Vector2f(Rectangle.Position.X, Sf.H - pos + relativePos - 30);
        Rectangle.Draw(Sf.window, RenderStates.Default);
    }
    protected int relativePos = 0;
}

