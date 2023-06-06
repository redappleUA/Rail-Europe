using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public interface IUISceneLoader
{
    void LoadScene();
    void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle);
}
