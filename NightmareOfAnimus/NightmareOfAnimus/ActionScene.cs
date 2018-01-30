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
    public class ActionScene : GameScene
    {
        SpriteBatch spriteBatch;
        World world;
        Knight knight;
        Obstacle rock;
        EnemyCollision cds1;
        EnemyCollision cds2;
        EnemyCollision cds3;
        CollisionDetection cd;
        Enemy skeleton1;
        Enemy skeleton2;
        Enemy skeleton3;

        public ActionScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;

            Texture2D sky = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0010_1");
            Texture2D treeFaded = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0009_2");
            Texture2D treeFar = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0008_3");
            Texture2D lighFar = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0007_Lights");
            Texture2D treeNearFar = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0006_4");
            Texture2D treeClose = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0005_5");
            Texture2D lightClose = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0004_Lights");
            Texture2D treeClosest = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0003_6");
            Texture2D topForest = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0002_7");
            Texture2D terrain = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0001_8");
            Texture2D grass = g.Content.Load<Texture2D>("Images/Action_Background/Layer_0000_9");
            Song gameTheme = game.Content.Load<Song>("OST/Light_of_Nibel");
            world = new World(game, spriteBatch, sky, treeFaded, treeFar, lighFar, treeNearFar, treeClose, lightClose, treeClosest, topForest, terrain, grass, gameTheme);
            this.Components.Add(world);

            Texture2D knightTexIdle = g.Content.Load<Texture2D>("Images/Knight/knight_idle");
            Texture2D knightTexWalk = g.Content.Load<Texture2D>("Images/Knight/knight_walk");
            Texture2D knightTexAttack = g.Content.Load<Texture2D>("Images/Knight/knight_attack");
            Texture2D knightTexDeath = g.Content.Load<Texture2D>("Images/Knight/knight_death");
            SpriteFont hpFont = g.Content.Load<SpriteFont>("Fonts/hpFont");
            SoundEffect sfxFootstep = g.Content.Load<SoundEffect>("SFX/footsteps");
            SoundEffect sfxSwordSwing = g.Content.Load<SoundEffect>("SFX/sword_swing");
            int hp = 50;
            knight = new Knight(game, spriteBatch, knightTexIdle, knightTexWalk, knightTexAttack, knightTexDeath, world, hp, hpFont, sfxFootstep, sfxSwordSwing);
            this.Components.Add(knight);

            Texture2D skeletonTexIdle = g.Content.Load<Texture2D>("images/Skeleton/Sprite Sheets/Skeleton Idle");
            Texture2D skeletonTexWalk = g.Content.Load<Texture2D>("images/Skeleton/Sprite Sheets/Skeleton Walk");
            Texture2D skeletonTexAttack = g.Content.Load<Texture2D>("images/Skeleton/Sprite Sheets/Skeleton Attack");
            Texture2D skeletonTexHit = g.Content.Load<Texture2D>("images/Skeleton/Sprite Sheets/Skeleton Hit");
            Texture2D skeletonTexDead = g.Content.Load<Texture2D>("images/Skeleton/Sprite Sheets/Skeleton Dead");
            int hpEnemy = 30;
            SoundEffect sfxSkeletonWalk = g.Content.Load<SoundEffect>("SFX/skeleton_walk");
            SoundEffect sfxAxeSwing = g.Content.Load<SoundEffect>("SFX/axe_swing");

            skeleton1 = new Enemy(game, spriteBatch, skeletonTexIdle, skeletonTexWalk, skeletonTexAttack, skeletonTexHit, skeletonTexDead, world, new Vector2(Shared.stage.X + 50, 0), knight, hpEnemy, sfxSkeletonWalk, sfxAxeSwing);
            this.Components.Add(skeleton1);

            skeleton2 = new Enemy(game, spriteBatch, skeletonTexIdle, skeletonTexWalk, skeletonTexAttack, skeletonTexHit, skeletonTexDead, world, new Vector2(Shared.stage.X + 500, 0), knight, hpEnemy, sfxSkeletonWalk, sfxAxeSwing);
            this.Components.Add(skeleton2);

            skeleton3 = new Enemy(game, spriteBatch, skeletonTexIdle, skeletonTexWalk, skeletonTexAttack, skeletonTexHit, skeletonTexDead, world, new Vector2(Shared.stage.X + 1200, 0), knight, 50, sfxSkeletonWalk, sfxAxeSwing);
            this.Components.Add(skeleton3);

            Texture2D rockTex = g.Content.Load<Texture2D>("Images/Rock");
            rock = new Obstacle(game, spriteBatch, rockTex, new Vector2(-10, (733f / world.terrain.Height) * Shared.stage.Y - rockTex.Height));
            this.Components.Add(rock);

            cds1 = new EnemyCollision(game, knight, skeleton1);
            this.Components.Add(cds1);

            cds2 = new EnemyCollision(game, knight, skeleton2);
            this.Components.Add(cds2);

            cds3 = new EnemyCollision(game, knight, skeleton3);
            this.Components.Add(cds3);

            cd = new CollisionDetection(game, knight, rock);
            this.Components.Add(cd);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (skeleton3.Enabled == false)
            {
                Shared.gameOver = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
