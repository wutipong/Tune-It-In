using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tune_It_In
{
    internal class StaticScene : Scene
    {
        public string Resource { get; set; }
        private Texture2D texture;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(0, 0), Color.White);
        }

        public override void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>(Resource);
        }

        public override bool Update(GameTime gameTime, Input input)
        {
            if (input != Input.None) return true;

            return false;
        }
    }
}