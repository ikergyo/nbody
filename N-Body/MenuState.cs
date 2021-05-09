using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace N_Body
{
    public delegate void LoadNextState(int objectNumber, int taskNumber);
    public class MenuState : GameState
    {
        
        private Desktop _desktop;
        LoadNextState nextState;
        Game game;
        public MenuState(Game game, GraphicsDeviceManager _graphics, GraphicsDevice GraphicsDevice, ContentManager Content, LoadNextState nextState)
            :base(_graphics, GraphicsDevice, Content)
        {
            this.game = game;
            this.nextState = nextState;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _desktop.Render();
        }

        protected override void GMInitialize()
        {
            
        }

        protected override void GMLoadContent()
        {
            MyraEnvironment.Game = game;

            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };
            grid.ColumnsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            grid.ColumnsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Fill,
            });
            grid.RowsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            grid.RowsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            grid.RowsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            grid.RowsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            grid.RowsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            grid.RowsProportions.Add(new Proportion
            {
                Type = Myra.Graphics2D.UI.ProportionType.Auto,
            });
            grid.Id = "_gridRight";
            /*grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            */
            /*var helloWorld = new Label
            {
                Id = "label",
                Text = "Hello, World!"
            };
            grid.Widgets.Add(helloWorld);*/
            var label1 = new Label();
            label1.Text = "Object number";
            label1.GridRow = 1;
            grid.Widgets.Add(label1);

            /*var textBox1 = new TextBox();
            textBox1.GridColumn = 1;
            textBox1.GridRow = 1;
            grid.Widgets.Add(textBox1);*/
            var spinButton1 = new SpinButton
            {
                GridColumn = 1,
                GridRow = 1,
                Width = 100,
                Nullable = false,
            };
            grid.Widgets.Add(spinButton1);


            var label2 = new Label();
            label2.Text = "Task number";
            label2.GridRow = 2;
            grid.Widgets.Add(label2);

            /*var textBox2 = new TextBox();
            textBox2.GridColumn = 1;
            textBox2.GridRow = 2;
            grid.Widgets.Add(textBox2);*/
            var spinButton2 = new SpinButton
            {
                GridColumn = 1,
                GridRow = 2,
                Width = 100,
                Nullable = false,
            };
            grid.Widgets.Add(spinButton2);


            // Button
            var button = new TextButton
            {
                GridColumn = 2,
                GridRow = 3,
                Text = "Start"
            };

            button.Click += (s, a) =>
            {

                /*var messageBox = Dialog.CreateMessageBox("Message", spinButton1.Value.ToString());
                messageBox.ShowModal(_desktop);*/
                int objectNumber = (int)spinButton1.Value;
                int taskNumber = (int)spinButton2.Value;
                nextState(objectNumber, taskNumber);
            };

            grid.Widgets.Add(button);

            // Add it to the desktop
            _desktop = new Desktop();
            _desktop.Root = grid;
        }

        protected override void GMUnInitialize()
        {
            
        }

        protected override void GMUnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            
        }
    }
}
