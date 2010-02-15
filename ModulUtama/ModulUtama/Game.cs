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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using ModulUtama.Class;
using TubesAI.Model;

namespace ModulUtama
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        MenuController MC;
        Texture2D BG;
        Texture2D Bar;
        int drawingdelay;
        int elapsedtime = 0;
        public static int isGameOver = 0;
        public static Sound sound = new Sound();
        bool play = false;
        
        public Game()
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
            // Inisialisasi gambar
            Archer.textureL = Content.Load<Texture2D>(@"Images/S-ArcherL");
            Archer.textureR = Content.Load<Texture2D>(@"Images/S-ArcherR");
            Swordsman.textureL = Content.Load<Texture2D>(@"Images/S-SwordsmanL");
            Swordsman.textureR = Content.Load<Texture2D>(@"Images/S-SwordsmanR");
            Spearman.textureL = Content.Load<Texture2D>(@"Images/S-SpearmanL");
            Spearman.textureR = Content.Load<Texture2D>(@"Images/S-SpearmanR");
            Rider.textureL = Content.Load<Texture2D>(@"Images/S-RiderL");
            Rider.textureR = Content.Load<Texture2D>(@"Images/S-RiderR");
            Medic.textureL = Content.Load<Texture2D>(@"Images/S-MedicL");
            Medic.textureR = Content.Load<Texture2D>(@"Images/S-MedicR");
            BG = Content.Load<Texture2D>(@"Images/bg_battle");
            Bar = Content.Load<Texture2D>(@"Images/bg_battle");

            /* Inisialisasi letak karakter
             * Letak KARAKTER berdasarkan index:
             * Tim 1        Tim 2
             * 7 |3 |        |14|18       
             *   |  |0     11|  |
             * 8 |4 |        |15|19
             *   |  |1     12|  |
             * 9 |5 |        |16|20
             *   |  |2     13|  |
             * 10|6 |        |17|21
             */
            ViewGame.places[0] = new Vector2(250, 150);
            ViewGame.places[1] = new Vector2(230, 250);
            ViewGame.places[2] = new Vector2(210, 350);
            ViewGame.places[3] = new Vector2(190, 110);
            ViewGame.places[4] = new Vector2(170, 200);
            ViewGame.places[5] = new Vector2(150, 300);
            ViewGame.places[6] = new Vector2(130, 400);
            ViewGame.places[7] = new Vector2(120, 110);
            ViewGame.places[8] = new Vector2(100, 200);
            ViewGame.places[9] = new Vector2(80, 300);
            ViewGame.places[10] = new Vector2(60, 400);

            ViewGame.places[11] = new Vector2(500, 150);
            ViewGame.places[12] = new Vector2(520, 250);
            ViewGame.places[13] = new Vector2(540, 350);
            ViewGame.places[14] = new Vector2(560, 110);
            ViewGame.places[15] = new Vector2(580, 200);
            ViewGame.places[16] = new Vector2(600, 300);
            ViewGame.places[17] = new Vector2(620, 400);
            ViewGame.places[18] = new Vector2(630, 110);
            ViewGame.places[19] = new Vector2(650, 200);
            ViewGame.places[20] = new Vector2(670, 300);
            ViewGame.places[21] = new Vector2(690, 400);

            sound.BGM_simulate(this);

            // TODO: Add your initialization logic here
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
            spriteFont = Content.Load<SpriteFont>("Info");
            MC = new MenuController(@"D:\Study\6th-Semester\IF3054 - AI\Tubes\Tubes1\IBTBS\Algoritma\Algoritma\bin\Debug\Algoritma.dll",
                                    @"D:\Study\6th-Semester\IF3054 - AI\Tubes\Tubes1\IBTBS\Algoritma\Algoritma\bin\Debug\Algoritma.dll",
                                    ViewMenu.kload(), ViewMenu.kload(), "BFS", "BFS");

            // TODO: use this.Content to load your game content here
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //if (Keyboard.GetState().IsKeyDown(Keys.A) && elapsedtime > 500)
            //{
                //Console.WriteLine("HALOO");
                //TODO: Add your update logic here
                MC.GC.GameLoop();
                //elapsedtime = 0;
            //}

            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (isGameOver == 0 || ViewGame.drawcount > 50)
            {
                // TODO: Add your drawing code here
                if (drawingdelay > 150)
                {
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Begin();
                    spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 600), Color.White);
                    ViewGame.drawBar(spriteBatch, Bar);
                    ViewGame.draw(spriteBatch, spriteFont, this);
                    spriteBatch.End();
                    drawingdelay = 0;
                }

                drawingdelay += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                if (!play)
                {
                    sound.BGM_main(this);
                    play = true;
                }
                spriteBatch.Begin();
                if (isGameOver == 1)
                {
                    spriteBatch.DrawString(spriteFont, "TEAM 1 WINS!", new Vector2(100, 300), Color.Green, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                }
                else if (isGameOver == 2)
                    spriteBatch.DrawString(spriteFont, "TEAM 2 WINS!", new Vector2(500, 300), Color.Yellow, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
