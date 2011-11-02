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
    /// The options screen lets you configure the game.
    /// </summary>
    class OptionsScreen : Screen
    {
        LayeredBackground lbBackground;
        Menu mMainMenu, mMusicMenu, mHeroMenu, mResolutionMenu, mCurrentMenu;
        GraphicsDeviceManager graphics;

        /// <summary>
        /// Create a new options menu.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        /// <param name="graphics"></param>
        public OptionsScreen(EventHandler theScreenEvent,GraphicsDeviceManager graphics)
            : base(theScreenEvent)
        {
            //Load the background texture for the screen
            this.graphics = graphics;
            lbBackground = new LayeredBackground();
            mMainMenu = new Menu(new Vector2(50, 100));
            mMusicMenu = new Menu(new Vector2(250, 100), mMainMenu, new EventHandler(BackOutEvent));
            mHeroMenu = new Menu(new Vector2(250, 100), mMainMenu, new EventHandler(BackOutEvent));
            mResolutionMenu = new Menu(new Vector2(250, 100), mMainMenu, new EventHandler(BackOutEvent));
        }

        /// <summary>
        /// Load the content for the options menu.
        /// </summary>
        /// <param name="contentManager"></param>
        public override void LoadContent(ContentManager contentManager)
        {
            string[] assets = { "Backgrounds/dusksky1280x960", "Clouds/cloudsthree" };
            lbBackground.LoadContent(contentManager, assets);


            string[] opts = { "Resolution!", "Music!", "Hero!", "Ok!"};
            EventHandler[] handlers = { new EventHandler(SelectResolutionEvent), new EventHandler(SelectMusicEvent),
                                          new EventHandler(SelectHeroEvent), new EventHandler(SelectConfirmEvent)};
            mMainMenu.LoadContent(contentManager, opts, handlers);
            mMainMenu.SetBGColors(Gradients.GoldGradient);

            string[] opts2 = { "None!", "Punk!", "Chill!" };
            EventHandler[] handlers2 = { new EventHandler(SelectMusicNoneEvent), new EventHandler(SelectMusicPunkEvent),
                                          new EventHandler(SelectMusicChillEvent) };
            mMusicMenu.LoadContent(contentManager, opts2, handlers2);


            string[] opts3 = { "Barry!", "Seamus'San!" };
            EventHandler[] handlers3 = { new EventHandler(SelectHeroBarryEvent), new EventHandler(SelectHeroSeamusEvent) };
            mHeroMenu.LoadContent(contentManager, opts3, handlers3);

            string[] opts4 = { "1600x1200", "1280x1024", "1280x720", "1024x768", "1024x576",
                                 "800x600",  "800x480", "FullScreen", "LetterBox"};
            EventHandler[] handlers4 = {    new EventHandler(Select1600x1200Event), 
                                           new EventHandler(Select1280x1024Event), new EventHandler(Select1280x720Event), 
                                           new EventHandler(Select1024x768Event), new EventHandler(Select1024x576Event), 
                                           new EventHandler(Select800x480Event), new EventHandler(Select800x600Event), 
                                           new EventHandler(EnableFullScreenEvent), new EventHandler(EnableLetterBoxEvent)};
            mResolutionMenu.LoadContent(contentManager, opts4, handlers4);

            mCurrentMenu = mMainMenu;
        }


        /// <summary>
        /// Update the option screen's elements.
        /// </summary>
        /// <param name="theTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public override void Update(GameTime theTime, KeyboardState keyState, KeyboardState prevState)
        {
            lbBackground.Update(theTime);

            mCurrentMenu.Update(theTime, keyState, prevState);

            if (keyState.IsKeyDown(Keys.G) && prevState.IsKeyUp(Keys.G))
            {
                CUtil.Graphics.IsFullScreen = !CUtil.Graphics.IsFullScreen;
                CUtil.Graphics.ApplyChanges();
            }

            base.Update(theTime, keyState);
        }

        /// <summary>
        /// Draw the option screen's elements.
        /// </summary>
        /// <param name="theBatch"></param>
        public override void Draw(SpriteBatch theBatch)
        {
            lbBackground.Draw(theBatch);

            mCurrentMenu.Draw(theBatch);
            base.Draw(theBatch);
        }


        #region Main Options Menu Events

        public void SelectResolutionEvent(object o, EventArgs e)
        {
            mCurrentMenu = mResolutionMenu;
            return;
        }


        public void SelectMusicEvent(object o, EventArgs e)
        {
            mCurrentMenu = mMusicMenu;
            return;
        }

        public void SelectHeroEvent(object o, EventArgs e)
        {
            mHeroMenu.TransitionEnter.Start();
            mCurrentMenu = mHeroMenu;
            return;
        }

        public void SelectConfirmEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Quit, new EventArgs());
            return;
        }

        // Not used
        public void SelectCancelEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Quit, new EventArgs());
            return;
        }
        #endregion

        #region Music Menu Events
        public void SelectMusicPunkEvent(object o, EventArgs e)
        {
            Options.CurrentMusicSelection = Options.MusicSelection.PUNK;
            mCurrentMenu = mMainMenu;
            return;
        }

        public void SelectMusicChillEvent(object o, EventArgs e)
        {
            Options.CurrentMusicSelection = Options.MusicSelection.CHILL;
            mCurrentMenu = mMainMenu;
            return;
        }

        public void SelectMusicNoneEvent(object o, EventArgs e)
        {
            Options.CurrentMusicSelection = Options.MusicSelection.NONE;
            mCurrentMenu = mMainMenu;
            return;
        }
        #endregion

        #region Character Menu Events

        public void SelectHeroBarryEvent(object o, EventArgs e)
        {
            Options.CurrentHeroSelection = Options.HeroSelection.BARRY;
            mCurrentMenu = mMainMenu;
            return;
        }

        public void SelectHeroSeamusEvent(object o, EventArgs e)
        {
            Options.CurrentHeroSelection = Options.HeroSelection.SEAMUS;
            mCurrentMenu = mMainMenu;
            return;
        }

        #endregion

        #region Resolution MenuEvents

        public void Select1600x1200Event(object o, EventArgs e)
        {
            CUtil.SetResolutionAndWindowed(1600, 1200);

            mCurrentMenu = mMainMenu;
            return;
        }

        public void Select1280x1024Event(object o, EventArgs e)
        {
            CUtil.SetResolutionAndWindowed(1280, 1024);
            mCurrentMenu = mMainMenu;
            return;
        }

        public void Select1280x720Event(object o, EventArgs e)
        {
            //Set resolution stuff here and resize, perhaps make a global in CUtil
            CUtil.SetResolutionAndWindowed(1280, 720);

            mCurrentMenu = mMainMenu;
            return;
        }

        public void Select1024x768Event(object o, EventArgs e)
        {
            CUtil.SetResolutionAndWindowed(1024, 768);

            mCurrentMenu = mMainMenu;
            return;
        }
        public void Select1024x576Event(object o, EventArgs e)
        {
            CUtil.SetResolutionAndWindowed(1024, 576);

            mCurrentMenu = mMainMenu;
            return;
        }

        public void Select800x600Event(object o, EventArgs e)
        {
            //Set resolution stuff here and resize, perhaps make a global in CUtil
            CUtil.SetResolutionAndWindowed(800, 600);
            
            mCurrentMenu = mMainMenu;
            return;
        }
        public void Select800x480Event(object o, EventArgs e)
        {
            //Set resolution stuff here and resize, perhaps make a global in CUtil
            CUtil.SetResolutionAndWindowed(800, 480);

            mCurrentMenu = mMainMenu;
            return;
        }        

        // Not used
        //public void Select480x320Event(object o, EventArgs e)
        //{
        //    Set resolution stuff here and resize, perhaps make a global in CUtil
        //    CUtil.SetResolutionAndWindowed(480, 320);

        //    mCurrentMenu = mMainMenu;
        //    return;
        //}

        // Not used
        //public void Select240x160Event(object o, EventArgs e)
        //{
        //    Set resolution stuff here and resize, perhaps make a global in CUtil
        //    CUtil.SetResolutionAndWindowed(1024, 576);

        //    mCurrentMenu = mMainMenu;
        //    return;
        //}

        public void EnableFullScreenEvent(object o, EventArgs e)
        {
            CUtil.EnableFullScreen();
        }
        public void EnableLetterBoxEvent(object o, EventArgs e)
        {
            CUtil.EnableLetterBox();
        }


        #endregion

        // Just about any menu can use this event to back out
        public void BackOutEvent(object o, EventArgs e)
        {
            if (mCurrentMenu.Parent != null)
                mCurrentMenu = mCurrentMenu.Parent;

            return;
        }
    }
}
