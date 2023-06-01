using UnityEngine;
using UnityEngine.UIElements;

public class DefeatScreenController : MonoBehaviour, IUIController, IUIActivator
{
    [SerializeField] UIDocument _UIDocument;

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
}
