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
    /// The camera moves things relative to the hero when he is close to moving off-screen.
    /// </summary>
    class Camera
    {
        // This magic number makes the backgrounds scroll slower vertically
        const int BG_SLOW_FACTOR = 10;

        // The altimeter needs to know when movement is being hidden by the camera
        public Altimeter altimeter;

        // How much the camera has panned down in a row
        private float fallenAmount;

        // The bounds of the camera
        Rectangle rectCamera = new Rectangle(0,0,0,0);

        public Camera(int x, int y, int width, int height)
        {
            rectCamera = new Rectangle(x, y, width, height);
            fallenAmount = 0;
        }
        
        public Camera(int x, int y, int width, int height, Altimeter alt)
        {
            rectCamera = new Rectangle(x, y, width, height);
            altimeter = alt;
            fallenAmount = 0;
        }





        /// <summary>
        /// Update the Camera
        /// </summary>
        /// <param name="theGameTime">The Game Time</param>
        /// <param name="blocks">The list of blocks that are affected by the camera.</param>
        /// <param name="csHero">The hero object.</param>
        /// <param name="bg">The current background</param>
        public void Update(GameTime theGameTime, List<Sprite> blocks, ControlledSprite csHero, LayeredBackground bg)
        {

            // Whether or not the hero is outside the camera bounds
            if ((csHero.Position.Y < rectCamera.Y && (csHero.mSpeed.Y < 0 || csHero.spLinkBlock.Velocity.Y < 0))
                || (csHero.Position.Y + csHero.Size.Height > rectCamera.Y + rectCamera.Height && (csHero.mSpeed.Y > 0 || csHero.spLinkBlock.Velocity.Y > 0)))
            {
                float goalDiffShift = 0;//how much it wants to compensate
                if (csHero.Position.Y <= rectCamera.Y && (csHero.mSpeed.Y < 0 || csHero.spLinkBlock.Velocity.Y < 0))//pushing cam up
                {
                    goalDiffShift = (csHero.Position.Y - rectCamera.Y);//should be neg
                    fallenAmount = 0;
                }
                else if (csHero.Position.Y + csHero.Size.Height > rectCamera.Y + rectCamera.Height && (csHero.mSpeed.Y > 0 || csHero.spLinkBlock.Velocity.Y > 0))//dragging cam down
                {
                    goalDiffShift = ((csHero.Position.Y + csHero.Size.Height) - (rectCamera.Y + rectCamera.Height));//should be pos
                    fallenAmount += goalDiffShift; // Add up how much the hero has fallen.
                }

                // Check to see if the game is over
                if (fallenAmount > 1000)
                {
                    csHero.IsDead = true;
                }

                float fShiftAmount = 0; // The amount of the hero's motion hidden by the camera movement

                // If the hero is surfing a block we must also factor in the blocks speed to know how much to move
                if (csHero.bLinked)
                    fShiftAmount = (csHero.spLinkBlock.Velocity.Y * (float)theGameTime.ElapsedGameTime.TotalSeconds)
                        + (csHero.mSpeed.Y * (float)theGameTime.ElapsedGameTime.TotalSeconds);

                else // Otherwise just use the hero's speed
                    fShiftAmount = csHero.mSpeed.Y * (float)theGameTime.ElapsedGameTime.TotalSeconds;

                if (fShiftAmount > 0)
                    fShiftAmount = Math.Min(fShiftAmount, goalDiffShift);
                else
                    fShiftAmount = Math.Max(fShiftAmount, goalDiffShift);


                csHero.Position.Y -= fShiftAmount; // Slide hero back so he doesn't move

                // Update altimeter based on camera
                altimeter.UpdateShift(fShiftAmount);

                foreach (Sprite block in blocks) // Move everyone relative to the hero.
                        block.Position.Y -= fShiftAmount;

                // Shift Backgrounds
                // Divided by i so ones in back move slower
                for (int i = 0; i < bg.layers.Count; i++)
                {
                        foreach (Sprite sp in bg.layers[i].Grid)
                            sp.Position.Y -= fShiftAmount / (i + 3);

                    bg.UpdateVertical(theGameTime);       
                }         
            }    
        }



    }
}
