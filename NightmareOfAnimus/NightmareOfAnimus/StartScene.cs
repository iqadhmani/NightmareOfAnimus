using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace NightmareOfAnimus
{
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        private SpriteBatch spriteBatch;
        string[] menuItems = { "New Game", "Help", "Credit", "Quit" };
        public SpriteFont regularFont;

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            Texture2D background = game.Content.Load<Texture2D>("Images/Main_Menu_Background");
            regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont hilightFont = game.Content.Load<SpriteFont>("Fonts/hilightFont");
            SoundEffect sfxMenuSelect = game.Content.Load<SoundEffect>("SFX/CURSOL_SELECT");
            Song mainMenuTheme = game.Content.Load<Song>("OST/Sono_Chi_no_Sadame");
            


            Menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, menuItems, sfxMenuSelect, background, mainMenuTheme);
            this.Components.Add(Menu);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
