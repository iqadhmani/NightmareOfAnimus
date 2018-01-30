using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NightmareOfAnimus
{
    public class Obstacle : DrawableGameComponent
    {
        public SpriteBatch spriteBatch;
        public Texture2D obstacleTex;
        public Vector2 obstaclePos;

        public Obstacle(Game game, SpriteBatch spriteBatch, Texture2D obstacleTex, Vector2 obstaclePos) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.obstacleTex = obstacleTex;
            this.obstaclePos = obstaclePos;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) && !Shared.gameOver)
            {
                obstaclePos.X -= Shared.speed.X;
            }
            else if (ks.IsKeyDown(Keys.Left) && !Shared.gameOver)
            {
                obstaclePos.X += Shared.speed.X;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(obstacleTex, obstaclePos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle GetBound()
        {
            return new Rectangle((int)obstaclePos.X, (int)obstaclePos.Y, obstacleTex.Width, obstacleTex.Height);
        }
    }
}
