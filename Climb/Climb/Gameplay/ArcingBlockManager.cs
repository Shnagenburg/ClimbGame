/**
 * By: Tyler Young
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Climb
{
    /// <summary>
    /// This randomly generates arcing blocks
    /// </summary>
    class ArcingBlockManager
    {
        Camera camera;
        List<Sprite> blocks;
        int rate, variance, gravity,nextVariance;
        double lastAdd;
        Random rand;
        ContentManager contentManager;
        String picName;

        // Whether or not we are spawning blocks
        public bool IsSpawning;

        /// <summary>
        /// Create a new Arcing Block generator
        /// </summary>
        /// <param name="camera">The camera for the game.</param>
        /// <param name="blocks">The primary list of blocks.</param>
        /// <param name="contentManager">The Content Manager.</param>
        /// <param name="rate">The rate at which blocks spawn.</param>
        /// <param name="variance">The randomness of the speed of which blocks spawn.</param>
        /// <param name="gravity">The gravity applied to the blocks.</param>
        /// <param name="picName">The image used for the blocks.</param>
        public ArcingBlockManager(Camera camera, List<Sprite> blocks, ContentManager contentManager, int rate, int variance, int gravity, String picName )
        {
            IsSpawning = true;

            this.camera=camera;
            this.rate = rate;
            this.variance = variance;
            this.gravity = gravity;
            this.blocks = blocks;
            this.contentManager=contentManager;
            this.picName=picName;
            rand = new Random();
            lastAdd = -rate;
            nextVariance = 0;
        }

        /// <summary>
        /// Generate new blocks
        /// </summary>
        /// <param name="theGameTime"></param>
        public void Update(GameTime theGameTime)
        {
            if (lastAdd + rate + nextVariance <= theGameTime.TotalGameTime.TotalMilliseconds && IsSpawning)
            {
                lastAdd = theGameTime.TotalGameTime.TotalMilliseconds;
                nextVariance = rand.Next(-variance, variance);
                ArcingBlock newBlock = new ArcingBlock(new Vector2(rand.Next(50, 1230), 720), new Vector2(rand.Next(-230, 230), rand.Next(-700, -400)), 100);
                newBlock.LoadContent(contentManager, picName);
                newBlock.Scale = 0.75f;
                blocks.Add(newBlock);
            }

        }
    }
}
