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
    public class CollisionDetection : GameComponent
    {
        Obstacle obstacle;
        Knight knight;

        public CollisionDetection(Game game, Knight knight, Obstacle obstacle) : base(game)
        {
            this.knight = knight;
            this.obstacle = obstacle;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (knight.GetBound().Intersects(obstacle.GetBound()))
            {
                Shared.speed.X = 0;
                if (obstacle.obstaclePos.X - ((Shared.stage.X / 2) - (knight.characterInGameWidth / 2)) < 0)
                {
                    if (ks.IsKeyDown(Keys.Right))
                    {
                        Shared.speed.X = 2;
                    }
                }
                else
                {
                    if (ks.IsKeyDown(Keys.Left))
                    {
                        Shared.speed.X = 2;
                    }
                }
            }            
            base.Update(gameTime);
        }
    }
}
