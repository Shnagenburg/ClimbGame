/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Climb.Util;

namespace Climb
{
    /// <summary>
    /// The game screen is the screen where all of the gameplay happens.
    /// </summary>
    class GameScreen : Screen
    {
        ControlledSprite csHero;
        Camera cCamera;
        Altimeter altimeter;
        BackgroundProgression bpProgression;
        DanLabel dlDoubleJumpTimer;
        DoubleJumpEffect djeJumpEffect;        

        List<Sprite> blocks;
        Sprite spGround;
        LayeredBackground bg;
        ArcingBlockManager arcingBlockManager;
        ContentManager contentManager;

        bool bScoreWasAdded;

        /// <summary>
        /// Create a new game screen. Should be done every time there is a new game.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        /// <param name="contentManager"></param>
        public GameScreen(EventHandler theScreenEvent,ContentManager contentManager): base(theScreenEvent)
        {
            bScoreWasAdded = false;

            this.contentManager = contentManager;
            dlDoubleJumpTimer = new DanLabel(1150, 20, 100, 50);

            //Init our intrepid hero
            csHero = new ControlledSprite();

            bg = new LayeredBackground();

            djeJumpEffect = new DoubleJumpEffect();

            altimeter = new Altimeter(); // Make a camera for the screen with an altimeter
            cCamera = new Camera(50, 100, 600, 520, altimeter);

            blocks = new List<Sprite>();
            Sprite sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);

            // REVIST Set up the Arcing Block Manager with the difficulty
            arcingBlockManager = new ArcingBlockManager(cCamera, blocks, contentManager, 500, 300, 150, "Sprites/block2");

        }

        /// <summary>
        /// Load the content for the GameScreen.
        /// </summary>
        /// <param name="contentManager"></param>
        public override void LoadContent(ContentManager contentManager)
        {
            string heroStr;
            HandleOptions(out heroStr);

            dlDoubleJumpTimer.LoadContent(contentManager);

            djeJumpEffect.LoadContent(contentManager);

            altimeter.LoadContent(contentManager);

            csHero.LoadContent(contentManager, heroStr);
            csHero.Position = new Vector2(550, 200);
            csHero.Scale = 0.5f;
                        
            bg.LoadContent(contentManager, Backgrounds.INITAL_BACKGROUNDS);
            
            foreach (Sprite sp in blocks)
                sp.LoadContent(contentManager, "Sprites/block2");
            
            blocks[0].LoadContent(contentManager, "Sprites/ground");
            blocks[0].Scale = 1.0f;
            blocks[0].Position = new Vector2(0, 400);
            blocks[0].OldPosition = new Vector2(0, 400);

            blocks[1].Position = new Vector2(550, 350);
            blocks[1].OldPosition = new Vector2(0, 400);

            blocks[2].Position = new Vector2(700, 100);
            blocks[2].OldPosition = new Vector2(700, 100);

            blocks[3].Position = new Vector2(350, 300);
            blocks[3].OldPosition = new Vector2(350, 300);
            blocks[3].Velocity = new Vector2(50, 0);

            blocks[4].Position = new Vector2(50, 400);
            blocks[4].OldPosition = new Vector2(50, 400);
            blocks[4].Velocity = new Vector2(0, 50);

            // Create the background progression
            string[] assets = { "Backgrounds/orange_basic", "Backgrounds/space_basic", "Backsgrounds/duskbg" };
            string[] assets2 = { "Clouds/duskclouds", "Clouds/cloudsthree", "Clouds/cloudsspace" };
            string[] assets3 = { "Clouds/duskclouds", "Clouds/cloudsthree", "Clouds/cloudsspace" };

            // Set the heights. This number is the number you see in game times 10
            int [] heights = {300, 600, 900};
            bpProgression = new BackgroundProgression(bg, altimeter, assets, assets2, assets3, heights);

            csHero.DoubleJumpEffect = djeJumpEffect;
        }


        /// <summary>
        /// Update the GameScreen's elements.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>

        public override void Update(GameTime gameTime, KeyboardState keyState, KeyboardState prevState)
        {
            // Return to the menu
            if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
            {
                if (!bScoreWasAdded)
                    CUtil.AddScore(altimeter.MaxHeight);

                ScreenEvent.Invoke(this, new EventArgs());
                return;
            }
            // In this game, blocks take priority over Barry.
            // Since they will knock him around, we need to know where
            // The blocks will be

            List<Sprite> removing = new List<Sprite>();
            foreach (Sprite sp in blocks)
            {
                sp.Update(gameTime);
                if (sp.IsDying == true)
                    removing.Add(sp);
            }
            foreach (Sprite sp in removing)
            {
                //sp.mSpriteTexture.Dispose();
                blocks.Remove(sp);
            }

            arcingBlockManager.Update(gameTime);

            // Once we know where the blocks will be, adjust Barry accordingly
            csHero.Update(gameTime, keyState, prevState, blocks);
            
            // Update backgrounds
            bg.Update(gameTime);

            // Update the camera
            cCamera.Update(gameTime, blocks, csHero, bg);

            // REVISIT magic number
            altimeter.UpdateHeroHeight(480 - csHero.Position.Y);

            // Check for death
            CheckDeath();

            // Just for testing
            Oscillate();

            UpdateDoubleJumpTimer();

            djeJumpEffect.Update(gameTime);

            // Update the level progression
            bpProgression.Update();

            base.Update(gameTime, keyState);
        }

        /// <summary>
        /// Draw the GameScreen's elements.
        /// </summary>
        /// <param name="theBatch"></param>
        public override void Draw(SpriteBatch theBatch)
        {
            bg.Draw(theBatch);

            foreach (Sprite sp in blocks)
                sp.Draw(theBatch);

            csHero.Draw(theBatch);

            altimeter.Draw(theBatch);

            dlDoubleJumpTimer.Draw(theBatch);

            djeJumpEffect.Draw(theBatch);

            base.Draw(theBatch);
        }

        // Just to test collision detection
        public void Oscillate()
        {
            if (blocks[3].Position.X > 400)
                blocks[3].Velocity.X = -50;
            else if (blocks[3].Position.X < 300)
                blocks[3].Velocity.X = 50;

            if (blocks[4].Position.Y > 450)
                blocks[4].Velocity.Y = -100;
            else if (blocks[4].Position.Y < 0)
                blocks[4].Velocity.Y = 100;
        }

        /// <summary>
        /// Sets parameters based on various options.
        /// </summary>
        /// <param name="hero_selection"></param>
        private void HandleOptions(out string hero_selection)
        {
            if (Options.CurrentHeroSelection == Options.HeroSelection.BARRY)
                hero_selection = "Sprites/pixeldude";
            else
                hero_selection = "Sprites/pixeldude2";
        }

        /// <summary>
        /// Check to see if our hero has fallen too far.
        /// If so, the game is over.
        /// </summary>
        private void CheckDeath()
        {
            if (csHero.IsDead)
            {
                arcingBlockManager.IsSpawning = false;

                CUtil.AddScore(altimeter.MaxHeight);
                bScoreWasAdded = true;
            }
        }

        /// <summary>
        /// Check if we can double jump again and increment counter.
        /// </summary>
        private void UpdateDoubleJumpTimer()
        {
            if (csHero.CanDoubleJump)
                dlDoubleJumpTimer.Text = "Ok!";
            else
                dlDoubleJumpTimer.Text = "" + (ControlledSprite.DOUBLE_JUMP_TIMER - csHero.DoubleJumpCounter);
        }

    }
}
