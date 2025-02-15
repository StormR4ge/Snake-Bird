public interface IScenesManager
{
    void Load<T>() where T : Scene, new();

}

public class ScenesManager : IScenesManager
{
    private static Scene? _currentScene;

    public ScenesManager() 
    {
        ServiceLocator.Register<IScenesManager>(this);
    }

    public void Load<T>() where T : Scene, new()
    {
        if (_currentScene != null) _currentScene.Unload();
        _currentScene = new T();
        _currentScene.Load();
    }

    public void Update() => _currentScene?.Update();
    public void Draw() => _currentScene?.Draw();
}
