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
    public class EnemyCollision : GameComponent
    {
        Knight knight;
        Enemy skeleton;

        public EnemyCollision(Game game, Knight knight, Enemy skeleton) : base(game)
        {
            this.knight = knight;
            this.skeleton = skeleton;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            
            if (knight.GetBound().Intersects(skeleton.GetBound()))
            {
                Shared.speed.X = 0;
                if (skeleton.position.X - ((Shared.stage.X / 2) - (knight.characterInGameWidth / 2)) < 0)
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
                if (skeleton.Enabled == false)
                {
                    Shared.speed.X = 2;
                }

                if (skeleton.attackIndex == 7)
                {
                    knight.hp -= 1 * 2;
                    if (knight.hp <= 0)
                    {
                        knight.currentPlayerStatus = knight.playerStatus[3];
                    }
                }
                if (knight.attackIndex == 5 && skeleton.position.X - (Shared.stage.X/2) <= 15)
                {
                        skeleton.hp -= 1;
                    if (skeleton.hp <= 0)
                    {
                        skeleton.currentSkeletonStatus = skeleton.skeletonStatus[4];
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
