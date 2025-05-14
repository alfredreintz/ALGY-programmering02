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

namespace slutprojekt;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        // Ändrar standardupplösning
        _graphics.PreferredBackBufferWidth = 1250;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    /// <summary>
    /// Laddar i logik för spelet
    /// </summary>
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        GameElements.currentState = GameElements.State.Menu;
        GameElements.Initialize();
        base.Initialize();
    }

    /// <summary>
    /// Laddar in textures, fonts osv. för spelet
    /// </summary>
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        GameElements.LoadContent(Content, Window);
        // TODO: use this.Content to load your game content here
    }
    
    /// <summary>
    /// Justerar logik innan speleet avslutas
    /// </summary>
    protected override void UnloadContent()
    {
        GameElements.UnloadContent();
    }

    /// <summary>
    /// Uppdaterar logik för varje frame
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Update(GameTime gameTime)
    {

        // TODO: Add your update logic here

        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
                GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                break;
            case GameElements.State.PrintHighScore:
                GameElements.currentState = GameElements.HighScoreUpdate(gameTime);
                break;
            case GameElements.State.EnterHighScore:
                GameElements.currentState = GameElements.HighScoreUpdate(gameTime);
                break;
            case GameElements.State.Quit:
                this.Exit();
                break;
            default:
                GameElements.currentState = GameElements.MenuUpdate(gameTime);
                break;
        }
        
        base.Update(gameTime);
    }

    /// <summary>
    /// Ritar ut allt i spelet
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        spriteBatch.Begin();

        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
                GameElements.RunDraw(spriteBatch, gameTime);
                break;
            case GameElements.State.PrintHighScore:
                GameElements.HighScoreDraw(spriteBatch);
                break;
            case GameElements.State.EnterHighScore:
                GameElements.HighScoreDraw(spriteBatch);
                break;
            case GameElements.State.Quit:
                this.Exit();
                break;
            default:
                GameElements.MenuDraw(spriteBatch);
                break;
        }
        
        spriteBatch.End();
        
        base.Draw(gameTime);
    }
}