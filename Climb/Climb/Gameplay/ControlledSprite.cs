/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;  
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Climb.Util;

namespace Climb
{
    /// <summary>
    /// A controlled sprite is the hero of the game.
    /// </summary>
    class ControlledSprite : Sprite
    {
        // The animation for the hero.
        private Animation aAnimation;

        // The block that the hero is standing on
        public Sprite spLinkBlock = new Sprite();

        // Whether or not the hero is "surfing" a block
        public bool bLinked;

        // Whether or not the hero is dead
        public bool IsDead = false;

        //const string HERO_ASSETNAME = "pixeldude";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int SPRITE_ACCEL = 2000;
        const int FRICTION_ACCEL = 4000;
        const int SPRITE_SPEED = 0;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const int GRAVITY = 900;

        // Seconds before you can double jump again
        public const int DOUBLE_JUMP_TIMER = 5000;

        // Timer that tracks when the player can double jump again.
        float fDoubleJumpCounter = 0.0f;
        public float DoubleJumpCounter
        {
            get { return fDoubleJumpCounter; }
            set { fDoubleJumpCounter = value; }
        }
                
        bool bCanDoubleJump = true;
        public bool CanDoubleJump
        {
            get { return bCanDoubleJump; }
            set { bCanDoubleJump = value; }
        }

        // The special cloud effect that happens when the hero double jumps.
        DoubleJumpEffect djeEffect;
        public DoubleJumpEffect DoubleJumpEffect
        {
            get { return djeEffect; }
            set { djeEffect = value; }
        }

        bool bFacingRight = true;
        bool bCanJump = false;

        // The state that the hero is in. Affects animation.
        enum State
        {
            Walking,
            Jumping
        }
        State mCurrentState = State.Walking;

        Vector2 mDirection = Vector2.Zero;
        public Vector2 mSpeed = Vector2.Zero;
        Vector2 mAccel = Vector2.Zero;

        KeyboardState mPreviousKeyboardState;

        /// <summary>
        /// Load the content for the hero.
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="heroName"></param>
        public override void LoadContent(ContentManager theContentManager, string heroName)
        {
            // To make things simple, gravity always pushes you down
            // So we dont mess with the Y acceleration direction
            mDirection.Y = 1;
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, heroName);
            BoundingBox.Width = (int)(mSpriteTexture.Width * Scale);
            BoundingBox.Height = (int)(mSpriteTexture.Height * Scale);

            // Set up the animation.
            aAnimation = new Animation();
            aAnimation.LoadContent(theContentManager, AnimationSets.TestHeroSwap, AnimationSets.TestHeroSwapOrder, 1000);
        }

        /// <summary>
        /// Update the hero.
        /// </summary>
        /// <param name="theGameTime"></param>
        /// <param name="aCurrentKeyboardState"></param>
        /// <param name="prevState"></param>
        /// <param name="blocks"></param>
        public void Update(GameTime theGameTime, KeyboardState aCurrentKeyboardState, KeyboardState prevState, List<Sprite> blocks)
        {
            UpdateMovement(aCurrentKeyboardState, prevState, blocks);
            UpdateVelocity(theGameTime);
            UpdateDoubleJump(theGameTime);
            mPreviousKeyboardState = aCurrentKeyboardState;

            aAnimation.Update(theGameTime);

            base.Update(theGameTime, mSpeed, mDirection, mAccel);
            CheckCollision(blocks);

        }

        /// <summary>
        /// Update the movement of the hero.
        /// </summary>
        /// <param name="aCurrentKeyboardState"></param>
        /// <param name="prevState"></param>
        /// <param name="blocks"></param>
        private void UpdateMovement(KeyboardState aCurrentKeyboardState, KeyboardState prevState, List<Sprite> blocks)
        {
            if (mCurrentState == State.Walking)
            {
                mAccel = Vector2.Zero;
                mDirection.X = 0;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) 
                    || aCurrentKeyboardState.IsKeyDown(Keys.A))
                {
                    mAccel.X = bCanJump ? SPRITE_ACCEL : SPRITE_ACCEL / 2; // You cannot manuever as well midair
                    mDirection.X = MOVE_LEFT;
                    bFacingRight = false;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right)
                    || aCurrentKeyboardState.IsKeyDown(Keys.D))
                {
                    mAccel.X = bCanJump ? SPRITE_ACCEL : SPRITE_ACCEL / 2;  // You cannot manuever as well midair
                    mDirection.X = MOVE_RIGHT;
                    bFacingRight = true;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
                {
                    Jump(blocks);
                }
                    //Stop moving (Decelerate)
                else if (aCurrentKeyboardState.IsKeyUp(Keys.Left) && aCurrentKeyboardState.IsKeyUp(Keys.Right)
                    && aCurrentKeyboardState.IsKeyUp(Keys.A) && aCurrentKeyboardState.IsKeyUp(Keys.D))
                {
                    if (bCanJump)
                    {
                        mAccel.X = FRICTION_ACCEL;
                        if (mSpeed.X > 0)
                            mDirection.X = MOVE_LEFT;
                        else if (mSpeed.X < 0)
                            mDirection.X = MOVE_RIGHT;
                    }
                    // This is to prevent fidgeting need to fix this with some math
                    if (Math.Abs(mSpeed.X) < 100)
                    {
                        mSpeed.X = 0;
                        mAccel.X = 0;
                    }
                }

                if (Position.X < 0)
                {
                    Position.X = 0;
                }
                else if (Position.X > Climb.Util.CUtil.SCREEN_WIDTH_PREFMAX - BoundingBox.Width)
                {
                    Position.X = Climb.Util.CUtil.SCREEN_WIDTH_PREFMAX - BoundingBox.Width;
                }


                
            }
        }

        private void UpdateVelocity(GameTime gameTime)
        {
            // -----------------------------X----------------------
            // This is the max run speed
            if (Math.Abs(mSpeed.X) < 401)
                mSpeed.X += mAccel.X * mDirection.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (mSpeed.X > 0)
                mSpeed.X = 400;
            else
                mSpeed.X = -400;

                        
            // ----------------------Y----------------------
            mSpeed.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        private void CheckCollision(List<Sprite> blocks)
        {
            // Record Barry's old Pos
            
            BoundingBox.X = (int)Position.X;
            BoundingBox.Y = (int)Position.Y;
            //Console.WriteLine("!%d %d\n",BoundingBox.Top,BoundingBox.Bottom);

            // Check collision
            bool collided = false;
            foreach (Sprite sp in blocks)
            {
                // If we collided with something
                if (BoundingBox.Intersects(sp.BoundingBox))
                {

                    if (BoundingBox.Bottom + 1 >= sp.BoundingBox.Top && OldBox.Bottom <= sp.OldBox.Top + 1)//if standing on top and came from top
                    {
                        mSpeed.Y = 0;

                        //Added to move horizontally with the box you are on.
                        Position.X += (int)(sp.BoundingBox.Left - sp.OldBox.Left);                        
                        BoundingBox.X = (int)Position.X;

                        Position.Y = sp.BoundingBox.Top - Size.Height+1;
                        bCanJump = true;
                        collided = true;

                        bLinked = true;
                        spLinkBlock = sp;
                    }
                    else if (BoundingBox.Top <= sp.BoundingBox.Bottom && OldBox.Top >= sp.OldBox.Bottom)//If hitting head and came from bottom
                    {
                        sp.Velocity.Y = -Math.Abs(sp.Velocity.Y + mSpeed.Y);

                        if (!bLinked)
                        {
                            mSpeed.Y = 0;
                            Position.Y = sp.BoundingBox.Bottom;
                            collided = true;
                        }
                    }

                    if (BoundingBox.Right >= sp.BoundingBox.Left && OldBox.Right <= sp.OldBox.Left + 1)//running from the right
                    {
                        if (mSpeed.X > 0)
                            mSpeed.X = 0;
                        Position.X = sp.Position.X - Size.Width + 1;
                        collided = true;

                        //bLinked = false;
                    }
                    else if (BoundingBox.Left <= sp.BoundingBox.Right && OldBox.Left >= sp.OldBox.Right)//running from the left
                    {
                        if (mSpeed.X < 0)
                            mSpeed.X = 0;
                        Position.X = sp.BoundingBox.Right;
                        collided = true;

                        //bLinked = false;
                    }
                    // This is the case where a block falls through you... kind of
                    // needs the conditions to be worked a bit better
                    else if (Position.Y > sp.OldPosition.Y)
                    {
                        sp.Velocity.Y = -Math.Abs(sp.Velocity.Y + mSpeed.Y);

                    }

                }
                    // If we didn't collide with the block we are surfing on
                else if (spLinkBlock == sp)
                {
                    bLinked = false;
                    bCanJump = false;

                }
            }            
        }

        /// <summary>
        /// Makes the hero jump.
        /// </summary>
        /// <param name="blocks"></param>
        private void Jump(List<Sprite> blocks)
        {
            // Check to see if there is a sprite we could be slightly above,
            // If so, that warrents a free double jump, since my code
            // doesn't allow surfing of falling blocks very well
            Sprite dblSprite = CheckFreeDoubleJump(blocks);

            if (bCanJump) // Standard jump.
            {
                mSpeed.Y = -500 + (spLinkBlock != null ? spLinkBlock.Velocity.Y : 0);
            }
            else if (dblSprite != null) // Free double jump
            {
                mSpeed.Y = -500 + dblSprite.Velocity.Y;
            }
            else if (bCanDoubleJump) // Double Jump
            {
                mSpeed.Y = -500 + (spLinkBlock != null ? spLinkBlock.Velocity.Y : 0);

                fDoubleJumpCounter = 0;
                bCanDoubleJump = false;

                djeEffect.Begin(
                    new Point( 
                        (int)Position.X + (BoundingBox.Width / 2),
                        (int)Position.Y + BoundingBox.Height));
               
            }

            bCanJump = false;
            bLinked = false;
        }

        /// <summary>
        /// When a playing is bouncing on a falling block, this
        /// checks to see if they can jump without consuming their
        /// double jump. Looks at all the blocks for candidates as
        /// a block slightly under the player. This happens
        /// because players cannot surf falling blocks easily.
        /// </summary>
        private Sprite CheckFreeDoubleJump(List<Sprite> blocks)
        {
            Rectangle box = this.BoundingBox;

            //REVISIT magic leeway
            box.Y += 20;
            foreach (Sprite sp in blocks)
            {
                if (box.Intersects(sp.BoundingBox))
                {                   
                    return sp;
                }
            }
            return null;
        }

        // Add to the counter for the time based double jump
        private void UpdateDoubleJump(GameTime theTime)
        {
            if (bCanDoubleJump)
                return;

            fDoubleJumpCounter += theTime.ElapsedGameTime.Milliseconds;

            if (fDoubleJumpCounter > DOUBLE_JUMP_TIMER)
                bCanDoubleJump = true;
        }


        //Draw the sprite to the screen
        public override void Draw(SpriteBatch theSpriteBatch)
        {

            theSpriteBatch.Draw(aAnimation.CurrentTexture, Position,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White,
                0.0f, Vector2.Zero, Scale, bFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
        }
    }
}
