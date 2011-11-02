using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ScreenDir = Climb.ScreenUtil.DestinationScreen;

using Climb.Util;

namespace Climb
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //time since last FPS update in seconds
        float deltaFPSTime = 0;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Screen currentScreen;
        MenuScreen menuScreen;
        GameScreen gameScreen;
        OptionsScreen optionsScreen;
        HighScoreScreen highscoreScreen;
        KeyboardState prevState;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menuScreen = new MenuScreen(new EventHandler(MenuScreenEvent));
            gameScreen = new GameScreen(new EventHandler(GameScreenEvent),this.Content);
            optionsScreen = new OptionsScreen(new EventHandler(OptionsScreenEvent), this.graphics);
            highscoreScreen = new HighScoreScreen(new EventHandler(HighScoreScreenEvent), this.Content, CUtil.Config);
            currentScreen = menuScreen;

            ShapeGenerator.SetGraphicsDevice(this.GraphicsDevice);
            

            //////////////////////GLOBAL VARIALBLEZ///////////////////
            // Sets the preferred resolution which get used as a basis for game
            // A scale matrix of 1.0f will run at this resolution
            // Xbox resolution / hd resolution is 1280x720 by default
            CUtil.SCREEN_HEIGHT_PREFMAX = 720;
            CUtil.SCREEN_WIDTH_PREFMAX = 1280;

            // Set the default resolutions
            graphics.PreferredBackBufferHeight = CUtil.SCREEN_HEIGHT_PREFMAX;
            graphics.PreferredBackBufferWidth = CUtil.SCREEN_WIDTH_PREFMAX;

            // Set the default scale matrix
            CUtil.ScaleMatrix = Matrix.CreateScale(1.0f);


            CUtil.GraphicsDevice = this.GraphicsDevice;
            CUtil.ContentManager = this.Content;
            CUtil.Graphics = this.graphics;

            if (Options.IsFullScreen)
            {
                graphics.IsFullScreen = Options.IsFullScreen;
                CUtil.SetResolution(CUtil.FullScreenResolution.X, CUtil.FullScreenResolution.Y);
            }
            else
            {
                CUtil.SetResolution(CUtil.InitalResolution.X, CUtil.InitalResolution.Y);
            }

            if (Options.IsLetterBox)
                CUtil.EnableLetterBox();
            

            CUtil.GenerateMOTD();
            ///////////////////////////////////////////////
            
            graphics.ApplyChanges();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Planning on this game being small, no loading screen
            menuScreen.LoadContent(this.Content);
            optionsScreen.LoadContent(this.Content);
            highscoreScreen.LoadContent(this.Content);

            prevState = Keyboard.GetState();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            ////////////////////////FPS STUFFFF/////////////
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float fps = 1 / elapsed;
            deltaFPSTime += elapsed;
            if (deltaFPSTime > 1)
            {
                Window.Title = "I am running at  <" + fps.ToString() + "> FPS";
                deltaFPSTime -= 1;
            }
            ////////////////////////FPS STUFFFF/////////////


            KeyboardState keyboardState = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            currentScreen.Update(gameTime, keyboardState, prevState);
            // TODO: Add your update logic here

            prevState = keyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null, null, null, CUtil.ScaleMatrix);
            currentScreen.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void MenuScreenEvent(Object o, EventArgs e)
        {
            ScreenDir destination = (ScreenDir)o;
            switch (destination)
            {
                case ScreenDir.Play:
                    
                    gameScreen = new GameScreen(new EventHandler(GameScreenEvent), this.Content);
                    gameScreen.LoadContent(Content);
                    currentScreen = gameScreen;
                    break;
                case ScreenDir.Options:
                    currentScreen = optionsScreen;
                    break;
                case ScreenDir.HighScore:
                    highscoreScreen.SetHighScoreLabel();
                    currentScreen = highscoreScreen;
                    break;
                case ScreenDir.Quit:
                    this.Exit();
                    break;
            }
        }

        public void GameScreenEvent(Object sender, EventArgs e)
        {
            currentScreen = menuScreen;
        }

        public void OptionsScreenEvent(Object sender, EventArgs e)
        {
            currentScreen = menuScreen;
        }

        public void HighScoreScreenEvent(Object sender, EventArgs e)
        {
            currentScreen = menuScreen;
        }

    }
}
