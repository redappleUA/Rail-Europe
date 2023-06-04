using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseScreenController : MonoBehaviour, IUIController, IUIActivator, IUISceneLoader
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] AssetReference sceneReference;

    private Button _continueButton, _mainMenuButton;

    void Start()
    {
        Activate();
    }

    public void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        _continueButton = root.Q<Button>("ContinueButton");
        _mainMenuButton = root.Q<Button>("MainMenuButton");

        _continueButton.clicked += Activate;
        _mainMenuButton.clicked += LoadScene;
    }

    public void Activate()
    {
        if (!gameObject.activeSelf)
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }

    public void LoadScene()
    {
        AsyncOperationHandle<SceneInstance> loadHandle = Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Single, activateOnLoad: true);
        loadHandle.Completed += OnSceneLoaded;
    }

    public void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // Scene loaded successfully, you can access the loaded scene using handle.Result
            SceneInstance loadedScene = handle.Result;
            ResourcesData.ResetCounts();

            Debug.Log("Loaded");
        }
        else
        {
            // Failed to load the scene, handle the error
            Debug.LogError($"Failed to load scene: {handle.OperationException}");
        }
    }
}
