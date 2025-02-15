public abstract class Scene
{
    public virtual void Load() { }
    public abstract void Update();
    public abstract void Draw();
    public virtual void Unload() { }
}
