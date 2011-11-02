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
 

namespace Climb
{
    /// <summary>
    /// An arcing block flys across the screen.
    /// </summary>
    class ArcingBlock : Sprite
    {
        int gravity;

        /// <summary>
        /// Create a new ArcingBlock
        /// </summary>
        /// <param name="pos">The starting position</param>
        /// <param name="vel">The velocity</param>
        /// <param name="gravity">The gravity applied to the block</param>
        public ArcingBlock(Vector2 pos, Vector2 vel,int gravity)
            : base()
        {
            Position = pos;
            Velocity = new Vector2(vel.X, vel.Y);
            Vector2 Direction = new Vector2(1, 1);
            this.gravity=gravity;
        }

        // Move the block
        public override void Update(GameTime theGameTime)
        {
            UpdateVelocity(theGameTime);
            base.Update(theGameTime);
            CheckOffScreen();
        }

        // Apply gravity
        public void UpdateVelocity(GameTime gameTime)
        {
            Velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        // Check to see if a block is off the screen
        public void CheckOffScreen()
        {
            if (Position.Y > 800)
                IsDying = true;
        }
    }
}
