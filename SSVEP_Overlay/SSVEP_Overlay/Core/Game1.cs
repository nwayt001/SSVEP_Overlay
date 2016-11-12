#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SSVEP_Overlay.BCI_Logic.Stimulator_DirectX;
using SSVEP_Overlay.BCI_Logic.BCI2000_Control.SpecificApp;
using SSVEP_Overlay.BCI_Logic.BCI2000_Control;
using System.Runtime.InteropServices;
#endregion Using Statements

namespace SSVEP_Overlay
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        KeyboardState currentKS, prevKS;
        MouseState mouseState;
        MOUSEPOINT mousePos;

        #region Fields
        int selectedApp = 0; // selected app
        //Game
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Windows Forms
        System.Windows.Forms.Form form;

        #endregion Fields
        //Constructor
        public Game1(int selectedApp)
        {
            this.selectedApp = selectedApp;

            //directX graphics 
            graphics = new GraphicsDeviceManager(this)
                {
                    //Configure DirectX graphics device
                    SynchronizeWithVerticalRetrace=true,
                    PreferredBackBufferFormat = SurfaceFormat.Color,
                    PreferredDepthStencilFormat = DepthFormat.None,
                };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = new TimeSpan(10000000L / 60L); //frame rate (30 or 60 Hz only)
            InactiveSleepTime = TimeSpan.Zero;
            //automatically size the window based on screen dimensions
            User32.SetWindowPos((uint)this.Window.Handle, 1, 1366, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 0);
        }

        //Initialize
        protected override void Initialize()
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;

            //Set the windows forms, and directX graphics buffer to match the screen of the host computer
            int maxHeight = 0; int maxWidth = 0; // vars to choose the max width and height
            GraphicsAdapter g = graphics.GraphicsDevice.Adapter;
            foreach(DisplayMode dm in g.SupportedDisplayModes)
            {
                if (maxHeight < dm.Height)
                {
                    maxHeight = dm.Height;
                }
                if (maxWidth < dm.Width)
                {
                    maxWidth = dm.Width;
                }
            }
            graphics.PreferredBackBufferHeight = maxHeight;
            graphics.PreferredBackBufferWidth = maxWidth;
            
            //Configure DirectX graphics device
            graphics.GraphicsDevice.DepthStencilState = DepthStencilState.None;
            graphics.PreferMultiSampling = false;  //Set to true to have smooth texture edges
            GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.One;
            graphics.ApplyChanges();

            //Merge directX drawing buffer with GDI+ drawing surface
            int[] margins = new int[] { 0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight };
            User32.DwmExtendFrameIntoClientArea(Window.Handle, ref margins);
            //xna game form
            form = System.Windows.Forms.Control.FromHandle(Window.Handle).FindForm();
            form.Visible = true;
            form.AllowTransparency = true;
            //form.BackColor = System.Drawing.Color.Transparent;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.TransparencyKey = form.BackColor;
            form.TopMost = true;
            form.DesktopLocation = new System.Drawing.Point(0, 0);
            form.ClientSize = new System.Drawing.Size(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);

            //Create a new directX spritebatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Initialize SSVEP (Using DirectX)
            SSVEP_DirectX_Advanced_V2 ssvepDX = new SSVEP_DirectX_Advanced_V2(this);
            ssvepDX.Initialize(form, spriteBatch);
            Components.Add(ssvepDX);
            
            switch (selectedApp)
            {
                case 0:
                    var appControl = new BCI_Logic.BCI2000_Control.AnyApp.AdvancedControl(this);
                    Components.Add(appControl);
                    break;
                case 1:
                    var appControl1 = new BCI_Logic.BCI2000_Control.AnyApp.BasicControl(this);
                    Components.Add(appControl1);
                    break;
                case 2:
                    var appControl2 = new WowControl(this, "World Of Warcraft");
                    Components.Add(appControl2);
                    break;
                case 3:
                    var appControl3 = new SpecificAppControl(this, "Google Earth");
                    Components.Add(appControl3);
                    break;
                case 4:
                    var appControl4 = new CursorControl(this);
                    Components.Add(appControl4);
                    break;
                case 5:
                    var appControl5 = new BCI_Logic.BCI2000_Control.AnyApp.AdvancedControl(this);
                    Components.Add(appControl5);
                    break;
            }
            

            //Cursor practice
            // InitCursor();
            //mousePos.X=GraphicsDevice.Viewport.Width / 2;
            //mousePos.Y=GraphicsDevice.Viewport.Height / 2;
            //Mouse.SetPosition(mousePos.X,mousePos.Y);

            base.Initialize();
        }

        //Initialize Cursor
        public void InitCursor()
        {

            //Create a new cursor
            form.Cursor = new System.Windows.Forms.Cursor(form.Cursor.Handle);
            //place the cursor in the center of the screen
            System.Windows.Forms.Cursor myCursor = new System.Windows.Forms.Cursor(System.Windows.Forms.Cursor.Current.Handle);

            //Mouse.SetPosition
            System.Windows.Forms.Form newTempForm = new System.Windows.Forms.Form();
            
            System.Windows.Forms.Control cursorControl = new System.Windows.Forms.Control(); 
            
        }


        // Update Logic 
        protected override void Update(GameTime gameTime)
        {
            //mouseState= Mouse.GetState();
            //Mouse.SetPosition(mouseState.X, mouseState.Y);
            
            ////move mouse
            //currentKS = Keyboard.GetState();

            //if (currentKS.IsKeyDown(Keys.Up))
            //{
            //    mousePos.Y -= 5;
            //    Mouse.SetPosition(mousePos.X, mousePos.Y);
            //}
            //if (currentKS.IsKeyDown(Keys.Down))
            //{
            //    mousePos.Y += 5;
            //    Mouse.SetPosition(mousePos.X, mousePos.Y);
            //}
            //if (currentKS.IsKeyDown(Keys.Left))
            //{
            //    mousePos.X -= 5;
            //    Mouse.SetPosition(mousePos.X, mousePos.Y);
            //}
            //if (currentKS.IsKeyDown(Keys.Right))
            //{
            //    mousePos.X += 5;
            //    Mouse.SetPosition(mousePos.X, mousePos.Y);
            //}
            //prevKS = currentKS;   

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            base.Draw(gameTime);
        }

        //Memory CleanUp
        protected override void Dispose(bool disposing)
        {
            try
            {
                foreach (GameComponent gc in Components)
                {
                    gc.Dispose();
                }
            }
            catch (Exception) { }
            base.Dispose(disposing);
        }

    }
}
