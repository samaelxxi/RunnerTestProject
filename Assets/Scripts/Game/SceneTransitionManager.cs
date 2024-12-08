using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Attributes.AutoRegisteredService]
public class SceneTransitionManager : MonoBehaviour, Services.IRegistrable
{
    public string transitionSceneName = "Loading";

    public IEnumerator TransitionToSceneAsync(string nextSceneName)
    {
        AsyncOperation loadTransition = SceneManager.LoadSceneAsync(transitionSceneName, LoadSceneMode.Additive);
        while (!loadTransition.isDone)
            yield return null;

        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation unloadCurrent = SceneManager.UnloadSceneAsync(currentScene);
        if (unloadCurrent != null)
            while (!unloadCurrent.isDone)
                yield return null;

        yield return new WaitForSeconds(1); // just to look at pig

        AsyncOperation loadNext = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        while (!loadNext.isDone)
            yield return null;

        Scene nextScene = SceneManager.GetSceneByName(nextSceneName);
        if (nextScene.IsValid())
            SceneManager.SetActiveScene(nextScene);
        else
            Debug.LogError($"Scene '{nextSceneName}' could not be found or is invalid.");

        AsyncOperation unloadTransition = SceneManager.UnloadSceneAsync(transitionSceneName);
        if (unloadTransition != null)
            while (!unloadTransition.isDone)
                yield return null;

    }

    public void StartTransition(string nextSceneName)
    {
        StaticCoroutine.Start(TransitionToSceneAsync(nextSceneName));
    }
}
