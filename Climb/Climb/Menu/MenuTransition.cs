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
    /// A menu transition happens when moving from one menu to another.
    /// Not fully implemented.
    /// </summary>
    class MenuTransition
    {
        Menu mMenu;
        TransitionStyle tStyle;
        private bool bDone = true;
        private Vector2 targetPosition;

        public enum TransitionStyle
        {
            FADE_IN,
            FADE_OUT,
            FLY_IN_TOP
        }

        public MenuTransition(TransitionStyle style, Menu targetMenu)
        {
            tStyle = style;
            mMenu = targetMenu;
        }

        public void Update(GameTime gameTime)
        {
            if (!bDone)
            {
                switch (tStyle)
                {
                    case TransitionStyle.FADE_IN:
                        UpdateFadeIn(gameTime);
                        break;
                    case TransitionStyle.FADE_OUT:
                        UpdateFadeOut(gameTime);
                        break;
                    case TransitionStyle.FLY_IN_TOP:
                        UpdateFlyInTop(gameTime);
                        break;
                }
            }
            
        }

        public void UpdateFadeOut(GameTime gameTime)
        {
            //if (mMenu.Alpha > 0)
            //    mMenu.Alpha -= 16;
            if (mMenu.Alpha > 16) // MAGIC NUMBERS FIX LATER
                mMenu.Alpha -= 8;
            else
                bDone = true;
        }

        public void UpdateFadeIn(GameTime gameTime)
        {
            //if (mMenu.Alpha < 255)
            //    mMenu.Alpha += 16;
            if (mMenu.Alpha < 240)
                mMenu.Alpha += 8;
            else
                bDone = true;
        }

        public void UpdateFlyInTop(GameTime gameTime)
        {
            if (mMenu.Position.Y < targetPosition.Y)
            {
                Vector2 v = new Vector2(0, 1200.0f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                mMenu.Position += v;
            }
            else
            {
                mMenu.Position = new Vector2(targetPosition.X, targetPosition.Y);
                bDone = true;
            }
        }

        public void Start()
        {
            bDone = false;

            switch (tStyle)
            {
                case TransitionStyle.FADE_IN:
                    mMenu.Alpha = 0;
                    break;
                case TransitionStyle.FADE_OUT:
                    mMenu.Alpha = 255;
                    break;
                case TransitionStyle.FLY_IN_TOP:
                    targetPosition = new Vector2(mMenu.Position.X, mMenu.Position.Y);
                    mMenu.Position = new Vector2(mMenu.Position.X, 0);
                    break;
            }
        }


    }
}
