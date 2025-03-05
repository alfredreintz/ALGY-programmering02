using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceShooter;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        GameElements.currentState = GameElements.State.Menu;
        GameElements.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice); 
        GameElements.LoadContent(Content, Window);
    }

    protected override void UnloadContent()
    {
    }
    

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIl,ö.ndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        player.Update(Window, gameTime);

        foreach (Enemy e in enemies.ToList())
        {
            foreach (Bullet b in player.Bullets)
            {
                if (e.CheckCollision(b))
                {
                    e.IsAlive = false;
                }
            }

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
            if (gc.IsAlive)
            {
                gc.Update(gameTime);

                if (gc.CheckCollision(player))
                {
                    goldCoins.Remove(gc);
                    player.Points++;
                }
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