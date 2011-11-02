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
    /// This class handles animating sprites.
    /// </summary>
    class Animation
    {
        // The collection of textures that comprise the animation
        Texture2D[] mTextures;

        Texture2D mCurrentTexture;
        public Texture2D CurrentTexture
        {
            get { return mCurrentTexture; }
            set { mCurrentTexture = value; }
        }

        int[] mOrdering;

        float fAnimationTimer;
        float fAnimationDelay;

        // Which texture do we show?
        int iTextureIndex;

        // Where are we in the ordering array?
        int iOrderIndex;

        public Animation()
        { }
                  
        /// <summary>
        /// Create a new animation with the images, ordering, and delay specified.
        /// </summary>
        /// <param name="content">The Content Manager</param>
        /// <param name="assets">The images</param>
        /// <param name="ordering">The order the images show up in</param>
        /// <param name="delay">The miliseconds between images</param>
        public void LoadContent(ContentManager content, string [] assets, int [] ordering, int delay)
        {
            // Load all the textures needed
            mTextures = new Texture2D[assets.Length];
            for (int i = 0; i < assets.Length; i++)
            {
                mTextures[i] = content.Load<Texture2D>(assets[i]);
            }

            mOrdering = ordering;
            fAnimationDelay = delay;
            fAnimationTimer = 0;

            // Set the first texture
            iOrderIndex = 0;
            iTextureIndex = ordering[0];
            mCurrentTexture = mTextures[iTextureIndex];
        }

        public void Update(GameTime gameTime)
        {
            fAnimationTimer += gameTime.ElapsedGameTime.Milliseconds;

            // If the time's up, go to the next image in the animation
            if (fAnimationTimer > fAnimationDelay)
            {
                // If were at the end of the ordering array
                if (iOrderIndex >= mOrdering.Length - 1)                
                    iOrderIndex = 0;                
                else
                    iOrderIndex++;

                // Set the next texture;
                iTextureIndex = mOrdering[iOrderIndex];
                mCurrentTexture = mTextures[iTextureIndex];

                fAnimationTimer = 0;

            }
        }


    }
}
