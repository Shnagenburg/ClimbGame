/**
 * By: Daniel Fuller 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Climb.Util
{
    /// <summary>
    /// REVIST The config file will ideally be written to a file.
    /// It'll hold the options and highscores.
    /// </summary>
    [Serializable]    
    public class MyConfig 
    {
        public int [] Highscores = new int[10];
                
    }
}
