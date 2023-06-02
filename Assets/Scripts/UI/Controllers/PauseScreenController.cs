using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseScreenController : MonoBehaviour, IUIController, IUIActivator
{
    [SerializeField] UIDocument _UIDocument;
    [SerializeField] Object _bootstrap;

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
        _mainMenuButton.clicked += LoadMenu;//delegate () { SceneManager.LoadSceneAsync(_bootstrap.name); };
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

    private void LoadMenu()
    {
        SceneManager.LoadSceneAsync(_bootstrap.name);
        Debug.Log("Loaded");
    }
}
