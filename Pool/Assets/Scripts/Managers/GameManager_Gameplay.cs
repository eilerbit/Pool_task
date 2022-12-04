using System;
using UnityEngine;


public class GameManager_Gameplay : GameManager, IManager
{
    [SerializeField] private TargetFactory targetFactory;
    [SerializeField] private DefenderFactory defenderFactory;
        
    [SerializeField] private LevelConfig levelConfig;

    [SerializeField] private InputController inputController;
    [SerializeField] private Arrow arrow;

    [SerializeField] private Puck puckPrefab;
    [SerializeField] private Transform puckSpawnPoint;

    [SerializeField] private int healthPoints = 3;

    private UIController_Gameplay uiController;
    private PauseController pauseController;

    private PuckSpawner puckSpawner;
    private TargetSpawner targetSpawner;
    private DefenderSpawner defenderSpawner;

    private LevelController levelController;

    private void Start()
    {
        uiController = (UIController_Gameplay)UIController;
        pauseController = new PauseController();

        levelController = new LevelController(levelConfig);

        uiController.MenuButtonClicked += PauseGame;
        uiController.ExitToMainMenuButtonClicked += ExitToMainMenu;
        uiController.BackToGameButtonClicked += ResumeGame;

        uiController.Initialize(levelController, healthPoints);

        puckSpawner = new PuckSpawner(puckPrefab, puckSpawnPoint.position, this);
        targetSpawner = new TargetSpawner(targetFactory, this);
        defenderSpawner = new DefenderSpawner(defenderFactory);

        arrow.Initialize(puckSpawnPoint.position);
        inputController.Initialize(arrow, puckSpawner.Unit.GetComponent<IMovable>());

        StartNextLevel();        
    }

    public void StartNextLevel()
    {
        defenderSpawner.DestroyAllDefenders();

        bool levelUpdatedSuccessfully = levelController.UpdateLevel();

        if (!levelUpdatedSuccessfully)
        {
            GameOver(victory: true);
            return;
        }            
                
        targetSpawner.UpdateData(ref levelController.GetTargetsData());
        defenderSpawner.UpdateData(ref levelController.GetDefendersData());
                
        targetSpawner.Spawn();
        defenderSpawner.Spawn();        
    }

    public void Restart()
    {
        inputController.Restart();
    }

    public void OnPlayerLostHitpoint()
    {
        healthPoints--;

        uiController.ReduceHealthView();

        if (healthPoints == 0) GameOver(victory: false);
    }

    public void GameOver(bool victory)
    {
        if(victory)
        {
            PauseGame();
        }

        else
        {
            PauseGame();
            levelController.OnLooseGame();
        }

        uiController.OnGameOver();
    }

    private void PauseGame()
    {
        pauseController.Pause(true);
    }

    private void ResumeGame()
    {
        pauseController.Pause(false);
    }

    private void ExitToMainMenu()
    {        
        SceneLoader.OpenMainMenu();
    }

    private void OnDestroy()
    {
        PauseController.Instance = null;
    }
}