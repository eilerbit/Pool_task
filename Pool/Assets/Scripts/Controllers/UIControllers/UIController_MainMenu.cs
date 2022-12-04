using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController_MainMenu : UIController
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    public event Action PlayButtonClicked;
    public event Action ExitButtonClicked;

    private void Awake()
    {
        playButton.onClick.AddListener(() => PlayButtonClicked?.Invoke());
        exitButton.onClick.AddListener(() => ExitButtonClicked?.Invoke());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        playButton.onClick.RemoveListener(() => PlayButtonClicked?.Invoke());
        exitButton.onClick.RemoveListener(() => ExitButtonClicked?.Invoke());        
    }
}
