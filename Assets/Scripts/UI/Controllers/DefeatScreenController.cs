using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DefeatScreenController : MonoBehaviour, IUIController, IUIActivator, IUISceneLoader
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] AssetReference sceneReference;

    public string Reason { get; set; }
    private Label _reasonLabel, _scoreLabel;
    private Button _mainMenuButton;

    void Start()
    {
        Activate();
    }

    public void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        _mainMenuButton = root.Q<Button>("MainMenuButton");
        _reasonLabel = root.Q<Label>("Reason");
        _scoreLabel = root.Q<Label>("Record");

        _reasonLabel.text = Reason;
        Debug.Log(_reasonLabel.text);
        _scoreLabel.text += ScoreService.Score.ToString();

        _mainMenuButton.clicked += LoadScene;
    }

    public void Activate()
    {
        if (!gameObject.activeSelf)
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
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
