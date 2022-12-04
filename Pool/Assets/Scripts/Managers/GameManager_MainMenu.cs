using UnityEngine;

public class GameManager_MainMenu : GameManager
{
    private UIController_MainMenu uiController;

    private void Start()
    {
        uiController = (UIController_MainMenu)UIController;

        uiController.PlayButtonClicked += SceneLoader.PlayGame;
        uiController.ExitButtonClicked += SceneLoader.Exit;
    }

    private void OnDestroy()
    {
        uiController.PlayButtonClicked -= SceneLoader.PlayGame;
        uiController.ExitButtonClicked -= SceneLoader.Exit;
    }
}
