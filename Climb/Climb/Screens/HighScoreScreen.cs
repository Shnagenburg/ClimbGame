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
    /// The highscore screen surprisingly enough displays the high scores.
    /// </summary>
    class HighScoreScreen : Screen
    {
        LayeredBackground lbBackground;
        DanLabel dlHighscoreText;

        // The config stores the high scores.
        MyConfig config;


        /// <summary>
        /// Create a new high score screen.
        /// </summary>
        /// <param name="theScreenEvent"></param>
        /// <param name="theContent"></param>
        /// <param name="myConfig">The high scores are stored in the config object.</param>
        public HighScoreScreen(EventHandler theScreenEvent, ContentManager theContent, MyConfig myConfig)
            : base(theScreenEvent)
        {
            config = myConfig;

            lbBackground = new LayeredBackground();

            dlHighscoreText = new DanLabel(600, 100, 200, 400);
        }

        /// <summary>
        /// Load the content for the screen.
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {

            lbBackground.LoadContent(content, Backgrounds.Space);
            dlHighscoreText.LoadContent(content);
            SetHighScoreLabel();

            base.LoadContent(content);
        }


        /// <summary>
        /// Update the HighScoreScreen.
        /// </summary>
        /// <param name="theTime"></param>
        /// <param name="keyState"></param>
        /// <param name="prevState"></param>
        public override void Update(GameTime theTime, KeyboardState keyState, KeyboardState prevState)
        {
            lbBackground.Update(theTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Back) || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                    ScreenEvent.Invoke(this, new EventArgs());
                    return;
            }
       
            base.Update(theTime, keyState);
        }

        /// <summary>
        /// Draw the HighScoreScreen.
        /// </summary>
        /// <param name="theBatch"></param>

        public override void Draw(SpriteBatch theBatch)
        {
            lbBackground.Draw(theBatch);

            dlHighscoreText.Draw(theBatch);

            base.Draw(theBatch);
        }

        /// <summary>
        /// Sets the label for the high score screen.
        /// </summary>
        public void SetHighScoreLabel()
        {
            string text = "";
            for (int i = 0; i < config.Highscores.Length; i++)
            {
                if (config.Highscores[i] == 0)
                    text += "---\n";
                else
                    text += "" + config.Highscores[i] + "\n";
            }

            dlHighscoreText.Text = text;
        }

    }
}
