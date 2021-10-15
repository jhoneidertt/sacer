using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace SpaceWar
{
    /// <summary>
    /// the player , controlled by the keyboard
    /// </summary>
    class Player
    {

        SoundEffect CrossAudioEffect;



        Texture2D[] CrossTexture;
        Rectangle[] CrossRectangle;
        bool[] isCrossVisible;
        KeyboardState previousState;


        int CrossCount;
        const int numberOfCross = 100;






        /// <summary>
        /// a reference to the game that will contain the player
        /// </summary>
        Game1 root;

        /// <summary>
        /// the current position of the player
        /// </summary>
        Point position;

        /// <summary>
        /// the name of the sprite image
        /// </summary>
        const String imageName = "sacer";
        const String imagename = "cruz";
        //byte imagename;
        /// <summary>
        /// an image texture for the player sprite
        /// </summary>
        Texture2D spriteImageInMemory;

        /// <summary>
        /// represents the number of pixels to move the player in the Y axis
        /// </summary>
        Point velocity;

        /// <summary>
        /// to save the previos state of the keyboard to avoid undesired keyboard 
        /// </summary>
        KeyboardState previousKeyboardState;

        /// <summary>
        /// this is a variable that indicates the scale size of the player.
        /// e.g. a size of 30 will tell us that
        /// the height and the width of the player is 30
        /// </summary>
        public int Size { get; set; }


        /// <summary>
        /// the propierly scaled position rectangle for the player sprite
        /// </summary>
        public Rectangle PositionRectangle
        {
            get
            {
                return new Rectangle(position, new Point(Size));
                //return new Rectangle(position,x,position y,size,size);
            }
        }

        public Rectangle[] CrossRectangle1 { get => CrossRectangle; set => CrossRectangle = value; }
        public bool[] IsCrossVisible { get => isCrossVisible; set => isCrossVisible = value; }




        /// <summary>
        /// initialize
        /// </summary>
        /// <param name="_root">this is the class that manipulates the game loop</param>
        public Player(Game1 _root) 
        {
            //initialize a player
            this.root = _root;
            Size=150;
            velocity = new Point(10, 0);
            position = new Point(0, 700);
            
            previousKeyboardState = Keyboard.GetState();
       




            CrossRectangle1 = new Rectangle[numberOfCross];
            CrossTexture = new Texture2D[numberOfCross];
            IsCrossVisible = new bool[numberOfCross];
            CrossCount = 0;
            for (int i = 0; i < numberOfCross; i++)
            {
                CrossRectangle1[i] = new Rectangle(0, 0, 30, 30);
            }

            this.LoadContent();
        }
        /// <summary>
        /// initialize
        /// </summary>
        /// <param name="_root">this is the class that manipulates the game loop</param>
        /// <param name="_position">the initial position of the player </param>
        public Player(Game1 _root,Point _position)
        {
            //initialize values
            this.root = _root;
            Size = 150;
            velocity = new Point(4, 0);
            position = _position;        
            previousKeyboardState = Keyboard.GetState();
         
            //initialize the  code of the power of the cross /rectangle code /number cross /arrays of cross
            CrossRectangle1 = new Rectangle[numberOfCross];
            CrossTexture = new Texture2D[numberOfCross];
            IsCrossVisible = new bool[numberOfCross];
            CrossCount = 0;
            for (int i = 0; i < numberOfCross; i++)
            {
                CrossRectangle1[i] = new Rectangle(0, 0, 140, 140);
            }

            this.LoadContent();
        }


        /// <summary>
        /// method to load external content
        /// </summary>
        private void LoadContent()
        {
            CrossAudioEffect = this.root.Content.Load<SoundEffect>("soundCross");
            spriteImageInMemory = this.root.Content.Load<Texture2D>(imageName);
            //array for make new cross 
            for (int i = 0; i < numberOfCross; i++)
            {
                CrossTexture[i] = this.root.Content.Load<Texture2D>(imagename);
            }
        }

        /// <summary>
        /// this method will help us to detct what kwy or keys have benn pressed and actuall
        /// </summary>
        /// <param name="currentkeyboardState">the current state of the keyboard</param>
        private void HandInput(KeyboardState currentkeyboardState)
        {
            if (currentkeyboardState.IsKeyDown(Keys.Left))
            {
                if (position.Y > 0)
                {
                    position = position - velocity;
                }
            }


            if (currentkeyboardState.IsKeyDown(Keys.Right))
            {
                if (position.Y < (root.Window.ClientBounds.Height - Size))
                {
                    position = position + velocity;
                }
            }




           
            //code to detect and control the amount of time the keyboard is pressed
            if (currentkeyboardState.IsKeyDown(Keys.Space) && !(previousState.IsKeyDown(Keys.Space)))
            {

                if (CrossCount < (numberOfCross - 1))
                {
                    IsCrossVisible[CrossCount] = true;
                    CrossRectangle1[CrossCount].X = position.X + (position.X / 20) - 10;
                    CrossRectangle1[CrossCount].Y = position.Y;                 
                    CrossCount++;
                    CrossAudioEffect.Play();

                }
            }


            for (int i = 0; i < numberOfCross; i++)
            {
             
                if (IsCrossVisible[i])
                {
                    CrossRectangle1[i].X += 10;
                }
                if (CrossRectangle1[i].X < 0)
                {
                    IsCrossVisible[i] = false;                  
                }
            }
            // TODO: Add your update logic here
            previousState = currentkeyboardState;
        }

        public void Update(GameTime gameTime)
        {
            //handle any movent input
            HandInput(Keyboard.GetState());
            //CrossRectangle1[1].X
 
        }

        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            //use the sprite batch to draw 
            spriteBatch.Draw(spriteImageInMemory, PositionRectangle,Color.White);
            for (int i = 0; i < numberOfCross; i++)
            {
                if (IsCrossVisible[i])
                {
                    spriteBatch.Draw(CrossTexture[i], CrossRectangle1[i], Color.White);
                }
            }
        } 
       


    }
}
