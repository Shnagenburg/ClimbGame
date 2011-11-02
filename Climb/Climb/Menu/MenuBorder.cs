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
using Climb.Util; 

namespace Climb
{
    /// <summary>
    /// A menu border has a filled rectangle and a set of lines.
    /// </summary>
    class MenuBorder
    {
        FilledRectangle rect;
        List<Line> lines = new List<Line>();

        public Point Position
        {
            get{ return rect.Box.Location;}
            set
            { 
                rect.Box.Location = value;
                lines[0].Point1 = value;
                lines[3].Point2 = value;
            }
        }

        public int Width
        {
            get
            {
                return rect.Box.Width;
            }
            set
            {
                rect.Box.Width = value;
                lines[0].Point2 = new Point(rect.Box.Right, rect.Box.Top);
                lines[1].Point1 = lines[0].Point2;
                lines[1].Point2 = new Point(rect.Box.Right, rect.Box.Bottom);
                lines[2].Point1 = lines[1].Point2;
            }
        }

        public int Height
        {
            get
            {
                return rect.Box.Height;
            }
            set
            {
                rect.Box.Height = value;
                lines[1].Point2 = new Point(rect.Box.Right, rect.Box.Bottom);
                lines[2].Point1 = lines[1].Point2;
                lines[2].Point2 = new Point(rect.Box.Left, rect.Box.Bottom);
                lines[3].Point1 = lines[2].Point2;

            }
        }

        /// <summary>
        /// The thickness of the border lines.
        /// </summary>
        public int Thickness
        {
            set
            {
                foreach (Line line in lines)
                    line.Thickness = value;
            }
        }

        public byte Alpha
        {
            set
            {
                rect.Alpha = value;
                foreach (Line line in lines)
                    line.Alpha = value;
            }
            get { return rect.Alpha; }
        }

        /// <summary>
        /// Load the menu border.
        /// </summary>
        /// <param name="theContentManager"></param>
        public void LoadContent(ContentManager theContentManager)
        {
            rect = new FilledRectangle(CUtil.GraphicsDevice);
            
            Line line = new Line();
            line.LoadContent(CUtil.GraphicsDevice);
            lines.Add(line);
            line = new Line();
            line.LoadContent(CUtil.GraphicsDevice);
            lines.Add(line);
            line = new Line();
            line.LoadContent(CUtil.GraphicsDevice);
            lines.Add(line);
            line = new Line();
            line.LoadContent(CUtil.GraphicsDevice);
            lines.Add(line);

        }

        /// <summary>
        /// Draw The menu Border
        /// </summary>
        /// <param name="theBatch"></param>
        public void Draw(SpriteBatch theBatch)
        {
            rect.Draw(theBatch);
            foreach (Line line in lines)
            {
                line.Draw(theBatch);
            }
        }

        /// <summary>
        /// Set the background colors for the menu border.
        /// </summary>
        /// <param name="gradient">A set of four colors that makes the menu gradient.</param>
        public void SetColors(Color[] gradient)
        {
            rect.SetColors(gradient);
        }
    }
}
