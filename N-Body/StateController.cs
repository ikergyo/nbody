using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace N_Body
{
    public class StateController
    {
        List<GameState> gameStates = new List<GameState>();
        GameState active;

        public StateController(Game game, int Window_Height, int Window_Width, GraphicsDeviceManager _graphics, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            Initialize(game, Window_Height, Window_Width, _graphics, GraphicsDevice, Content);
        }
        private void Initialize(Game game, int Window_Height, int Window_Width, GraphicsDeviceManager _graphics, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            MenuState menu = new MenuState(game, _graphics, GraphicsDevice, Content, SwitchToSimulation);
            gameStates.Add(menu);
            menu.MakeActive(true);
            active = menu;
            gameStates.Add(new SimulatorState(game, Window_Height, Window_Width, _graphics, GraphicsDevice, Content));
        }
        public GameState GetActive()
        {
            return GameState.GetActive();
        }
        private void SwitchToSimulation(int objectNumber, int taskNumber)
        {
            active.MakeActive(false);
            foreach (var item in gameStates)
            {
                SimulatorState state = item as SimulatorState;
                if (state != null)
                {
                    active = state;
                    state.SetState(objectNumber, taskNumber);
                    state.MakeActive(true);
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            active.GMUpdate(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            active.GMDraw(gameTime);
        }
    }
}
