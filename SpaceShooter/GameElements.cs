using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace SpaceShooter;

static class GameElements
{
    private static Texture2D menuSprite;
    private static Vector2 menuPos;
    private static Menu menu;
    private static Player player;
    private static List<Enemy> enemies;
    private static List<GoldCoin> goldCoins;
    private static Texture2D goldCoinSprite;
    private static SpriteFont arial32;
    private static Background Background;

    public enum State
    {
        Menu,
        Run,
        HighScore,
        Quit
    };

    public static State currentState;

    public static void Initialize()
    {
        goldCoins = new List<GoldCoin>();
    }

    public static void LoadContent(ContentManager content, GameWindow window)
    {
        // TODO: use this.Content to load your game content here
        menu = new Menu((int)State.Menu);
        menu.AddItem(content.Load<Texture2D>("menu/start"), (int)State.Run);
        menu.AddItem(content.Load<Texture2D>("menu/highscore"), (int)State.HighScore);
        menu.AddItem(content.Load<Texture2D>("menu/exit"), (int)State.Quit);

        menuSprite = content.Load<Texture2D>("menu");
        menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
        menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;

        player = new Player(content.Load<Texture2D>("ship"), 280, 400, 2.5f, 4.5f, content.Load<Texture2D>("bullet"));
        
        enemies = new List<Enemy>();
        Random random = new Random();
        Texture2D tmpSprite = content.Load<Texture2D>("mine");
        for (int i = 0; i < 5; i++)
        {
            int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
            int rndY = random.Next(0, window.ClientBounds.Height / 2);

            Enemy temp = new Mine(tmpSprite, rndX, rndY);

            enemies.Add(temp);
        }

        tmpSprite = content.Load<Texture2D>("tripod");
        for (int i = 0; i < 5; i++)
        {
            int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
            int rndY = random.Next(0, window.ClientBounds.Height / 2);

            Enemy temp = new Tripod(tmpSprite, rndX, rndY);

            enemies.Add(temp);
        }
        
        arial32 = content.Load<SpriteFont>("fonts/arial32");

        goldCoinSprite = content.Load<Texture2D>("coin");

        Background = new Background(content.Load<Texture2D>("background"), window);
    }

    public static State MenuUpdate(GameTime gameTime)
    {
        return (State)menu.Update(gameTime);
    }

    public static void MenuDraw(SpriteBatch spriteBatch)
    {
        Background.Draw(spriteBatch);
        menu.Draw(spriteBatch);
    }

    public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
    {
        Background.Update(window);
        
        player.Update(window, gameTime);

        foreach (Enemy e in enemies.ToList())
        {
            foreach (Bullet b in player.Bullets)
            {
                if (e.CheckCollision(b))
                {
                    e.IsAlive = false;
                    player.Points++;
                }
            }

            if (e.IsAlive)
            {
                if (e.CheckCollision(player)) player.IsAlive = false;

                e.Update(window);
            }
            else enemies.Remove(e);
        }

        Random random = new Random();
        int newCoin = random.Next(1, 200);
        if (newCoin == 1)
        {
            int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
            int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);

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

        if (!player.IsAlive)
        {
            Reset(window, content);
            return State.Menu;
        }

        return State.Run;
    }

    public static void RunDraw(SpriteBatch spriteBatch)
    {
        Background.Draw(spriteBatch);
        player.Draw(spriteBatch);
        foreach (Enemy e in enemies) e.Draw(spriteBatch);
        foreach (GoldCoin gc in goldCoins) gc.Draw(spriteBatch);
        spriteBatch.DrawString(arial32, "Points: " + player.Points, new Vector2(0, 0), Color.White);
    }

    public static State HighScoreUpdate()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Escape)) return State.Menu;
        return State.HighScore;
    }

    public static void HighScoreDraw(SpriteBatch spriteBatch)
    {
        
    }

    private static void Reset(GameWindow window, ContentManager content)
    {
        player.Reset(380, 400, 2.5f, 4.5f);
        
        enemies.Clear();
        Random random = new Random();

        Texture2D tmpSprite = content.Load<Texture2D>("mine");

        for (int i = 0; i < 5; i++)
        {
            int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
            int rndY = random.Next(0, window.ClientBounds.Height / 2);
            Mine temp = new Mine(tmpSprite, rndX, rndY);
            enemies.Add(temp);
        }
        tmpSprite = content.Load<Texture2D>("tripod");
        for (int i = 0; i < 5; i++)
        {
            int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
            int rndY = random.Next(0, window.ClientBounds.Height / 2);
            Tripod temp = new Tripod(tmpSprite, rndX, rndY);
            enemies.Add(temp);
        }
    }
}