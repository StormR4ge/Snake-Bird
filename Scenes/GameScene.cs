using Raylib_cs;
using Snake_Projet.Scenes;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Numerics;
using Color = Raylib_cs.Color;

public class GameScene : Scene
{
    #region Fields

    private List<Entity> entities = new List<Entity>();
    private Grid grid;
    private Snake snake;
    private Bird bird;
    private Vector2 food;

    private int Score = 0;

    private int gridSize = 15;
    public int cellSize = 50;

    private float timer = 0f;
    private float interval = 0.5f;

    #endregion

    #region Constructor

    public GameScene()
    {
        entities = new List<Entity>();
        grid = new Grid(gridSize, gridSize);
        snake = new Snake(new Vector2(7, 7), cellSize);
        bird = new Bird();
        food = new Vector2(0, 0);
    }

    #endregion

    #region Load/Unload

    public override void Load()
    {
        grid = new Grid(gridSize, gridSize);
        snake = new Snake(new Vector2(7, 7), cellSize);
        bird = new Bird();
        food = grid.SpawnFood(snake.Body);
        entities = new List<Entity> { snake };
    }

    #endregion

    #region Update

    public override void Update()
    {

        foreach (var entity in entities)
        {
            entity.Update();
        }

        timer += Raylib.GetFrameTime();
        if (timer >= interval)
        {
            snake.Move(grid);
            timer = 0f;
        }

        bird.Update(snake, grid, () => grid.ClearDangerZones());

        if (snake.Body[0] == food)
        {
            snake.Grow();
            food = grid.SpawnFood(snake.Body);
            Score++;
        }

        if (snake.CheckCollision(gridSize))
        {
            ServiceLocator.Get<IScenesManager>().Load<GameOverScene>();
        }
    }

    #endregion

    #region Draw

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Brown);

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                int px = x * cellSize;
                int py = y * cellSize;

                if ((x + y) % 2 == 0)
                {
                    Raylib.DrawRectangle(px, py, cellSize, cellSize, new Color(173, 214, 68, 255));
                }
                else
                {
                    Raylib.DrawRectangle(px, py, cellSize, cellSize, new Color(166,209,60,255));
                }
            }
        }

        foreach (var entity in entities)
        {
            entity.Draw();
        }

        Raylib.DrawCircle((int)(food.X * cellSize + cellSize * 0.5), (int)(food.Y * cellSize + cellSize * 0.5), (float)(cellSize * 0.5), Color.Red);

        foreach (var dangerZone in grid.DangerZones)
        {
            Raylib.DrawRectangle((int)dangerZone.X * cellSize, (int)dangerZone.Y * cellSize, cellSize, cellSize, Color.Gray);
        }

        Raylib.DrawTextEx(GameResources.GameFont, $"Score : {Score}", new Vector2(150, 760), 30, 2, Color.White);
        Raylib.DrawTextEx(GameResources.GameFont, $"Bird : {(int)bird.timer}", new Vector2(500, 760), 30, 2, Color.White);
    }

    #endregion
}
