using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ModulUtama
{

    public class Sound
    {
        //background music
        public Song mainBGM, makeBGM, simulateBGM;
        //sfx
        public SoundEffect foundSFX, notfoundSFX, okSFX, searchSFX, startSFX;
        SoundEffectInstance s;

        public Sound()
        {
           
        }

        public void BGM_main(Game game)
        {
            MediaPlayer.Stop();
            mainBGM = game.Content.Load<Song>(@"BGM/main");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainBGM);
        }

        public void BGM_make(Game game)
        {
            MediaPlayer.Stop();
            makeBGM = game.Content.Load<Song>(@"BGM/make");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(makeBGM);
        }

        public void BGM_simulate(Game game)
        {
            MediaPlayer.Stop();
            simulateBGM = game.Content.Load<Song>(@"BGM/simulate");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(simulateBGM);
        }

        public void SFX_notfound(Game game)
        {
            notfoundSFX = game.Content.Load<SoundEffect>(@"SFX/notfound");
            s = notfoundSFX.CreateInstance();
            s.Play();
        }

        public void SFX_found(Game game)
        {
            foundSFX = game.Content.Load<SoundEffect>(@"SFX/found");
            s = foundSFX.CreateInstance();
            s.Play();
        }

        public void SFX_ok(Game game)
        {
            okSFX = game.Content.Load<SoundEffect>(@"SFX/ok");
            s = okSFX.CreateInstance();
            s.Play();
        }

        public void SFX_search(Game game)
        {
            searchSFX = game.Content.Load<SoundEffect>(@"SFX/search");
            s = searchSFX.CreateInstance();
            s.Play();
        }

        public void SFX_start(Game game)
        {
            startSFX = game.Content.Load<SoundEffect>(@"SFX/start");
            s = startSFX.CreateInstance();
            s.Play();
        }
    }
}
