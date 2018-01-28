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
    internal class ResultControl
    {
        private string text = "";

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                show = true;
            }
        }

        private bool show = false;
        private bool isShowing = false;
        private BitmapFont font;
        private TimeSpan showStart;

        private void Reset()
        {
            show = true;
        }

        public void Hit(int score)
        {
            Text = String.Format("+ {0}", score);
        }

        public void Miss()
        {
            Text = "Miss";
        }

        public void Load(ContentManager content)
        {
            font = content.Load<BitmapFont>("font");
        }

        public void Update(GameTime gameTime)
        {
            if (show)
            {
                if (!isShowing)
                {
                    showStart = gameTime.TotalGameTime;
                    isShowing = true;
                }
                if (gameTime.TotalGameTime.TotalMilliseconds - showStart.TotalMilliseconds > 500)
                {
                    show = false;
                    isShowing = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (show)
            {
                spriteBatch.DrawString(font, text, new Vector2(1000, 50), Color.DimGray);
            }
        }
    }
}