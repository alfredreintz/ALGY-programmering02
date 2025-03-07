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
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            this.Exit();

        // TODO: Add your update logic here

        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
                GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                break;
            case GameElements.State.HighScore:
                GameElements.currentState = GameElements.HighScoreUpdate();
                break;
            case GameElements.State.Quit:
                this.Exit();
                break;
            default:
                GameElements.currentState = GameElements.MenuUpdate();
                break;
                
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        spriteBatch.Begin();

        switch (GameElements.currentState)
        {
            case GameElements.State.Run:
                GameElements.RunDraw(spriteBatch);
                break;
            case GameElements.State.HighScore:
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