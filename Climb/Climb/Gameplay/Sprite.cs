/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
 

namespace Climb
{
    /// <summary>
    /// A sprite is any 2d image.
    /// </summary>
    class Sprite
    {
        // Whether or not the sprite needs to be removed.
        public bool IsDying = false;

        public Vector2 Velocity = new Vector2(0, 0);
        public Vector2 Direction = new Vector2(0, 0);

        //Size of the sprite (with scale applied)
        public Rectangle Size;

        //Where the sprite is for collision detection
        public Rectangle BoundingBox = new Rectangle(0, 0, 0, 0);        

        //Used to size the sprite up or down from originial image
        private float mScale = 1.0f;

        //The current position of the sprite
        public Vector2 Position = new Vector2(0,0);
        public Vector2 OldPosition = new Vector2(0, 0);
        public Rectangle OldBox = new Rectangle();
        //The texture object used when drawing the sprite
        public Texture2D mSpriteTexture;

        //The asset name for the sprite's texture
        public string AssetName;

        //Wether or not you itll collide
        public bool isCollidable;

        //When the scale is modified throught he property, the Size of the 
        //sprite is recalculated with the new scale applied.
        
        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale

                try
                {
                    if (mSpriteTexture != null)
                    {
                        Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * mScale), (int)(mSpriteTexture.Height * mScale));
                    }
                    else
                    {
                        Size = new Rectangle(0, 0, 0, 0);
                    }
                    BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Error: Something wound up null: {0}", e);
                }
            }
        }

        // The color used with the sprite batch.
        private Color cDrawColor = Color.White;
        public byte Alpha
        {
            get { return cDrawColor.A; }
            set { cDrawColor.A = value; }
        }
        /// <summary>
        /// When fading, set Tint to black to completely remove sprite with alpha value of 255
        /// </summary>
        public Color Tint
        {
            get { return cDrawColor; }
            set { cDrawColor = value; }
        }
                

        /// <summary>
        /// Load the texture for the sprite using the Content Pipeline
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="theAssetName"></param>
        public virtual void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale) );
            BoundingBox = new Rectangle( (int)Position.X, (int)Position.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Dispose of the sprite. May cause strang errors!
        /// </summary>
        public virtual void UnloadContent()
        {
            mSpriteTexture.Dispose();
        }

        /// <summary>
        /// Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        /// </summary>
        /// <param name="theGameTime"></param>
        /// <param name="theSpeed"></param>
        /// <param name="theDirection"></param>
        /// <param name="theAccel"></param>
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection, Vector2 theAccel)
        {   
            // Record the last position
            OldPosition = Position;
            OldBox = new Rectangle((int)Position.X, (int)Position.Y, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            
            // Move the sprite
            Position += theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            
            BoundingBox.X = (int)Position.X;
            BoundingBox.Y = (int)Position.Y;
        }
                
        /// <summary>
        /// Update the sprite using its given Velocity
        /// </summary>
        /// <param name="theGameTime"></param>
        public virtual void Update(GameTime theGameTime)
        {
            // Record the last position
            OldPosition = Position;
            OldBox = new Rectangle((int)Position.X, (int)Position.Y, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));

            // Move the sprite
            Position += Velocity * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            
            BoundingBox.X = (int)Position.X;
            BoundingBox.Y = (int)Position.Y;
        }

        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            if (!mSpriteTexture.IsDisposed)
            {
                theSpriteBatch.Draw(mSpriteTexture, Position,
                    new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), cDrawColor,
                    0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            }
            else // For debug
            {
            }
            
        }
    }
}
