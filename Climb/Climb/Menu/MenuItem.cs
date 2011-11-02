/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Climb
{
    /// <summary>
    /// A menu item is an option in a menu.
    /// </summary>
    class MenuItem
    {
        //The draw color of the menu item. Mainly for transparency.
        private Color cAlpha = Color.Black;
        public Color Color
        {
            get { return Color; }
            set { cAlpha = value; }
        }
        /// <summary>
        /// Transparency of the item. 255 is fully visible.
        /// </summary>
        public byte Alpha 
        {
            get { return cAlpha.A; }
            set { cAlpha.A = value; }
        }

        private String sLabel;
        public String Label
        {
            get {return sLabel;}
            set { sLabel = value;}
        }

        // Get the length of the string in pixels
        public int Length
        {
            get { return (int)font.MeasureString(sLabel).X; }
        }

        SpriteFont font;
        public Vector2 Position = new Vector2(0,0);

        public float Scale = 1.0f;

        // The method that is called when the item is selected.
        public EventHandler SelectEvent;

        public MenuItem(EventHandler theMenuEvent, string theLabel)
        {
            SelectEvent = theMenuEvent;
            Label = theLabel;
        }

        public void LoadContent(ContentManager contentManager)
        {
            font = contentManager.Load<SpriteFont>("verdana");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, sLabel, Position, cAlpha, 0.0f, 
                (font.MeasureString(Label) / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
