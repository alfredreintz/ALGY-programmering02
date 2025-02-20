using System;
using System.Collections.Generic;
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
    private Player player;
    private List<Enemy> enemies;
    
    private SpriteFont arial32;
    
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
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        arial32 = Content.Load<SpriteFont>("fonts/arial32");
        
        player = new Player(Content.Load<Texture2D>("ship"), 380, 400, 2.5f, 4.5f);

        enemies = new List<Enemy>();
        Random random = new Random();
        Texture2D tmpSprite = Content.Load<Texture2D>("mine");
        for (int i = 0; i < 10; i++)
        {
            int rndX = random.Next(0, Window.ClientBounds.Width - tmpSprite.Width);
            int rndY = random.Next(0, Window.ClientBounds.Height / 2);

            Enemy temp = new Enemy(tmpSprite, rndX, rndY);
            
            enemies.Add(temp);
        }

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        player.Update(Window);
        
        foreach (Enemy e in enemies) e.Update(Window);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.DrawString(arial32, "testutskrift", new Vector2(0, 0), Color.White);
        player.Draw(_spriteBatch);
        foreach (Enemy e in enemies) e.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}