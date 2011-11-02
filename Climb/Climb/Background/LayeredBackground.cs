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

using Climb.Util;

namespace Climb
{
    /// <summary>
    /// A layered background is comprised of BGLayers.
    /// </summary>
    class LayeredBackground
    {
        //const int SCREEN_HEIGHT = 640;
        //const int SCREEN_WIDTH = 800;

        const int SCREEN_HEIGHT = 720;
        const int SCREEN_WIDTH = 1280;

        private bool bIsSwapping = false;
        private int iSwapIndex;

        public byte Alpha
        {
            get { return layers[0].Alpha; }
            set
            {
                foreach (BGLayer layer in layers)
                    layer.Alpha = value;
            }

        }

        /// <summary>
        /// When fading, set Tint to black to completely remove sprite with alpha value of 255
        /// </summary>
        public Color Tint
        {
            get { return layers[0].Tint; }
            set
            {
                foreach (BGLayer layer in layers)
                    layer.Tint = value;
            }
        }

        // The list of sprites that make up the background
        public List<BGLayer> layers = new List<BGLayer>();
        
        public LayeredBackground()
        {
        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager contentManager, string[] assets)
        {
            for (int i = 0; i < assets.Length; i++)
            {
                BGLayer layer = new BGLayer();
                layer.LoadContent(contentManager, assets[i], i);
                layers.Add(layer);
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (BGLayer layer in layers)
            {
                layer.Update(gameTime);
            }            
        }

        public void UpdateVertical(GameTime gameTime)
        {
            foreach (BGLayer layer in layers)
                layer.UpdateVerticle(gameTime);

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Draw(spriteBatch);
            }
        }

        public void Insert(string asset)
        {
            //BGLayer layer = new BGLayer();
            //layer.LoadContent(CUtil.ContentManager, asset, 0);
            //layers.RemoveRange(0,4);
            //layers.Insert(0, layer);
        }

        /// <summary>
        /// Swaps out a layer with another while fading in / out
        /// </summary>
        /// <param name="asset"></param>
        public void SwapWithFade(string asset, int index)
        {
            bIsSwapping = true;
            iSwapIndex = index;

            // Make the new layer and push it on the front of the list
            BGLayer layer = new BGLayer();
            layer.LoadContent(CUtil.ContentManager, asset, iSwapIndex);
            layer.Tint = Color.Black;
            layer.Alpha = 0;

            layers.Insert(iSwapIndex,layer);
        }
    }
}

