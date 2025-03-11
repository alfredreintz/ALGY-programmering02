using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace highscoreDemo;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;
    SpriteFont myFont;
    HighScore highscore;
    public enum State
    {
        PrintHighScore,
        EnterHighScore,
    };

    private State currentState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        highscore = new HighScore(10);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        
        myFont = Content.Load<SpriteFont>("fonts/arial32");
        highscore.LoadFromFile("highscore.txt");
    }

    protected override void UnloadContent()
    {
        highscore.SaveToFile("highscore.txt");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        switch (currentState)
        {
            case State.EnterHighScore:
                if (highscore.EnterUpdate(gameTime, 10)) currentState = State.PrintHighScore;
                break;
            default:
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.E))
                {
                    currentState = State.EnterHighScore;
                }
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        spriteBatch.Begin();
        switch (currentState)
        {
            case State.EnterHighScore:
                highscore.EnterDraw(spriteBatch, myFont);
                break;
            default:
                highscore.PrintDraw(spriteBatch, myFont);
                break;
        }
        spriteBatch.End();

        base.Draw(gameTime);
    }
}