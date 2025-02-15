using Raylib_cs;
using System;

class Program
{
    const int WindowWidth = 750;
    const int WindowHeight = 800;
    static ScenesManager scenesManager = new ScenesManager();

    static void Main()
    {
        
        Raylib.InitWindow(WindowWidth, WindowHeight, "Snake Game");
        Raylib.SetTargetFPS(60);
        GameResources.Load();

        scenesManager.Load<MainMenuScene>();

        while (!Raylib.WindowShouldClose())
        {
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Brown);
            scenesManager.Update();
            scenesManager.Draw();
            Raylib.EndDrawing();
        }

        GameResources.Unload();
        Raylib.CloseWindow();
    }
}
