using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceWar
{
    /// <summary>
    /// the demons hell have come out
    /// </summary>
    class Enemys
    {

        bool isenemyvisible;

        private byte framesCounter;
        /// <summary>
        /// a reference to the game that will contain the Enemy
        /// </summary>
        Game1 root;
        /// <summary>
        /// the current position of the player
        /// </summary>
        Point position;
        /// <summary>
        /// the name of the sprite image
        /// </summary>
        const String imageName = "d1";
        byte imagename;
        /// <summary>
        /// an image texture for the Enemy sprite
        /// </summary>
        Texture2D spriteImageInMemory;
        /// <summary>
        /// represents the number of pixels to move the Enemy in the Y axis
        /// </summary>
        Point velocity;
        /// <summary>
        /// this is a variable that indicates the scale size of the Enemy.
        /// e.g. a size of 30 will tell us that
        /// the height and the width of the player is 30
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// array for walk
        /// </summary>
        Texture2D[] walkingTextures;
        /// <summary>
        /// initialize a Enemy
        /// </summary>
        /// <param name="_root">this is the class that manipulates the game loop</param>
        public Enemys(Game1 _root)
        {
            //initialize a values
            this.root = _root;
            Size = 150;
            velocity = new Point(60, 0);
            position = new Point(this.root.Window.ClientBounds.Width - Size, 750);
            walkingTextures = new Texture2D[7];
            imagename = 0;
            framesCounter = 0;
            Isenemyvisible = true;

            this.LoadContent();
        }
        /// <summary>
        /// initialize
        /// </summary>
        /// <param name="_root">this is the class that manipulates the game loop</param>
        /// <param name="_position">the initial position of the Enemy </param>
        public Enemys(Game1 _root, Point _position)
        {
            //initialize values
            this.root = _root;
            Size =150;
            velocity = new Point(60, 0);
            position = _position;

            walkingTextures = new Texture2D[7];
            imagename = 0;
            framesCounter = 0;
            Isenemyvisible = true;
            this.LoadContent();
        }
        /// <summary>
        /// method to load external content
        /// </summary>
        private void LoadContent()
        {
            for (int i = 0; i < 6; i++)
            {
                walkingTextures[i] = this.root.Content.Load<Texture2D>("d" + (i + 1));
            }
        }
        public void Update(GameTime gameTime)
        {
            if (isenemyvisible == true)
            {


                framesCounter++;
                if (framesCounter > 7)
                {
                    framesCounter = 0;
                    if (this.position.X > 0)
                    {
                        imagename++;
                        if (imagename <= 5)
                        {
                            return;
                        }
                        imagename = 0;
                        position = position - velocity;
                    }
                    else
                    {
                        position.X = this.root.Window.ClientBounds.Width ;
                    }
                }
            }
            if (isenemyvisible == false)
            {
                //velocity = new Point(30, 0) + velocity;
                isenemyvisible = true;
                position.X = this.root.Window.ClientBounds.Width ;
            }


        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //use the sprite batch to draw 
            if (Isenemyvisible == true)
            {
                spriteBatch.Draw(walkingTextures[imagename], PositionRectangle, Color.White);

            }

        }
        /// <summary>
        /// the propierly scaled position rectangle for the Enemy sprite
        /// </summary>
        public Rectangle PositionRectangle
        {

            get
            {
                return new Rectangle(position, new Point(Size));
            }
        }

        public bool Isenemyvisible { get => isenemyvisible; set => isenemyvisible = value; }
    }
}
