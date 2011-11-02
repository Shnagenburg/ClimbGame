/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Climb
{
    /// <summary>
    /// This class can generate shapes based on the graphics device.
    /// It is currently not used.
    /// </summary>
    static class ShapeGenerator
    {
        static GraphicsDevice graphics = null;
        public static void SetGraphicsDevice(GraphicsDevice theGraphicsDevice)
        {
            graphics = theGraphicsDevice;
        }
        public static Texture2D MakeRectangle(int width, int height, Color color)
        {
            // create the rectangle texture, ,but it will have no color! lets fix that    
            Color alpha = new Color(255, 255, 255, 127);

            Texture2D rectangleTexture = new Texture2D(graphics, width, height, false, SurfaceFormat.Color);
            Color[] colorArray = new Color[width * height];//set the color to the amount of pixels
            for (int i = 0; i < colorArray.Length; i++)//loop through all the colors setting them to whatever values we want
            {
                colorArray[i] = alpha;
            }
            rectangleTexture.SetData(colorArray);//set the color data on the texture
            return rectangleTexture;            
        }

        public static Texture2D MakePixel()
        {
            Texture2D pixelTexture = new Texture2D(graphics, 1, 1);
            Color[] colorArray = { Color.White };
            pixelTexture.SetData(colorArray);
            return pixelTexture;
        }
    }
}
