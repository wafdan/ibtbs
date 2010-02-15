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
        int drawingdelay = 0;
        int elapsedtime = 0;
        public static int isGameOver = 0;
        public static Sound sound = new Sound();
        bool play = false;

        /* PAGE AWAL
         */
        Texture2D[] Screen;
        Texture2D[] Button;
        Texture2D[] Symbol;
        int ScreenState;
        int player_index, char_index;
        int[] composition_p1;
        int[] composition_p2;
        int absis, ordinat;
        bool pressed, up_pressed, down_pressed;
        bool dll_p1_hovered, komp_p1_hovered, dll_p2_hovered, komp_p2_hovered, go_hovered, slide_finished;
        bool start_hovered, help_hovered, exit_hovered;
        bool dll_p1_selected, dll_p2_selected;

        Texture2D spriteSheet;
        float timer = 0f;
        float interval = 1000f / 25f;
        int frameCount = 22;
        int currentFrame = 0;
        int spriteWidth = 200;
        int spriteHeight = 200;
        Rectangle sourceRect;
        Rectangle destinationRect;

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
            start_hovered = false; help_hovered = false; exit_hovered = false;
            slide_finished = false;
            dll_p1_selected = false; dll_p2_selected = false;
            player_index = 1;
            char_index = 1;
            composition_p1 = new int[11];
            composition_p2 = new int[11];
            for (int i = 0; i < 11; i++)
            {
                composition_p1[i] = 0;
                composition_p2[i] = 0;
            }

            // Inisialisasi gambar

            //GAMBAR PAGE AWAL
            Screen = new Texture2D[6];
            Screen[0] = Content.Load<Texture2D>("Resource\\mainmenu");
            Screen[2] = Content.Load<Texture2D>("Resource\\player1deck");
            Screen[3] = Content.Load<Texture2D>("Resource\\player2deck");
            Screen[4] = Content.Load<Texture2D>("Resource\\player1mask");
            Screen[5] = Content.Load<Texture2D>("Resource\\player2mask");

            Symbol = new Texture2D[2];
            Symbol[0] = Content.Load<Texture2D>("Resource\\unit-sprite");
            Symbol[1] = Content.Load<Texture2D>("Resource\\cursor");

            Button = new Texture2D[19];
            Button[0] = Content.Load<Texture2D>("Resource\\StartButton");
            Button[1] = Content.Load<Texture2D>("Resource\\arrow-up");
            Button[2] = Content.Load<Texture2D>("Resource\\arrow-down");
            Button[3] = Content.Load<Texture2D>("Resource\\go");
            Button[4] = Content.Load<Texture2D>("Resource\\arrow-up-pressed");
            Button[5] = Content.Load<Texture2D>("Resource\\arrow-down-pressed");
            Button[6] = Content.Load<Texture2D>("Resource\\load-dll");
            Button[7] = Content.Load<Texture2D>("Resource\\load-komp");
            Button[8] = Content.Load<Texture2D>("Resource\\load-dll-p1");
            Button[9] = Content.Load<Texture2D>("Resource\\load-comp-p1");
            Button[10] = Content.Load<Texture2D>("Resource\\load-dll-p2");
            Button[11] = Content.Load<Texture2D>("Resource\\load-comp-p2");
            Button[12] = Content.Load<Texture2D>("Resource\\go-hover");
            Button[13] = Content.Load<Texture2D>("Resource\\mainmenu_start1");
            Button[14] = Content.Load<Texture2D>("Resource\\mainmenu_start2");
            Button[15] = Content.Load<Texture2D>("Resource\\mainmenu_help1");
            Button[16] = Content.Load<Texture2D>("Resource\\mainmenu_help2");
            Button[17] = Content.Load<Texture2D>("Resource\\mainmenu_exit1");
            Button[18] = Content.Load<Texture2D>("Resource\\mainmenu_exit2");

            //spriteSheet = new Texture2D(graphics, 4384, 199);
            spriteSheet = Content.Load<Texture2D>(@"Resource\coin-blue");
            destinationRect = new Rectangle(300, 200, spriteWidth, spriteHeight);
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
            /*MC = new MenuController(@"D:\Study\6th-Semester\IF3054 - AI\Tubes\Tubes1\IBTBS\Algoritma\Algoritma\bin\Debug\Algoritma.dll",
                                    @"D:\Study\6th-Semester\IF3054 - AI\Tubes\Tubes1\IBTBS\Algoritma\Algoritma\bin\Debug\Algoritma.dll",
                                    ViewMenu.kload(), ViewMenu.kload(), "BFS", "BFS");
            */
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
            // TODO: Add your update logic here
            absis = Mouse.GetState().X;
            ordinat = Mouse.GetState().Y;

            if (ScreenState == 0)
            {
                // Start
                if (absis >= 400 - (Button[13].Width / 2) && absis <= 400 + (Button[13].Width / 2) && ordinat >= 350 && ordinat <= 350 + Button[13].Height)
                {
                    start_hovered = true;
                }
                // Help
                else if (absis >= 400 - (Button[15].Width / 2) && absis <= 400 + (Button[15].Width / 2) && ordinat >= (350 + Button[15].Height + 10) && ordinat <= (350 + Button[15].Height + 10) + Button[15].Height)
                {
                    help_hovered = true;
                }
                // Exit
                else if (absis >= 400 - (Button[17].Width / 2) && absis <= 400 + (Button[17].Width / 2) && ordinat >= (350 + 2 * (Button[17].Height + 10)) && ordinat <= (350 + 2 * (Button[17].Height + 10)) + Button[17].Height)
                {
                    exit_hovered = true;
                }
                else
                {
                    start_hovered = false; help_hovered = false; exit_hovered = false;
                }

                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !pressed)
                {
                    pressed = true;

                    // Start
                    if (absis >= 400 - (Button[13].Width / 2) && absis <= 400 + (Button[13].Width / 2) && ordinat >= 350 && ordinat <= 350 + Button[13].Height)
                    {
                        ScreenState = 1;
                    }
                    // Help
                    else if (absis >= 400 - (Button[15].Width / 2) && absis <= 400 + (Button[15].Width / 2) && ordinat >= (350 + Button[15].Height + 10) && ordinat <= (350 + Button[15].Height + 10) + Button[15].Height)
                    {
                        // display Help
                    }
                    // Exit
                    else if (absis >= 400 - (Button[17].Width / 2) && absis <= 400 + (Button[17].Width / 2) && ordinat >= (350 + 2 * (Button[17].Height + 10)) && ordinat <= (350 + 2 * (Button[17].Height + 10)) + Button[17].Height)
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
                // Go
                if (absis >= 360 && absis <= 440 && ordinat >= 260 && ordinat <= 340)
                {
                    go_hovered = true;
                }
                // Load DLL P1
                else if (absis >= 25 && absis <= 25 + Button[8].Width && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                {
                    dll_p1_hovered = true;
                }
                // Load Komposisi P1
                else if (absis >= 25 + Button[8].Width + 10 && absis <= 25 + 2 * Button[8].Width + 10 && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                {
                    komp_p1_hovered = true;
                }
                // Load DLL P2
                else if (absis >= 475 && absis <= 475 + Button[9].Width && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                {
                    dll_p2_hovered = true;
                }
                // Load Komposisi P2
                else if (absis >= 475 + Button[9].Width + 10 && absis <= 475 + 2 * Button[9].Width + 10 && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                {
                    komp_p2_hovered = true;
                }
                else
                {
                    komp_p1_hovered = false; dll_p1_hovered = false;
                    komp_p2_hovered = false; dll_p2_hovered = false;
                    go_hovered = false;
                }


                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !pressed)
                {
                    pressed = true;

                    // Go
                    if (absis >= 360 && absis <= 440 && ordinat >= 260 && ordinat <= 340)
                    {
                        // go_hovered = true;
                        ScreenState = 2;
                    }
                    // Load DLL P1
                    else if (absis >= 25 && absis <= 25 + Button[8].Width && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                    {
                        ViewMenu.DLL_P1 = ViewMenu.dload();
                        System.Console.Out.WriteLine(ViewMenu.DLL_P1);
                        if (String.Compare(ViewMenu.DLL_P1, "") == 0) dll_p1_selected = true;
                        // dll_p1_hovered = true;
                    }
                    // Load Komposisi P1
                    else if (absis >= 25 + Button[8].Width + 10 && absis <= 25 + 2 * Button[8].Width + 10 && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                    {
                        int[] K_P1 = new int[11];
                        composition_p1 = ViewMenu.kload();
                        K_P1 = composition_p1;
                        // komp_p1_hovered = true;
                    }
                    // Load DLL P2
                    else if (absis >= 475 && absis <= 475 + Button[9].Width && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                    {
                        ViewMenu.DLL_P2 = ViewMenu.dload();
                        System.Console.Out.WriteLine(ViewMenu.DLL_P2);
                        if (String.Compare(ViewMenu.DLL_P2, "") == 0) dll_p2_selected = true;
                        // dll_p2_hovered = true;
                    }
                    // Load Komposisi P2
                    else if (absis >= 475 + Button[9].Width + 10 && absis <= 475 + 2 * Button[9].Width + 10 && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                    {
                        int[] K_P2 = new int[11];
                        composition_p2 = ViewMenu.kload();
                        K_P2 = composition_p2;
                        // komp_p2_hovered = true;
                    }
                }
                else if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                {
                    pressed = false;
                }

                // Ganti Pemain yang Melakukan Seleksi
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Tab) && elapsedtime >= 150)
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
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) && elapsedtime >= 150)
                {
                    if (player_index == 1)
                    {
                        composition_p1[char_index - 1]--;
                        if (composition_p1[char_index - 1] < 1) composition_p1[char_index - 1] = 5;
                    }
                    else if (player_index == 2)
                    {
                        composition_p2[char_index - 1]--;
                        if (composition_p2[char_index - 1] < 1) composition_p2[char_index - 1] = 5;
                    }
                    down_pressed = true;
                    elapsedtime = 0;
                }
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) && elapsedtime >= 150)
                {
                    if (player_index == 1)
                    {
                        composition_p1[char_index - 1]++;
                        if (composition_p1[char_index - 1] > 5) composition_p1[char_index - 1] = 1;
                    }
                    else if (player_index == 2)
                    {
                        composition_p2[char_index - 1]++;
                        if (composition_p2[char_index - 1] > 5) composition_p2[char_index - 1] = 1;
                    }
                    up_pressed = true;
                    elapsedtime = 0;
                }
                // Ganti indeks unit (1 s.d. 11)
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) && elapsedtime >= 150)
                {
                    char_index--;
                    if (char_index < 1) char_index = 11;
                    elapsedtime = 0;
                }
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) && elapsedtime >= 150)
                {
                    char_index++;
                    if (char_index > 11) char_index = 1;
                    elapsedtime = 0;
                }
            }
            else if (ScreenState == 2)
            {
                // Tes flip-coin
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > interval)
                {
                    currentFrame++;
                    if (currentFrame > frameCount - 1)
                    {
                        currentFrame = 0;
                        slide_finished = true;
                    }
                    timer = 0f;
                }
                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                //
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) && elapsedtime >= 150)
                {
                    // aksi flip coin
                    ScreenState = 3;
                    MC = new MenuController(
                        "D:\\Study\\6th-Semester\\IF3054 - AI\\Tubes\\Tubes1\\IBTBS\\ModulUtama\\ModulUtama\\bin\\x86\\Debug\\Algoritma\\Algoritma.dll"
                        , "D:\\Study\\6th-Semester\\IF3054 - AI\\Tubes\\Tubes1\\IBTBS\\ModulUtama\\ModulUtama\\bin\\x86\\Debug\\Algoritma\\Algoritma.dll"
                        , composition_p1, composition_p2, "BFS", "BFS");
                    // tentukan player mana yang jalan terlebih dahulu
                }
            }
            else if (ScreenState == 3)
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
            if (drawingdelay > 0)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();
                // spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 600), Color.White);
                ViewGame.draw(spriteBatch, spriteFont, this);
                if (ScreenState == 0)
                {
                    spriteBatch.Draw(Screen[0], new Rectangle(0, 0, Screen[0].Width, Screen[0].Height), Color.White);
                    // Start Button
                    if (start_hovered)
                    {
                        spriteBatch.Draw(Button[14], new Rectangle(400 - (Button[14].Width / 2), 350, Button[14].Width, Button[14].Height), Color.White);
                    }
                    else spriteBatch.Draw(Button[13], new Rectangle(400 - (Button[13].Width / 2), 350, Button[13].Width, Button[13].Height), Color.White);
                    // Help Button
                    if (help_hovered)
                    {
                        spriteBatch.Draw(Button[16], new Rectangle(400 - (Button[16].Width / 2), 350 + Button[16].Height + 10, Button[16].Width, Button[16].Height), Color.White);
                    }
                    else spriteBatch.Draw(Button[15], new Rectangle(400 - (Button[15].Width / 2), 350 + Button[15].Height + 10, Button[15].Width, Button[15].Height), Color.White);
                    // Exit Button
                    if (exit_hovered)
                    {
                        spriteBatch.Draw(Button[18], new Rectangle(400 - (Button[18].Width / 2), 350 + 2 * (Button[18].Height + 10), Button[18].Width, Button[18].Height), Color.White);
                    }
                    else spriteBatch.Draw(Button[17], new Rectangle(400 - (Button[17].Width / 2), 350 + 2 * (Button[17].Height + 10), Button[17].Width, Button[17].Height), Color.White);
                }
                else if (ScreenState == 1)
                {
                    // layar bagian atas
                    spriteBatch.Draw(Screen[2], new Rectangle(0, 0, Screen[2].Width, Screen[2].Height), Color.White);
                    // layar bagian bawah
                    spriteBatch.Draw(Screen[3], new Rectangle(0, 600 - Screen[3].Height, Screen[3].Width, Screen[3].Height), Color.White);
                    // kolom sprite unit
                    for (int i = 0; i < 11; i++)
                    {
                        spriteBatch.Draw(Symbol[0], new Rectangle(43 + i * (11 + Symbol[0].Width), 55, Symbol[0].Width, Symbol[0].Height), Color.White);
                        spriteBatch.Draw(Symbol[0], new Rectangle(43 + i * (11 + Symbol[0].Width), 435, Symbol[0].Width, Symbol[0].Height), Color.White);
                    }
                    // panah atas
                    if (up_pressed)
                    {
                        spriteBatch.Draw(Button[4], new Rectangle(43 + (char_index - 1) * (11 + Button[4].Width), 20 + (player_index - 1) * 379, Button[4].Width, Button[4].Height), Color.White);
                        up_pressed = false;
                    }
                    else spriteBatch.Draw(Button[1], new Rectangle(43 + (char_index - 1) * (11 + Button[1].Width), 20 + (player_index - 1) * 379, Button[1].Width, Button[1].Height), Color.White);
                    // panah bawah
                    if (down_pressed)
                    {
                        spriteBatch.Draw(Button[5], new Rectangle(43 + (char_index - 1) * (11 + Button[5].Width), 179 + (player_index - 1) * 379, Button[5].Width, Button[5].Height), Color.White);
                        down_pressed = false;
                    }
                    else spriteBatch.Draw(Button[2], new Rectangle(43 + (char_index - 1) * (11 + Button[2].Width), 179 + (player_index - 1) * 379, Button[2].Width, Button[2].Height), Color.White);
                    // kursor
                    spriteBatch.Draw(Symbol[1], new Rectangle(43 + (char_index - 1) * (11 + Symbol[1].Width), 55 + (player_index - 1) * 380, Symbol[1].Width, Symbol[1].Height), Color.White);
                    // load P1 - DLL
                    if (dll_p1_hovered)
                    {
                        spriteBatch.Draw(Button[8], new Rectangle(25, 260, Button[8].Width, Button[8].Height), Color.White);
                        // dll_p1_hovered = false;
                    }
                    else spriteBatch.Draw(Button[6], new Rectangle(25, 260, Button[6].Width, Button[6].Height), Color.White);
                    // load P1 - Komposisi
                    if (komp_p1_hovered)
                    {
                        spriteBatch.Draw(Button[9], new Rectangle(25 + Button[7].Width + 10, 260, Button[9].Width, Button[9].Height), Color.White);
                        // komp_p1_hovered = false;
                    }
                    else spriteBatch.Draw(Button[7], new Rectangle(25 + Button[7].Width + 10, 260, Button[7].Width, Button[7].Height), Color.White);
                    // load P2 - DLL
                    if (dll_p2_hovered)
                    {
                        spriteBatch.Draw(Button[10], new Rectangle(475, 310, Button[10].Width, Button[10].Height), Color.White);
                        // dll_p2_hovered = false;
                    }
                    else spriteBatch.Draw(Button[6], new Rectangle(475, 310, Button[6].Width, Button[6].Height), Color.White);
                    // load P2 - Komposisi
                    if (komp_p2_hovered)
                    {
                        spriteBatch.Draw(Button[11], new Rectangle(475 + Button[11].Width + 10, 310, Button[11].Width, Button[11].Height), Color.White);
                        // komp_p2_hovered = false;
                    }
                    else spriteBatch.Draw(Button[7], new Rectangle(475 + Button[7].Width + 10, 310, Button[7].Width, Button[7].Height), Color.White);

                    /* if (player_index == 1)
                    {
                        spriteBatch.Draw(Screen[5], new Rectangle(0, 600 - Screen[2].Height, Screen[2].Width, Screen[2].Height), Color.White);
                    }
                    else if (player_index == 2)
                    {
                        spriteBatch.Draw(Screen[4], new Rectangle(0, 0, Screen[2].Width, Screen[2].Height), Color.White);
                    } */

                    // tombol go
                    if (go_hovered)
                    {
                        spriteBatch.Draw(Button[12], new Rectangle(360, 260, Button[12].Width, Button[12].Height), Color.White);
                        // go_hovered = false;
                    }
                    else spriteBatch.Draw(Button[3], new Rectangle(360, 260, Button[3].Width, Button[3].Height), Color.White);
                }
                else if (ScreenState == 2)
                {
                    GraphicsDevice.Clear(Color.TransparentWhite);
                    // Tes flip-coin
                    spriteBatch.Draw(spriteSheet, destinationRect, sourceRect, Color.White);
                    //
                    if (slide_finished == false)
                    {
                        // layar bagian atas
                        spriteBatch.Draw(Screen[2], new Rectangle(0, 0 - (currentFrame * 15), Screen[2].Width, Screen[2].Height), Color.White);
                        // layar bagian bawah
                        spriteBatch.Draw(Screen[3], new Rectangle(0, 600 - Screen[3].Height + (currentFrame * 15), Screen[3].Width, Screen[3].Height), Color.White);
                    }

                }
                spriteBatch.End();
                drawingdelay = 0;
            }
            else if (ScreenState == 3)
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
            drawingdelay += gameTime.ElapsedGameTime.Milliseconds;
            base.Draw(gameTime);
        }
    }
}
