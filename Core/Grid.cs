using System.Drawing;
using System.Numerics;
using System.Collections.Generic;

public class Grid
{
    #region Properties

    public int Width { get; }
    public int Height { get; }

    // Liste des zones dangereuses générées sur la grille
    public List<Vector2> DangerZones { get; private set; } = new List<Vector2>();

    #endregion

    #region Constructor

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Génère de la nourriture à une position aléatoire non occupée par le Snake.
    /// </summary>
    /// <param name="occupied">Liste des positions occupées par le Snake.</param>
    /// <returns>La position où la nourriture est placée.</returns>
    public Vector2 SpawnFood(List<Vector2> occupied)
    {
        Vector2 food;
        do
        {
            food = new Vector2(new Random().Next(Width), new Random().Next(Height));
        }
        while (occupied.Contains(food));

        return food;
    }

    /// <summary>
    /// Ajoute une zone dangereuse à un emplacement vide de la grille.
    /// </summary>
    /// <param name="occupied">Liste des positions occupées par le Snake.</param>
    public Vector2 SpawnDangerZone(List<Vector2> occupied)
    {
        Vector2 dangerZone;
        do
        {
            dangerZone = new Vector2(new Random().Next(Width), new Random().Next(Height));
        }
        while (occupied.Contains(dangerZone) || DangerZones.Contains(dangerZone));

        DangerZones.Add(dangerZone);
        return dangerZone;
    }

    /// <summary>
    /// Supprime toutes les zones dangereuses.
    /// </summary>
    public void ClearDangerZones()
    {
        DangerZones.Clear();
    }

    #endregion
}
