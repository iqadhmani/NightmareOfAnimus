using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NightmareOfAnimus
{
    public class World : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D sky;
        Texture2D treeFaded;
        Texture2D treeFar;
        Texture2D lighFar;
        Texture2D treeNearFar;
        Texture2D treeClose;
        Texture2D lightClose;
        Texture2D treeClosest;
        Texture2D topForest;
        public Texture2D terrain { get; set; }
        Texture2D grass;

        public Vector2 speed;
        public Rectangle position1;
        public Rectangle position2;

        Song gameTheme;

        public World(Game game, SpriteBatch spriteBatch, Texture2D sky, Texture2D treeFaded, Texture2D treeFar, Texture2D lighFar, Texture2D treeNearFar, Texture2D treeClose,
                        Texture2D lightClose, Texture2D treeClosest, Texture2D topForest, Texture2D terrain, Texture2D grass, Song gameTheme) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.sky = sky;
            this.treeFaded = treeFaded;
            this.treeFar = treeFar;
            this.lighFar = lighFar;
            this.treeNearFar = treeNearFar;
            this.treeClose = treeClose;
            this.lightClose = lightClose;
            this.treeClosest = treeClosest;
            this.topForest = topForest;
            this.terrain = terrain;
            this.grass = grass;
            this.gameTheme = gameTheme;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(gameTheme);
            speed = new Vector2(1, 0);
            position1 = new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y);
            position2 = new Rectangle((int)Shared.stage.X, 0, (int)Shared.stage.X, (int)Shared.stage.Y);

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) && !Shared.gameOver)
            {
                if (position1.X > -Shared.stage.X)
                {
                    position1.X -= (int)Shared.speed.X;
                }
                else
                {
                    position1.X = position2.X + (int)Shared.stage.X - (int)Shared.speed.X;
                }

                if (position2.X > -Shared.stage.X)
                {
                    position2.X -= (int)Shared.speed.X;
                }
                else
                {
                    position2.X = position1.X + (int)Shared.stage.X - (int)Shared.speed.X;
                }
            }

            else if (ks.IsKeyDown(Keys.Left) && !Shared.gameOver)
            {
                if (position2.X < Shared.stage.X)
                {
                    position2.X += (int)Shared.speed.X;
                }
                else
                {
                    position2.X = position1.X - (int)Shared.stage.X + (int)Shared.speed.X;
                }

                if (position1.X < Shared.stage.X)
                {
                    position1.X += (int)Shared.speed.X;
                }
                else
                {
                    position1.X = position2.X - (int)Shared.stage.X + (int)Shared.speed.X;
                }

                
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(sky, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);

            spriteBatch.Draw(treeFaded, position1, new Rectangle(0, 0, treeFaded.Width, treeFaded.Height), Color.White);
            spriteBatch.Draw(treeFaded, position2, new Rectangle(0, 0, treeFaded.Width, treeFaded.Height), Color.White);

            spriteBatch.Draw(treeFar, position1, new Rectangle(0, 0, treeFar.Width, treeFar.Height), Color.White);
            spriteBatch.Draw(treeFar, position2, new Rectangle(0, 0, treeFar.Width, treeFar.Height), Color.White);

            spriteBatch.Draw(lighFar, position1, new Rectangle(0, 0, lighFar.Width, lighFar.Height), Color.White);
            spriteBatch.Draw(lighFar, position2, new Rectangle(0, 0, lighFar.Width, lighFar.Height), Color.White);

            spriteBatch.Draw(treeNearFar, position1, new Rectangle(0, 0, treeNearFar.Width, treeNearFar.Height), Color.White);
            spriteBatch.Draw(treeNearFar, position2, new Rectangle(0, 0, treeNearFar.Width, treeNearFar.Height), Color.White);

            spriteBatch.Draw(treeClose, position1, new Rectangle(0, 0, treeClose.Width, treeClose.Height), Color.White);
            spriteBatch.Draw(treeClose, position2, new Rectangle(0, 0, treeClose.Width, treeClose.Height), Color.White);

            spriteBatch.Draw(lightClose, position1, new Rectangle(0, 0, lightClose.Width, lightClose.Height), Color.White);
            spriteBatch.Draw(lightClose, position2, new Rectangle(0, 0, lightClose.Width, lightClose.Height), Color.White);

            spriteBatch.Draw(treeClosest, position1, new Rectangle(0, 0, treeClosest.Width, treeClosest.Height), Color.White);
            spriteBatch.Draw(treeClosest, position2, new Rectangle(0, 0, treeClosest.Width, treeClosest.Height), Color.White);

            spriteBatch.Draw(topForest, position1, new Rectangle(0, 0, topForest.Width, topForest.Height), Color.White);
            spriteBatch.Draw(topForest, position2, new Rectangle(0, 0, topForest.Width, topForest.Height), Color.White);

            spriteBatch.Draw(terrain, position1, new Rectangle(0, 0, terrain.Width, terrain.Height), Color.White);
            spriteBatch.Draw(terrain, position2, new Rectangle(0, 0, terrain.Width, terrain.Height), Color.White);

            spriteBatch.Draw(grass, position1, new Rectangle(0, 0, grass.Width, grass.Height), Color.White);
            spriteBatch.Draw(grass, position2, new Rectangle(0, 0, grass.Width, grass.Height), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
