using System.Numerics;
using Color = Raylib_cs.Color;
using Raylib_cs;

namespace Snake_Projet.Scenes
{
    public class GameOverScene : Scene
    {
        public override void Load() {}

        public override void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                ServiceLocator.Get<IScenesManager>().Load<GameScene>(); // Change la scène vers le jeu

        }

        public override void Draw()
        {
            Raylib.DrawTextEx(GameResources.GameFont,"You loose ... Press ENTER to ReStart", new Vector2(100, 100), 40, 2, Color.White);
            Raylib.DrawTextEx(GameResources.GameFont, "Or Press ESCAPE to RageQuit", new Vector2(200, 200), 40, 2, Color.White);

        }

        public override void Unload()
        {
            Console.WriteLine("Unload");
        }
    }
}
