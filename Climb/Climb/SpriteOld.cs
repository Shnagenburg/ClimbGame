using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
 

namespace Climb
{
    
    class Sprite
    {

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

        //The texture object used when drawing the sprite
        protected Texture2D mSpriteTexture;

        //The asset name for the sprite's texture
        public string AssetName;

        //Wether or not you itll collide
        public bool isCollidable;

        //For collis detection
        //public Rectangle BoundingBox;

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




        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale) );
            BoundingBox = new Rectangle( (int)Position.X, (int)Position.Y, Size.Width, Size.Height);
        }


        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection, Vector2 theAccel)
        {            
            //Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            Position += theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            BoundingBox.X = (int)Position.X;
            BoundingBox.Y = (int)Position.Y;
        }


        //Dont do anything wacky
        public virtual void Update(GameTime theGameTime)
        {
            OldPosition = Position;
            Position += Velocity * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            BoundingBox.X = (int)Position.X;
            BoundingBox.Y = (int)Position.Y;
        }

        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, 
                new Rectangle(0,0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White,
                0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}
