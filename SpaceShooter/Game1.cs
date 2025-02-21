using System;
using System.Collections.Generic;
using System.Linq;
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
    List<GoldCoin> goldCoins;
    private Texture2D goldCoinSprite;
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
        goldCoins = new List<GoldCoin>();
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

        goldCoinSprite = Content.Load<Texture2D>("coin");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        player.Update(Window);

        foreach (Enemy e in enemies.ToList())
        {
            if (e.IsAlive)
            {
                if (e.CheckCollision(player)) this.Exit();
                
                e.Update(Window);
            }
            else enemies.Remove(e);
        }

        Random random = new Random();
        int newCoin = random.Next(1, 200);
        if (newCoin == 1)
        {
            int rndX = random.Next(0, Window.ClientBounds.Width - goldCoinSprite.Width);
            int rndY = random.Next(0, Window.ClientBounds.Height - goldCoinSprite.Height);
            
            goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
        }

        foreach (GoldCoin gc in goldCoins.ToList())
        {
            gc.Update(gameTime);

            if (gc.CheckCollision(player))
            {
                goldCoins.Remove(gc);
                player.Points++;
            }
            else
            {
                goldCoins.Remove(gc);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        player.Draw(_spriteBatch);
        
        foreach (Enemy e in enemies) e.Draw(_spriteBatch);

        foreach (GoldCoin gc in goldCoins) gc.Draw(_spriteBatch);
        _spriteBatch.DrawString(arial32, "Points: " + player.Points, new Vector2(0, 0), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}