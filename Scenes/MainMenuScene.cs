using System.Numerics;
using Color = Raylib_cs.Color;
using Raylib_cs;

public class MainMenuScene : Scene
{
    public override void Load()
    {
        Console.WriteLine("Load");
    }

    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            ServiceLocator.Get<IScenesManager>().Load<GameScene>(); // Change la scène vers le jeu
    }

    public override void Draw()
    {
        Raylib.DrawTextEx(GameResources.GameFont,"Press ENTER to Start", new Vector2(100, 100), 60, 2, Color.White);
    }
}
