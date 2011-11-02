using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Climb.Util
{
    /// <summary>
    /// Save the options and high scores.
    /// </summary>
    public static class MySerializer
    {
        static public void SerializeObject(string filename, MyConfig objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        /// <summary>
        /// Turn a file into the MyConfig file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public MyConfig DeSerializeObject(string filename)
        {
            MyConfig objectToSerialize;

            try
            {
                Stream stream = File.Open(filename, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                objectToSerialize = (MyConfig)bFormatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception e)
            {
                //If the file wasn't found.
                return null;
            }
            return objectToSerialize;
        }

        /// <summary>
        /// Write the MyConfig object to the config.cfg file.
        /// </summary>
        static public void SaveConfig()
        {
            //save the config to a file
            MyConfig objectToSerialize = new MyConfig();
            objectToSerialize.Highscores = CUtil.Config.Highscores;

            objectToSerialize.ScreenWidth = CUtil.ResolutionWidth;
            objectToSerialize.ScreenHeight = CUtil.ResolutionHeight;

            objectToSerialize.FullScreenWidth = CUtil.FullScreenResolution.X;
            objectToSerialize.FullScreenHeight = CUtil.FullScreenResolution.Y;

            objectToSerialize.IsFullscreen = CUtil.Graphics.IsFullScreen;
            objectToSerialize.IsLetterbox = CUtil.IsLetterBoxed;

            objectToSerialize.IsSFXOn = Options.IsSFXOn;

            objectToSerialize.MusicSelection = Options.CurrentMusicSelection;
            objectToSerialize.HeroSelection = Options.CurrentHeroSelection;


            MySerializer.SerializeObject("config.cfg", objectToSerialize);


        }

        /// <summary>
        /// Load config.cfg. Returns true if the file was found, false otherwise
        /// </summary>
        /// <returns></returns>
        static public bool LoadConfig()
        {
            MyConfig objectToSerialize = new MyConfig();

            objectToSerialize = MySerializer.DeSerializeObject("config.cfg");


            // If the file wasn't found.           
            if (objectToSerialize == null)
            {
                CUtil.Config = new MyConfig();
                return false;
            }

            CUtil.Config = objectToSerialize;
            return true;
        }
    }
}
