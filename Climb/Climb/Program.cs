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


            // load the config
            MyConfig myconfig = new MyConfig();
            CUtil.Config = myconfig;

            Form1 form = new Form1();
            form.ShowDialog();
            if (form.DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                //using (Game1 game = new Game1())
                //{
                //    game.Run();
                //}
                using (Game1 game = new Game1())
                {
                    game.Run();
                }
            }
        }
    }
#endif
}

