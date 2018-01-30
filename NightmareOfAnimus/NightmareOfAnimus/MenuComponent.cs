using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NightmareOfAnimus
{
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont;
        private SpriteFont hilightFont;
        private List<string> menuItems;
        private SoundEffect sfxMenuSelect;
        private Texture2D background;
        private Song mainMenuTheme; 
        public int SelectedIndex { get; set; }
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;
        private KeyboardState oldState;

        private Vector2 dimension = new Vector2(800, 450);
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private const int DELAY = 10;
        private int delayCounter;

        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont hilightFont, String[] menus, SoundEffect sfxMenuSelect, Texture2D background, Song mainMenuTheme) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menus.ToList();
            this.sfxMenuSelect = sfxMenuSelect;
            this.background = background;
            this.mainMenuTheme = mainMenuTheme;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainMenuTheme);
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);

            CreateFrames();
        }

        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 8; i++)
            {
                int x = 0;
                int y = i * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                frames.Add(r);
            }
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > DELAY)
            {
                frameIndex++;
                if (frameIndex > 7)
                {
                    frameIndex = 0;
                    //this.Enabled = false;
                    //this.Enabled = false;
                }
                delayCounter = 0;
            }

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                sfxMenuSelect.Play();
                SelectedIndex++;
                if (SelectedIndex == menuItems.Count)
                {
                    SelectedIndex = 0;
                } 
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                sfxMenuSelect.Play();
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            float menuLength = 0;

            spriteBatch.Begin();
            
            spriteBatch.Draw(background, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), frames[0], Color.White);
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), frames[frameIndex], Color.White);
            }

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i], new Vector2(tempPos.X - (hilightFont.MeasureString(menuItems[i]).X / 2) ,tempPos.Y), hilightColor);
                    tempPos.Y += regularFont.LineSpacing;
                    menuLength += hilightFont.MeasureString(menuItems[i]).Y;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], new Vector2(tempPos.X - (regularFont.MeasureString(menuItems[i]).X / 2) ,tempPos.Y), regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                    menuLength += regularFont.MeasureString(menuItems[i]).Y;
                }
            }
            position.Y = Shared.stage.Y - menuLength;
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
