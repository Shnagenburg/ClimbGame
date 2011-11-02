using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Climb
{
    class ControlledSpriteOld : Sprite
    {
        const string HERO_ASSETNAME = "pixeldude";
        const int START_POSITION_X = 125;
        const int START_POSITION_Y = 245;
        const int SPRITE_ACCEL = 1000;
        const int SPRITE_SPEED = 0;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const int GRAVITY = 900;        

        bool bFacingRight = true;
        bool bCanJump = false;
        enum State
        {
            Walking,
            Jumping
        }
        State mCurrentState = State.Walking;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;
        Vector2 mAccel = Vector2.Zero;

        KeyboardState mPreviousKeyboardState;

        public void LoadContent(ContentManager theContentManager)
        {
            // To make things simple, gravity always pushes you down
            // So we dont mess with the Y acceleration direction
            mDirection.Y = 1;
            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(theContentManager, HERO_ASSETNAME);
            BoundingBox.Width = (int)(mSpriteTexture.Width * Scale);
            BoundingBox.Height = (int)(mSpriteTexture.Height * Scale);
        }

        public void Update(GameTime theGameTime, KeyboardState aCurrentKeyboardState, List<Sprite> blocks)
        {

            UpdateMovement(aCurrentKeyboardState);

            UpdateVelocity(theGameTime, blocks);

            mPreviousKeyboardState = aCurrentKeyboardState;
           
            base.Update(theGameTime, mSpeed, mDirection, mAccel);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            if (mCurrentState == State.Walking)
            {
                mAccel = Vector2.Zero;
                mDirection.X = 0;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true 
                    || aCurrentKeyboardState.IsKeyDown(Keys.A))
                {
                    mAccel.X = bCanJump ? SPRITE_ACCEL : SPRITE_ACCEL / 2; // You cannot manuever as well midair
                    mDirection.X = MOVE_LEFT;
                    bFacingRight = false;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true
                    || aCurrentKeyboardState.IsKeyDown(Keys.D))
                {
                    mAccel.X = bCanJump ? SPRITE_ACCEL : SPRITE_ACCEL / 2;  // You cannot manuever as well midair
                    mDirection.X = MOVE_RIGHT;
                    bFacingRight = true;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
                {
                    Jump();
                }
                    //Stop moving (Decelerate)
                else if (aCurrentKeyboardState.IsKeyUp(Keys.Left) && aCurrentKeyboardState.IsKeyUp(Keys.Right)
                    && aCurrentKeyboardState.IsKeyUp(Keys.A) && aCurrentKeyboardState.IsKeyUp(Keys.D))
                {
                    if (bCanJump)
                    {
                        mAccel.X = SPRITE_ACCEL;
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
            }
        }

        private void UpdateVelocity(GameTime gameTime, List<Sprite> blocks)
        {
            // -----------------------------X----------------------
            // This is the max run speed
            if (Math.Abs(mSpeed.X) < 201)
                mSpeed.X += mAccel.X * mDirection.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (mSpeed.X > 0)
                mSpeed.X = 200;
            else
                mSpeed.X = -200;

            
            // ----------------------Y----------------------
            mSpeed.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;


            // Check collision
            // Find out where barry will be.
            float fYPos = Position.Y + ( mSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            float fXPos = Position.X + ( mSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds);

            BoundingBox.X = (int)fXPos;
            BoundingBox.Y = (int)fYPos + 1;

            bool collided = false;

            foreach (Sprite sp in blocks)
            {
                if (BoundingBox.Intersects(sp.BoundingBox))
                {

                    //Set the box to where he came from
                    BoundingBox.X = (int)Position.X;
                    BoundingBox.Y = (int)Position.Y;

                    if (BoundingBox.Bottom <= sp.BoundingBox.Top)
                    {
                        mSpeed.Y = 0;
                        Position.Y = sp.Position.Y - Size.Height;
                        bCanJump = true;
                        collided = true;
                    }
                    else if (BoundingBox.Top >= sp.BoundingBox.Bottom)
                    {
                        mSpeed.Y = 0;
                        Position.Y = sp.Position.Y + sp.Size.Height + 1;
                        collided = true;
                    }

                    if (BoundingBox.Left >= sp.BoundingBox.Right)
                    {
                        mSpeed.X = 0;
                        Position.X = sp.BoundingBox.Right + 1;
                        collided = true;
                    }
                    else if (BoundingBox.Right <= sp.BoundingBox.Left)
                    {
                        mSpeed.X = 0;
                        Position.X = sp.Position.X - Size.Width + 1;
                        collided = true;
                    }
                }
            }



                /*if (fXPos + Size.Width > sp.Position.X
                    && fYPos < sp.Position.Y + sp.Size.Height
                    && fXPos < sp.Position.X + sp.Size.Width
                    && Position.Y > sp.Position.Y - Size.Height)
                {   
                    // He is inside a block, now we need to know from which direction

                    // If before he was over the block, he must be on top
                    if ( (Position.Y + Size.Height) - 50 < sp.OldPosition.Y)
                        above = true;
                    if (Position.Y + 50 > sp.OldPosition.Y + sp.Size.Height)
                        below = true;
                    if ((Position.X + Size.Width) - 50 < sp.OldPosition.X)
                        left = true;
                    if (Position.X + 50 > sp.OldPosition.X + sp.Size.Width)
                        right = true;

                    if (above && !right && !left && !below)
                    {
                        mSpeed.Y = 0;
                        Position.Y = sp.Position.Y - Size.Height;
                        bCanJump = true;
                    }
                    else if (!above && !right && !left && below)
                    {
                        mSpeed.Y = 0;
                        Position.Y = sp.Position.Y + sp.Size.Height;
                    }
                    if (!above && right && !left && !below)
                    {
                        mSpeed.X = 0;
                        Position.X = sp.Position.X + sp.Size.Width;
                    }
                    else if (!above && !right && left && !below)
                    {
                        mSpeed.X = 0;
                        Position.X = sp.Position.X - Size.Width;
                    }
                    //if (below)
                    //{
                    //    mSpeed.Y = 0;
                    //    Position.Y = sp.Position.Y + sp.Size.Height;
                    //}
                }*/
            
            
        }

        private void Jump()
        {
            if (bCanJump)
                mSpeed.Y = -500;
            bCanJump = false;
        }


        //Draw the sprite to the screen
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White,
                0.0f, Vector2.Zero, Scale, bFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
        }
    }
}
