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
    private static Texture2D background;
    
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

        player = new Player(content.Load<Texture2D>("character/characterWalkLeft1"), 280, 400, 9f, 20f, 70f, content.Load<Texture2D>("bullet"));
        raft = new Raft(content.Load<Texture2D>("raft" ), window.ClientBounds.Width / 2 - 616 / 2, window.ClientBounds.Height - 150, 0, 0);
        
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft1"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft2"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft3"));
        characterTexturesLeft.Add(content.Load<Texture2D>("character/characterWalkLeft4"));
        
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight1"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight2"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight3"));
        characterTexturesRight.Add(content.Load<Texture2D>("character/characterWalkRight4"));
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
        player.Draw(spriteBatch);
        player.Walkcycle(gameTime, characterTexturesLeft, characterTexturesRight);
        raft.Draw(spriteBatch);
    }

    /* public static State HighScoreUpdate(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Escape)) return State.Menu;

        if (currentState == State.EnterHighScore)
        {
            if (highscore.EnterUpdate(gameTime, highscorePoints)) currentState = State.PrintHighScore;
            else return State.EnterHighScore;
        }
        
        return State.PrintHighScore;
    } */

    /* public static void HighScoreDraw(SpriteBatch spriteBatch)
    {   
        switch (currentState)
        {
            case State.EnterHighScore:
                highscore.EnterDraw(spriteBatch, myFont);
                break;
            default:
                highscore.PrintDraw(spriteBatch, myFont);
                break;
        }
    } */

    private static void Reset(GameWindow window, ContentManager content)
    {
        player.Reset(380, 400, 2.5f, 4.5f);
    }
}