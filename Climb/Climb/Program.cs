using System;
using Climb.Util;
namespace Climb
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // load the config first thing
            // If it doesn't find something prompt the form.
            if (!MySerializer.LoadConfig())
            {

                Form1 form = new Form1();
                form.ShowDialog();               

                if (form.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                {
                    using (Game1 game = new Game1())
                    {
                        game.Run();
                    }
                }
            }
            else // If there was a config just run the game
            {
                CUtil.InitalResolution.X = CUtil.Config.ScreenWidth;
                CUtil.InitalResolution.Y = CUtil.Config.ScreenHeight;

                Options.IsFullScreen = CUtil.Config.IsFullscreen;
                Options.IsLetterBox = CUtil.Config.IsLetterbox;

                CUtil.FullScreenResolution.X = CUtil.Config.FullScreenWidth;
                CUtil.FullScreenResolution.Y = CUtil.Config.FullScreenHeight;

                Options.IsSFXOn = CUtil.Config.IsSFXOn;

                Options.CurrentMusicSelection = CUtil.Config.MusicSelection;
                Options.CurrentHeroSelection = CUtil.Config.HeroSelection;

                using (Game1 game = new Game1())
                {
                    game.Run();
                }
            }

        }
    }
#endif
}

