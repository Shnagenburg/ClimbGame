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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using Climb.Util;

namespace Climb
{
    /// <summary>
    /// The menu screen is the main menu players see when starting the game.
    /// </summary>
    class MenuScreen : Screen
    {
        LayeredBackground lbBackground;

        //Title texture for the screen
        Sprite spTitle;

        Menu mMenu;
        DanLabel dlLabel;
        /// <summary>
        /// Create a new main menu screen.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        public MenuScreen(EventHandler theScreenEvent): base(theScreenEvent)
        {
            //Load the background texture for the screen
            lbBackground = new LayeredBackground();
            spTitle = new Sprite();
            mMenu = new Menu(new Vector2(300, 250));
        }

        /// <summary>
        /// Load the main menu screen.
        /// </summary>
        /// <param name="contentManager"></param>
        public override void LoadContent(ContentManager contentManager)
        {
            // Set the background.
            string [] assets = { "Backgrounds/space", "Clouds/cloudsspace"};
            lbBackground.LoadContent(contentManager, assets);

            // Set the Title picture
            spTitle.LoadContent(contentManager, "Sprites/climbtitle");
            spTitle.Position = new Vector2(100, 50);

            // Set up the menu.
            string [] opts = { "Play!", "Options!", "High Scores!", "Quit!" };
            EventHandler[] handlers = { new EventHandler(SelectPlayEvent), new EventHandler(SelectOptionsEvent),
                                          new EventHandler(SelectHighScoreEvent), new EventHandler(SelectQuitEvent) };
            mMenu.LoadContent(contentManager, opts, handlers);
            mMenu.SetBGColors(Gradients.TransparentBlueGradient);
            
            // Set up the MOTD.
            dlLabel = new DanLabel(100, 550, 600, 100, CUtil.MOTD);
            dlLabel.LoadContent(contentManager);

            // audio testing
            Song song = contentManager.Load<Song>("Audio/Sun and My Synth LOOPY");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(song);
            MediaPlayer.Stop();
            SoundEffect soundEffect = contentManager.Load<SoundEffect>("Audio/jumpsound");
            soundEffect.Play();
        }


        /// <summary>
        /// Update the main menu's elements
        /// </summary>
        /// <param name="theTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public override void Update(GameTime theTime, KeyboardState keyState, KeyboardState prevState)
        {
            lbBackground.Update(theTime);

            mMenu.Update(theTime, keyState, prevState);
       
            base.Update(theTime, keyState);
        }

        /// <summary>
        /// Draw the main menu's elements.
        /// </summary>
        public override void Draw(SpriteBatch theBatch)
        {
            lbBackground.Draw(theBatch);

            spTitle.Draw(theBatch);
            mMenu.Draw(theBatch);
            dlLabel.Draw(theBatch);

            base.Draw(theBatch);
        }

        public void SelectPlayEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Play, new EventArgs());
            return;
        }

        public void SelectOptionsEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Options, new EventArgs());
            return;
        }

        public void SelectQuitEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.Quit, new EventArgs());
            return;
        }

        public void SelectHighScoreEvent(object o, EventArgs e)
        {
            ScreenEvent.Invoke(ScreenUtil.DestinationScreen.HighScore, new EventArgs());
            return;
        }

    }
}
