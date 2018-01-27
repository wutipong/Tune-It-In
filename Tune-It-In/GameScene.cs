using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;

namespace Tune_It_In
{
    internal class GameScene : Scene
    {
        private SoundEffect pinkNoise;
        private SoundEffect brownNoise;
        private SoundEffectInstance noise1;
        private SoundEffectInstance noise2;

        private SoundEffect bgm;
        private SoundEffectInstance bgmInstance;

        private SoundEffect correct;
        private SoundEffect incorrect;
        private BitmapFont font;

        private bool begin = true;

        private int position = 5;
        private int target = 7;
        private const int count = 10;
        private const int MinPosition = 0;
        private const int MaxPosition = count - 1;
        private const float factor = 1.0f / count;
        private Random r = new Random();

        //private int duration = 105;

        private CountDownTimer countdown = new CountDownTimer { Duration = 105 };

        public override void Draw(SpriteBatch spriteBatch)
        {
            countdown.Draw(spriteBatch);
        }

        public override void Load(ContentManager content)
        {
            pinkNoise = content.Load<SoundEffect>("noise/pink");
            brownNoise = content.Load<SoundEffect>("noise/brown");
            bgm = content.Load<SoundEffect>("music/bgm0");

            correct = content.Load<SoundEffect>("sfx/correct");
            incorrect = content.Load<SoundEffect>("sfx/incorrect");
            font = content.Load<BitmapFont>("font");

            countdown.Load(content);
        }

        public int NextTarget()
        {
            return r.Next(MinPosition, MaxPosition);
        }

        public override void Update(GameTime gameTime, Input input)
        {
            if (begin)
            {
                begin = false;

                bgmInstance = bgm.CreateInstance();
                bgmInstance.Play();

                noise1 = pinkNoise.CreateInstance();
                noise1.Pan = 0.5f;
                noise1.Volume = 0.5f;
                noise1.IsLooped = true;

                noise1.Play();

                noise2 = brownNoise.CreateInstance();
                noise2.Pan = -0.5f;
                noise2.Volume = 0.5f;
                noise2.IsLooped = true;

                noise2.Play();
            }
            switch (input)
            {
                case Input.Left:
                    position--;

                    break;

                case Input.Right:
                    position++;

                    break;

                case Input.Enter:
                    if (position == target)
                        correct.Play();
                    else
                        incorrect.Play();

                    target = NextTarget();
                    break;
            }

            position = MathHelper.Clamp(position, MinPosition, MaxPosition);
            noise1.Volume = position * factor;
            noise2.Volume = 1.0f - (position * factor);

            var noiseVolumeFactor = Math.Abs(target - position) * factor;

            noise1.Volume *= noiseVolumeFactor;
            noise2.Volume *= noiseVolumeFactor;
            bgmInstance.Volume = 0.5f;

            bgmInstance.Volume *= 1.0f - noiseVolumeFactor * factor;

            countdown.Update(gameTime);
        }
    }
}