using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace slutprojekt;

static class GameElements
{
    private static Texture2D menuSprite;
    private static Vector2 menuPos;
    private static Menu menu;
    private static Player player;
    private static List<Texture2D> characterTexturesLeft = new List<Texture2D>();
    private static List<Texture2D> characterTexturesRight = new List<Texture2D>();
    private static Raft raft;
    private static GameObject background;
    private static Sea sea;
    private static List<Enemy> enemies;
    
    public enum State
    {
        Menu,
        Run,
        PrintHighScore,
        EnterHighScore,
        About,
        Quit
    };
    
    public static State currentState;

    public static void Initialize()
    {
    }

    public static void LoadContent(ContentManager content, GameWindow window)
    {
        // TODO: use this.Content to load your game content here
        
        menu = new Menu((int)State.Menu);
        menu.AddItem(content.Load<Texture2D>("menu/start"), (int)State.Run);
        // menu.AddItem(content.Load<Texture2D>("menu/highscore"), (int)State.PrintHighScore);
        menu.AddItem(content.Load<Texture2D>("menu/exit"), (int)State.Quit);

        menuSprite = content.Load<Texture2D>("menu");
        menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
        menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;

        player = new Player(content.Load<Texture2D>("character/characterWalkLeft1"), 320, -1000, 9f, 20f, 70f, content.Load<Texture2D>("bullet"));
        raft = new Raft(content.Load<Texture2D>("raft" ), window.ClientBounds.Width / 2 - 616 / 2, window.ClientBounds.Height - 150, 0, 0);
        background = new GameObject(content.Load<Texture2D>("sky"), 0, -50);
        sea = new Sea(content.Load<Texture2D>("sea" ), 0, window.ClientBounds.Height - 80, 0, 0);
        enemies = new List<Enemy>();
        
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft1"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft2"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft3"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft4"));
        
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight1"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight2"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight3"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight4"));

        Random rand = new Random();
        Texture2D tmpSprite = content.Load<Texture2D>("birds/pinkBird");
        int rndX;
        float constY = 460f;
        float tmpspeedX = 7f;
        
        for (int i = 0; i < 1; i++)
        {
            rndX = rand.Next(-2000, 500);

            Enemy temp = new horizontalBird(tmpSprite, rndX, constY, tmpspeedX, 0);
            enemies.Add(temp);
        }
    }

    public static void UnloadContent()
    {
    }

    public static State MenuUpdate(GameTime gameTime)
    {
        return (State)menu.Update(gameTime);
    }

    public static void MenuDraw(SpriteBatch spriteBatch)
    {
        menu.Draw(spriteBatch);
    }

    public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
    {
        player.Update(window, gameTime);
        player.checkTouchable(gameTime, raft);
        
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
        
        if (player.Y > window.ClientBounds.Height - sea.Height)
        {
            player.IsAlive = false;
        }

        if (!player.IsAlive)
        {
            Reset(window, content);
            return State.Menu;
        }
        
        return State.Run;
    }
    
    public static State AboutUpdate()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Escape)) return State.Menu;
        
        return State.About;
    }

    public static void AboutDraw(SpriteBatch spriteBatch)
    {
    }

    public static void RunDraw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        background.Draw(spriteBatch);
        player.Draw(spriteBatch);
        player.Walkcycle(gameTime, characterTexturesLeft, characterTexturesRight);
        raft.Draw(spriteBatch);
        foreach (Enemy e in enemies) e.Draw(spriteBatch);
        sea.Draw(spriteBatch);
    }

    private static void Reset(GameWindow window, ContentManager content)
    {
        player.Reset(320, -1000, 9f, 20f);
    }
}