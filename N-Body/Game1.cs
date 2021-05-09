using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using System.Collections.Generic;

namespace N_Body
{
    
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Desktop _desktop;

        const int Window_Width = 1280;
        const int Window_Height = 768;
        StateController state;
        //Base config
        
        //GameState activeState;

        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Window_Width;
            _graphics.PreferredBackBufferHeight = Window_Height;  
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //activeState = new SimulatorState(Window_Height, Window_Width, _graphics, GraphicsDevice, Content);
            


        }

        protected override void Initialize()
        {

            //activeState = new MenuState(this, _graphics, GraphicsDevice, Content);
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
            //activeState = new MenuState(this, _graphics, GraphicsDevice, Content);
            //activeState.MakeActive(true);
            state = new StateController(this, Window_Height, Window_Width, _graphics, GraphicsDevice, Content);
        }
        protected override void Update(GameTime gameTime)
        {

            
            state.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            state.Draw(gameTime);
            base.Draw(gameTime);
        }

    }
}
