using Snake_Projet.Scenes;
using System;
using System.Collections.Generic;
using System.Numerics;
using Color = Raylib_cs.Color;
using System.Drawing;
using Raylib_cs;
using static System.Formats.Asn1.AsnWriter;

public class Bird
{
    #region Fields

    public float timer = 0f;
    private float dangerZoneDuration = 10f; // Temps avant l'attaque (en S)
    private Vector2 dangerZonePosition;
    private bool isDangerZoneActive = false;

    #endregion

    #region Public Methods

    public void Update(Snake snake, Grid grid, Action onDangerZoneCleared)
    {
        timer += Raylib.GetFrameTime(); // ++ temps

        if (!isDangerZoneActive) // Si aucune zone n'est active, en créer une
        {
            dangerZonePosition = grid.SpawnDangerZone(snake.Body);
            isDangerZoneActive = true;
            timer = 0f; // Réinitialisation du timer
        }

        if (timer >= dangerZoneDuration) // Attaque
        {
            HandleAttack(snake, grid, onDangerZoneCleared);
        }
    }

    private void HandleAttack(Snake snake, Grid grid, Action onDangerZoneCleared)
    {
        // Vérifie si le serpent est sur la zone dangereuse
        if (snake.Body[0] == dangerZonePosition) // Tête
        {
            ServiceLocator.Get<IScenesManager>().Load<GameOverScene>();
        }
        else if (snake.Body.Contains(dangerZonePosition)) // Corps
        {
            snake.CutAt(dangerZonePosition);
        }

        // Efface la zone dangereuse et réinitialise le timer
        grid.ClearDangerZones();
        isDangerZoneActive = false;
        timer = 0f;

        onDangerZoneCleared?.Invoke(); // Notifie le jeu que la zone est supprimée
    }

    public void Draw()
    {
        if (isDangerZoneActive)
        {
            Raylib.DrawRectangle((int)dangerZonePosition.X * 50, (int)dangerZonePosition.Y * 50, 50, 50, Color.Red);
        }
    }

    #endregion
}
