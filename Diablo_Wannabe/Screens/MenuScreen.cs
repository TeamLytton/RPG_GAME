using System;
using System.Xml.Serialization;
using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Screens
{
    public class MenuScreen : Screen
    {
        private Image[] menuImages;
        private Image currentImage;
        private TimeSpan timePassed;

        private bool hasAction;
        private bool startNewGame;
        private bool openOptions;
        private bool openCredits;
        private bool Quit;


        public MenuScreen()
        {
            this.menuImages = new Image[5];
            for (int i = 0; i < 5; i++)
            {
                menuImages[i] = new Image();
            }
            this.timePassed = new TimeSpan();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            menuImages[0].Path = "buttons";
            menuImages[1].Path = "new-game";
            menuImages[2].Path = "options";
            menuImages[3].Path = "credits";
            menuImages[4].Path = "quit";
            currentImage = menuImages[0];
            foreach (var img in menuImages)
            {
                img.LoadContent();
            }
            currentImage.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            foreach (var img in menuImages)
            {
                img.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            if (!hasAction)
            {
                if (mouse.Position.X > 305 && mouse.Position.X < 505
                    && mouse.Position.Y > 200 && mouse.Position.Y < 240 && mouse.LeftButton == ButtonState.Pressed)
                {
                    currentImage = menuImages[1];
                    startNewGame = true;
                    hasAction = true;
                    timePassed = gameTime.TotalGameTime;
                }
                else if (mouse.Position.X > 305 && mouse.Position.X < 505
                         && mouse.Position.Y > 260 && mouse.Position.Y < 300 && mouse.LeftButton == ButtonState.Pressed)
                {
                    currentImage = menuImages[2];
                    openOptions = true;
                    hasAction = true;


                }
                else if (mouse.Position.X > 305 && mouse.Position.X < 505
                         && mouse.Position.Y > 320 && mouse.Position.Y < 360 && mouse.LeftButton == ButtonState.Pressed)
                {
                    currentImage = menuImages[3];
                    openCredits = true;
                    hasAction = true;

                }
                else if (mouse.Position.X > 305 && mouse.Position.X < 505
                         && mouse.Position.Y > 380 && mouse.Position.Y < 420 && mouse.LeftButton == ButtonState.Pressed)
                {
                    currentImage = menuImages[4];
                    Quit = true;
                    hasAction = true;

                }
                else
                {
                    currentImage = menuImages[0];
                }
            }
            else
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timePassed.TotalMilliseconds + 150)
                {
                    if (startNewGame)
                    {
                        ScreenManager.Manager.CurrentScreen = ScreenManager.Manager.GameScreen;
                        startNewGame = false;
                    }
                }
            }
            

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentImage.Draw(spriteBatch);
        }
    }
}