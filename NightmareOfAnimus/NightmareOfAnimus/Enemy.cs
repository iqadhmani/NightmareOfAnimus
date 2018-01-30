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
    public class Enemy : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D skeletonTexIdle;
        Texture2D skeletonTexWalk;
        Texture2D skeletonTexAttack;
        Texture2D skeletonTexHit;
        Texture2D skeletonTexDead;

        private const int IDLE_TEX_FRAMES = 11;
        private const int WALK_TEX_FRAMES = 13;
        private const int ATTACK_TEX_FRAMES = 18;
        private const int HIT_TEX_FRAMES = 8;
        private const int DEATH_TEX_FRAMES = 15;

        private Vector2 dimension = new Vector2(24, 32);
        private const float SKELETON_WIDTH_RATIO = 0.0375f;   //24 * 3 / 1920
        private const float SKELETON_HEIGHT_RATIO = 0.0889f;  //32 * 3 / 1080

        private Vector2 dimensionWalk = new Vector2(22, 33);
        private const float WALK_WIDTH_RATIO = 0.034375f;
        private const float WALK_HEIGHT_RATIO = 0.088889f;

        private Vector2 dimensionAttack = new Vector2(43, 37);
        private const float ATTACK_WIDTH_RATIO = 0.0671875f;
        private const float ATTACK_HEIGHT_RATIO = 0.1027778f;

        private Vector2 dimensionHit = new Vector2(30, 32);
        private const float HIT_WIDTH_RATIO = 0.046875f;
        private const float HIT_HEIGHT_RATIO = 0.088889f;

        private Vector2 dimensionDeath = new Vector2(33, 32);
        private const float DEATH_WIDTH_RATIO = 0.0515625f;
        private const float DEATH_HEIGHT_RATIO = 0.0888889f;

        public float skeletonInGameWidth;
        private float skeletonInGameHeight;

        public float walkInGameWidth;
        private float walkInGameHeight;

        public float attackInGameWidth;
        private float attackInGameHeight;

        public float hitInGameWidth;
        private float hitInGameHeight;

        public float deathInGameWidth;
        private float deathInGameHeight;

        private List<Rectangle> idleFrames;
        private List<Rectangle> walkFrames;
        private List<Rectangle> attackFrames;
        private List<Rectangle> hitFrames;
        private List<Rectangle> deathFrames;

        private int frameIndex = -1;
        public int attackIndex = -1;
        private int hitIndex = -1;
        public int deathIndex = -1;

        private const int DELAY_IDLE = 10;
        private const int DELAY_WALK = 10;
        private const int DELAY_ATTACK = 7;
        private const int DELAY_HIT = 3;
        private const int DELAY_DEATH = 5;
        private int delayCounter;

        public string[] skeletonStatus = { "Idle", "Walk", "Attack", "Hit", "Death" };
        public string currentSkeletonStatus;

        World world;
        Knight knight;
        public Vector2 position;

        float speed = 1;

        public int hp;

        SoundEffect sfxSkeletonWalk;
        SoundEffect sfxAxeSwing;
        public Enemy(Game game, SpriteBatch spriteBatch, Texture2D skeletonTexIdle, Texture2D skeletonTexWalk, Texture2D skeletonTexAttack, Texture2D skeletonTexHit, Texture2D skeletonTexDead, World world, Vector2 position, Knight knight, int hp, SoundEffect sfxSkeletonWalk, SoundEffect sfxAxeSwing) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.skeletonTexIdle = skeletonTexIdle;
            this.skeletonTexWalk = skeletonTexWalk;
            this.skeletonTexAttack = skeletonTexAttack;
            this.skeletonTexHit = skeletonTexHit;
            this.skeletonTexDead = skeletonTexDead;
            this.world = world;
            this.knight = knight;
            this.position = position;
            this.hp = hp;
            this.sfxSkeletonWalk = sfxSkeletonWalk;
            this.sfxAxeSwing = sfxAxeSwing;

            skeletonInGameWidth = SKELETON_WIDTH_RATIO * Shared.stage.X;
            skeletonInGameHeight = SKELETON_HEIGHT_RATIO * Shared.stage.Y;

            walkInGameWidth = WALK_WIDTH_RATIO * Shared.stage.X;
            walkInGameHeight = WALK_HEIGHT_RATIO * Shared.stage.Y;

            attackInGameWidth = ATTACK_WIDTH_RATIO * Shared.stage.X;
            attackInGameHeight = ATTACK_HEIGHT_RATIO * Shared.stage.Y;

            hitInGameWidth = HIT_WIDTH_RATIO * Shared.stage.X;
            hitInGameHeight = HIT_HEIGHT_RATIO * Shared.stage.Y;

            deathInGameWidth = DEATH_WIDTH_RATIO * Shared.stage.X;
            deathInGameHeight = DEATH_HEIGHT_RATIO * Shared.stage.Y;

            currentSkeletonStatus = skeletonStatus[0];

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
                int x = i * (int)dimensionWalk.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimensionWalk.X, (int)dimensionWalk.Y);
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
            hitFrames = new List<Rectangle>();
            for (int i = 0; i < HIT_TEX_FRAMES; i++)
            {
                int x = i * (int)dimensionHit.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimensionHit.X, (int)dimensionHit.Y);
                hitFrames.Add(r);
            }
            deathFrames = new List<Rectangle>();
            for (int i = 0; i < DEATH_TEX_FRAMES; i++)
            {
                int x = i * (int)dimensionDeath.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimensionDeath.X, (int)dimensionDeath.Y);
                deathFrames.Add(r);
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) && !Shared.gameOver)
            {
                position.X -= Shared.speed.X;
            }
            else if (ks.IsKeyDown(Keys.Left) && !Shared.gameOver)
            {
                position.X += Shared.speed.X;
            }

            if (currentSkeletonStatus == skeletonStatus[0])
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
                    }
                    delayCounter = 0;
                }
            }

            if (position.X - ((Shared.stage.X / 2) - (knight.characterInGameWidth / 2)) < Shared.stage.X / 4 && !(position.X - (Shared.stage.X / 2) <= 0) && attackIndex == -1 && currentSkeletonStatus != skeletonStatus[3] && currentSkeletonStatus != skeletonStatus[4])
            {
                currentSkeletonStatus = skeletonStatus[1];
                position.X -= speed;
                attackIndex = -1;
                if ((frameIndex == 3 || frameIndex == 7 || frameIndex == 11) && delayCounter == 0)
                {
                    sfxSkeletonWalk.Play();
                }
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
            if ((position.X - (Shared.stage.X / 2) <= 0 || attackIndex != -1) && currentSkeletonStatus != skeletonStatus[3] && currentSkeletonStatus != skeletonStatus[4])
            {
                currentSkeletonStatus = skeletonStatus[2];
                frameIndex = -1;
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
                if (attackIndex == 7 && delayCounter == 0)
                {
                    sfxAxeSwing.Play();
                }
            }
            if (currentSkeletonStatus == skeletonStatus[3])
            {
                if (delayCounter < DELAY_HIT && hitIndex == -1)
                {
                    delayCounter = DELAY_HIT;
                }
                delayCounter++;
                if (delayCounter > DELAY_HIT)
                {
                    hitIndex++;
                    delayCounter = 0;
                }
            }
            //else
            //{
            //    currentSkeletonStatus = skeletonStatus[0];
            //    if (frameIndex > IDLE_TEX_FRAMES - 1)
            //    {
            //        frameIndex = -1;
            //        delayCounter = 0;
            //    }
            //}

            if (currentSkeletonStatus == skeletonStatus[4])
            {
                //currentSkeletonStatus = skeletonStatus[4];
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (currentSkeletonStatus == skeletonStatus[0])
            {
                if (frameIndex >= 0)
                {
                    spriteBatch.Draw(skeletonTexIdle, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - skeletonInGameHeight), (int)skeletonInGameWidth, (int)skeletonInGameHeight), idleFrames[frameIndex], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else if (frameIndex == -1 && delayCounter == 0)
                {
                    spriteBatch.Draw(skeletonTexIdle, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - skeletonInGameHeight), (int)skeletonInGameWidth, (int)skeletonInGameHeight), idleFrames[0], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else if (frameIndex > IDLE_TEX_FRAMES - 1)
                {
                    spriteBatch.Draw(skeletonTexIdle, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - skeletonInGameHeight), (int)skeletonInGameWidth, (int)skeletonInGameHeight), idleFrames[0], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
            }

            else if (currentSkeletonStatus == skeletonStatus[1])
            {
                if (frameIndex >= 0)
                {
                    spriteBatch.Draw(skeletonTexWalk, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - walkInGameHeight), (int)walkInGameWidth, (int)walkInGameHeight), walkFrames[frameIndex], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else if (frameIndex == -1 && delayCounter == 0)
                {
                    spriteBatch.Draw(skeletonTexWalk, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - walkInGameHeight), (int)walkInGameWidth, (int)walkInGameHeight), walkFrames[0], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
            }

            else if (currentSkeletonStatus == skeletonStatus[2])
            {
                if (attackIndex >= 0)
                {
                    spriteBatch.Draw(skeletonTexAttack, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - attackInGameHeight), (int)attackInGameWidth, (int)attackInGameHeight), attackFrames[attackIndex], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else if (attackIndex == -1 && delayCounter == 0)
                {
                    spriteBatch.Draw(skeletonTexAttack, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - attackInGameHeight), (int)attackInGameWidth, (int)attackInGameHeight), attackFrames[0], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
            }

            else if (currentSkeletonStatus == skeletonStatus[3])
            {
                if (hitIndex >= 0 && hitIndex < HIT_TEX_FRAMES)
                {
                    spriteBatch.Draw(skeletonTexHit, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - hitInGameHeight), (int)hitInGameWidth, (int)hitInGameHeight), hitFrames[hitIndex], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else if (hitIndex >= HIT_TEX_FRAMES)
                {
                    currentSkeletonStatus = skeletonStatus[0];
                    hitIndex = -1;
                }
            }

            else if (currentSkeletonStatus == skeletonStatus[4])
            {
                if (deathIndex >= 0 && deathIndex < DEATH_TEX_FRAMES)
                {
                    spriteBatch.Draw(skeletonTexDead, new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - deathInGameHeight), (int)deathInGameWidth, (int)deathInGameHeight), deathFrames[deathIndex], Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else if (deathIndex >= DEATH_TEX_FRAMES)
                {
                    this.Enabled = false;
                    this.Visible = false;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle GetBound()
        {
            if (currentSkeletonStatus == skeletonStatus[0])
            {
                return new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - skeletonInGameHeight), (int)skeletonInGameWidth, (int)skeletonInGameHeight);
            }
            else if (currentSkeletonStatus == skeletonStatus[1])
            {
                return new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - walkInGameHeight), (int)walkInGameWidth, (int)walkInGameHeight);
            }
            else if (currentSkeletonStatus == skeletonStatus[2])
            {
                return new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - attackInGameHeight), (int)attackInGameWidth, (int)attackInGameHeight);
            }
            else
            {
                return new Rectangle((int)position.X, (int)((729f / world.terrain.Height) * Shared.stage.Y - deathInGameHeight), (int)deathInGameWidth, (int)deathInGameHeight);
            }
            
        }
    }
}
