using System;
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
        private Texture2D line;

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

            Window.Title = "RocketLeagueIRL Simulator";

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

            line = new Texture2D(GraphicsDevice, 1, 1);
            line.SetData(new[] { Color.Red });// fill the texture with white
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

            float leftWheelSpeed = (GamePad.GetState(PlayerIndex.One).Triggers.Left);
            float rightWheelSpeed = (GamePad.GetState(PlayerIndex.One).Triggers.Right);

            buggy.MoveForward(leftWheelSpeed, rightWheelSpeed);
            //Console.WriteLine(leftWheelSpeed + " / " + rightWheelSpeed + " = " + leftWheelSpeed / rightWheelSpeed);

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

            //Buggy line
            DrawLine(spriteBatch, buggy.leftWheelPosition, buggy.rightWheelPosition);

            //Buggy
            //spriteBatch.Draw(Buggy.texture, buggy.position, Buggy.hitbox, Color.White, buggy.rotation, Buggy.origin, 1.0f, SpriteEffects.None, 1);

            //Ball
            spriteBatch.Draw(Ball.texture, ball.position, Ball.hitbox, Color.White, 0, Ball.origin, 0.5f, SpriteEffects.None, 1);

            //Arrow
            //spriteBatch.Draw(arrow, buggy.position, new Rectangle(0,0,5,5), Color.White, buggy.rotation, new Vector2(0,0), 1.0f, SpriteEffects.None, 1);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(line,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
