using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace planets;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D basePlanetTexture;
    private Vector2 basePlanetVector;
    private Vector2 basePlanetSpeed;
    private float gravityConstant;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        // Ändrar skärmstorleken till helskärm
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1700;
        _graphics.PreferredBackBufferHeight = 1000;
        _graphics.ApplyChanges();

        basePlanetSpeed.Y = 0;
        gravityConstant = 1.2f;
        
        base.Initialize();    
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        basePlanetTexture = Content.Load<Texture2D>("basePlanet");
        
        basePlanetVector.X = Window.ClientBounds.Width / 2 - basePlanetTexture.Width / 2;
        basePlanetVector.Y = 0;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        basePlanetSpeed.Y += gravityConstant;
        basePlanetVector.Y += basePlanetSpeed.Y;

        if (basePlanetVector.Y > Window.ClientBounds.Height - basePlanetTexture.Height)
        {
            basePlanetSpeed.Y *= -0.95f;
            basePlanetVector.Y = Window.ClientBounds.Height - basePlanetTexture.Height;
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Sätter bakgrundsfärg med rgb
        GraphicsDevice.Clear(new Color(31, 39, 56 ));

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(basePlanetTexture, basePlanetVector, Color.White);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}