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
    /// This lays out how the background will change the higher you go.
    /// </summary>
    class BackgroundProgression
    {
        private LayeredBackground bg;
        private List<string> lImageAssets; // implement these later (for inserting/deleting) for INFINATE BGS!!!
        private List<int> lImageHeights;

        private string[] bgAssets;
        private string[] layer1Assets;
        private string[] layer2Assets;

        private int[] heights;
        private bool[] bCheckPoints;  // Whether or not the height has been passed
        private Altimeter altimeter;
        private int iProgIndex = 0; // Where you are in the progression

        /// <summary>
        /// Make a new progression given the background, the altimeter, and what pictures 
        /// to swap in at what heights.
        /// </summary>
        public BackgroundProgression(LayeredBackground theBg, Altimeter theAltimeter, string[] theBGAssets,
            string[] theLayer1Assets, string[] theLayer2Assets, int[] theHeights)
        {
            bg = theBg;
            altimeter = theAltimeter;

            bgAssets = theBGAssets;
            layer1Assets = theLayer1Assets;
            layer2Assets = theLayer2Assets;

            heights = theHeights;

            bCheckPoints = new bool[heights.Length];

            for (int i = 0; i < heights.Length; i++)
                bCheckPoints[i] = false;

        }

        /// <summary>
        /// Check to see if we have gone high enough to change the background.
        /// </summary>
        public void Update()
        {
            if (altimeter.MaxHeight > heights[iProgIndex] && !bCheckPoints[iProgIndex] && heights.Length > iProgIndex + 1)   //remove last check later
            {
                bg.layers[0].BeginFade(bgAssets[iProgIndex]);

                bg.layers[1].BeginFade(layer1Assets[iProgIndex]);

                bg.layers[2].BeginFade(layer2Assets[iProgIndex]);
                

                bCheckPoints[iProgIndex] = true;
                iProgIndex++;
            }

        }
    }
}
