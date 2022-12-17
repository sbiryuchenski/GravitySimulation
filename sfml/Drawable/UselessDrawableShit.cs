using sfml;
using sfml.Constants;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sfml
{
    /// <summary>
    /// Some drawable in menu
    /// </summary>
    class UselessDrawableShit
    {
        ButtonShape PButton, MButtonW, CButtonW, F3ButtonW, Info, LMB, RMB, Scroll;
        List<ButtonShape> buttonsList = new();
        List<ButtonShape> iconList = new();
        Font font = new Font($"{Environment.CurrentDirectory}\\Recources\\font.ttf");

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
                new Vector2f(650, Sf.H - 35),
                Buttons.PW,
                TextConstantsEN.PButton);

            MButtonW = new ButtonShape(
              ButtonsTexture.GetTexture(Buttons.MW),
              ButtonsTexture.GetTexture(Buttons.MR),
              new Vector2f(650, Sf.H - 70),
              Buttons.MW,
              TextConstantsEN.MButton);

            CButtonW = new ButtonShape(
                ButtonsTexture.GetTexture(Buttons.CW),
                ButtonsTexture.GetTexture(Buttons.CR),
                new Vector2f(750, Sf.H - 35),
                Buttons.CW,
                TextConstantsEN.CButton);

            F3ButtonW = new ButtonShape(
                ButtonsTexture.GetTexture(Buttons.F3W),
                ButtonsTexture.GetTexture(Buttons.F3R),
                new Vector2f(750, Sf.H - 70),
                Buttons.F3W,
                TextConstantsEN.F3Button);

            Info = new ButtonShape(
                ButtonsTexture.GetTexture(Buttons.InfoW),
                ButtonsTexture.GetTexture(Buttons.InfoR),
                new Vector2f(850, Sf.H - 53),
                Buttons.InfoW,
                TextConstantsEN.Info);

            LMB = new ButtonShape(
              ButtonsTexture.GetTexture(Buttons.LMB),
              ButtonsTexture.GetTexture(Buttons.LMB),
              new Vector2f(950, Sf.H - 35),
              Buttons.NotAButton,
              TextConstantsEN.LMBButton);

            RMB = new ButtonShape(
              ButtonsTexture.GetTexture(Buttons.RMB),
              ButtonsTexture.GetTexture(Buttons.RMB),
              new Vector2f(950, Sf.H - 70),
              Buttons.NotAButton,
              TextConstantsEN.RMButton);

            Scroll = new ButtonShape(
              ButtonsTexture.GetTexture(Buttons.Scroll),
              ButtonsTexture.GetTexture(Buttons.Scroll),
              new Vector2f(1100, Sf.H - 53),
              Buttons.NotAButton,
              TextConstantsEN.Scroll);

            buttonsList = new()
            { PButton, MButtonW, CButtonW, F3ButtonW, Info };

            iconList = new()
            {LMB, RMB, Scroll };

            foreach (var btn in buttonsList)
            {
                btn.DrawableText.Font = font;
                btn.DrawableText.DisplayedString = btn.text;
                btn.DrawableText.FillColor = new Color(255, 255, 255);
                btn.DrawableText.CharacterSize = 15;
                btn.DrawableText.Position = new Vector2f(0, 0);
            }
            foreach(var icn in iconList)
            {

                icn.SetWhiteTexture();
                icn.DrawableText.Font = font;
                icn.DrawableText.DisplayedString = icn.text;
                icn.DrawableText.FillColor = new Color(255, 255, 255);
                icn.DrawableText.CharacterSize = 15;
                icn.DrawableText.Position = new Vector2f(0, 0);
            }

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
                    if (!btn.isForcedRed)
                        btn.SetWhiteTexture();
                }
            }
            foreach (var btn in buttonsList)
            {
                btn.Draw(getpos);
            }
        }

        private void DrawIcons(int getpos)
        {
            foreach (var icn in iconList)
            {
                icn.Draw(getpos);
            }
        }
        public void DrawAll(int getpos, bool isDrawIcons)
        {
            DrawButtons(getpos);
            if(isDrawIcons)
                DrawIcons(getpos);
        }

        /// <summary>
        /// Get Button that under mouse now
        /// </summary>
        /// <returns></returns>
        public Buttons GetPressedButton()
        {
            Buttons localBtn = Buttons.NotAButton;
            foreach (var btn in buttonsList)
            {
                if (btn.isInButton())
                {
                    localBtn = btn.btn;
                }
            }
            return localBtn;
        }

        /// <summary>
        /// Check is mouse over any info button
        /// </summary>
        /// <returns></returns>
        public bool IsMouseInAnyButton()
        {
            bool isInButton = false;
            foreach (var btn in buttonsList)
            {
                if (btn.isInButton())
                    isInButton = true;
            }
            return isInButton;
        }

        /// <summary>
        /// Make certain button red
        /// </summary>
        /// <param name="btn"></param>
        public void MakeButtonRed(Buttons btn) => buttonsList.Select(_ => _).Where(_ => _.btn == btn).First().SetRedTexture().isForcedRed = true;

        public void MakeButtonWhite(Buttons btn) => buttonsList.Select(_ => _).Where(_ => _.btn == btn).First().SetWhiteTexture().isForcedRed = false;
    }

}

class ButtonShape : BaseShape
{
    public ButtonShape(Texture texture, Texture textureR, Vector2f position, Buttons btn, string text)
    {
        Init(texture, textureR, position, btn, text);
    }

    Texture textureR;

    private void Init(Texture texture, Texture textureR, Vector2f position, Buttons btn, string text)
    {
        this.texture = texture;
        this.textureR = textureR;
        Rectangle.Size = new Vector2f(30, 30);
        Rectangle.Texture = this.texture;
        Rectangle.Position = position;
        relativePos = (int)(Sf.H - position.Y);
        this.btn = btn;
        this.text = text;
    }

    public ButtonShape SetRedTexture()
    {
        Rectangle.Texture = textureR;
        return this;
    }

    public ButtonShape SetWhiteTexture()
    {
        Rectangle.Texture = texture;
        return this;
    }
    public Buttons btn { get; private set; }
    public bool isForcedRed { get; set; } = false;
    public string text { get; private set; } = "Text not found. Fix it!!!"; // String text
    public Text DrawableText = new Text();
    public override void Draw(int pos)
    {
        Rectangle.Position = new Vector2f(Rectangle.Position.X, Sf.H - pos + relativePos - 30);
        Rectangle.Draw(Sf.window, RenderStates.Default);
        DrawableText.Position = new Vector2f(Rectangle.Position.X + 33, Rectangle.Position.Y + 5);
        DrawableText.Draw(Sf.window, RenderStates.Default);
    }

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
    public virtual void Draw(int pos)
    {
        Rectangle.Position = new Vector2f(Rectangle.Position.X, Sf.H - pos + relativePos - 30);
        Rectangle.Draw(Sf.window, RenderStates.Default);
    }
    protected int relativePos = 0;
}

