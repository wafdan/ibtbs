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
        MenuController MC;
        int drawingdelay;
        
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
            Archer.texture = Content.Load<Texture2D>(@"Images/template");
            Swordsman.texture = Content.Load<Texture2D>(@"Images/template");
            Spearman.texture = Content.Load<Texture2D>(@"Images/template");
            Rider.texture = Content.Load<Texture2D>(@"Images/template");
            Medic.texture = Content.Load<Texture2D>(@"Images/template");

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
            ViewGame.places[0] = new Vector2(230, 110);
            ViewGame.places[1] = new Vector2(230, 250);
            ViewGame.places[2] = new Vector2(230, 390);
            ViewGame.places[3] = new Vector2(160, 50);
            ViewGame.places[4] = new Vector2(160, 180);
            ViewGame.places[5] = new Vector2(160, 320);
            ViewGame.places[6] = new Vector2(160, 460);
            ViewGame.places[7] = new Vector2(90, 50);
            ViewGame.places[8] = new Vector2(90, 180);
            ViewGame.places[9] = new Vector2(90, 320);
            ViewGame.places[10] = new Vector2(90, 460);

            ViewGame.places[11] = new Vector2(520, 110);
            ViewGame.places[12] = new Vector2(520, 250);
            ViewGame.places[13] = new Vector2(520, 390);
            ViewGame.places[14] = new Vector2(590, 50);
            ViewGame.places[15] = new Vector2(590, 180);
            ViewGame.places[16] = new Vector2(590, 320);
            ViewGame.places[17] = new Vector2(590, 460);
            ViewGame.places[18] = new Vector2(660, 50);
            ViewGame.places[19] = new Vector2(660, 180);
            ViewGame.places[20] = new Vector2(660, 320);
            ViewGame.places[21] = new Vector2(660, 460);

            ViewGame.waitFrame = 0;

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

            // TODO: Add your update logic here
            MC.GC.GameLoop();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here
            if (drawingdelay > 100)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                ViewGame.draw(spriteBatch);
                drawingdelay = 0;
            }

            drawingdelay += gameTime.ElapsedGameTime.Milliseconds;

            base.Draw(gameTime);
        }
    }
}
