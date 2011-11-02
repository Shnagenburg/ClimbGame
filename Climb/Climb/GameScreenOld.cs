using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Climb
{
    class GameScreen : Screen
    {
        //Background texture for the screen
        ControlledSprite csHero;


        List<Sprite> blocks;
        Sprite spGround;
        Background bg;

        public GameScreenOld(EventHandler theScreenEvent): base(theScreenEvent)
        {
            //Init our intrepid hero
            csHero = new ControlledSprite();

            bg = new Background();
          
            blocks = new List<Sprite>();
            Sprite sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);
            sp = new Sprite();
            blocks.Add(sp);
        }


        public override void LoadContent(ContentManager contentManager)
        {
            csHero.LoadContent(contentManager);
            csHero.Position = new Vector2(550, 200);

            string [] strBgs = { "duskbg", "duskclouds" };
            bg.Loadcontent(contentManager, strBgs);

            foreach (Sprite sp in blocks)
                sp.LoadContent(contentManager, "block");

            blocks[0].Scale = 8.0f;
            blocks[0].Position = new Vector2(0, 400);
            blocks[0].OldPosition = new Vector2(0, 400);

            blocks[1].Position = new Vector2(550, 350);
            blocks[1].OldPosition = new Vector2(0, 400);

            blocks[2].Position = new Vector2(700, 100);
            blocks[2].OldPosition = new Vector2(700, 100);

            blocks[3].Position = new Vector2(350, 300);
            blocks[3].OldPosition = new Vector2(350, 300);
            blocks[3].Velocity = new Vector2(50, 0);

            blocks[4].Position = new Vector2(50, 400);
            blocks[4].OldPosition = new Vector2(50, 400);
            blocks[4].Velocity = new Vector2(0, 50);
        }




        //Update all of the elements that need updating in the Game Screen
        public override void Update(GameTime gameTime, KeyboardState keyState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back) == true)
            {
                    ScreenEvent.Invoke(this, new EventArgs());
                    return;
            }

            // In this game, blocks take priority over Barry.
            // Since they will knock him around, we need to know where
            // The blocks will be
            foreach (Sprite sp in blocks)
                sp.Update(gameTime);

            // Once we know where the blocks will be, adjust Barry accordingly
            csHero.Update(gameTime, keyState, blocks);

            bg.Update(gameTime);

            // Just for testing
            Oscillate();

            base.Update(gameTime, keyState);
        }

        //Draw all of the elements that make up the Controller Detect Screen
        public override void Draw(SpriteBatch theBatch)
        {
            bg.Draw(theBatch);

            foreach (Sprite sp in blocks)
                sp.Draw(theBatch);

            csHero.Draw(theBatch);
            base.Draw(theBatch);
        }

        // Just to test collision detection
        public void Oscillate()
        {
            if (blocks[3].Position.X > 400)
                blocks[3].Velocity.X = -50;
            else if (blocks[3].Position.X < 300)
                blocks[3].Velocity.X = 50;

            if (blocks[4].Position.Y > 450)
                blocks[4].Velocity.Y = -100;
            else if (blocks[4].Position.Y < 300)
                blocks[4].Velocity.Y = 100;
        }


    }
}
