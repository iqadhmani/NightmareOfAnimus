using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace NightmareOfAnimus
{
    public class Knight : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D knightTexIdle;
        private Texture2D knightTexWalk;
        private Texture2D knightTexAttack;
        private Texture2D knightTexDeath;

        private const int IDLE_TEX_FRAMES = 4;
        private const int WALK_TEX_FRAMES = 8;
        private const int ATTACK_TEX_FRAMES = 10;
        private const int DEATH_TEX_FRAMES = 9;

        World world;

        private Vector2 dimension = new Vector2(42, 42);
        private const float CHARACTER_WIDTH_RATIO = 0.065625f;   //42 * 3 / 1920
        private const float CHARACTER_HEIGHT_RATIO = 0.116667f;  //42 * 3 / 1080

        private Vector2 dimensionAttack = new Vector2(80, 80);
        private const float ATTACK_WIDTH_RATIO = 0.125f;
        private const float ATTACK_HEIGHT_RATIO = 0.222f;

        public float characterInGameWidth;
        private float characterInGameHeight;
        public float attackInGameWidth;
        private float attackInGameHeight;
        private List<Rectangle> idleFrames;
        private List<Rectangle> walkFrames;
        private List<Rectangle> attackFrames;
        private List<Rectangle> deathFrames;
        private int frameIndex = -1;
        public int attackIndex = -1;
        public int deathIndex = -1;
        private const int DELAY_IDLE = 20;
        private const int DELAY_WALK = 7;
        private const int DELAY_ATTACK = 5;
        private const int DELAY_DEATH = 15;
        private int delayCounter;

        public string[] playerStatus = { "Idle", "Walk", "Attack", "Death" };
        public string currentPlayerStatus;

        private KeyboardState oldState;

        public int hp;
        SpriteFont hpFont;
        SoundEffect sfxFootstep;
        SoundEffect sfxSwordSwing;

        public Knight(Game game, SpriteBatch spriteBatch, Texture2D knightTexIdle, Texture2D knightTexWalk, Texture2D knightTexAttack, Texture2D knightTexDeath, World world, int hp, SpriteFont hpFont, SoundEffect sfxFootstep, SoundEffect sfxSwordSwing) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.knightTexIdle = knightTexIdle;
            this.knightTexWalk = knightTexWalk;
            this.knightTexAttack = knightTexAttack;
            this.knightTexDeath = knightTexDeath;
            this.world = world;
            this.hp = hp;
            this.hpFont = hpFont;
            this.sfxFootstep = sfxFootstep;
            this.sfxSwordSwing = sfxSwordSwing;
            characterInGameWidth = CHARACTER_WIDTH_RATIO * Shared.stage.X;
            characterInGameHeight = CHARACTER_HEIGHT_RATIO * Shared.stage.Y;
            attackInGameWidth = ATTACK_WIDTH_RATIO * Shared.stage.X;
            attackInGameHeight = ATTACK_HEIGHT_RATIO * Shared.stage.Y;
            currentPlayerStatus = playerStatus[0];

            CreateFrames();
        }

        public void CreateFrames()
        {
            idleFrames = new List<Rectangle>();
            for (int i = 0; i < IDLE_TEX_FRAMES; i++)
            {
                int x = i * (int)dimension.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                idleFrames.Add(r);
            }
            walkFrames = new List<Rectangle>();
            for (int i = 0; i < WALK_TEX_FRAMES; i++)
            {
                int x = i * (int)dimension.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                walkFrames.Add(r);
            }
            attackFrames = new List<Rectangle>();
            for (int i = 0; i < ATTACK_TEX_FRAMES; i++)
            {
                int x = i * (int)dimensionAttack.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimensionAttack.X, (int)dimensionAttack.Y);
                attackFrames.Add(r);
            }
            deathFrames = new List<Rectangle>();
            for (int i = 0; i < DEATH_TEX_FRAMES; i++)
            {
                int x = i * (int)dimension.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                deathFrames.Add(r);
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (currentPlayerStatus == playerStatus[0])
            {
                attackIndex = -1;
                if (delayCounter < DELAY_IDLE && frameIndex == -1)
                {
                    delayCounter = DELAY_IDLE;
                }
                delayCounter++;
                if (delayCounter > DELAY_IDLE)
                {
                    frameIndex++;
                    if (frameIndex > IDLE_TEX_FRAMES - 1)
                    {
                        frameIndex = -1;
                        //this.Enabled = false;
                        //this.Enabled = false;
                    }
                    delayCounter = 0;
                }
            }


            if ((ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Space)) && currentPlayerStatus != playerStatus[3])
            {
                if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Left))
                {
                    currentPlayerStatus = playerStatus[1];
                    attackIndex = -1;
                    if ((frameIndex == 2 || frameIndex == 5) && delayCounter == 0)
                    {
                        sfxFootstep.Play();
                    }
                    if (ks.IsKeyDown(Keys.Right))
                    {
                        if (delayCounter < DELAY_WALK && frameIndex == -1)
                        {
                            delayCounter = DELAY_WALK;
                        }
                        delayCounter++;
                        if (delayCounter > DELAY_WALK)
                        {
                            frameIndex++;
                            if (frameIndex > WALK_TEX_FRAMES - 1)
                            {
                                frameIndex = -1;
                            }
                            delayCounter = 0;
                        }
                        
                    }
                    else if (ks.IsKeyDown(Keys.Left))
                    {
                        if (delayCounter < DELAY_WALK && frameIndex == -1)
                        {
                            delayCounter = DELAY_WALK;
                        }
                        delayCounter++;
                        if (delayCounter > DELAY_WALK)
                        {
                            frameIndex--;
                            if (frameIndex < 0)
                            {
                                frameIndex = WALK_TEX_FRAMES;
                            }
                            delayCounter = 0;
                        }
                    }
                }
                else if (ks.IsKeyDown(Keys.Space))
                {
                    currentPlayerStatus = playerStatus[2];
                    if (delayCounter < DELAY_ATTACK && attackIndex == -1)
                    {
                        delayCounter = DELAY_ATTACK;
                    }
                    delayCounter++;
                    if (delayCounter > DELAY_ATTACK)
                    {
                        attackIndex++;
                        if (attackIndex > ATTACK_TEX_FRAMES - 1)
                        {
                            attackIndex = -1;
                        }
                        delayCounter = 0;
                    }
                    if (attackIndex == 5 && delayCounter == 0)
                    {
                        sfxSwordSwing.Play();
                    }
                }
            }

            else
            {
                if (currentPlayerStatus != playerStatus[3])
                {
                    currentPlayerStatus = playerStatus[0];
                    if (frameIndex > IDLE_TEX_FRAMES - 1)
                    {
                        frameIndex = -1;
                        delayCounter = 0;
                    }
                }
                
            }

            if (currentPlayerStatus == playerStatus[3])
            {
                Shared.gameOver = true;
                if (delayCounter < DELAY_DEATH && deathIndex == -1)
                {
                    delayCounter = DELAY_DEATH;
                }
                delayCounter++;
                if (delayCounter > DELAY_DEATH)
                {
                    deathIndex++;
                    delayCounter = 0;
                }
            }

            //oldState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (hp > 0)
            {
                spriteBatch.DrawString(hpFont, hp.ToString(), Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.DrawString(hpFont, 0.ToString(), Vector2.Zero, Color.White);
            }
            if (currentPlayerStatus == playerStatus[0])
            {
                if (frameIndex >= 0)
                {
                    spriteBatch.Draw(knightTexIdle, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight), idleFrames[frameIndex], Color.White);
                }
                else if (frameIndex == -1 && delayCounter == 0)
                {
                    spriteBatch.Draw(knightTexIdle, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight), idleFrames[0], Color.White);
                }
            }
            else if (currentPlayerStatus == playerStatus[1])
            {
                if (frameIndex >= 0 && frameIndex < WALK_TEX_FRAMES)
                {
                    spriteBatch.Draw(knightTexWalk, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight), walkFrames[frameIndex], Color.White);
                }
                else if (frameIndex == -1 && delayCounter == 0)
                {
                    spriteBatch.Draw(knightTexWalk, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight), walkFrames[0], Color.White);
                    delayCounter = DELAY_WALK;
                }
                else if (frameIndex == WALK_TEX_FRAMES)
                {
                    spriteBatch.Draw(knightTexWalk, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight), walkFrames[WALK_TEX_FRAMES - 1], Color.White);
                    delayCounter = DELAY_WALK;
                }
            }
            else if (currentPlayerStatus == playerStatus[2])
            {
                if (attackIndex >= 0)
                {
                    spriteBatch.Draw(knightTexAttack, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - attackInGameHeight), (int)attackInGameWidth, (int)attackInGameHeight), attackFrames[attackIndex], Color.White);
                }
                else if (attackIndex == -1 && delayCounter == 0)
                {
                    spriteBatch.Draw(knightTexAttack, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - attackInGameHeight), (int)attackInGameWidth, (int)attackInGameHeight), attackFrames[0], Color.White);
                }
            }
            else if (currentPlayerStatus == playerStatus[3])
            {
                if (deathIndex >= 0 && deathIndex < DEATH_TEX_FRAMES)
                {
                    spriteBatch.Draw(knightTexDeath, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight), deathFrames[deathIndex], Color.White);
                }
                else if (deathIndex >= DEATH_TEX_FRAMES)
                {
                    spriteBatch.Draw(knightTexDeath, new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight), deathFrames[DEATH_TEX_FRAMES - 1], Color.White);
                }
                if (deathIndex >= 2)
                {
                    spriteBatch.DrawString(hpFont, "Game Over", new Vector2(Shared.stage.X / 2 - hpFont.MeasureString("Game Over").X / 2, Shared.stage.Y / 2 - hpFont.MeasureString("Game Over").Y / 2), Color.Red);
                }
            }
            if (currentPlayerStatus != playerStatus[3] && Shared.gameOver)
            {
                spriteBatch.DrawString(hpFont, "The End", new Vector2(Shared.stage.X / 2 - hpFont.MeasureString("The End").X / 2, Shared.stage.Y / 2 - hpFont.MeasureString("The End").Y / 2), Color.Red);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle GetBound()
        {
            if (currentPlayerStatus == playerStatus[0] || currentPlayerStatus == playerStatus[1] || currentPlayerStatus == playerStatus[3])
            {
                return new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - characterInGameHeight), (int)characterInGameWidth, (int)characterInGameHeight);
            }
            else
            {
                return new Rectangle((int)((Shared.stage.X / 2) - (characterInGameWidth / 2)), (int)((733f / world.terrain.Height) * Shared.stage.Y - attackInGameHeight), (int)attackInGameWidth, (int)attackInGameHeight);
            }
        }
    }
}
