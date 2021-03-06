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
        Texture2D[] textABC2 = new Texture2D[27];
        Texture2D[] cajaABC2 = new Texture2D[27];
        int[] posY = new int[10] { 20, 120, 220, 320, 420, 520, 620, 720, 820, 920 };
        string[] abc = new string[27] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "�", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        string[] cabc = new string[27] { "cajaA", "cajaB", "cajaC", "cajaD", "cajaE", "cajaF", "cajaG", "cajaH", "cajaI", "cajaJ", "cajaK", "cajaL", "cajaM", "cajaN", "caja�", "cajaO", "cajaP", "cajaQ", "cajaR", "cajaS", "cajaT", "cajaU", "cajaV", "cajaW", "cajaX", "cajaY", "cajaZ" };
        Rectangle[] recABC2 = new Rectangle[27];
        Rectangle[] recCajaABC2 = new Rectangle[27];
        int[] cosas = new int[10];
        int[] pressedY = new int[10];
        int[] pressedX = new int[10];
        int x = 20;
        int y = 20;
        int iSele;
        bool paraTodo = true;
        bool RompeTodo = false;
        bool[] clickeado = new bool[10];
        bool[] clickeado2 = new bool[10];
        int[] flags = new int[10];
        int[] banderas = new int[10];
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
            repeat = Content.Load<Texture2D>("repeat");
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
        int num;
        public void Jugar()
        {
            Random rand = new Random();
           
            for (int i = 0; i < 10; i++)
            {
                num = rand.Next(27);
                if (!listRand.Contains(num))
                {
                    listRand.Add(num);
                    textABC[i] = Content.Load<Texture2D>(abc[num]);
                    textABC[i].Name = abc[i];
                    recABC[i] = new Rectangle(x, cosas[i], textABC[i].Width, textABC[i].Height);

                    x = x + textABC[i].Width;
                }
                else
                    i--;
            }
            Random rand2 = new Random();
            int num2;
            for (int i = 0; i < 3; i++)
            {
                num2 = rand.Next(27);
                if (!listRand3.Contains(num2) && listRand.Contains(num2))
                {
                    listRand3.Add(num2);
                    cajaABC[i] = Content.Load<Texture2D>(cabc[num2]);
                    cajaABC[i].Name = abc[i];

                }
                else
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
        
        private float holdTimer;
        public void Apretar()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            for (int i = 0; i < textABC.Length; i++)
            {
                    
                    if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[i].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[i].Contains(previousMouseState.X, previousMouseState.Y))))
                    {
                        
                        clickeado[i] = true;
                        clicked = true;
                        clicked2 = true;
                        clickeado2[i] = true;
                        flags[i] = 1;
                        banderas[i] = 1;
                        pressedX[i] = currentMouseState.X - 30;
                        pressedY[i] = currentMouseState.Y - 30;
                        paraTodo = false;
                    }
                    else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        clicked = false;
                        clicked2 = false;
                        clickeado[i] = false;
                        clickeado2[i] = false;
                        noDraw = false;
                        malTocado = false;
                        paraTodo = true;
                    }
                    else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Released)
                    {
                        clicked = false;
                        clicked2 = false;
                        clickeado[i] = false;
                        clickeado2[i] = false;
                        noDraw = false;
                        malTocado = false;
                        paraTodo = true;
                    }
                    else if (previousMouseState.LeftButton == ButtonState.Pressed & currentMouseState.LeftButton == ButtonState.Released)
                    {
                        clicked = false;
                        clicked2 = false;
                        clickeado[i] = false;
                        clickeado2[i] = false;
                        noDraw = false;
                        malTocado = false;
                        paraTodo = true;
                    }
                    if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recRepeat.Contains(currentMouseState.X, currentMouseState.Y)))
                    {
                        clicked = true;
                        clicked2 = true;
                        clickeado[i] = true;
                        clickeado2[i] = true;
                        paraTodo = false;
                        flag = 0;
                        bandera = 0;
                        presedX = currentMouseState.X - 30;
                        presedY = currentMouseState.Y - 30;
                        for (int j = 0; j < 10; j++)
                        {
                            pressedX[j] = currentMouseState.X - 30;
                            pressedY[j] = currentMouseState.Y - 30;
                        }
                        cosa = 0;
                        cosa2 = 0;
                        noDraw = false;
                        malTocado = false;
                        clicked = false;
                        clicked2 = false;
                        clickeado[i] = false;
                        clickeado2[i] = false;
                    }
                
               
            }
            if (previousMouseState.LeftButton == ButtonState.Pressed & currentMouseState.LeftButton == ButtonState.Released)
            {
                
                noDraw = false;
                malTocado = false;
                paraTodo = true;
            }
            if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Released)
            {

                noDraw = false;
                malTocado = false;
                paraTodo = true;
            } 
        }
        MouseState current, last;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (!(previousMouseState.LeftButton == ButtonState.Pressed & currentMouseState.LeftButton == ButtonState.Released))
            {                
                Apretar();                
            }                       
          
           
            // TODO: Add your update logic here
            cosa2++;
            recCaja = new Rectangle(50, 350, caja.Width, caja.Height);
            recCaja2 = new Rectangle(200, 350, otraCaja.Width, otraCaja.Height);
            recRepeat = new Rectangle(500, 10, repeat.Width, repeat.Height);

            recCajaABC[0] = new Rectangle(250, 500, cajaABC[0].Width, cajaABC[0].Height);
            recCajaABC[1] = new Rectangle(450, 500, cajaABC[1].Width, cajaABC[1].Height);
            recCajaABC[2] = new Rectangle(650, 500, cajaABC[2].Width, cajaABC[2].Height);

            //////////////////////////////////////////////
            for (int i = 0; i < textABC.Length; i++)
            {
                if (clicked)
                {
                    if (clickeado[i])
                    {
                        recABC[0] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[0].Width, textABC[0].Height);
                    }

                }
                else
                {
                    if (flags[i] == 0)
                    {
                        recABC[i] = new Rectangle(posY[i], cosas[i], textABC[i].Width, textABC[i].Height);
                    }
                    else if (flags[i] == 1)
                    {
                        cosas[i] = pressedY[i];
                        recABC[i] = new Rectangle(pressedX[i], cosas[i], textABC[i].Width, textABC[i].Height);
                    }
                }
                if (banderas[i] == 1)
                {

                    if (cajaABC[0].Name == textABC[i].Name && recCajaABC[0].Intersects(recABC[i]) /*&& !clickeado2[i]*/)
                    {
                        noDraw = true;
                    }
                    else if (cajaABC[1].Name == textABC[i].Name && recCajaABC[1].Intersects(recABC[i])/* && !clickeado2[i]*/)
                    {
                        noDraw = true;
                    }
                    else if (cajaABC[2].Name == textABC[i].Name && recCajaABC[2].Intersects(recABC[i]) /*&& !clickeado2[i]*/)
                    {
                        noDraw = true;
                    }
                }

            }
            //////////////////////////////////////////////////////
            /*
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

            if (bandera == 1)
            {
                if (recCaja.Intersects(recPlayer) && !clicked2)
                {
                    noDraw = true;
                }
                else if (recCaja2.Intersects(recPlayer) && !clicked2)
                {
                    malTocado = true;
                }
            }
            

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            

            /* if (cosa == 400)
             {
                 cosa = 0;
                 bandera = 0;
                 flag = 0;
                 cosa2 = 0;
                 GraphicsDevice.Clear(Color.CornflowerBlue);
                 noDraw = false;
                 malTocado = false;
             }*/
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

           
            spriteBatch.Draw(cajaABC[0], new Vector2(250, 500), Color.White);
            spriteBatch.Draw(cajaABC[1], new Vector2(450, 500), Color.White);
            spriteBatch.Draw(cajaABC[2], new Vector2(650, 500), Color.White);
            if (!noDraw && !malTocado)
            {
                for (int i = 0; i < textABC.Length; i++)
                {

                    if (clickeado[i])
                    {
                        if (!paraTodo)
                        {
                            spriteBatch.Draw(textABC[i], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);
                        }
                    }
                    else
                    {
                        if (flags[i] == 0 && !paraTodo)
                        {
                            cosas[i]++;
                            spriteBatch.Draw(textABC[i], new Vector2(posY[i], cosas[i]), Color.White);
                            recABC[i] = new Rectangle(posY[i], cosas[i], textABC[i].Width, textABC[i].Height);
                        }
                        else if (flags[i] == 1)
                        {
                            //cosa = pressedY;
                            //cosa++;
                            pressedY[i]++;
                            spriteBatch.Draw(textABC[i], new Vector2(pressedX[i], pressedY[i]), Color.White);
                        }
                        else
                        {
                            if (flags[i] == 0)
                            {
                                cosas[i]++;
                                spriteBatch.Draw(textABC[i], new Vector2(posY[i], cosas[i]), Color.White);
                                recABC[i] = new Rectangle(posY[i], cosas[i], textABC[i].Width, textABC[i].Height);
                            }
                            else if (flags[i] == 1)
                            {
                                //cosa = pressedY;
                                //cosa++;
                                pressedY[i]++;
                                spriteBatch.Draw(textABC[i], new Vector2(pressedX[i], pressedY[i]), Color.White);
                            }


                        }
                    }
                }
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
            
        
        private void Eliminado()
        {
            /*
            if (!noDraw && !malTocado)
            {
                if (clicked)
                    {
                        // spriteBatch.Draw(letra, new Vector2(currentMouseState.X-30, currentMouseState.Y-30), Color.White);

                    }
                    else
                    {
                        if (flag == 0)
                        {
                            cosa++;
                            //spriteBatch.Draw(letra, new Vector2(300, cosa), Color.White);
                            recPlayer = new Rectangle(300, cosa, letra.Width, letra.Height);
                        }
                        else if (flag == 1)
                        {
                            //cosa = pressedY;
                            //cosa++;
                            presedY++;
                            // spriteBatch.Draw(letra, new Vector2(presedX, presedY), Color.White);
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
            */
            /*

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
                           for (int i = 0; i < 10; i++)
                           {
                               pressedX[0]=currentMouseState.X-30;
                               pressedY[0] = currentMouseState.Y - 30;
                           }
                           cosa = 0;
                           cosa2 = 0;
                           noDraw = false;
                           malTocado = false;
                           clicked = false;
                           clicked2 = false;
                       }
            * */
            /*
            if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[0].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[0].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[0] = true;
                clickeado2[0] = true;
                flags[0] = 1;
                banderas[0] = 1;
                pressedX[0] = currentMouseState.X - 30;
                pressedY[0] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[1].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[1].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[1] = true;
                clickeado2[1] = true;
                flags[1] = 1;
                banderas[1] = 1;
                pressedX[1] = currentMouseState.X - 30;
                pressedY[1] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[2].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[2].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[2] = true;
                clickeado2[2] = true;
                flags[2] = 1;
                banderas[2] = 1;
                pressedX[2] = currentMouseState.X - 30;
                pressedY[2] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[3].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[3].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[3] = true;
                clickeado2[3] = true;
                flags[3] = 1;
                banderas[3] = 1;
                pressedX[3] = currentMouseState.X - 30;
                pressedY[3] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[4].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[4].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[4] = true;
                clickeado2[4] = true;
                flags[4] = 1;
                banderas[4] = 1;
                pressedX[4] = currentMouseState.X - 30;
                pressedY[4] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[5].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[5].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[5] = true;
                clickeado2[5] = true;
                flags[5] = 1;
                banderas[5] = 1;
                pressedX[5] = currentMouseState.X - 30;
                pressedY[5] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[6].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[6].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[6] = true;
                clickeado2[6] = true;
                flags[6] = 1;
                banderas[6] = 1;
                pressedX[6] = currentMouseState.X - 30;
                pressedY[6] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[7].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[7].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[7] = true;
                clickeado2[7] = true;
                flags[7] = 1;
                banderas[7] = 1;
                pressedX[7] = currentMouseState.X - 30;
                pressedY[7] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[8].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[8].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[8] = true;
                clickeado2[8] = true;
                flags[8] = 1;
                banderas[8] = 1;
                pressedX[8] = currentMouseState.X - 30;
                pressedY[8] = currentMouseState.Y - 30;
            }
            else if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recABC[9].Contains(currentMouseState.X, currentMouseState.Y) && (recABC[9].Contains(previousMouseState.X, previousMouseState.Y))))
            {
                clickeado[1] = true;
                clickeado2[1] = true;
                flags[1] = 1;
                banderas[1] = 1;
                pressedX[1] = currentMouseState.X - 30;
                pressedY[1] = currentMouseState.Y - 30;
            }
            else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < 10; i++)
                {
                    clickeado[i] = false;
                    clickeado2[i] = false;
                }

            }
            else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < 10; i++)
                {
                    clickeado[i] = false;
                    clickeado2[i] = false;
                }

                noDraw = false;
                malTocado = false;
            }*/
            /* if (clickeado[0])
             {
                 recABC[0] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[0].Width, textABC[0].Height);

             }
             else
             {
                 if (flags[0] == 0)
                 {
                     recABC[0] = new Rectangle(20, cosas[0], textABC[0].Width, textABC[0].Height);
                 }
                 else if (flags[0] == 1)
                 {
                     cosas[0] = pressedY[0];
                     recABC[0] = new Rectangle(pressedX[0], cosas[0], textABC[0].Width, textABC[0].Height);
                 }

             }
             if (clickeado[1])
             {
                 recABC[1] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[1].Width, textABC[1].Height);

             }
             else
             {
                 if (flags[1] == 0)
                 {
                     recABC[1] = new Rectangle(120, cosas[1], textABC[1].Width, textABC[1].Height);
                 }
                 else if (flags[1] == 1)
                 {
                     cosas[1] = pressedY[1];
                     recABC[1] = new Rectangle(pressedX[1], cosas[1], textABC[1].Width, textABC[1].Height);
                 }

             }
             if (clickeado[2])
             {
                 recABC[2] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[2].Width, textABC[2].Height);

             }
             else
             {
                 if (flags[2] == 0)
                 {
                     recABC[2] = new Rectangle(220, cosas[2], textABC[2].Width, textABC[2].Height);
                 }
                 else if (flags[2] == 1)
                 {
                     cosas[2] = pressedY[2];
                     recABC[2] = new Rectangle(pressedX[2], cosas[2], textABC[2].Width, textABC[2].Height);
                 }

             }
             if (clickeado[3])
             {
                 recABC[3] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[3].Width, textABC[3].Height);

             }
             else
             {
                 if (flags[3] == 0)
                 {
                     recABC[3] = new Rectangle(320, cosas[3], textABC[3].Width, textABC[3].Height);
                 }
                 else if (flags[3] == 1)
                 {
                     cosas[3] = pressedY[3];
                     recABC[3] = new Rectangle(pressedX[3], cosas[3], textABC[3].Width, textABC[3].Height);
                 }

             }
             if (clickeado[4])
             {
                 recABC[4] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[4].Width, textABC[4].Height);

             }
             else
             {
                 if (flags[4] == 0)
                 {
                     recABC[4] = new Rectangle(420, cosas[4], textABC[4].Width, textABC[4].Height);
                 }
                 else if (flags[4] == 1)
                 {
                     cosas[4] = pressedY[4];
                     recABC[4] = new Rectangle(pressedX[4], cosas[4], textABC[4].Width, textABC[4].Height);
                 }

             }
             if (clickeado[5])
             {
                 recABC[5] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[5].Width, textABC[5].Height);

             }
             else
             {
                 if (flags[5] == 0)
                 {
                     recABC[5] = new Rectangle(520, cosas[5], textABC[5].Width, textABC[5].Height);
                 }
                 else if (flags[5] == 1)
                 {
                     cosas[5] = pressedY[5];
                     recABC[5] = new Rectangle(pressedX[5], cosas[5], textABC[5].Width, textABC[5].Height);
                 }

             }
             if (clickeado[6])
             {
                 recABC[6] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[6].Width, textABC[6].Height);

             }
             else
             {
                 if (flags[6] == 0)
                 {
                     recABC[6] = new Rectangle(620, cosas[6], textABC[6].Width, textABC[6].Height);
                 }
                 else if (flags[6] == 1)
                 {
                     cosas[6] = pressedY[6];
                     recABC[6] = new Rectangle(pressedX[6], cosas[6], textABC[6].Width, textABC[6].Height);
                 }

             }
             if (clickeado[7])
             {
                 recABC[7] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[7].Width, textABC[7].Height);

             }
             else
             {
                 if (flags[7] == 0)
                 {
                     recABC[7] = new Rectangle(720, cosas[7], textABC[7].Width, textABC[7].Height);
                 }
                 else if (flags[7] == 1)
                 {
                     cosas[7] = pressedY[7];
                     recABC[7] = new Rectangle(pressedX[7], cosas[7], textABC[7].Width, textABC[7].Height);
                 }

             }
             if (clickeado[8])
             {
                 recABC[8] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[8].Width, textABC[8].Height);

             }
             else
             {
                 if (flags[8] == 0)
                 {
                     recABC[8] = new Rectangle(820, cosas[8], textABC[8].Width, textABC[8].Height);
                 }
                 else if (flags[8] == 1)
                 {
                     cosas[8] = pressedY[8];
                     recABC[8] = new Rectangle(pressedX[8], cosas[8], textABC[8].Width, textABC[8].Height);
                 }

             }
             if (clickeado[9])
             {
                 recABC[9] = new Rectangle(currentMouseState.X - 30, currentMouseState.Y - 30, textABC[9].Width, textABC[9].Height);

             }
             else
             {
                 if (flags[9] == 0)
                 {
                     recABC[9] = new Rectangle(920, cosas[9], textABC[9].Width, textABC[9].Height);
                 }
                 else if (flags[9] == 1)
                 {
                     cosas[9] = pressedY[9];
                     recABC[9] = new Rectangle(pressedX[9], cosas[9], textABC[9].Width, textABC[9].Height);
                 }

             }
         */
            /*
                if (clickeado[0])
                {
                    spriteBatch.Draw(textABC[0], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[0] == 0)
                    {
                        cosas[0]++;
                        spriteBatch.Draw(textABC[0], new Vector2(20, cosas[0]), Color.White);
                        recABC[0] = new Rectangle(300, cosas[0], textABC[0].Width, textABC[0].Height);
                    }
                    else if (flags[0] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[0]++;
                        spriteBatch.Draw(textABC[0], new Vector2(pressedX[0], pressedY[0]), Color.White);
                    }

                }
                if (clickeado[1])
                {
                    spriteBatch.Draw(textABC[1], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[1] == 0)
                    {
                        cosas[1]++;
                        spriteBatch.Draw(textABC[1], new Vector2(120, cosas[1]), Color.White);
                        recABC[1] = new Rectangle(300, cosas[1], textABC[1].Width, textABC[1].Height);
                    }
                    else if (flags[1] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[1]++;
                        spriteBatch.Draw(textABC[1], new Vector2(pressedX[1], pressedY[1]), Color.White);
                    }
                }
                if (clickeado[2])
                {
                    spriteBatch.Draw(textABC[2], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[2] == 0)
                    {
                        cosas[2]++;
                        spriteBatch.Draw(textABC[2], new Vector2(220, cosas[2]), Color.White);
                        recABC[2] = new Rectangle(300, cosas[2], textABC[2].Width, textABC[2].Height);
                    }
                    else if (flags[2] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[2]++;
                        spriteBatch.Draw(textABC[2], new Vector2(pressedX[2], pressedY[2]), Color.White);
                    }
                }
                if (clickeado[3])
                {
                    spriteBatch.Draw(textABC[3], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[3] == 0)
                    {
                        cosas[3]++;
                        spriteBatch.Draw(textABC[3], new Vector2(320, cosas[3]), Color.White);
                        recABC[3] = new Rectangle(300, cosas[3], textABC[3].Width, textABC[3].Height);
                    }
                    else if (flags[0] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[3]++;
                        spriteBatch.Draw(textABC[3], new Vector2(pressedX[3], pressedY[3]), Color.White);
                    }
                }
                if (clickeado[4])
                {
                    spriteBatch.Draw(textABC[4], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[4] == 0)
                    {
                        cosas[4]++;
                        spriteBatch.Draw(textABC[4], new Vector2(420, cosas[4]), Color.White);
                        recABC[4] = new Rectangle(300, cosas[4], textABC[4].Width, textABC[4].Height);
                    }
                    else if (flags[4] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[4]++;
                        spriteBatch.Draw(textABC[4], new Vector2(pressedX[4], pressedY[4]), Color.White);
                    }
                }
                if (clickeado[5])
                {
                    spriteBatch.Draw(textABC[5], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[5] == 0)
                    {
                        cosas[5]++;
                        spriteBatch.Draw(textABC[5], new Vector2(520, cosas[5]), Color.White);
                        recABC[5] = new Rectangle(300, cosas[5], textABC[5].Width, textABC[5].Height);
                    }
                    else if (flags[5] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[5]++;
                        spriteBatch.Draw(textABC[5], new Vector2(pressedX[5], pressedY[5]), Color.White);
                    }
                }
                if (clickeado[6])
                {
                    spriteBatch.Draw(textABC[6], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[6] == 0)
                    {
                        cosas[6]++;
                        spriteBatch.Draw(textABC[6], new Vector2(620, cosas[6]), Color.White);
                        recABC[6] = new Rectangle(300, cosas[6], textABC[6].Width, textABC[6].Height);
                    }
                    else if (flags[6] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[6]++;
                        spriteBatch.Draw(textABC[6], new Vector2(pressedX[6], pressedY[6]), Color.White);
                    }
                }
                if (clickeado[7])
                {
                    spriteBatch.Draw(textABC[7], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[7] == 0)
                    {
                        cosas[7]++;
                        spriteBatch.Draw(textABC[7], new Vector2(720, cosas[7]), Color.White);
                        recABC[7] = new Rectangle(300, cosas[7], textABC[7].Width, textABC[7].Height);
                    }
                    else if (flags[7] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[7]++;
                        spriteBatch.Draw(textABC[7], new Vector2(pressedX[7], pressedY[7]), Color.White);
                    }
                }
                if (clickeado[8])
                {
                    spriteBatch.Draw(textABC[8], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[8] == 0)
                    {
                        cosas[8]++;
                        spriteBatch.Draw(textABC[8], new Vector2(820, cosas[8]), Color.White);
                        recABC[8] = new Rectangle(300, cosas[8], textABC[8].Width, textABC[8].Height);
                    }
                    else if (flags[8] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[8]++;
                        spriteBatch.Draw(textABC[8], new Vector2(pressedX[8], pressedY[8]), Color.White);
                    }
                }
                if (clickeado[9])
                {
                    spriteBatch.Draw(textABC[9], new Vector2(currentMouseState.X - 30, currentMouseState.Y - 30), Color.White);

                }
                else
                {
                    if (flags[9] == 0)
                    {
                        cosas[9]++;
                        spriteBatch.Draw(textABC[9], new Vector2(920, cosas[9]), Color.White);
                        recABC[9] = new Rectangle(300, cosas[9], textABC[9].Width, textABC[9].Height);
                    }
                    else if (flags[9] == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY[9]++;
                        spriteBatch.Draw(textABC[9], new Vector2(pressedX[9], pressedY[9]), Color.White);
                    }

                }
            }*/
            /*spriteBatch.Draw(textABC[0], new Vector2(20, cosas[0]), Color.White);
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
           cosas[9]++;*/
        }
    }
}
