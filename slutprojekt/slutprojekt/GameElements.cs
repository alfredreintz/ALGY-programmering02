using System;
using System.IO;
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
    private static Enemy tempEnemy;
    private static int randX1;
    private static int randX2;
    private static int randY1;
    private static int randY2;
    private static float randSpeedX;
    private static float randSpeedY;
    private static int randPos;
    private static List<Enemy> enemies;
    private static Random rand;
    private static int spawnEnemy;
    private static double increaseChanceDeltaTime;
    private static int upperChanceLimit;
    private static SpriteFont arial32;
    private static SpriteFont myFont;
    private static HighScore highscore;
    private static int highscorePoints;
    private static Texture2D menuBackground;
    private static Texture2D highscoreBackground;
    private static int deltaPoints;
    private static string HSFileName;
    private static double pointDeltaTime;

    // State
    public enum State
    {
        Menu,
        Run,
        PrintHighScore,
        EnterHighScore,
        Quit
    };

    public static State currentState;

    public static void Initialize()
    {
        // I denna metod brukar en stor del av den förberedande logiken att placeras
        // ... men eftersom nästan alla logik har tillhörande texturer ligger det i LoadContent
        upperChanceLimit = 1002;
        highscore = new HighScore(5);
    }

    public static void LoadContent(ContentManager content, GameWindow window)
    {
        // TODO: use this.Content to load your game content here
        HSFileName = "HS1.txt";
        highscore.LoadFromFile(HSFileName);

        rand = new Random();

        // Laddar in meny
        menu = new Menu((int)State.Menu);
        
        menu.AddItem(content.Load<Texture2D>("menu/btnStartGame"), (int)State.Run, window);
        menu.AddItem(content.Load<Texture2D>("menu/btnViewHighscore"), (int)State.PrintHighScore, window);
        menu.AddItem(content.Load<Texture2D>("menu/btnQuitGame"), (int)State.Quit, window);
        
        arial32 = content.Load<SpriteFont>("fonts/arial32");

        // Laddar in spelarobjekten
        raft = new Raft(content.Load<Texture2D>("raft"), window.ClientBounds.Width / 2 - 616 / 2,
            window.ClientBounds.Height - 150, 0, 0);
        player = new Player(content.Load<Texture2D>("character/characterWalkLeft1"), rand.Next((int)raft.X, (int)(raft.X + raft.Width)), -1000, 9f, 20f, 70f,
            content.Load<Texture2D>("bullet"));
        background = new GameObject(content.Load<Texture2D>("sky"), 0, -50);
        sea = new Sea(content.Load<Texture2D>("sea"), 0, window.ClientBounds.Height - 80, 0, 0);
        enemies = new List<Enemy>();
        menuBackground = content.Load<Texture2D>("menuBackground");
        highscoreBackground = content.Load<Texture2D>("highscoreBackground");
    

        // Lägger till walkcycle-sprites i lista
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft1"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft2"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft3"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft4"));

        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight1"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight2"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight3"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight4"));
        

        // Laddar in sprite
        tmpSprite = content.Load<Texture2D>("birds/pinkBird");

        // Sätter in värdet som automatiskt triggar en tillrättalagd utplasering och hastighet hos objektet
        // Att sätta dessa variabler i construcorn är kanske inte så snyggt men det triggar objektet att placeras rätt
        // Alternativet är att göra samma sak i konstruktorn som i update-metoden men det blir väldigt repititivt
        // Detta fungerar och jag sparar väldigt mycket kod
        tempEnemy = new HBird(tmpSprite, 10000, 10000, 10000, 0, 7f);
        enemies.Add(tempEnemy);
        
        myFont = content.Load<SpriteFont>("fonts/arial32");
    }

    /// <summary>
    /// Uppdaterar logik när spelet stängs av
    /// </summary>
    public static void UnloadContent()
    {
        highscore.SaveToFile(HSFileName);
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
    /// <param name="spriteBatch">Inbyggt ritverktyg</param>
    public static void MenuDraw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(menuBackground, new Vector2(0, 0), Color.White);
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
        player.checkTouchable(raft);
        
        // Lägger till värde för varje loop mätt i sekunder
        pointDeltaTime += gameTime.ElapsedGameTime.TotalSeconds;
        increaseChanceDeltaTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (pointDeltaTime > 2)
        {
            // Lägger till poäng och resettar
            pointDeltaTime = 0;
            player.Points++;
        }

        if (increaseChanceDeltaTime > 0.1)
        {
            // Ökar chansen att enemies spawnar och resettar
            increaseChanceDeltaTime = 0;
            upperChanceLimit--;
        }

        spawnEnemy = rand.Next(1, upperChanceLimit);

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
                    spawnEnemy = rand.Next(1, 4);
                    player.Points++;
                    upperChanceLimit -= 4;
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

        // Kollar random-värde
        if (spawnEnemy == 1)
        {
            // Lägger till en fågel
            tmpSprite = content.Load<Texture2D>("birds/pinkBird");

            tempEnemy = new HBird(tmpSprite, 10000, 10000, 10000, 0, 7f);
            enemies.Add(tempEnemy);
        }
        else if (spawnEnemy == 2)
        {
            tmpSprite = content.Load<Texture2D>("birds/blueBird");

            Enemy temp = new VBird(tmpSprite, 10000, 10000, 0, 10000, 4f);
            enemies.Add(temp);
        }
        else if (spawnEnemy == 3)
        {
            tmpSprite = content.Load<Texture2D>("birds/greenBird");

            Enemy temp = new DBird(tmpSprite, 10000, 10000, 10000, 10000, 7f);
            enemies.Add(temp);
        }

        // Om spelaren hamnar under hasnivån
        if (player.Y > window.ClientBounds.Height - sea.Height) player.IsAlive = false;
        
        // Om spelaren är död
        if (!player.IsAlive)
        {
            highscorePoints = player.Points;
            Reset(window, content);
            return State.EnterHighScore;
        }

        return State.Run;
    }

    /// <summary>
    /// Ritar ut när spelet körs
    /// </summary>
    /// <param name="spriteBatch">Inbyggt ritverktyg</param>
    /// <param name="gameTime">Speltiden</param>
    public static void RunDraw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Ritar ut alla texturer genom objektens metoder
        background.Draw(spriteBatch);
        raft.Draw(spriteBatch);
        player.Draw(spriteBatch);
        player.Walkcycle(gameTime, characterTexturesLeft, characterTexturesRight);
        // Loopar igenom varje enemy
        foreach (Enemy e in enemies) e.Draw(spriteBatch);
        sea.Draw(spriteBatch);
        spriteBatch.DrawString(arial32, "Points: " + player.Points, new Vector2(0, 0), Color.White);
    }
    
    /// <summary>
    /// Uppdaterar highscore-vyn för spelaren
    /// </summary>
    /// <param name="gameTime">speltidn</param>
    /// <returns>State</returns>
    public static State HighScoreUpdate(GameTime gameTime)
    {
        // Kollar state
        if (currentState == State.EnterHighScore)
        {
            // Skickar spelaren rätt
            if (highscore.EnterUpdate(gameTime, highscorePoints)) currentState = State.PrintHighScore;
            else return State.EnterHighScore;
        }
        
        // Då spelaren endast är i vyn för att se highscore-listan kan den gå ut genom knapptryck
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape)) return State.Menu;
                
        return State.PrintHighScore;
    }

    /// <summary>
    /// Rita ut rätt delar för highscore
    /// </summary>
    /// <param name="spriteBatch">Inbyggt ritverktyg</param>
    public static void HighScoreDraw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(highscoreBackground, new Vector2(0, 0), Color.White);
        switch (currentState)
        {
            case State.EnterHighScore:
                highscore.EnterDraw(spriteBatch, myFont);
                break;
            default:
                highscore.PrintDraw(spriteBatch, myFont);
                break;
        }
    }

    /// <summary>
    /// Resettar spelet
    /// </summary>
    /// <param name="window">Spelarfönstret</param>
    /// <param name="content">Content i spelet</param>
    private static void Reset(GameWindow window, ContentManager content)
    {
        player.Reset(rand.Next((int)raft.X, (int)(raft.X + raft.Width)), -1000, 9f, 20f);
        enemies.Clear();
        
        tmpSprite = content.Load<Texture2D>("birds/pinkBird");
        tempEnemy = new HBird(tmpSprite, 10000, 10000, 10000, 0, 7f);
        enemies.Add(tempEnemy);

        upperChanceLimit = 2001;
        increaseChanceDeltaTime = 0; 
    }
}