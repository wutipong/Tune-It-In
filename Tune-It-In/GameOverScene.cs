using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace Tune_It_In
{
    internal class GameOverScene : Scene
    {
        private BitmapFont font;

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                if (score > HighScore)
                {
                    HighScore = score;
                }
            }
        }

        private int score;
        public int HighScore { get; private set; } = 0;
        private bool begin = true;
        private bool showSkip = false;
        private TimeSpan beginTime;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Game Over.", new Vector2(360, 300), Color.DarkSlateGray);
            spriteBatch.DrawString(font, String.Format("Your Score : {0}", Score), new Vector2(360, 380), Color.DarkGray);
            if (Score == HighScore && Score != 0)
                spriteBatch.DrawString(font, "New Record!!", new Vector2(800, 380), Color.DarkSlateGray);

            spriteBatch.DrawString(font, String.Format("Current High Score : {0}", HighScore), new Vector2(360, 460), Color.DarkSlateGray);

            if (showSkip)
                spriteBatch.DrawString(font, "Press Any Key to continue.", new Vector2(360, 620), Color.DimGray);
        }

        public override void Load(ContentManager content)
        {
            font = content.Load<BitmapFont>("font");
        }

        public override bool Update(GameTime gameTime, Input input)
        {
            if (begin)
            {
                beginTime = gameTime.TotalGameTime;
                begin = false;
                showSkip = false;
            }

            if (gameTime.TotalGameTime.TotalSeconds - beginTime.TotalSeconds < 5)
                return false;

            showSkip = true;
            if (input != Input.None)
                return true;

            return false;
        }

        public void Reset()
        {
            begin = true;
        }
    }
}