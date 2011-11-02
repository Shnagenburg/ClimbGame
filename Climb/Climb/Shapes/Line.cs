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
    /// A line connecting two points.
    /// </summary>
    class Line
    {
        // Alpha value of line color
        private Color cAlpha = Color.Black;
        public byte Alpha
        {
            get { return cAlpha.A; }
            set { cAlpha.A = value;}
        }

        // The two points
        private Point pPoint1;
        public Point Point1
        {
            get { return pPoint1; }
            set
            {
                pPoint1 = value;
                rect.Location = pPoint1;
                RecalcuateTheta();
            }
        }

        private Point pPoint2;
        public Point Point2
        {
            get { return pPoint2; }
            set
            {
                pPoint2 = value;
                RecalcuateTheta();
            }
        }

        // Angle of the line
        private float fTheta;
        public float Theta
        {
            get { return fTheta; }
            set { fTheta = value; }
        }

        // The rectangle is a strip of pixels. The height is
        // the thickness of the line. The width is the length
        private Rectangle rect = new Rectangle(1,1,1,1);

        public int Thickness
        {
            set { rect.Height = value; }
        }
        // The pixel that we stretch into the rectangle.
        private Texture2D tPixel;

        public void LoadContent(GraphicsDevice graphics)
        {
            // Make the single pixel texture
            tPixel = new Texture2D(graphics, 1, 1);

            // Set the color of the pixel to white.
            // This isnt the real color, the actual color is specified in draw
            Color[] colorArray = { Color.White };
            tPixel.SetData(colorArray);

            // Put rect into starting position
            rect.Location = pPoint1;
        }

        /// <summary>
        /// Draw the line.
        /// </summary>
        /// <param name="theBatch"></param>
        public void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(tPixel, rect, null, cAlpha, fTheta, new Vector2(0, 0.5f) , SpriteEffects.None, 0);
        }

        /// <summary>
        /// Update the angel of the line.
        /// </summary>
        private void RecalcuateTheta()
        {
            int width = pPoint2.X - pPoint1.X;
            int height = pPoint2.Y - pPoint1.Y;

            int length = (width * width) + (height * height);
            length = (int)Math.Sqrt((double)length);

            fTheta = (float)Math.Atan((double)height / (double)width);

            if (pPoint2.X < pPoint1.X)
                fTheta = fTheta + 3.141f;
            Console.WriteLine(length);
            rect.Width = length;
        }

    }
}
