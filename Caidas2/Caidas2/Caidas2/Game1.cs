using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Caidas2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D[] textABC2=new Texture2D[27];
        Texture2D[] cajaABC2 = new Texture2D[27];
        string[] abc = new string[27] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "Ò", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y","z" };
        string[] cabc = new string[27] { "cajaA", "cajaB", "cajaC", "cajaD", "cajaE", "cajaF", "cajaG", "cajaH", "cajaI", "cajaJ", "cajaK", "cajaL", "cajaM", "cajaN", "caja—", "cajaO", "cajaP", "cajaQ", "cajaR", "cajaS", "cajaT", "cajaU", "cajaV", "cajaW", "cajaX", "cajaY", "cajaZ" };
        Rectangle[] recABC2 = new Rectangle[27];
        Rectangle[] recCajaABC2 = new Rectangle[27];
        int[] cosas = new int[27];
        int[] pressedY = new int[27];
        int[] pressedX = new int[27];
        int x = 20;
        int y = 20;
        
        Texture2D[] textABC = new Texture2D[10];
        Texture2D[] cajaABC = new Texture2D[3];
        Rectangle[] recABC = new Rectangle[10];
        Rectangle[] recCajaABC = new Rectangle[3];
        List<int> listRand = new List<int>();
        List<int> listRand3 = new List<int>();

        Texture2D caja;
        Texture2D repeat;
        Texture2D letra;
        Texture2D bien;
        Texture2D otraCaja;
        Texture2D mal;
        Rectangle recCaja;
        Rectangle recCaja2;
        Rectangle recRepeat;
        Rectangle recPlayer;
        int cosa = 0;
        int flag = 0;
        int bandera = 0;
        int presedX, presedY;
        bool noDraw = false;
        bool malTocado = false;
        bool clicked = false;
        bool clicked2 = false;
        int cosa2 = 0;
        MouseState currentMouseState;
        MouseState previousMouseState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //this.graphics.IsFullScreen = true;
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
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 650;
            graphics.ApplyChanges();
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

            // TODO: use this.Content to load your game content here

            caja = Content.Load<Texture2D>("caja");
            repeat= Content.Load<Texture2D>("repeat");
            otraCaja = Content.Load<Texture2D>("caja2");
            mal = Content.Load<Texture2D>("cancel");
            letra = Content.Load<Texture2D>("a");
            bien = Content.Load<Texture2D>("Bien");
            for (int i = 0; i < textABC.Length; i++)
            {
                cosas[i] = 0;
                textABC2[i] = Content.Load<Texture2D>(abc[i]);
                recABC2[i] = new Rectangle(x, cosas[i], textABC2[i].Width, textABC2[i].Height);
                cajaABC2[i] = Content.Load<Texture2D>(cabc[i]);
                x = x + textABC2[i].Width;
            }
            recPlayer = new Rectangle(300, cosa, letra.Width, letra.Height);
            Jugar();
        }
        public void Jugar()
        {
            Random rand = new Random();          
            for (int i = 0; i < 10; i++)
            {
                int num = rand.Next(10);
                if (!listRand.Contains(num)) //If it's not contains, add number to array;
                {
                    listRand.Add(num);
                    textABC[i] = Content.Load<Texture2D>(abc[num]);
                    textABC[i].Name = i.ToString();
                    recABC[i] = new Rectangle(x, cosas[num], textABC[i].Width, textABC[i].Height);
                    
                    x = x + textABC[i].Width;
                }
                else //If it contains, restart random process
                    i--;
            }
            Random rand2 = new Random();
            for (int i = 0; i < 3; i++)
            {
                int num = rand.Next(10);
                if (!listRand3.Contains(num)) //If it's not contains, add number to array;
                {
                    listRand.Add(num);                    
                    cajaABC[i] = Content.Load<Texture2D>(cabc[num]);
                    cajaABC[i].Name = num.ToString();
                   
                }
                else //If it contains, restart random process
                    i--;
            }
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        MouseState current, last;
        private float holdTimer;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recPlayer.Contains(currentMouseState.X, currentMouseState.Y) && (recPlayer.Contains(previousMouseState.X, previousMouseState.Y))))
            {
                
                clicked = true;
                clicked2 = true;
                flag = 1;
                bandera = 1;
                presedX = currentMouseState.X-30;
                presedY = currentMouseState.Y-30;
            }
            else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Pressed) 
            {
                clicked = false;
                clicked2 = false;
            }
            else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Released)
            {
                clicked = false;
                clicked2 = false;
                noDraw = false;
                malTocado = false;
            }
            if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recRepeat.Contains(currentMouseState.X, currentMouseState.Y)))
            {
                clicked = true;
                clicked2 = true;
                flag = 0;
                bandera = 0;
                presedX = currentMouseState.X-30;
                presedY = currentMouseState.Y-30;
                cosa = 0;
                cosa2 = 0;
                noDraw = false;
                malTocado = false;
                clicked = false;
                clicked2 = false;
            }
            // TODO: Add your update logic here
            cosa2++;
            recCaja = new Rectangle(50, 350, caja.Width, caja.Height);
            recCaja2 = new Rectangle(200, 350, otraCaja.Width, otraCaja.Height);
            recRepeat = new Rectangle(500, 10,repeat.Width,repeat.Height);
            if (clicked)
            {
                recPlayer = new Rectangle(currentMouseState.X-30, currentMouseState.Y-30, letra.Width, letra.Height);
               
            }
            else
            {
                if (flag==0)
                {
                    recPlayer = new Rectangle(100, cosa2, letra.Width, letra.Height);
                }
                else if (flag == 1)
                {
                    cosa2 = presedY;
                    recPlayer = new Rectangle(presedX, cosa2, letra.Width, letra.Height);
                }
               
            }
            if (bandera==1)
            {
                if (recCaja.Intersects(recPlayer)&&!clicked2)
                {
                    noDraw = true;
                }
                else if (recCaja2.Intersects(recPlayer) && !clicked2)
                {
                    malTocado = true;
                }
            }
            
            if (cosa == 400)
            {
                cosa = 0;
                bandera = 0;
                flag = 0;
                cosa2 = 0;
                GraphicsDevice.Clear(Color.CornflowerBlue);
                noDraw = false;
                malTocado = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
           //////////////////////////////////////////////////////////////////
                        
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
                spriteBatch.Draw(textABC[0], new Vector2(20, cosas[0]), Color.White);
                cosas[0]++;
                spriteBatch.Draw(textABC[1], new Vector2(120, cosas[1]), Color.White);
                cosas[1]++;
                spriteBatch.Draw(textABC[2], new Vector2(220, cosas[2]), Color.White);
                cosas[2]++;
                spriteBatch.Draw(textABC[3], new Vector2(320, cosas[3]), Color.White);
                cosas[3]++;
                spriteBatch.Draw(textABC[4], new Vector2(420, cosas[4]), Color.White);
                cosas[4]++;
                spriteBatch.Draw(textABC[5], new Vector2(520, cosas[5]), Color.White);
                cosas[5]++;
                spriteBatch.Draw(textABC[6], new Vector2(620, cosas[6]), Color.White);
                cosas[6]++;
                spriteBatch.Draw(textABC[7], new Vector2(720, cosas[7]), Color.White);
                cosas[7]++;
                spriteBatch.Draw(textABC[8], new Vector2(820, cosas[8]), Color.White);
                cosas[8]++;
                spriteBatch.Draw(textABC[9], new Vector2(920, cosas[9]), Color.White);
                cosas[9]++;

                spriteBatch.Draw(cajaABC[0], new Vector2(250, 500), Color.White);
                spriteBatch.Draw(cajaABC[1], new Vector2(450, 500), Color.White);
                spriteBatch.Draw(cajaABC[2], new Vector2(650, 500), Color.White);
                
          
            if (!noDraw && !malTocado)
            {
                if (clicked)
                {
                    spriteBatch.Draw(letra, new Vector2(currentMouseState.X-30, currentMouseState.Y-30), Color.White);
                }
                else
                {
                    if (flag == 0)
                    {
                        cosa++;
                        spriteBatch.Draw(letra, new Vector2(300, cosa), Color.White);
                        recPlayer = new Rectangle(300, cosa, letra.Width, letra.Height);
                    }
                    else if (flag == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        presedY++;
                        spriteBatch.Draw(letra, new Vector2(presedX, presedY), Color.White);
                    }

                }

                spriteBatch.Draw(caja, new Vector2(50, 350), Color.White);
                spriteBatch.Draw(otraCaja, new Vector2(200, 350), Color.White);
            }
            else
            {
                if (noDraw)
                {
                    GraphicsDevice.Clear(Color.Orange);
                    spriteBatch.Draw(bien, new Vector2(200, 200), Color.White);
                }
                if (malTocado)
                {
                    GraphicsDevice.Clear(Color.Orange);
                    spriteBatch.Draw(mal, new Vector2(200, 200), Color.White);
                }
            }
           
            spriteBatch.Draw(repeat, new Vector2(500, 10), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
