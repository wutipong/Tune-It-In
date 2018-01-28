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

        private CountDownTimer countdown = new CountDownTimer { Duration = 105 };
        private ScoreCounter score = new ScoreCounter();
        private RadioControl radio = new RadioControl();
        private ResultControl result = new ResultControl();
        private Texture2D background;
        public int Score { get { return score.Score; } }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            countdown.Draw(spriteBatch);
            score.Draw(spriteBatch);

            radio.Draw(spriteBatch);
            result.Draw(spriteBatch);
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
            score.Load(content);
            result.Load(content);

            bgmInstance = bgm.CreateInstance();
            noise1 = pinkNoise.CreateInstance();
            noise2 = pinkNoise.CreateInstance();

            background = content.Load<Texture2D>("game");
        }

        public int NextTarget()
        {
            return r.Next(MinPosition, MaxPosition);
        }

        public override bool Update(GameTime gameTime, Input input)
        {
            if (begin)
            {
                begin = false;

                bgmInstance.Play();

                noise1.Pan = 0.5f;
                noise1.Volume = 0.5f;
                noise1.IsLooped = true;

                noise1.Play();

                noise2.Pan = -0.5f;
                noise2.Volume = 0.5f;
                noise2.IsLooped = true;

                noise2.Play();

                score.LastScoreTime = gameTime.TotalGameTime;
                score.Score = 0;

                countdown.Duration = 105;
                countdown.Reset();
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
                    {
                        correct.Play();
                        var s = score.AddScore(gameTime);
                        target = NextTarget();
                        result.Hit(s);
                    }
                    else

                    {
                        incorrect.Play();
                        result.Miss();
                    }

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
            result.Update(gameTime);
            if (countdown.Duration == 0)
            {
                noise1.Stop();
                noise2.Stop();
                return true;
            }

            radio.Position = position;

            return false;
        }

        public void Reset()
        {
            begin = true;
        }
    }
}