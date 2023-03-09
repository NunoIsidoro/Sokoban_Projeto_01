using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Sokoban_Projeto_01
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private int _linesNumber;
        private int _columnsNumber;
        private char[,] _level;
        private Texture2D _wall, _player, _dot, _box;
        private int _tileSize = 64;
        private Player _sokoban;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            LoadLevel("Content/Levels/level1.txt");

            // Change the size of the window to full screen
            _graphics.PreferredBackBufferWidth = _columnsNumber * _tileSize + 128;
            _graphics.PreferredBackBufferHeight = _linesNumber * _tileSize + 128;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Font");

            _player = Content.Load<Texture2D>("Character4");
            _dot = Content.Load<Texture2D>("EndPoint_Yellow");
            _box = Content.Load<Texture2D>("CrateDark_Brown");
            _wall = Content.Load<Texture2D>("WallRound_Black");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            // TODO: Add your drawing code

            _spriteBatch.Begin();


            Rectangle position = new Rectangle(0, 0, _tileSize, _tileSize);




            for (int x = 0; x < _columnsNumber; x++)
            {
                for (int y = 0; y < _linesNumber; y++)
                {
                    position.X = x * _tileSize + 64;
                    position.Y = y * _tileSize + 64;

                    switch (_level[x, y])
                    {
                        case 'X':
                            _spriteBatch.Draw(_wall, position, Color.White);
                            break;
                        case '.':
                            _spriteBatch.Draw(_dot, position, Color.White);
                            break;
                        case '#':
                            _spriteBatch.Draw(_box, position, Color.White);
                            break;
                    }
                }
            }

            position.X = _sokoban.Position.X * _tileSize; //posição do Player
            position.Y = _sokoban.Position.Y * _tileSize; //posição do Player
            _spriteBatch.Draw(_player, position, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // Function to read the file and return the number of lines and columns
        void LoadLevel(string path)
        {
            string[] lines = File.ReadAllLines(path);
            _linesNumber = lines.Length;
            _columnsNumber = lines[0].Length;
            _level = new char[_columnsNumber, _linesNumber];

            for (int x = 0; x < _columnsNumber; x++)
            {
                for (int y = 0; y < _linesNumber; y++)
                {
                    _level[x, y] = lines[y][x];

                    if (lines[y][x] == 'Y')
                    {
                        _sokoban = new Player(x, y);
                        _level[x, y] = ' '; // put a blank instead of the sokoban 'Y'
                    }
                    else
                    {
                        _level[x, y] = lines[y][x];
                    }
                }
            }
        }
    }
}