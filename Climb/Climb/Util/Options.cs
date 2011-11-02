/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Climb
{
    /// <summary>
    /// The options are various parameters for the game.
    /// </summary>
    public static class Options
    {
        public static bool IsMusicOn = false;
        public static bool IsSFXOn = false;
        public static bool IsFullScreen = false;
        public static bool IsLetterBox = false;
        public enum MusicSelection
        {
            NONE,
            PUNK,
            CHILL
        }
        public enum HeroSelection
        {
            BARRY,
            SEAMUS            
        }
                
        private static MusicSelection m_currentMusicSelection = MusicSelection.NONE;
        public static MusicSelection CurrentMusicSelection
        {
            get{return m_currentMusicSelection;}
            set { m_currentMusicSelection = value; }
        }

        private static HeroSelection m_currentHeroSelection = HeroSelection.BARRY;
        public static HeroSelection CurrentHeroSelection
        {
            get { return m_currentHeroSelection; }
            set { m_currentHeroSelection = value; }
        }

        private static String[] m_highscores = new String[10];
        public static String[] HighScores
        {
            get { return m_highscores; }
            set { m_highscores = value; }
        }
    }
}
