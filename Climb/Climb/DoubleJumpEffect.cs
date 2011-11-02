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
    /// The visual effect that happens when the hero double jumps.
    /// </summary>
    class DoubleJumpEffect
    {
        const string CLOUD_IMAGE = "Sprites/smallcloud";
        const float DURATION = 200.0f;
        const float SPEED = 600.0f;       

        Point mPointOfOrigin;
        public Point PointOfOrigin
        {
            get { return mPointOfOrigin; }
            set { mPointOfOrigin = value; }
        }

        Sprite mCloud1;
        Sprite mCloud2;

        bool bIsDone;
        float fCounter;

        public DoubleJumpEffect()
        {
            mCloud1 = new Sprite();
            mCloud2 = new Sprite();

            bIsDone = true;
            fCounter = 0;
        }

        public void LoadContent(ContentManager content)
        {
            mCloud1.LoadContent(content, CLOUD_IMAGE);
            mCloud2.LoadContent(content, CLOUD_IMAGE);
        }

        public void Update(GameTime theTime)
        {
            if (!bIsDone)
            {
                mCloud1.Position.X += (float) (theTime.ElapsedGameTime.Milliseconds / 1000.0f) * SPEED;
                mCloud2.Position.X -= (float) (theTime.ElapsedGameTime.Milliseconds / 1000.0f) * SPEED;

                fCounter += (float)theTime.ElapsedGameTime.Milliseconds;
            }

            if (fCounter > DURATION && !bIsDone)
            {
                bIsDone = true;
                fCounter = 0;
            }
        }

        public void Draw(SpriteBatch theBatch)
        {
            if (!bIsDone)
            {
                mCloud1.Draw(theBatch);
                mCloud2.Draw(theBatch);
            }
        }

        /// <summary>
        /// Start the double jump effect.
        /// </summary>
        /// <param name="p"></param>
        public void Begin(Point p)
        {
            PointOfOrigin = p;
            mCloud1.Position.X = p.X;
            mCloud1.Position.Y = p.Y;

            mCloud2.Position.X = p.X;
            mCloud2.Position.Y = p.Y;
            
            bIsDone = false;
            fCounter = 0;
        }
    }
}
