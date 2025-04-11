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
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        GameElements.currentState = GameElements.State.Menu;
        GameElements.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        GameElements.LoadContent(Content, Window);
        // TODO: use this.Content to load your game content here
    }
    
    protected override void UnloadContent()
    {
        GameElements.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
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

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        
        spriteBatch.Begin();

        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
                GameElements.RunDraw(spriteBatch);
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