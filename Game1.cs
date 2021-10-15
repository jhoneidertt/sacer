using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceWar
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont vida;
        private Texture2D _destello;
        private Texture2D _background;
        private Texture2D _kill;
        private SpriteFont over;
        private int cant;
        private int xx;
        private int yy;
        private int x;
        private int y;


        Player myPlayer;
        Enemy myenemy;
        Enemys enemys;
        enemy1 enemy;

        Song song1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            myPlayer = new Player(this, new Point(0, 720));
            myenemy = new Enemy(this);
            enemys = new Enemys(this);
            enemy = new enemy1(this);

            cant = 10;
            xx = 800;
            yy = 300;
            x = 200;
            y = 200;
            base.Initialize();
        }

        protected override void LoadContent()
        {
           
            _destello = Content.Load<Texture2D>("destello");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            vida = this.Content.Load<SpriteFont>("lives");
            over = this.Content.Load<SpriteFont>("lives");
            _background = this.Content.Load<Texture2D>("background");
            song1 = this.Content.Load<Song>("Adventure-320bit");
            _kill = this.Content.Load<Texture2D>("black");
            MediaPlayer.Play(song1);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            myPlayer.Update(gameTime);
            myenemy.Update(gameTime);
            enemys.Update(gameTime);
            enemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int i = 0; i < 99; i++)
            {
                if (myPlayer.IsCrossVisible[i] == true)
                {
                    if (myPlayer.CrossRectangle1[i].Intersects(enemy.PositionRectangle))
                    {
                        myPlayer.IsCrossVisible[i] = false;
                        enemy.Isenemyvisible = false;
                        //cant -= 1;
                        _spriteBatch.Draw(_destello, new Vector2(enemy.PositionRectangle.X, myPlayer.PositionRectangle.X), Color.White);
                    }
                }
            }
            for (int i = 0; i < 99; i++)
            {
                if (myPlayer.IsCrossVisible[i] == true)
                {
                    if (myPlayer.CrossRectangle1[i].Intersects(myenemy.PositionRectangle))
                    {
                        myPlayer.IsCrossVisible[i] = false;
                        myenemy.Isenemyvisible = false;
                        //cant -= 1;
                        _spriteBatch.Draw(_destello, new Vector2(enemy.PositionRectangle.X, myPlayer.PositionRectangle.X), Color.White);
                    }
                }
            }




            for (int i = 0; i < 99; i++)
            {
                if (myPlayer.IsCrossVisible[i] == true)
                {
                    if (myPlayer.CrossRectangle1[i].Intersects(enemys.PositionRectangle))
                    {
                        myPlayer.IsCrossVisible[i] = false;
                        enemys.Isenemyvisible = false;
                        //cant -= 1;
                        _spriteBatch.Draw(_destello, new Vector2(enemys.PositionRectangle.X, myPlayer.PositionRectangle.X), Color.White);
                    }
                }
            }




            if (cant > 0)
            {
                _spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
                myPlayer.Draw(gameTime, _spriteBatch);
                enemys.Draw(gameTime, _spriteBatch);
                myenemy.Draw(gameTime, _spriteBatch);
                enemy.Draw(gameTime, _spriteBatch);
            }
            if (myPlayer.PositionRectangle.Intersects(enemy.PositionRectangle))
            {
                enemy.Isenemyvisible = false; 
                cant -= 1;
                _spriteBatch.Draw(_destello, new Vector2(enemy.PositionRectangle.X,myPlayer.PositionRectangle.X), Color.White);
            }

            if (myPlayer.PositionRectangle.Intersects(enemys.PositionRectangle))
            {
                enemys.Isenemyvisible = false;
                cant -= 1;
                _spriteBatch.Draw(_destello, new Vector2(enemys.PositionRectangle.X, myPlayer.PositionRectangle.X), Color.White);
            }

            if (myPlayer.PositionRectangle.Intersects(myenemy.PositionRectangle))
            {
                myenemy.Isenemyvisible = false;
                cant -= 1;
                _spriteBatch.Draw(_destello, new Vector2(myenemy.PositionRectangle.X, myPlayer.PositionRectangle.X), Color.White);
            }
            if (cant <= 0)
            {
                _spriteBatch.Draw(_kill, new Vector2(0,0), Color.White);
                _spriteBatch.DrawString(over, "GAME OVER", new Vector2(xx, yy), Color.DarkRed);
                cant = 0;
                MediaPlayer.Stop();
            }
            string cantString = "" + cant;
            _spriteBatch.DrawString(vida, "VIDAS " + cantString, new Vector2(x, y), Color.DarkRed);          
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
