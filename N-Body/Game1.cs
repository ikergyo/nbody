using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace N_Body
{
    
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _texture;
        private Vector2 _position;

        const int Window_Width = 1280;
        const int Window_Height = 768;

        float window_OffsetX = 0;
        float window_OffsetY = 0;
        Vector2 window_Offset;

        //Base config
        int objectNumber = 1000;
        int taskNumber = 1;

        PhysicsController pc;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Window_Width;
            _graphics.PreferredBackBufferHeight = Window_Height;
            window_OffsetX = Window_Width / 2;
            window_OffsetY = Window_Height / 2;
            window_Offset = new Vector2(window_OffsetX, window_OffsetY);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            pc = new PhysicsController(taskNumber, 1500, 1, objectNumber, Window_Height, Window_Width);

            #region test
            /*Physics.AddObject(new NObject("Main Sun", 0.0, 0.0, 0.0, 0.0, 0.015f, 1.98e+30, Color.Gold));
            Physics.AddObject(new NObject("Earth", 143.0, 0.0, 0.0, 0.0, 0.010f, 5.972e+24, Color.Aqua));
            */

            //ps.AddObject(new NObject("Main Sun", new System.Numerics.Vector2(-75, 125), new System.Numerics.Vector2(0, 0.0f), 0.015f, 1.98e+30f, Color.Gold));
            //ps.AddObject(new NObject("Second Sun", new System.Numerics.Vector2(-350, -250), new System.Numerics.Vector2(0, 0.0f), 0.015f, 1.98e+28f, Color.GhostWhite));
            //ps.AddObject(new NObject("Third Sun", new System.Numerics.Vector2(50, 50), new System.Numerics.Vector2(0, -0), 0.015f, 1.98e+25f, Color.GhostWhite));
            //ps.AddObject(new NObject("SunMoon", new System.Numerics.Vector2(-80, 0), new System.Numerics.Vector2(0, 0), 0.008f, 3.28e+23f, Color.Aqua));

            //ps.AddObject(new NObject("SunMoon", new System.Numerics.Vector2(-49, 30), new System.Numerics.Vector2(0, 0), 0.008f, 3.28e+23f, Color.Aqua));
            //ps.AddObject(new NObject("Earth", new System.Numerics.Vector2(-90, -43), new System.Numerics.Vector2(0, 0), 0.008f, 3.28e+23f, Color.Aqua));
            //ps.AddObject(new NObject("SunMoon", new System.Numerics.Vector2(-50,23), new System.Numerics.Vector2(0, 0), 0.008f, 3.28e+23f, Color.Aqua));
            //ps.AddObject(new NObject("Earth", new System.Numerics.Vector2(-97, 43), new System.Numerics.Vector2(0, 0), 0.008f, 3.28e+23f, Color.Aqua));
            //ps.AddObject(new NObject("SunMoon", new System.Numerics.Vector2(70, -60), new System.Numerics.Vector2(0, 0), 0.008f, 3.28e+23f, Color.Aqua));
            //ps.AddObject(new NObject("Earth", new System.Numerics.Vector2(-96, -43), new System.Numerics.Vector2(0, 0), 0.008f, 3.28e+23f, Color.Aqua));
            //ps.AddObject(new NObject("Mars", new System.Numerics.Vector2(130, -73), new System.Numerics.Vector2(0, 0), 0.01f, 6.38e+23f, Color.OrangeRed));
            #endregion
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            for (int i = 0; i < Physics.objectIndex; i++)
            {
                NObject obj = Physics.objects[i];
                obj.LoadTexture(Content.Load<Texture2D>("circle"));
            }
            //_texture = Content.Load<Texture2D>("circle");
            

            _position = new Vector2(0,0);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                pc.Exit();
                Exit();
            }

            bool st = false;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                st = true;

            pc.Control(st);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int i = 0; i < Physics.objectIndex; i++)
            {
                NObject obj = Physics.objects[i];
                System.Numerics.Vector2 objPos = obj.getDrawVector();
                _spriteBatch.Draw(obj.Texture, new Vector2(objPos.X, objPos.Y) + window_Offset, null, obj.Color, 0, Vector2.Zero, obj.Size, SpriteEffects.None, 0);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
