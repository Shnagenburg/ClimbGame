using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Climb
{
    class Background
    {
        List<Sprite> backgrounds = new List<Sprite>();

        public Background()
        {
        }

        public void Initialize()
        {
        }

        public void Loadcontent(ContentManager contentManager, string[] assets)
        {
            foreach (string asset in assets)
            {
                Sprite sp = new Sprite();
                sp.LoadContent(contentManager, asset);
                backgrounds.Add(sp);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Sprite sp in backgrounds)
            {
                sp.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite sp in backgrounds)
            {
                sp.Draw(spriteBatch);
            }
        }

    }
}

