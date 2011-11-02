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

namespace Climb
{
    /// <summary>
    /// Tracks the max height of the Hero. This is handled by the Camera class and GameScreen class.
    /// </summary>
    class Altimeter
    {
        // The height is recorded in pixels. This scales down the displayed height to reduce digits.
        const int SCALE_DOWN_FACTOR = 10;

        // The max height the hero has achieved.
        public float MaxHeight
        {
            get { return fMaxShift + iHeroHeight; }
        }
        private float fTotalShift = 0;
        private float fMaxShift = 0;
        private float iHeroHeight = 0;

        DanLabel dlLabel;

        SpriteFont font;

        /// <summary>
        /// Load a new Altimeter
        /// </summary>
        public void LoadContent(ContentManager contentManager)
        {
            dlLabel = new DanLabel(5, 5, 300, 35);
            dlLabel.LoadContent(contentManager);
            font = contentManager.Load<SpriteFont>("verdana");
        }

        /// <summary>
        /// Update how much height has been gained via the camera
        /// </summary>
        public void UpdateShift(float shift)
        {
            fTotalShift -= shift;
            if (fTotalShift > fMaxShift)
            {
                fMaxShift = fTotalShift;
                dlLabel.Text = "Height: " + (int)(fMaxShift + iHeroHeight) / SCALE_DOWN_FACTOR;
            }
        }

        /// <summary>
        /// Update the altitude based on where the hero is on the screen.
        /// </summary>
        public void UpdateHeroHeight(float hero_height_dif)
        {
            if (iHeroHeight < hero_height_dif)
            {
                iHeroHeight = hero_height_dif;
                dlLabel.Text = "Height: " + (int)(fMaxShift + iHeroHeight) / SCALE_DOWN_FACTOR;
            }
        }

        /// <summary>
        /// Show the altitude in the top left corner
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            dlLabel.Draw(spriteBatch);
        }

    }
}
