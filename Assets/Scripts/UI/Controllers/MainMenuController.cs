using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] UIDocument _UIDocument;

    private Button _playButton;
    private Label _scorelabel;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        var root = _UIDocument.rootVisualElement;

        _scorelabel = root.Q<Label>("Record");
        _playButton = root.Q<Button>("PlayButton");

        _scorelabel.text += ScoreService.BestScore.ToString();
        _playButton.clicked += delegate () { SceneManager.LoadSceneAsync("Bootstrap"); };
    }
}
