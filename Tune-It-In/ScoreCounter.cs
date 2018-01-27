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
    internal class ScoreCounter
    {
        private BitmapFont font;
        public TimeSpan LastScoreTime { get; set; }

        public int Score { get; set; }

        public void Load(ContentManager content)
        {
            font = content.Load<BitmapFont>("font");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, string.Format("Score: {0}", Score), new Vector2(800, 50), Color.DarkSlateGray);
        }

        public int AddScore(GameTime gameTime)
        {
            int score = 10000 - (int)(gameTime.TotalGameTime.TotalMilliseconds - LastScoreTime.TotalMilliseconds);
            if (score < 0) score = 0;

            score = (score / 1000) * 100;

            LastScoreTime = gameTime.TotalGameTime;

            Score += score;
            return score;
        }

        public void Update(GameTime gt)
        {
        }
    }
}