using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RLIRL_Simulator
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Sim : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D arrow;

        //Objects
        private Buggy buggy;
        private Ball ball;

        //general properties
        public static int windowHeight = 1080;
        public static int windowWidth = 1080;

        public Sim()
        {
            graphics = new GraphicsDeviceManager(this);
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
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            buggy = new Buggy(new Vector2((windowWidth - Buggy.size.X)/2, (windowHeight - Buggy.size.Y) / 2));
            ball = new Ball(new Vector2((windowWidth - Buggy.size.X) / 2, (windowHeight - Buggy.size.Y) / 4));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Buggy.texture = Content.Load<Texture2D>("buggy");
            Ball.texture = Content.Load<Texture2D>("ball");
            arrow = Content.Load<Texture2D>("arrow");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int leftWheelSpeed = (int) (GamePad.GetState(PlayerIndex.One).Triggers.Left * 255);
            int rightWheelSpeed = (int)(GamePad.GetState(PlayerIndex.One).Triggers.Right * 255);

            buggy.Move(leftWheelSpeed, rightWheelSpeed);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearWrap);

            //Buggy
            spriteBatch.Draw(Buggy.texture, buggy.position, Buggy.hitbox, Color.White, buggy.rotation, Buggy.origin, 1.0f, SpriteEffects.None, 1);

            //Ball
            spriteBatch.Draw(Ball.texture, ball.position, Ball.hitbox, Color.White, 0, Ball.origin, 0.5f, SpriteEffects.None, 1);

            //Arrow
            spriteBatch.Draw(arrow, buggy.position, new Rectangle(0,0,5,5), Color.White, buggy.rotation, new Vector2(0,0), 1.0f, SpriteEffects.None, 1);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
