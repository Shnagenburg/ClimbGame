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
    /// A label with a filled rectangle.
    /// </summary>
    class DanLabel
    {
        private Color cAlpha = Color.Black;
        public Color Color
        {
            get { return Color; }
            set { cAlpha = value; }
        }

        public Vector2 Position = new Vector2(0,0);
        private MenuBorder mBorder = new MenuBorder();

        private SpriteFont font;

        private string sText = "";
        public String Text
        {
            get { return sText; }
            set { sText = value; }
        }
        private int iWidth;
        private int iHeight;

        public DanLabel(int x, int y, int width, int height)
        {
            Position = new Vector2(x,y);
            
            iWidth = width;
            iHeight = height;
        }

        public DanLabel(int x, int y, int width, int height, string message) : this(x,y,width,height)
        {
            sText = message;
        }

        public void LoadContent(ContentManager contentManager)
        {

            font = contentManager.Load<SpriteFont>("verdana");
         
            // Set up the border/background and set default color to white.
            mBorder.LoadContent(contentManager);
            mBorder.SetColors(Gradients.TransparentBlueGradient);
            mBorder.Thickness = 2;

            mBorder.Position = new Point((int)Position.X, (int)Position.Y);
            mBorder.Width = iWidth;
            mBorder.Height = iHeight;


            Position.X += mBorder.Width / 2;
            Position.Y += mBorder.Height / 2;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mBorder.Draw(spriteBatch);

            spriteBatch.DrawString(font, sText, Position, cAlpha, 0.0f,
                (font.MeasureString(sText) / 2), 1.0f, SpriteEffects.None, 0);
        }



    }
}
