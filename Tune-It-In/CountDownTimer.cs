using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tune_It_In
{
    internal class CountDownTimer
    {
        public int Duration { get; set; }

        private TimeSpan lastUpdate;
        private BitmapFont font;

        public void Load(ContentManager content)
        {
            font = content.Load<BitmapFont>("font");
        }

        public void Update(GameTime gameTime)
        {
            if (lastUpdate == null)
            {
                lastUpdate = gameTime.TotalGameTime;
            }

            if (gameTime.TotalGameTime.TotalSeconds - lastUpdate.TotalSeconds >= 1)
            {
                Duration = Duration - (int)(gameTime.TotalGameTime.TotalSeconds - lastUpdate.TotalSeconds);
                if (Duration < 0)
                    Duration = 0;

                lastUpdate = gameTime.TotalGameTime;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, string.Format("{0:00}: {1:00}", Duration / 60, Duration % 60), new Vector2(50, 50), Color.DarkGray);
        }
    }
}