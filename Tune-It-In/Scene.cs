using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tune_It_In
{
    internal abstract class Scene
    {
        public abstract void Load(ContentManager content);

        public abstract bool Update(GameTime gameTime, Input input);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}