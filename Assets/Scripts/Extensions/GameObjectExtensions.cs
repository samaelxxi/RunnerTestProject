

using UnityEngine;

public static class GameObjectExtensions
{
    public static GameObject NewGameObjectAsChild(string name, Transform parent, bool worldPositionStays = true)
    {
        GameObject gameObject = new GameObject(name);
        gameObject.transform.SetParent(parent, worldPositionStays);

        return gameObject;
    }
}