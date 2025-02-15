using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Numerics;
using Color = Raylib_cs.Color;
using static System.Formats.Asn1.AsnWriter;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class Snake : Entity
{
    #region Properties

    public List<Vector2> Body { get; private set; }
    public Direction CurrentDirection { get; set; }
    public bool HasEaten { get; private set; } = false;
    private Direction nextDirection = Direction.Right;
    private int cellSize;

    #endregion

    #region Constructor

    public Snake(Vector2 startPosition, int cellSize) : base(startPosition)
    {
        Body = new List<Vector2> { startPosition };
        CurrentDirection = Direction.Right;
        this.cellSize = cellSize;
    }

    #endregion

    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Up) && CurrentDirection != Direction.Down)
            nextDirection = Direction.Up;
        if (Raylib.IsKeyPressed(KeyboardKey.Down) && CurrentDirection != Direction.Up)
            nextDirection = Direction.Down;
        if (Raylib.IsKeyPressed(KeyboardKey.Left) && CurrentDirection != Direction.Right)
            nextDirection = Direction.Left;
        if (Raylib.IsKeyPressed(KeyboardKey.Right) && CurrentDirection != Direction.Left)
            nextDirection = Direction.Right;
    }

    #region Public Methods

    public void Grow()
    {
        HasEaten = true;
    }

    public void CutAt(Vector2 position)
    {
        int cutIndex = Body.FindIndex(segment => segment == position);
        if (cutIndex > 0) // On ne coupe pas la tête
        {
            Body.RemoveRange(cutIndex, Body.Count - cutIndex);
        }
    }

    public void Move(Grid grid)
    {

        CurrentDirection = nextDirection;

        // Calcule la nouvelle position de la tête
        Vector2 newHead = Body[0];
        switch (CurrentDirection)
        {
            case Direction.Up:
                newHead.Y -= 1;
                break;
            case Direction.Down:
                newHead.Y += 1;
                break;
            case Direction.Left:
                newHead.X -= 1;
                break;
            case Direction.Right:
                newHead.X += 1;
                break;
        }

        // Ajoute la nouvelle tête en position 0
        Body.Insert(0, newHead);

        // Si le serpent n'a pas mangé, on retire la queue
        if (!HasEaten)
        {
            Body.RemoveAt(Body.Count - 1);
        }
        else
        {
            HasEaten = false;
        }
    }

    public bool CheckCollision(int gridSize)
    {
        Vector2 head = Body[0];

        // Collision avec les bords
        if (head.X < 0 || head.Y < 0 || head.X >= gridSize || head.Y >= gridSize)
        {
            return true;
        }

        // Collision avec soi-même
        for (int i = 1; i < Body.Count; i++)
        {
            if (head == Body[i])
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    public override void Draw()
    {
        foreach (var segment in Body)
        {
            Raylib.DrawRectangle((int)segment.X * cellSize, (int)segment.Y * cellSize, cellSize, cellSize, Color.Blue);
        }
    }
}
