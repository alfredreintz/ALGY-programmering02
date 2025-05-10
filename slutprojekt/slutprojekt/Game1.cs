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
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Back))
            Exit();

        // TODO: Add your update logic here

        // Loopar igenom alla states
        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
                // Skickar in spelaren i state
                GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
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

        // TODO: Add your drawing code here
        
        spriteBatch.Begin();

        // Loopar igenom states
        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
                // Anropar metod för att rita ut passande teturer för state
                GameElements.RunDraw(spriteBatch, gameTime);
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