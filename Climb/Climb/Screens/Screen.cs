/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Climb
{
    class Screen
    {
        //The event associated with the Screen. This event is used to raise events
        //back in the main game class to notify the game that something has changed
        //or needs to be changed
        protected EventHandler ScreenEvent;
        public Screen(EventHandler theScreenEvent)
        {
            ScreenEvent = theScreenEvent;
        }

        //Load some content
        public virtual void LoadContent(ContentManager content)
        {
        }

        //Update any information specific to the screen
        public virtual void Update(GameTime theTime, KeyboardState keyState)
        {
        }

        //Some need previous state
        public virtual void Update(GameTime theTime, KeyboardState keyState, KeyboardState prevState)
        {
        }

        //Draw any objects specific to the screen
        public virtual void Draw(SpriteBatch theBatch)
        {
        }


    }
}
