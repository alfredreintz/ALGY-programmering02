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
    // Medlemsvariabler
    private static Texture2D menuSprite;
    private static Vector2 menuPos;
    private static Menu menu;
    private static Player player;
    private static List<Texture2D> characterTexturesLeft = new List<Texture2D>();
    private static List<Texture2D> characterTexturesRight = new List<Texture2D>();
    private static Raft raft;
    private static GameObject background;
    private static Sea sea;
    private static Texture2D tmpSprite;
    private static int randX1;
    private static int randX2;
    private static int randY1;
    private static int randY2;
    private static float randSpeedX;
    private static float randSpeedY;
    private static int randPos;
    private static List<Enemy> enemies;
    private static Random rand = new Random();
    private static int spawnEnemy;

    // State
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
        // I denna metod brukar en stor del av den förberedande logiken att placeras
        // ... men eftersom nästan alla logik har tillhörande texturer ligger det i LoadContent
    }

    public static void LoadContent(ContentManager content, GameWindow window)
    {
        // TODO: use this.Content to load your game content here

        // Laddar in meny
        menu = new Menu((int)State.Menu);
        menu.AddItem(content.Load<Texture2D>("menu/start"), (int)State.Run);
        // menu.AddItem(content.Load<Texture2D>("menu/highscore"), (int)State.PrintHighScore);
        menu.AddItem(content.Load<Texture2D>("menu/exit"), (int)State.Quit);

        menuSprite = content.Load<Texture2D>("menu");
        menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
        menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;

        // Laddar in spelarobjekten
        player = new Player(content.Load<Texture2D>("character/characterWalkLeft1"), 320, -1000, 9f, 20f, 70f,
            content.Load<Texture2D>("bullet"));
        raft = new Raft(content.Load<Texture2D>("raft"), window.ClientBounds.Width / 2 - 616 / 2,
            window.ClientBounds.Height - 150, 0, 0);
        background = new GameObject(content.Load<Texture2D>("sky"), 0, -50);
        sea = new Sea(content.Load<Texture2D>("sea"), 0, window.ClientBounds.Height - 80, 0, 0);
        enemies = new List<Enemy>();

        // Lägger till walkcycle-sprites i lista
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft1"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft2"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft3"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft4"));

        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight1"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight2"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight3"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight4"));

        // -------------------------------------------------------------------------------------------
        /*
           Denna kod upprepas och är väldigt lik koden i RunUpdate och det beror på att jag under
           under projektets gång har valt att försöka styra alla "riktiga" värden på variabler från
           denna klass. Jag vill exempelvis inte att objektens hastigheter bestäms i dess klasser.
           En del sådan logik ligger såklart i klasserna men i och med att detta projekt inte har
           haft någon tydlig mall för hur detta ska läggas upp har jag valt att göra såhär.
           Detta resulterar alltså i att koden kan blir repititiv,
        */
        // -------------------------------------------------------------------------------------------

        // Laddar in sprite
        tmpSprite = content.Load<Texture2D>("birds/pinkBird");

        // Skapar raandom positioner
        randX1 = rand.Next(-2000, 500);
        randX2 = rand.Next(-2000, 500);
        randY1 = rand.Next(450, 515);

        randSpeedX = 7f * rand.Next(3, 20) / 10;

        randPos = rand.Next(1, 3);

        if (randPos == 0)
        {
            // Skapar och lägger till fågel
            Enemy temp = new HBird(tmpSprite, randX1, randY1, randSpeedX, 0, 7f);
            enemies.Add(temp);
        }
        else
        {
            Enemy temp = new HBird(tmpSprite, randX2, randY1, randSpeedX, 0, 7f);
            enemies.Add(temp);
        }
    }

    /// <summary>
    /// Uppdaterar logik när spelet stängs av
    /// </summary>
    public static void UnloadContent()
    {
    }

    /// <summary>
    /// Uppdaterar menyn
    /// </summary>
    /// <param name="gameTime">Speltiden</param>
    /// <returns>State</returns>
    public static State MenuUpdate(GameTime gameTime)
    {
        return (State)menu.Update(gameTime);
    }

    /// <summary>
    /// Ritar ut menym
    /// </summary>
    /// <param name="spriteBatch">Möjliggjör för att kunna rita</param>
    public static void MenuDraw(SpriteBatch spriteBatch)
    {
        menu.Draw(spriteBatch);
    }

    /// <summary>
    /// Uppdaterar logik när spelet körs
    /// </summary>
    /// <param name="content">Allt content</param>
    /// <param name="window">Fönstret där spelet körs</param>
    /// <param name="gameTime">Speltiden</param>
    /// <returns>State</returns>
    public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
    {
        player.Update(window, gameTime);
        player.checkTouchable(gameTime, raft);

        spawnEnemy = rand.Next(1, 1001);

        // Loopar genom alla enemies
        foreach (Enemy e in enemies.ToList())
        {
            // Kollar om något skott träffat en enemie
            foreach (Bullet b in player.Bullets)
            {
                // Om en enemie blir träffad
                if (e.CheckCollision(b))
                {
                    // Enemien dör
                    e.IsAlive = false;
                    spawnEnemy = 1;
                    player.Points++;
                }
            }

            // Om enemien lever
            if (e.IsAlive)
            {
                // Kollar spelaren kolliderar med den, isåfall dör spelaren
                if (e.CheckCollision(player)) player.IsAlive = false;

                e.Update(window);
                // Anropar metoden med imparametrar för objekt för att enemies. ska kunna positioneras utifrån objektet
                e.setRandPosition(window, 0, window.ClientBounds.Width, raft.Y);
            }
            // Tar bort enemien
            else enemies.Remove(e);
        }

        if (spawnEnemy == 1)
        {
            // Skapa en ny fågel med ny-slumpade värden
            randX1 = rand.Next(-2000, -500);
            randX2 = rand.Next(window.ClientBounds.Width + 500, window.ClientBounds.Width + 2000);
            randY1 = rand.Next(450, 515);

            randPos = rand.Next(1, 3);

            randSpeedX = 7f * rand.Next(3, 20) / 10;
            
            tmpSprite = content.Load<Texture2D>("birds/pinkBird");

            if (randPos == 0)
            {
                Enemy temp = new HBird(tmpSprite, randX1, randY1, 7f, 0, 7f);
                enemies.Add(temp);
            }
            else
            {
                Enemy temp = new HBird(tmpSprite, randX2, randY1, 7f, 0, 7f);
                enemies.Add(temp);
            }
        }
        else if (spawnEnemy == 2)
        {
            randY1 = rand.Next(-2000, -500);
            randX1 = rand.Next(0, window.ClientBounds.Width);

            randSpeedY = 7f * rand.Next(10, 30) / 10;
            
            tmpSprite = content.Load<Texture2D>("birds/blueBird");

            Enemy temp = new VBird(tmpSprite, randX1, randY1, 0, randSpeedY, 7f);
            enemies.Add(temp);
        }
        else if (spawnEnemy == 3 || spawnEnemy == 4 || spawnEnemy == 5)
        {
            randX1 = rand.Next(-2000, -500);
            randY1 = rand.Next(-2000, -500);
            // int startX2 = rand.Next(window.ClientBounds.Width + 500, window.ClientBounds.Width + 2000);
            
            randSpeedX = 7f * rand.Next(3, 20) / 10;
            randSpeedY = 7f * rand.Next(3, 30) / 10;
            
            tmpSprite = content.Load<Texture2D>("birds/greenBird");

            Enemy temp = new DBird(tmpSprite, randX1, randY1, randSpeedX, randSpeedY, 7f);
            enemies.Add(temp);
        }

        // Om spelaren hamnar under hasnivån
        if (player.Y > window.ClientBounds.Height - sea.Height) player.IsAlive = false;

        // Om spelaren är död
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

    /// <summary>
    /// Ritar ut när spelet körs
    /// </summary>
    /// <param name="spriteBatch">Möjliggjör för att kunna rita</param>
    /// <param name="gameTime">Speltiden</param>
    public static void RunDraw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Ritar ut alla texturer genom objektens metoder
        background.Draw(spriteBatch);
        player.Draw(spriteBatch);
        player.Walkcycle(gameTime, characterTexturesLeft, characterTexturesRight);
        raft.Draw(spriteBatch);
        // Loopar igenom varje enemy
        foreach (Enemy e in enemies) e.Draw(spriteBatch);
        sea.Draw(spriteBatch);
    }

    /// <summary>
    /// Resettar spelet
    /// </summary>
    /// <param name="window">Spelarfönstret</param>
    /// <param name="content">Content i spelet</param>
    private static void Reset(GameWindow window, ContentManager content)
    {
        player.Reset(320, -1000, 9f, 20f);
    }
}