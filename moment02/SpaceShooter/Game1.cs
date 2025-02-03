using System;
using System.Numerics;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceShooter;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D ship_texture;
    Vector2 ship_vector;
    Vector2 ship_speed;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        ship_vector.X = 380;
        ship_vector.Y = 400;

        ship_speed.X = 2.5f;
        ship_speed.Y = 2.5f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ship_texture = Content.Load<Texture2D>("ship");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        KeyboardState keyboardState = Keyboard.GetState();

        if (ship_vector.X <= Window.ClientBounds.Width - ship_texture.Width && ship_vector.X >= 0)
        {
            if (keyboardState.IsKeyDown(Keys.Right)) ship_vector.X += ship_speed.X;
            if (keyboardState.IsKeyDown(Keys.Left)) ship_vector.X -= ship_speed.X;
        }

        if (ship_vector.Y <= Window.ClientBounds.Height - ship_texture.Height && ship_vector.Y >= 0)
        {
            if (keyboardState.IsKeyDown(Keys.Down)) ship_vector.Y += ship_speed.Y;
            if (keyboardState.IsKeyDown(Keys.Up)) ship_vector.Y -= ship_speed.Y;
        }

        if (ship_vector.X < 0) ship_vector.X = 0;
        if (ship_vector.X > Window.ClientBounds.Width - ship_texture.Width) ship_vector.X = Window.ClientBounds.Width - ship_texture.Width;
        if (ship_vector.Y < 0) ship_vector.Y = 0;
        if (ship_vector.Y > Window.ClientBounds.Height - ship_texture.Height) ship_vector.Y = Window.ClientBounds.Height - ship_texture.Height;


        /*ship_vector.X += ship_speed.X;

        if (ship_vector.X > Window.ClientBounds.Width - ship_texture.Width || ship_vector.X < 0)
        {
            ship_speed.X *= -1;
        }

        ship_vector.Y += ship_speed.Y;

        if (ship_vector.Y > Window.ClientBounds.Height - ship_texture.Height || ship_vector.Y < 0)
        {
            ship_speed.Y *= -1;
        }*/

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(ship_texture, ship_vector, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}