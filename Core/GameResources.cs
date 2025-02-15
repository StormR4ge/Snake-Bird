using Raylib_cs;

public static class GameResources
{
    public static Font GameFont;

    public static void Load()
    {
        GameFont = Raylib.LoadFont("Assets\\Fonts\\CarterOne-Regular.ttf");
    }

    public static void Unload()
    {
        Raylib.UnloadFont(GameFont);
    }
}
