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
using Microsoft.Xna.Framework.Storage;

namespace Climb.Util
{
    /// <summary>
    /// This class is the core utilities and global variables for the game.
    /// I'm not going to lie, I probably break a lot of OOP principles by using it so much.
    /// It holds a lot of useful things like the Content Manager and the Graphics Device,
    /// As well as methods for changing the Resolution.
    /// </summary>
    static class CUtil
    {
        public static Point FullScreenResolution = new Point();
        public static Point InitalResolution = new Point();
        public static GraphicsDevice GraphicsDevice;
        public static ContentManager ContentManager;
        public static GraphicsDeviceManager Graphics;
        public static Matrix ScaleMatrix;

        // This is the magic int that says what the preferred resolution of the game is.
        // (where everything is 1 to 1 with the images and no scaling is applied)
        public static int SCREEN_WIDTH_PREFMAX; 
        public static int SCREEN_HEIGHT_PREFMAX;
        public static string MOTD = "";
        public static MyConfig Config;

        //Generate a random motd, all cool indie games have them.
        public static void GenerateMOTD()
        {
            Random random = new Random();
            int val = random.Next(5);

            // These are mostly lies, fix them
            switch (val)
            {
                case 0:
                    MOTD = "Snails exfolitate choloroform!";
                    break;
                case 1:
                    MOTD = "An average whale weighs as \nmuch as 65 mid sized caddies!";
                        break;
                case 2:
                    MOTD = "Become the legend, climb the boxes!";
                    break;
                case 3:
                    MOTD = "Space to jump, escape to escape!";
                    break;
                case 4:
                    MOTD = "Zero to one players!";
                    break;
                case 5:
                    MOTD = "Less than or equal to a flash game!";
                    break;
            
            }

        }

        private static int iResolutionWidth;
        public static int ResolutionWidth
        {
            set 
            {
                Graphics.PreferredBackBufferWidth = value;
                iResolutionWidth = value;
                ScaleMatrix = Matrix.CreateScale(
                    (float)iResolutionWidth / (float)SCREEN_WIDTH_PREFMAX,
                    (float)iResolutionHeight / (float)SCREEN_HEIGHT_PREFMAX,
                    1.0f);
                Graphics.ApplyChanges();
            }
            get { return iResolutionWidth; }
        }

        private static int iResolutionHeight;
        public static int ResolutionHeight
        {
            set 
            { 
                Graphics.PreferredBackBufferHeight = value;
                iResolutionHeight = value;
                ScaleMatrix = Matrix.CreateScale(
                    (float)iResolutionWidth / (float)SCREEN_WIDTH_PREFMAX,
                    (float)iResolutionHeight / (float)SCREEN_HEIGHT_PREFMAX,
                    1.0f);
                Graphics.ApplyChanges();
            }
            get { return iResolutionHeight; 
            }
        }

        public static void SetResolution(int newWidth, int newHeight)
        {
            //These calls will set up the prefback height with the setters
            ResolutionWidth = newWidth;
            ResolutionHeight = newHeight;            
        }

        public static void SetResolutionAndWindowed(int newWidth, int newHeight)
        {
            //These calls will set up the prefback height with the setters
            ResolutionWidth = newWidth;
            ResolutionHeight = newHeight;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();
        }

        public static void EnableFullScreen()
        {
            SetResolution(FullScreenResolution.X, FullScreenResolution.Y);
            Graphics.IsFullScreen = true;
            
            Graphics.ApplyChanges();
        }

        public static void EnableLetterBox()
        {
            SetResolution(SCREEN_WIDTH_PREFMAX, SCREEN_HEIGHT_PREFMAX);
            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
        }

        // REVISIT needs to be added to save options and highscores.
        public static void SaveConfig()
        {
            //config.magic = 5;

            //// Open a storage container.
            //IAsyncResult result =
            //    device.BeginOpenContainer("StorageDemo", null, null);

            //// Wait for the WaitHandle to become signaled.
            //result.AsyncWaitHandle.WaitOne();

            //StorageContainer container = device.EndOpenContainer(result);

            //// Close the wait handle.
            //result.AsyncWaitHandle.Close();

            //string filename = "savegame.sav";

            //// Check to see whether the save exists.
            //if (container.FileExists(filename))
            //    // Delete it so that we can create one fresh.
            //    container.DeleteFile(filename);

            //// Create the file.
            //Stream stream = container.CreateFile(filename);

            //// Convert the object to XML data and put it in the stream.
            //XmlSerializer serializer = new XmlSerializer(typeof(MyConfig));

            //serializer.Serialize(stream, config;

            //// Close the file.
            //stream.Close();

            //// Dispose the container, to commit changes.
            //container.Dispose();
        }

        /// <summary>
        /// Update the list of high scores.
        /// </summary>
        /// <param name="score"></param>
        public static void AddScore(float score)
        {
            score /= 10;

            for (int i = 0; i < Config.Highscores.Length; i++)
            {
                if (Config.Highscores[i] < score)
                {
                    Config.Highscores[i] = (int)score;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// This class has a bunch of four colored gradients I created.
    /// </summary>
    static class Gradients
    {
        public static Color[] BlueGradient = { Color.CornflowerBlue, Color.Blue, Color.Blue, Color.Navy };
        public static Color[] RedGradient = { Color.Pink, Color.Red, Color.Red, Color.Crimson };
        public static Color[] RedGradientVerticle = { Color.Red, Color.Red, Color.DarkRed, Color.DarkRed };
        public static Color[] GreenGradient = { Color.LightGreen, Color.Green, Color.Green, Color.DarkGreen };
        public static Color[] GoldGradient = { Color.Yellow, Color.Gold, Color.Gold, Color.DarkGoldenrod };
        public static Color[] WhiteGradient = { Color.White, Color.WhiteSmoke, Color.WhiteSmoke, Color.Gray };
        public static Color[] TransparentBlueGradient = { new Color(0, 0, 255, 127), new Color(0, 0, 255, 127), 
                                                        new Color(0,0,64,127), new Color(0,0,32,127) };
        public static Color[] TransparentRedGradient = { new Color(255, 0, 0, 127), new Color(255, 0, 0, 127), 
                                                        new Color(127,0,0,180), new Color(127,0,0,180) };

    }

    /// <summary>
    /// Sets of strings for the LayeredBackgrounds.
    /// In general, backgrounds are 3 layers deep but they can be more or less.
    /// </summary>
    static class Backgrounds
    {
        public static string[] Space = { "Backgrounds/space", "Clouds/cloudsspace" };

        // Note that making the initial backgrounds not 3 will cause a weird disposal error unless a hardcoded
        // check is put into the BGLayer class
        public static string[] INITAL_BACKGROUNDS = { "Backgrounds/dusksky1280x960", "Clouds/cloudssuper", "Clouds/cloudssuper" };
    }

    /// <summary>
    /// The animation sets the game uses.
    /// </summary>
    public static class AnimationSets
    {
        public static string [] TestHeroSwap = { "Sprites/pixeldude", "Sprites/pixeldude2" };
        public static int [] TestHeroSwapOrder = { 0, 1 };
    }

}


