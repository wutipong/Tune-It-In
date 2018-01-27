using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Graphics;

namespace Tune_It_In
{
    public enum Input
    {
        None, Left, Right, Enter
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardListener keyboardListener;
        private GamePadListener gamePadListener;

        private Scene scene;
        private Input input;

        private GameScene gameScene = new GameScene();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            scene = gameScene;

            keyboardListener = new KeyboardListener();
            keyboardListener.KeyReleased += KeyboardListener_KeyReleased;

            gamePadListener = new GamePadListener();
            gamePadListener.ButtonUp += GamePadListener_ButtonUp;
        }

        private void GamePadListener_ButtonUp(object sender, GamePadEventArgs e)
        {
            switch (e.Button)
            {
                case Buttons.DPadLeft:
                    input = Input.Left;
                    break;

                case Buttons.DPadRight:
                    input = Input.Right;
                    break;

                case Buttons.A:
                    input = Input.Enter;
                    break;
            }
        }

        private void KeyboardListener_KeyReleased(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.A:
                    input = Input.Left;
                    break;

                case Keys.Left:
                    input = Input.Left;
                    break;

                case Keys.D:
                    input = Input.Right;
                    break;

                case Keys.Right:
                    input = Input.Right;
                    break;

                case Keys.Space:
                    input = Input.Enter;
                    break;

                case Keys.Enter:
                    input = Input.Enter;
                    break;
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            gameScene.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            input = Input.None;

            keyboardListener.Update(gameTime);
            gamePadListener.Update(gameTime);

            scene.Update(gameTime, input);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();
            scene.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}