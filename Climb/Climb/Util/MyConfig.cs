/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Climb.Util
{
    /// <summary>
    /// REVISIT The config file will ideally be written to a file.
    /// It'll hold the options and highscores.
    /// </summary>
    [Serializable]
    public class MyConfig : ISerializable
    {
        public int [] Highscores = new int[10];

        public int ScreenWidth;
        public int ScreenHeight;

        public int FullScreenWidth;
        public int FullScreenHeight;

        public bool IsFullscreen;
        public bool IsLetterbox;

        public bool IsSFXOn;

        public Options.MusicSelection MusicSelection;
        public Options.HeroSelection HeroSelection;
        
        public MyConfig()
        { }
            

        public MyConfig (SerializationInfo info, StreamingContext ctxt)
        {
            this.Highscores = (int[])info.GetValue("Highscores", typeof(int[]));

            this.ScreenWidth = (int)info.GetValue("ScreenWidth", typeof(int));
            this.ScreenHeight = (int)info.GetValue("ScreenHeight", typeof(int));

            this.FullScreenWidth = (int)info.GetValue("FullScreenWidth", typeof(int));
            this.FullScreenHeight = (int)info.GetValue("FullScreenHeight", typeof(int));

            this.IsFullscreen = (bool)info.GetValue("IsFullscreen", typeof(bool));
            this.IsLetterbox = (bool)info.GetValue("IsLetterbox", typeof(bool));

            this.IsSFXOn = (bool)info.GetValue("IsSFXOn", typeof(bool));

            this.MusicSelection = (Options.MusicSelection)info.GetValue("MusicSelection", typeof(Options.MusicSelection));
            this.HeroSelection = (Options.HeroSelection)info.GetValue("HeroSelection", typeof(Options.HeroSelection));


        }


        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Highscores", this.Highscores);

            info.AddValue("ScreenWidth", this.ScreenWidth);
            info.AddValue("ScreenHeight", this.ScreenHeight);
            
            info.AddValue("FullScreenWidth", this.FullScreenWidth);
            info.AddValue("FullScreenHeight", this.FullScreenHeight);

            info.AddValue("IsFullscreen", this.IsFullscreen);
            info.AddValue("IsLetterbox", this.IsLetterbox);

            info.AddValue("IsSFXOn", this.IsSFXOn);

            info.AddValue("MusicSelection", this.MusicSelection);
            info.AddValue("HeroSelection", this.HeroSelection);
        }
    }



}
