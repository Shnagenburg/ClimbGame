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

using Climb.Util;
namespace Climb
{
    /// <summary>
    /// A menu is a set of MenuItems that can be used to make selections
    /// </summary>
    class Menu
    {
        // The filled rectangle and lined border of the menu
        MenuBorder border =  new MenuBorder();

        // For whether the pulsing item is shrinking/enlarging.
        private bool bPulseDir = true;

        List<MenuItem> lMenuItems;
        MenuItem miSelected; // The menu item we have in focus.

        Vector2 vPosition = new Vector2(0, 0);
        public Vector2 Position
        {
            set 
            {
                vPosition = value;
                foreach (MenuItem item in lMenuItems)
                    item.Position = value;

                CalculateBorders();
            }
            get { return vPosition; }
        }

        // Verticle space between menu items
        public int Spacing = 64;

        // Use this to tell where the menu came from to easily back out of a menu
        public Menu Parent;

        // The event that backspace calls
        private EventHandler BackOutEvent;

        public byte Alpha
        {
            set 
            { 
                border.Alpha = value;
                foreach (MenuItem item in lMenuItems)
                    item.Alpha = value;
            }
            get { return border.Alpha; }
        }

        // Implement later for transitions between menus
        public MenuTransition TransitionEnter;
        public MenuTransition TransitionExit;

        public Menu() { }
        public Menu(Vector2 thePosition)
        {
            vPosition = thePosition;
        }

        /// <summary>
        /// Create a new Menu
        /// </summary>
        /// <param name="thePosition">Position of the menu.</param>
        /// <param name="theParent">The menu this menu came from.</param>
        /// <param name="theBackOutEvent">The event fired when backspace is pressed.</param>
        public Menu(Vector2 thePosition, Menu theParent, EventHandler theBackOutEvent)
        {
            vPosition = thePosition;
            Parent = theParent;
            BackOutEvent = theBackOutEvent;
        }

        /// <summary>
        /// Load the content for the menu
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="options">The names of the options.</param>
        /// <param name="handlers">The event that is fired when the corrosponding option is selected.</param>
        public void LoadContent(ContentManager contentManager, string[] options, EventHandler[] handlers)
        {
            lMenuItems = new List<MenuItem>();
            for (int i = 0; i < options.Length; i++)
            {
                MenuItem item = new MenuItem(handlers[i], options[i]);
                item.LoadContent(contentManager);
                item.Position.Y = i * Spacing; // distance apart, default 64
                item.Position += vPosition; // Can be specified, 0 otherwise
                lMenuItems.Add(item);
            }
            miSelected = lMenuItems[0];

            // Set up the border/background and set default color to white.
            border.LoadContent(contentManager);
            CalculateBorders();
            border.SetColors(Gradients.WhiteGradient);
            border.Thickness = 2;

            TransitionEnter = new MenuTransition(MenuTransition.TransitionStyle.FLY_IN_TOP, this);
            TransitionExit = new MenuTransition(MenuTransition.TransitionStyle.FADE_OUT, this);
        }

        /// <summary>
        /// Update the menu.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public void Update(GameTime gameTime, KeyboardState keyState, KeyboardState prevState)
        {
            // If we are moving down an item.
            if ( (keyState.IsKeyDown(Keys.S) && prevState.IsKeyUp(Keys.S))
                || (keyState.IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down)))
            {
                if (lMenuItems.IndexOf(miSelected) == lMenuItems.Count - 1)
                {
                    miSelected.Scale = 1.0f;
                    miSelected = lMenuItems[0];
                }
                else
                {
                    miSelected.Scale = 1.0f;
                    miSelected = lMenuItems[lMenuItems.IndexOf(miSelected) + 1];
                }
            }
                // If we are moving up an item.

            else if ( (keyState.IsKeyDown(Keys.W) && prevState.IsKeyUp(Keys.W))
                || (keyState.IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up)))
            {
                if (lMenuItems.IndexOf(miSelected) == 0)
                {
                    miSelected.Scale = 1.0f;
                    miSelected = lMenuItems[lMenuItems.Count - 1];
                }
                else
                {
                    miSelected.Scale = 1.0f;
                    miSelected = lMenuItems[lMenuItems.IndexOf(miSelected) - 1];
                }
            }
                // If we are selecting an item.
            else if (keyState.IsKeyDown(Keys.Enter) && prevState.IsKeyUp(Keys.Enter) && prevState != null )
            {
                miSelected.SelectEvent.Invoke(this, new EventArgs());
                return;
            }
                // If we are backing out of the menu.
            else if (keyState.IsKeyDown(Keys.Back) && prevState.IsKeyUp(Keys.Back) && prevState != null)
            {
                // A backspace event is not required for a menu to have, so only do it if it was specified.
                if (BackOutEvent != null)
                    BackOutEvent.Invoke(this, new EventArgs());
                return;
            }            

            // Pulse the selected item.
            UpdatePulse(gameTime);

            TransitionEnter.Update(gameTime);
            TransitionExit.Update(gameTime);
        }

        /// <summary>
        /// Shrink/Enlarge the selected item.
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdatePulse(GameTime gameTime)
        {
            if (miSelected.Scale <= 1.1f && miSelected.Scale >= 0.9f && bPulseDir)
                miSelected.Scale += 0.3f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (miSelected.Scale <= 1.1f && miSelected.Scale >= 0.9f && !bPulseDir)
                miSelected.Scale -= 0.3f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (miSelected.Scale >= 1.1f  && bPulseDir)
            {
                bPulseDir = false;
                miSelected.Scale = 1.1f;
            }
            else if (miSelected.Scale < 0.9f && !bPulseDir)
            {
                bPulseDir = true;
                miSelected.Scale = 0.9f;
            }
        }

        /// <summary>
        /// Draw the menu.
        /// </summary>
        /// <param name="theBatch"></param>
        public void Draw(SpriteBatch theBatch)
        {

            border.Draw(theBatch);

            foreach (MenuItem item in lMenuItems)
            {
                item.Draw(theBatch);
            }

            if (Parent != null)
            {
                Parent.border.Draw(theBatch);
                foreach (MenuItem item in Parent.lMenuItems)
                {
                    item.Draw(theBatch);
                }
            }
        }

        /// <summary>
        /// Measure the menu to make an appropriate border.
        /// Needs to be done when moved or items have been added.
        /// </summary>
        private void CalculateBorders()
        {
            border.Position = new Point((int)vPosition.X, (int)vPosition.Y);
            int maxLength = 0;
            
            // Find how wide our menu must be.
            foreach (MenuItem item in lMenuItems)
            {
                if (maxLength < item.Length)
                    maxLength = item.Length;
            }
            border.Width = (int)(maxLength * 1.2f);
            border.Height = lMenuItems.Count * Spacing;

            // The menu items need to know how wide the menu is to get the centering correct
            for (int i = 0; i < lMenuItems.Count; i++ )
            {
                lMenuItems[i].Position = vPosition;
                lMenuItems[i].Position.X += border.Width / 2;
                lMenuItems[i].Position.Y += (Spacing * i) + (Spacing/2) - 2; // A bit weird, but this centers it pretty well
            }
        }

        /// <summary>
        /// Set the background colors for the menu.
        /// </summary>
        /// <param name="gradient">A set of four colors that makes the menu gradient.</param>
        public void SetBGColors(Color[] gradient)
        {
            border.SetColors(gradient);
        }

        /// <summary>
        /// Set the color of the text.
        /// </summary>
        /// <param name="color"></param>
        public void SetTextColor(Color color)
        {
            foreach (MenuItem item in lMenuItems)
            {
                item.Color = color;
            }
        }
    }
}
