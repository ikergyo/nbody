using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace N_Body
{
    public enum GameStatus { Inactive, Active, Hidden}
    public abstract class GameState
    {
        private static List<GameState> instances = new List<GameState>();

        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;
        protected GraphicsDevice GraphicsDevice;
        protected ContentManager Content;

        protected GameState(GraphicsDeviceManager _graphics, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            this._graphics = _graphics;
            this.GraphicsDevice = GraphicsDevice;
            this.Content = Content;
            instances.Add(this);
        }

        protected GameStatus status;
        protected abstract void GMInitialize();
        protected abstract void GMUnInitialize();
        protected abstract void GMLoadContent();
        protected abstract void GMUnloadContent();

        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw(GameTime gameTime);

        protected void InitScene()
        {
            GMInitialize();
            GMLoadContent();
            Debug.WriteLine("Scene has been activated");
        }

        public void MakeActive(bool active)
        {
            if (active)
            {
                status = GameStatus.Active;
                InitScene();
            }
            else
            {
                status = GameStatus.Inactive;
                GMUnloadContent();
                GMUnInitialize();
            }
        }
        public void GMUpdate(GameTime gameTime)
        {
            if (IsActive())
                Update(gameTime);
        }
        public void GMDraw(GameTime gameTime)
        {
            if (IsActive())
                Draw(gameTime);
        }
        public static GameState GetActive()
        {
            foreach (var item in instances)
            {
                if (item.IsActive())
                    return item;
            }
            return null;
        }
        protected bool IsActive()
        {
            return status == GameStatus.Active;
        }
    }
}
