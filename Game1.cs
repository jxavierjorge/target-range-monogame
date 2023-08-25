using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Threading.Tasks.Sources;

namespace DrawingShapesProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _CrosshairSprite;
        private Texture2D _SkyTexture;
        private Texture2D _TargetSprite;
        private SpriteFont _Font;
        private Song _MySong;
        private SoundEffect _HitSound;
        public int score = 0;
        private string _Name = "Tutorial";

        Texture2D whiteRect;


        Vector2 _TargetPosition = new Vector2(300, 300);
        Vector2 _CrosshairPos = new Vector2(0, 0);
        const int targetRadius = 45;
        Color _HitColor = Color.White;

        MouseState _MouseState;
        bool mReleased = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Window.Title = _Name;
            IsMouseVisible = false;
            _graphics.PreferredBackBufferHeight = 720/2;
            _graphics.PreferredBackBufferWidth = 1280/2;
            _graphics.ApplyChanges();
            MediaPlayer.Play(_MySong);
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _CrosshairSprite = Content.Load<Texture2D>("crosshairs");
            _SkyTexture = Content.Load<Texture2D>("sky");
            _TargetSprite = Content.Load<Texture2D>("target");
            _Font = Content.Load<SpriteFont>("galleryFont");
            _MySong = Content.Load<Song>("Airship Serenity");
            _HitSound = Content.Load<SoundEffect>("Hit");
            whiteRect = new Texture2D(_graphics.GraphicsDevice, 1,1);
            whiteRect.SetData(new[] { Color.White});



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _MouseState = Mouse.GetState();

            _CrosshairPos = _MouseState.Position.ToVector2();

            if (_MouseState.LeftButton == ButtonState.Pressed && mReleased)
            {
                float mouseTargetDist = Vector2.Distance(_TargetPosition, _CrosshairPos);
                if (mouseTargetDist < targetRadius)
                {
                    _HitColor = Color.IndianRed;
                    _HitSound.Play();

                    score++;
                    Random random = new Random();
                    _TargetPosition =new Vector2(random.Next(0+targetRadius,1280/2-targetRadius), random.Next(0+targetRadius,720/2-targetRadius));
                }
                mReleased = false;
            }

            if (_MouseState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
                _HitColor = Color.White;
            }


            base.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_SkyTexture, new Vector2(0 ,0), Color.White);
            _spriteBatch.Draw(_TargetSprite, _TargetPosition - new Vector2(_TargetSprite.Width / 2,
                _TargetSprite.Height / 2), _HitColor);
            _spriteBatch.Draw(_CrosshairSprite, _CrosshairPos-new Vector2(_CrosshairSprite.Width/2,
                _CrosshairSprite.Height/2), Color.White);
            _spriteBatch.DrawString(_Font, $"Score = {score}", new Vector2(0,0), Color.White);
            _spriteBatch.Draw(whiteRect, new Rectangle(10, 20, 80, 30),
           Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}