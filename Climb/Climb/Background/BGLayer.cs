using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Climb.Util;

namespace Climb
{
    /// <summary>
    /// A layer of a LayeredBackground, it pans across the screen with the current setup.
    /// </summary>
    class BGLayer
    {
        const float SWAP_DURATION = 5000;

        const int SCREEN_HEIGHT = 720;
        const int SCREEN_WIDTH = 1280;

        const int SCROLL_SPEED = 30;

        public byte Alpha
        {
            get { return Grid[0, 0].Alpha; }
            set
            {
                foreach (Sprite sp in Grid)
                    sp.Alpha = value;
            }
        }
        /// <summary>
        /// When fading, set Tint to black to completely remove sprite with alpha value of 255
        /// </summary>
        public Color Tint
        {
            get { return Grid[0,0].Tint; }
            set
            {
                foreach (Sprite sp in Grid)
                    sp.Tint = value;
            }
        }


        // The grid of sprites that make up the layer
        public Sprite[,] Grid;

        // The secondary grid for swapping layers
        public Sprite[,] SecondaryGrid;

        // Whether or not we are fading in / out
        bool bIsSwapping;
        public bool IsSwapping
        {
            get { return bIsSwapping; }
            set { bIsSwapping = value; }
        }
                
        float fSwapTimer;
                
        int iLayerDepth;

        public BGLayer()
        {
            bIsSwapping = false;
            fSwapTimer = 0;
        }

        /// <summary>
        /// Load the content for the background layer.
        /// </summary>
        /// <param name="contentManager">The Content Manager.</param>
        /// <param name="asset">The name of the image.</param>
        /// <param name="layer_depht">The index of this layer in the set.</param>
        public void LoadContent(ContentManager contentManager, string asset, int layer_depht)
        {
            iLayerDepth = layer_depht;

            // Currently, sprites must be atleast the size of the screen!
            // Make each part of the grid
            Sprite sp1 = new Sprite();
            sp1.LoadContent(contentManager, asset);
            sp1.Position.X = 0;
            sp1.Position.Y = 0;
            sp1.Velocity.X = (layer_depht + 1) * -SCROLL_SPEED; // Negative is right to left
            
            Sprite sp2 = new Sprite();
            sp2.LoadContent(contentManager, asset);
            sp2.Position.X = (sp2.Size.Width);
            sp2.Velocity.X = (layer_depht + 1) * -SCROLL_SPEED;

            Sprite sp3 = new Sprite();
            sp3.LoadContent(contentManager, asset);
            sp3.Position.Y = (sp2.Size.Height);
            sp3.Velocity.X = (layer_depht + 1) * -SCROLL_SPEED;

            Sprite sp4 = new Sprite();
            sp4.LoadContent(contentManager, asset);
            sp4.Position.X = (sp2.Size.Width);
            sp4.Position.Y = (sp2.Size.Height);
            sp4.Velocity.X = (layer_depht + 1) * -SCROLL_SPEED;

            Grid = new Sprite[2, 2];

            // Make the grid
            Grid[0, 0] = sp1;
            Grid[0, 1] = sp2;
            Grid[1, 0] = sp3;
            Grid[1, 1] = sp4;

            bIsSwapping = false;
            
        }

        /// <summary>
        /// Start fading in a new background
        /// </summary>
        /// <param name="asset"></param>
        public void BeginFade(string asset)
        {
            bIsSwapping = true;

            // Create the second background.
            SecondaryGrid = new Sprite[2, 2];

            Sprite Xsp1 = new Sprite();
            Xsp1.LoadContent(CUtil.ContentManager, asset);
            Xsp1.Position.X = 0;
            Xsp1.Position.Y = 0;
            Xsp1.Velocity.X = (iLayerDepth + 1) * -SCROLL_SPEED; // Negative is right to left

            Sprite Xsp2 = new Sprite();
            Xsp2.LoadContent(CUtil.ContentManager, asset);
            Xsp2.Position.X = (Xsp2.Size.Width);
            Xsp2.Velocity.X = (iLayerDepth + 1) * -SCROLL_SPEED;

            Sprite Xsp3 = new Sprite();
            Xsp3.LoadContent(CUtil.ContentManager, asset);
            Xsp3.Position.Y = (Xsp2.Size.Height);
            Xsp3.Velocity.X = (iLayerDepth + 1) * -SCROLL_SPEED;

            Sprite Xsp4 = new Sprite();
            Xsp4.LoadContent(CUtil.ContentManager, asset);
            Xsp4.Position.X = (Xsp2.Size.Width);
            Xsp4.Position.Y = (Xsp2.Size.Height);
            Xsp4.Velocity.X = (iLayerDepth + 1) * -SCROLL_SPEED;
            
            Color c2 = new Color(0, 0, 0, 0);
            
            // Make the secondary grid
            SecondaryGrid[0, 0] = Xsp1;
            SecondaryGrid[0, 1] = Xsp2;
            SecondaryGrid[1, 0] = Xsp3;
            SecondaryGrid[1, 1] = Xsp4;

            foreach (Sprite sp in SecondaryGrid)
                sp.Tint = c2;
        }

        /// <summary>
        /// Update the background by scrolling it and checking to see if the grid needs to be shuffled around.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            foreach (Sprite sp in Grid)
            {  
                {
                    sp.Update(gameTime);
                }
            }

            // If we are swapping out another image, scroll it as well
            if (bIsSwapping)
            {
                foreach (Sprite sp in SecondaryGrid)
                    sp.Update(gameTime);

                if (SecondaryGrid[0, 0].Position.X < -SecondaryGrid[0, 0].Size.Width)
                {
                    SecondaryGrid[0, 0].Position.X = SecondaryGrid[0, 1].Position.X + SecondaryGrid[0, 1].Size.Width;
                    SecondaryGrid[1, 0].Position.X = SecondaryGrid[1, 1].Position.X + SecondaryGrid[1, 1].Size.Width;
                }
                else if (SecondaryGrid[0, 1].Position.X < -SecondaryGrid[0, 1].Size.Width)
                {
                    SecondaryGrid[0, 1].Position.X = SecondaryGrid[0, 0].Position.X + SecondaryGrid[0, 0].Size.Width;
                    SecondaryGrid[1, 1].Position.X = SecondaryGrid[1, 0].Position.X + SecondaryGrid[1, 0].Size.Width;
                }
            }

            // Check to see if we need to shuffle the grid around
            if (Grid[0, 0].Position.X < -Grid[0, 0].Size.Width)
            {
                Grid[0, 0].Position.X = Grid[0, 1].Position.X + Grid[0, 1].Size.Width;
                Grid[1, 0].Position.X = Grid[1, 1].Position.X + Grid[1, 1].Size.Width;
            }
            else if (Grid[0, 1].Position.X < -Grid[0, 1].Size.Width)
            {
                Grid[0, 1].Position.X = Grid[0, 0].Position.X + Grid[0, 0].Size.Width;
                Grid[1, 1].Position.X = Grid[1, 0].Position.X + Grid[1, 0].Size.Width;
            }
                                

            // Update Swapping (Fade in / out)
            if (bIsSwapping)
            {
                fSwapTimer += gameTime.ElapsedGameTime.Milliseconds;

                // Calculate how transparent each image is
                float percent = fSwapTimer / SWAP_DURATION;

                int alphaVal = (int)(255.0f * percent);
                int alphaVal2 = 255 - alphaVal;

                Color c1 = new Color(alphaVal, alphaVal, alphaVal, alphaVal);
                Color c2 = new Color(alphaVal2, alphaVal2, alphaVal2, alphaVal2);

                foreach (Sprite sp in Grid)
                    sp.Tint = c2;

                foreach (Sprite sp in SecondaryGrid)
                    sp.Tint = c1;

                // End swapping
                if (fSwapTimer > SWAP_DURATION)
                {
                    fSwapTimer = 0;
                    bIsSwapping = false;

                    // REVISIT There is some weird error we get when disposing the starting background.
                    // The other bgs dispose fine I wonder what the problem is.
                    if (!Grid[0,0].AssetName.Equals(Backgrounds.INITAL_BACKGROUNDS[0])
                        && !Grid[0,0].AssetName.Equals(Backgrounds.INITAL_BACKGROUNDS[1])
                        && !Grid[0,0].AssetName.Equals(Backgrounds.INITAL_BACKGROUNDS[2] ) )
                    {
                        // Unload the old image
                        Grid[0, 0].UnloadContent();
                        Grid[1, 0].UnloadContent();
                        Grid[1, 1].UnloadContent();
                        Grid[0, 1].UnloadContent();
                    }
                    
                    // And copy it over to the grid.
                    Grid[0, 0] = SecondaryGrid[0, 0];
                    Grid[1, 0] = SecondaryGrid[1, 0];
                    Grid[1, 1] = SecondaryGrid[1, 1];
                    Grid[0, 1] = SecondaryGrid[0, 1];
                    
                    SecondaryGrid = null;

                }
            }
        }

        /// <summary>
        /// Update the verticle position of the grid based on the camera moving.
        /// </summary>
        public void UpdateVerticle(GameTime gameTime)
        {

            // If Row 0 goes up too high...
            if (Grid[0, 0].Position.Y < -Grid[0, 0].Size.Height)
            {
                Grid[0, 0].Position.Y = Grid[1, 0].Position.Y + Grid[1, 0].Size.Height;
                Grid[0, 1].Position.Y = Grid[1, 0].Position.Y + Grid[1, 0].Size.Height;
            }
                // If Row 1 goes up too high...
            else if (Grid[1, 0].Position.Y < -Grid[1, 0].Size.Height)
            {
                Grid[1, 0].Position.Y = Grid[0, 0].Position.Y + Grid[0, 0].Size.Height;
                Grid[1, 1].Position.Y = Grid[0, 0].Position.Y + Grid[0, 0].Size.Height;
            }

            // If Row 0 goes too low...
            if (Grid[0, 0].Position.Y > Grid[0, 0].Size.Height)
            {
                Grid[0, 0].Position.Y = Grid[1, 0].Position.Y - Grid[1, 0].Size.Height;
                Grid[0, 1].Position.Y = Grid[1, 0].Position.Y - Grid[1, 0].Size.Height;
            }
                // If Row 1 goes too low...
            else if (Grid[1, 0].Position.Y > Grid[1, 0].Size.Height)
            {
                Grid[1, 0].Position.Y = Grid[0, 0].Position.Y - Grid[0, 0].Size.Height;
                Grid[1, 1].Position.Y = Grid[0, 0].Position.Y - Grid[0, 0].Size.Height;
            }


            // Also handle a second image as well
            if (bIsSwapping)
            {
                // If Row 0 goes up too high...
                if (SecondaryGrid[0, 0].Position.Y < -SecondaryGrid[0, 0].Size.Height)
                {
                    SecondaryGrid[0, 0].Position.Y = SecondaryGrid[1, 0].Position.Y + SecondaryGrid[1, 0].Size.Height;
                    SecondaryGrid[0, 1].Position.Y = SecondaryGrid[1, 0].Position.Y + SecondaryGrid[1, 0].Size.Height;
                }
                // If Row 1 goes up too high...
                else if (SecondaryGrid[1, 0].Position.Y < -SecondaryGrid[1, 0].Size.Height)
                {
                    SecondaryGrid[1, 0].Position.Y = SecondaryGrid[0, 0].Position.Y + SecondaryGrid[0, 0].Size.Height;
                    SecondaryGrid[1, 1].Position.Y = SecondaryGrid[0, 0].Position.Y + SecondaryGrid[0, 0].Size.Height;
                }

                // If Row 0 goes too low...
                if (SecondaryGrid[0, 0].Position.Y > SecondaryGrid[0, 0].Size.Height)
                {
                    SecondaryGrid[0, 0].Position.Y = SecondaryGrid[1, 0].Position.Y - SecondaryGrid[1, 0].Size.Height;
                    SecondaryGrid[0, 1].Position.Y = SecondaryGrid[1, 0].Position.Y - SecondaryGrid[1, 0].Size.Height;
                }
                // If Row 1 goes too low...
                else if (SecondaryGrid[1, 0].Position.Y > SecondaryGrid[1, 0].Size.Height)
                {
                    SecondaryGrid[1, 0].Position.Y = SecondaryGrid[0, 0].Position.Y - SecondaryGrid[0, 0].Size.Height;
                    SecondaryGrid[1, 1].Position.Y = SecondaryGrid[0, 0].Position.Y - SecondaryGrid[0, 0].Size.Height;
                }
            }
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bIsSwapping)
            {
                foreach (Sprite sp in SecondaryGrid)
                    sp.Draw(spriteBatch);
            }

            foreach (Sprite sp in Grid)
                sp.Draw(spriteBatch);           

        }
    }
}
