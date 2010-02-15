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
        
        /* PAGE AWAL
         */
        Texture2D[] Screen;
        Texture2D[] Button;
        int ScreenState;
        int player_index, char_index;
        int[] unit_index;
        int absis, ordinat;
        Boolean pressed, up_pressed, down_pressed;


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
            // Inisialisai page awal
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            IsMouseVisible = true;
            graphics.ApplyChanges();
            ScreenState = 0;
            pressed = false; up_pressed = false; down_pressed = false;
            player_index = 1;
            char_index = 1;
            unit_index = new int[11];
            for (int i = 0; i < 11; i++) unit_index[i] = 0;

            // Inisialisasi gambar

            //GAMBAR PAGE AWAL
            Screen = new Texture2D[4];
            Screen[0] = Content.Load<Texture2D>("Resource\\TitleScreen");
            Screen[1] = Content.Load<Texture2D>("Resource\\TitleScreen");//Composition");
            Screen[2] = Content.Load<Texture2D>("Resource\\screen-up");
            Screen[3] = Content.Load<Texture2D>("Resource\\screen-down");

            Button = new Texture2D[10];
            Button[0] = Content.Load<Texture2D>("Resource\\StartButton");
            Button[1] = Content.Load<Texture2D>("Resource\\unit-sprite");
            Button[2] = Content.Load<Texture2D>("Resource\\arrow-up");
            Button[3] = Content.Load<Texture2D>("Resource\\arrow-down");
            Button[4] = Content.Load<Texture2D>("Resource\\go");
            Button[5] = Content.Load<Texture2D>("Resource\\arrow-up-pressed");
            Button[6] = Content.Load<Texture2D>("Resource\\arrow-down-pressed");
            Button[7] = Content.Load<Texture2D>("Resource\\cursor");
            Button[8] = Content.Load<Texture2D>("Resource\\load-p1");
            Button[9] = Content.Load<Texture2D>("Resource\\load-p2");

            //GAMBAR UNIT
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

            absis = Mouse.GetState().X;
            ordinat = Mouse.GetState().Y;

            if (ScreenState == 0)
            {
                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !pressed)
                {
                    pressed = true;

                    // Start
                    if (absis >= 330 && absis <= 470 && ordinat >= 310 && ordinat <= 360)
                    {
                        ScreenState = 1;
                    }
                    // Help
                    else if (absis >= 305 && absis <= 495 && ordinat >= 390 && ordinat <= 440)
                    {
                        // display Help
                    }
                    // Exit
                    else if (absis >= 285 && absis <= 515 && ordinat >= 480 && ordinat <= 530)
                    {
                        this.Exit();
                    }
                }
                else if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                {
                    pressed = false;
                }
            }
            else if (ScreenState == 1)
            {
                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !pressed)
                {
                    pressed = true;

                    // Go
                    if (absis >= 360 && absis <= 440 && ordinat >= 260 && ordinat <= 340)
                    {
                        ScreenState = 2;
                    }
                    // Load DLL P1
                    else if (absis >= 25 && absis <= 25 + Button[8].Width && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                    {
                        ViewMenu.DLL_P1 = ViewMenu.dload();
                        System.Console.Out.WriteLine(ViewMenu.DLL_P1);
                    }
                    // Load Komposisi P1
                    else if (absis >= 25 + Button[8].Width + 10 && absis <= 25 + 2 * Button[8].Width + 10 && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                    {
                        int[] K_P1 = new int[11];
                        K_P1 = ViewMenu.kload();
                    }
                    // Load DLL P2
                    else if (absis >= 475 && absis <= 475 + Button[9].Width && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                    {
                        ViewMenu.DLL_P2 = ViewMenu.dload();
                        System.Console.Out.WriteLine(ViewMenu.DLL_P2);
                    }
                    // Load Komposisi P2
                    else if (absis >= 475 + Button[9].Width + 10 && absis <= 475 + 2 * Button[9].Width + 10 && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                    {
                        int[] K_P2 = new int[11];
                        K_P2 = ViewMenu.kload();
                    }

                }
                else if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                {
                    pressed = false;
                }

                // Ganti Pemain yang Melakukan Seleksi
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Tab) && elapsedtime >= 300)
                {
                    if (player_index == 1)
                    {
                        player_index = 2;
                        char_index = 1;
                    }
                    else if (player_index == 2)
                    {
                        player_index = 1;
                        char_index = 1;
                    }
                    elapsedtime = 0;
                }
                // Ganti unit
                // Archer = 1, Swordsman = 2, Spearman = 3, Rider = 4, Medic = 5
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) && elapsedtime >= 300)
                {
                    unit_index[char_index - 1]--;
                    if (unit_index[char_index - 1] < 1) unit_index[char_index - 1] = 5;
                    down_pressed = true;
                    elapsedtime = 0;
                }
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) && elapsedtime >= 300)
                {
                    unit_index[char_index - 1]++;
                    if (unit_index[char_index - 1] > 5) unit_index[char_index - 1] = 1;
                    up_pressed = true;
                    elapsedtime = 0;
                }
                // Ganti indeks unit (1 s.d. 11)
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) && elapsedtime >= 300)
                {
                    char_index--;
                    if (char_index < 1) char_index = 11;
                    elapsedtime = 0;
                }
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) && elapsedtime >= 300)
                {
                    char_index++;
                    if (char_index > 11) char_index = 1;
                    elapsedtime = 0;
                }
            }
            else if (ScreenState == 2)
            {
                MC.GC.GameLoop();
            }

            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (ScreenState == 0)
            {
                spriteBatch.Draw(Screen[0], new Rectangle(0, 0, Screen[0].Width, Screen[0].Height), Color.White);
                spriteBatch.Draw(Button[0], new Rectangle(328, 305, 144, 52), Color.White);
            }
            else if (ScreenState == 1)
            {
                // layar bagian atas
                spriteBatch.Draw(Screen[2], new Rectangle(0, 0, Screen[2].Width, Screen[2].Height), Color.White);
                // layar bagian bawah
                spriteBatch.Draw(Screen[3], new Rectangle(0, 600 - Screen[2].Height, Screen[2].Width, Screen[2].Height), Color.White);
                // kolom sprite unit
                for (int i = 0; i < 11; i++)
                {
                    spriteBatch.Draw(Button[1], new Rectangle(43 + i * (11 + Button[1].Width), 55, Button[1].Width, Button[1].Height), Color.White);
                    spriteBatch.Draw(Button[1], new Rectangle(43 + i * (11 + Button[1].Width), 435, Button[1].Width, Button[1].Height), Color.White);
                }
                // tombol go
                spriteBatch.Draw(Button[4], new Rectangle(360, 260, Button[4].Width, Button[4].Height), Color.White);
                // panah atas
                if (up_pressed)
                {
                    spriteBatch.Draw(Button[5], new Rectangle(43 + (char_index - 1) * (11 + Button[5].Width), 20 + (player_index - 1) * 379, Button[5].Width, Button[5].Height), Color.White);
                    up_pressed = false;
                }
                else spriteBatch.Draw(Button[2], new Rectangle(43 + (char_index - 1) * (11 + Button[2].Width), 20 + (player_index - 1) * 379, Button[2].Width, Button[2].Height), Color.White);
                // panah bawah
                if (down_pressed)
                {
                    spriteBatch.Draw(Button[6], new Rectangle(43 + (char_index - 1) * (11 + Button[6].Width), 179 + (player_index - 1) * 379, Button[6].Width, Button[6].Height), Color.White);
                    down_pressed = false;
                }
                else spriteBatch.Draw(Button[3], new Rectangle(43 + (char_index - 1) * (11 + Button[3].Width), 179 + (player_index - 1) * 379, Button[3].Width, Button[3].Height), Color.White);
                // kursor
                spriteBatch.Draw(Button[7], new Rectangle(43 + (char_index - 1) * (11 + Button[7].Width), 55 + (player_index - 1) * 380, Button[7].Width, Button[7].Height), Color.White);
                // load P1 - DLL
                spriteBatch.Draw(Button[8], new Rectangle(25, 260, Button[8].Width, Button[8].Height), Color.White);
                // load P1 - Komposisi
                spriteBatch.Draw(Button[8], new Rectangle(25 + Button[8].Width + 10, 260, Button[8].Width, Button[8].Height), Color.White);
                // load P2 - DLL
                spriteBatch.Draw(Button[9], new Rectangle(475, 310, Button[9].Width, Button[9].Height), Color.White);
                // load P2 - Komposisi
                spriteBatch.Draw(Button[9], new Rectangle(475 + Button[9].Width + 10, 310, Button[9].Width, Button[9].Height), Color.White);
            }
            // TODO: Add your drawing code here

            else if (ScreenState == 2)
            {
                if (isGameOver == 0 || ViewGame.drawcount > 50)
                {
                    // TODO: Add your drawing code here
                    if (drawingdelay > 150)
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 600), Color.White);
                        ViewGame.drawBar(spriteBatch, Bar);
                        ViewGame.draw(spriteBatch, spriteFont, this);
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
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
