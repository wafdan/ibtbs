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
        // Graphics Device Manager 
        GraphicsDeviceManager graphics;       
        
        // Controller
        MenuController MC;
        
        // Texture2D
        Texture2D BG;
        Texture2D Bar;
        Texture2D[] Screen;
        Texture2D[] Button;
        Texture2D[] Symbol;
        Texture2D spriteSheet;

        // Sprite
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        int spriteWidth = 100;
        int spriteHeight = 100;
        
        // Sound
        public static Sound sound = new Sound();

        // Delay
        int drawingdelay = 0;
        int elapsedtime = 0;
        float timer = 0f;
        float interval = 1000f / 25f;
        int frameCount = 22;
        int currentFrame = 0;

        // Mouse State
        int absis, ordinat;

        // Variabel for State of Game
        int ScreenState;
        public static int isGameOver = 0;
        bool pressed, up_pressed, down_pressed;
        bool dll_p1_hovered, komp_p1_hovered, dll_p2_hovered, komp_p2_hovered, go_hovered, slide_finished;
        bool start_hovered, help_hovered, exit_hovered;
        bool dll_p1_selected, dll_p2_selected;
        bool play;         
        bool udahpilih;
        int countFlip;
        bool Hasilflipcoin;
        
        // Composition
        int player_index, char_index;
        int[] composition_p1;
        int[] composition_p2;

        // Algoritm File Path
        String algorithm_p1;
        String algorithm_p2;

        // Rectangle
        Rectangle sourceRect;
        Rectangle destinationRect;

        /// <summary>
        /// Game constructors
        /// </summary>
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Load Picture Content
        /// </summary>
        private void LoadPictureContent()
        {
            BG = Content.Load<Texture2D>(@"Images/bg_battle");
            Bar = Content.Load<Texture2D>(@"Resource/load-p2");

            Screen = new Texture2D[6];
            Screen[0] = Content.Load<Texture2D>("Resource\\mainmenu");
            Screen[1] = Content.Load<Texture2D>("Resource\\metalbg");
            Screen[2] = Content.Load<Texture2D>("Resource\\player1deck");
            Screen[3] = Content.Load<Texture2D>("Resource\\player2deck");
            Screen[4] = Content.Load<Texture2D>("Resource\\player1mask");
            Screen[5] = Content.Load<Texture2D>("Resource\\player2mask");

            Symbol = new Texture2D[2];
            Symbol[0] = Content.Load<Texture2D>("Resource\\unit-sprite");
            Symbol[1] = Content.Load<Texture2D>("Resource\\cursor");

            Button = new Texture2D[27];
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
            Button[19] = Content.Load<Texture2D>("Resource\\button_bfs");
            Button[20] = Content.Load<Texture2D>("Resource\\button_dfs");
            Button[21] = Content.Load<Texture2D>("Resource\\button_ucs");
            Button[22] = Content.Load<Texture2D>("Resource\\button_greedy");
            Button[23] = Content.Load<Texture2D>("Resource\\button_a");
            Button[24] = Content.Load<Texture2D>("Resource\\button_csp");
            Button[25] = Content.Load<Texture2D>("Resource\\coin-blue");
            Button[26] = Content.Load<Texture2D>("Resource\\coin-red");

            spriteSheet = Content.Load<Texture2D>(@"Resource\coin-flip");
            destinationRect = new Rectangle(400 - (spriteWidth / 2), 300 - (spriteHeight / 2), spriteWidth, spriteHeight);

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
        }

        /// <summary>
        /// Inisialisasi letak karakter
        /// </summary>
        private void InitializePlaceUnit()
        {
            /* 
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
        }

        /// <summary>
        /// Inisialisasi variabel untuk awal game
        /// </summary>
        private void InitializeVariable()
        {
            pressed = false; up_pressed = false; down_pressed = false;
            start_hovered = false; help_hovered = false; exit_hovered = false;
            slide_finished = false;
            dll_p1_selected = false; dll_p2_selected = false;
            ScreenState = 0;
            udahpilih = false;
            play = false;
            player_index = 1;
            char_index = 1;
            countFlip = 0;
            Hasilflipcoin = false;
            isGameOver = 0;


            algorithm_p1 = null;
            algorithm_p2 = null;

            composition_p1 = new int[11];
            composition_p2 = new int[11];
            for (int i = 0; i < 11; i++)
            {
                composition_p1[i] = 0;
                composition_p2[i] = 0;
            }
        }

        /// <summary>
        /// Reset variabel untuk memulai game baru
        /// </summary>
        private void ResetGame()
        {
            udahpilih = false;
            play = false;
            countFlip = 0;
            isGameOver = 0;
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
            graphics.PreferredBackBufferWidth = 800; // 800 * 600
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            IsMouseVisible = true;
            graphics.ApplyChanges();

            InitializeVariable();
            LoadPictureContent();
            InitializePlaceUnit();
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
                    help_hovered = false; exit_hovered = false;
                }
                // Help
                else if (absis >= 400 - (Button[15].Width / 2) && absis <= 400 + (Button[15].Width / 2) && ordinat >= (350 + Button[15].Height + 10) && ordinat <= (350 + Button[15].Height + 10) + Button[15].Height)
                {
                    help_hovered = true;
                    start_hovered = false; exit_hovered = false;
                }
                // Exit
                else if (absis >= 400 - (Button[17].Width / 2) && absis <= 400 + (Button[17].Width / 2) && ordinat >= (350 + 2 * (Button[17].Height + 10)) && ordinat <= (350 + 2 * (Button[17].Height + 10)) + Button[17].Height)
                {
                    exit_hovered = true;
                    start_hovered = false; help_hovered = false;
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
                    if (dll_p1_selected && dll_p2_selected)
                        go_hovered = true;
                    else
                        go_hovered = false;
                    komp_p1_hovered = false; dll_p1_hovered = false;
                    komp_p2_hovered = false; dll_p2_hovered = false;
                }
                // Load DLL P1
                else if (absis >= 25 && absis <= 25 + Button[8].Width && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                {
                    dll_p1_hovered = true;
                    komp_p1_hovered = false;
                    komp_p2_hovered = false; dll_p2_hovered = false;
                    go_hovered = false;
                }
                // Load Komposisi P1
                else if (absis >= 25 + Button[8].Width + 10 && absis <= 25 + 2 * Button[8].Width + 10 && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                {
                    komp_p1_hovered = true;
                    dll_p1_hovered = false;
                    komp_p2_hovered = false; dll_p2_hovered = false;
                    go_hovered = false;
                }
                // Load DLL P2
                else if (absis >= 475 && absis <= 475 + Button[9].Width && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                {
                    dll_p2_hovered = true;
                    komp_p1_hovered = false; dll_p1_hovered = false;
                    komp_p2_hovered = false;
                    go_hovered = false;
                }
                // Load Komposisi P2
                else if (absis >= 475 + Button[9].Width + 10 && absis <= 475 + 2 * Button[9].Width + 10 && ordinat >= 310 && ordinat <= 310 + Button[9].Height)
                {
                    komp_p2_hovered = true;
                    komp_p1_hovered = false; dll_p1_hovered = false;
                    dll_p2_hovered = false;
                    go_hovered = false;
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
                        if (dll_p1_selected && dll_p2_selected)
                            ScreenState = 2;
                    }
                    // Load DLL P1
                    else if (absis >= 25 && absis <= 25 + Button[8].Width && ordinat >= 260 && ordinat <= 260 + Button[8].Height)
                    {
                        algorithm_p1 = ViewMenu.dload();
                        if (!String.IsNullOrEmpty(algorithm_p1))
                            ViewMenu.DLL_P1 = algorithm_p1;
                        if (!String.IsNullOrEmpty(ViewMenu.DLL_P1))
                            dll_p1_selected = true; //??
                        else
                            dll_p1_selected = false;
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
                        algorithm_p2 = ViewMenu.dload();
                        if (!String.IsNullOrEmpty(algorithm_p2))
                            ViewMenu.DLL_P2 = algorithm_p2;
                        if (!String.IsNullOrEmpty(ViewMenu.DLL_P2))
                            dll_p2_selected = true; //??
                        else
                            dll_p2_selected = false;
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
                        if (composition_p1[char_index - 1] < 0) composition_p1[char_index - 1] = 4;
                    }
                    else if (player_index == 2)
                    {
                        composition_p2[char_index - 1]--;
                        if (composition_p2[char_index - 1] < 0) composition_p2[char_index - 1] = 4;
                    }
                    down_pressed = true;
                    elapsedtime = 0;
                }
                else if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) && elapsedtime >= 150)
                {
                    if (player_index == 1)
                    {
                        composition_p1[char_index - 1]++;
                        if (composition_p1[char_index - 1] > 4) composition_p1[char_index - 1] = 0;
                    }
                    else if (player_index == 2)
                    {
                        composition_p2[char_index - 1]++;
                        if (composition_p2[char_index - 1] > 4) composition_p2[char_index - 1] = 0;
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
            else if (ScreenState == 2) // memilih algoritma
            {
                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !pressed)
                {
                    bool abal = udahpilih;
                    pressed = true;
                    if (absis >= 100 && absis <= 350)
                    {
                        if (ordinat >= 200 && ordinat <= 300)
                        {
                            // klik BFS
                            if (!udahpilih)
                            {
                                algorithm_p1 = "BFS";
                                udahpilih = true;
                            }
                            else algorithm_p2 = "BFS";
                        }
                        else if (ordinat >= 325 && ordinat <= 425)
                        {
                            // klik DFS
                            if (!udahpilih)
                            {
                                algorithm_p1 = "DFS";
                                udahpilih = true;
                            }
                            else algorithm_p2 = "DFS";
                        }
                        else if (ordinat >= 450 && ordinat <= 550)
                        {
                            // klik UCS
                            if (!udahpilih)
                            {
                                algorithm_p1 = "UCS";
                                udahpilih = true;
                            }
                            else algorithm_p2 = "UCS";
                        }
                    }
                    else if (absis >= 450 && absis <= 700)
                    {
                        if (ordinat >= 200 && ordinat <= 300)
                        {
                            // klik Greedy
                            if (!udahpilih)
                            {
                                algorithm_p1 = "Greedy";
                                udahpilih = true;
                            }
                            else algorithm_p2 = "Greedy";
                        }
                        else if (ordinat >= 325 && ordinat <= 425)
                        {
                            // klik A*
                            if (!udahpilih)
                            {
                                algorithm_p1 = "Astar";
                                udahpilih = true;
                            }
                            else algorithm_p2 = "Astar";
                        }
                        else if (ordinat >= 450 && ordinat <= 550)
                        {
                            // klik CSP
                            if (!udahpilih)
                            {
                                algorithm_p1 = "CSP";
                                udahpilih = true;
                            }
                            else algorithm_p2 = "CSP";
                        }
                    }
                    if (abal) ScreenState = 3;
                }
                else if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                {
                    pressed = false;
                }
            }
            else if (ScreenState == 3)
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
                if (countFlip == 0)
                {
                    sourceRect = new Rectangle((currentFrame % 11) * spriteWidth, (currentFrame / 11) * spriteHeight, spriteWidth, spriteHeight);
                    if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) && elapsedtime >= 150)
                    {
                        MC = new MenuController(ViewMenu.DLL_P1
                            , ViewMenu.DLL_P2
                            , composition_p1, composition_p2, algorithm_p1, algorithm_p2);
                        Hasilflipcoin = MC.hasilFlipCoin;
                        countFlip = 1;
                        elapsedtime = 0;
                    }
                }
                else if (countFlip == 1)
                {
                    if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) && elapsedtime >= 150)
                        ScreenState = 4;
                }
            }
            else if (ScreenState == 4)
            {
                MC.GC.GameLoop();
            }

            if (play)
            {
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) && elapsedtime >= 150)
                {
                    ScreenState = 0;
                    sound.BGM_simulate(this);
                    ResetGame();
                }
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
                //GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();
                // spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 600), Color.White);
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

                    if (player_index == 1)
                    {
                        spriteBatch.Draw(Screen[5], new Rectangle(0, 600 - Screen[2].Height, Screen[2].Width, Screen[2].Height), Color.White);
                        if (dll_p1_selected)
                            spriteBatch.DrawString(spriteFont, "Player 1 has loaded algorithm", new Vector2(50, 220), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                        else
                            spriteBatch.DrawString(spriteFont, "Please load algorithm", new Vector2(50, 220), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                    else if (player_index == 2)
                    {
                        spriteBatch.Draw(Screen[4], new Rectangle(0, 0, Screen[2].Width, Screen[2].Height), Color.White);
                        if (dll_p2_selected)
                            spriteBatch.DrawString(spriteFont, "Player 2 has loaded algorithm", new Vector2(470, 360), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                        else
                            spriteBatch.DrawString(spriteFont, "Please load algorithm", new Vector2(550, 360), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }

                    // tombol go
                    if (go_hovered)
                    {
                        spriteBatch.Draw(Button[12], new Rectangle(360, 260, Button[12].Width, Button[12].Height), Color.White);
                        // go_hovered = false;
                    }
                    else spriteBatch.Draw(Button[3], new Rectangle(360, 260, Button[3].Width, Button[3].Height), Color.White);
                    
                    if (player_index == 1)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            if (composition_p1[i] == 0)
                            {
                                spriteBatch.Draw(Archer.textureL, new Vector2((i * 66) + 50, 50), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p1[i] == 1)
                            {
                                spriteBatch.Draw(Medic.textureL, new Vector2((i * 66) + 50, 50), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p1[i] == 2)
                            {
                                spriteBatch.Draw(Spearman.textureL, new Vector2((i * 66) + 50, 50), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p1[i] == 3)
                            {
                                spriteBatch.Draw(Rider.textureL, new Vector2((i * 66) + 50, 50), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p1[i] == 4)
                            {
                                spriteBatch.Draw(Swordsman.textureL, new Vector2((i * 66) + 50, 50), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            if (composition_p2[i] == 0)
                            {
                                spriteBatch.Draw(Archer.textureL, new Vector2((i * 66) + 50, 425), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p2[i] == 1)
                            {
                                spriteBatch.Draw(Medic.textureL, new Vector2((i * 66) + 50, 425), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p2[i] == 2)
                            {
                                spriteBatch.Draw(Spearman.textureL, new Vector2((i * 66) + 50, 425), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p2[i] == 3)
                            {
                                spriteBatch.Draw(Rider.textureL, new Vector2((i * 66) + 50, 425), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                            else if (composition_p2[i] == 4)
                            {
                                spriteBatch.Draw(Swordsman.textureL, new Vector2((i * 66) + 50, 425), new Rectangle(0, 0, 50, 80), Color.White);
                            }
                        }
                    }

                }

                else if (ScreenState == 2)
                {
                    spriteBatch.Draw(Screen[1], new Rectangle(0, 0, Screen[1].Width, Screen[1].Height), Color.White);

                    spriteBatch.Draw(Button[19], new Rectangle(100, 200, 300, 100), Color.White);
                    spriteBatch.Draw(Button[20], new Rectangle(100, 325, 300, 100), Color.White);
                    spriteBatch.Draw(Button[21], new Rectangle(100, 450, 300, 100), Color.White);
                    spriteBatch.Draw(Button[22], new Rectangle(450, 200, 300, 100), Color.White);
                    spriteBatch.Draw(Button[23], new Rectangle(450, 325, 300, 100), Color.White);
                    spriteBatch.Draw(Button[24], new Rectangle(450, 450, 300, 100), Color.White);

                    if (udahpilih)
                    {
                        spriteBatch.DrawString(spriteFont, "PILIH ALGORITMA 2", new Vector2(102, 52), Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(spriteFont, "PILIH ALGORITMA 2", new Vector2(100, 50), Color.Yellow, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont, "PILIH ALGORITMA 1", new Vector2(102, 52), Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(spriteFont, "PILIH ALGORITMA 1", new Vector2(100, 50), Color.Green, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
                    }

                }

                else if (ScreenState == 3)
                {
                    GraphicsDevice.Clear(Color.TransparentWhite);
                    // Tes flip-coin
                    if (countFlip == 0)
                    {
                        spriteBatch.Draw(spriteSheet, destinationRect, sourceRect, Color.White);
                        if (slide_finished == false)
                        {
                            // layar bagian atas
                            spriteBatch.Draw(Screen[2], new Rectangle(0, 0 - (currentFrame * 15), Screen[2].Width, Screen[2].Height), Color.White);
                            // layar bagian bawah
                            spriteBatch.Draw(Screen[3], new Rectangle(0, 600 - Screen[3].Height + (currentFrame * 15), Screen[3].Width, Screen[3].Height), Color.White);
                        }
                    }
                    else if (countFlip == 1)
                    {
                        if (Hasilflipcoin)
                        {
                            spriteBatch.Draw(Button[25], new Vector2(300, 200), Color.White);
                        }
                        else if (!Hasilflipcoin)
                        {
                            spriteBatch.Draw(Button[26], new Vector2(300, 200), Color.White);
                        }
                    }


                }
                else if (ScreenState == 4)
                {
                    if (isGameOver == 0 || ViewGame.drawcount > 40)
                    {
                        // TODO: Add your drawing code here
                        if (drawingdelay > 1)
                        {
                            GraphicsDevice.Clear(Color.CornflowerBlue);
                            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 600), Color.White);
                            ViewGame.drawBar(spriteBatch, Bar);
                            ViewGame.draw(spriteBatch, spriteFont, this);
                            drawingdelay = 0;
                        }
                    }
                    else
                    {
                        if (!play)
                        {
                            sound.BGM_main(this);
                            play = true;
                        }
                        if (isGameOver == 1)                        
                            spriteBatch.DrawString(spriteFont, "TEAM 1 WINS!", new Vector2(100, 300), Color.Green, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                        else if (isGameOver == 2)
                            spriteBatch.DrawString(spriteFont, "TEAM 2 WINS!", new Vector2(500, 300), Color.Yellow, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(spriteFont, "PRESS ENTER TO GO BACK TO MAIN SCREEN", new Vector2(80, 500), Color.SteelBlue, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
                    }
                }

                spriteBatch.End();
            drawingdelay += gameTime.ElapsedGameTime.Milliseconds;
            base.Draw(gameTime);
        }
    }
}
