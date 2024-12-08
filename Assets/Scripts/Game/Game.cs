using DesignPatterns.Singleton;
using RunnerGame.SaveSystem;

public class Game : Singleton<Game>
{
    void OnApplicationQuit()
    {
        ServiceLocator.Get<SaveService>().Save();
    }
}
