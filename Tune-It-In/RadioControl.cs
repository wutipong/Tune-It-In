using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using Microsoft.Xna.Framework;

namespace Tune_It_In
{
    internal class RadioControl
    {
        public int Position { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new RectangleF(240, 360, 800, 80), Color.DimGray);
            spriteBatch.FillRectangle(new RectangleF(240 + (80 * Position), 320, 80, 120), Color.DarkGray);
        }
    }
}