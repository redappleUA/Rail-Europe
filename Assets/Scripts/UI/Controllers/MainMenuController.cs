using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour, IUIController, IUISceneLoader
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] AssetReference sceneReference;

    private Button _playButton;
    private Label _scorelabel;

    void Start()
    {
        Initialize();
        Time.timeScale = 0f;
    }

    public void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        _scorelabel = root.Q<Label>("Record");
        _playButton = root.Q<Button>("PlayButton");

        _scorelabel.text += ScoreService.BestScore.ToString();
        _playButton.clicked += LoadScene;
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
            SceneInstance sceneInstance = handle.Result;
            Debug.Log("Loaded");
        }
        else
        {
            // Failed to load the scene, handle the error
            Debug.LogError($"Failed to load scene: {handle.OperationException}");
        }
    }
}
