using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace N_Body
{
    public class SimulatorState : GameState
    {
        
        int objectNumber = 1000;
        int taskNumber = 5;
        PhysicsController pc;
        int Window_Height;
        int Window_Width;
        Vector2 window_Offset;
        Game game;

        public SimulatorState(Game game, int Window_Height, int Window_Width, GraphicsDeviceManager _graphics, GraphicsDevice GraphicsDevice, ContentManager Content) 
            :base(_graphics, GraphicsDevice, Content)
        {
            
            this.Window_Width = Window_Width;
            this.Window_Height = Window_Height;

            int window_OffsetX = Window_Width / 2;
            int window_OffsetY = Window_Height / 2;
            window_Offset = new Vector2(window_OffsetX, window_OffsetY);
            this.game = game;
        }
        public void SetState(int objectNumber, int taskNumber)
        {
            this.objectNumber = objectNumber;
            this.taskNumber = taskNumber;
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                pc.Exit();
                game.Exit();
            }

            bool st = false;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                st = true;

            pc.Control(st);
        }

        protected override void GMInitialize()
        {
            pc = new PhysicsController(taskNumber, 1500, 1, objectNumber, Window_Height, Window_Width);
        }

        protected override void GMLoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            for (int i = 0; i < Physics.objectIndex; i++)
            {
                NObject obj = Physics.objects[i];
                obj.LoadTexture(Content.Load<Texture2D>("circle"));
            }


        }

        protected override void GMUnInitialize()
        {
            return;
            throw new NotImplementedException();
        }

        protected override void GMUnloadContent()
        {
            return;
            throw new NotImplementedException();
        }
    }
}
