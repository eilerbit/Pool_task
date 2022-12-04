using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;


public class UIController_Gameplay : UIController
{
    [SerializeField] private Button menuButton;    
    [SerializeField] private Button backToGameButton;
    [SerializeField] private Button exitToMainMenuButton;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI notificationText;

    [SerializeField] private Image pauseMenu;
    [SerializeField] private Transform healthPanel;
    [SerializeField] private Image lifeViewPrefab;

    [SerializeField] private float notificationDisplayTime = 1.5f;

    public event Action MenuButtonClicked;
    public event Action ExitToMainMenuButtonClicked;
    public event Action BackToGameButtonClicked;

    private INotifier notifier;

    private Vector3 enlargedNotificationScale = new Vector3(2, 2, 2);
    private Vector3 originalNotificationScale;

    private Image[] lifeViews;

    private int lifeViewNumber;

    private bool coroutineCoordinatorActive;

    private Queue<IEnumerator> notificationQeue = new Queue<IEnumerator>();

    public void Initialize(INotifier notifier, int healthpoints)
    {
        menuButton.onClick.AddListener(() => OnMenuButtonClicked());        
        backToGameButton.onClick.AddListener(() => OnBackToGameButtonClicked());
        exitToMainMenuButton.onClick.AddListener(() => ExitToMainMenuButtonClicked?.Invoke());

        originalNotificationScale = notificationText.gameObject.GetComponent<RectTransform>().localScale;

        this.notifier = notifier;

        this.notifier.Notification += Notify;
        this.notifier.LevelChanged += DisaplayLevelText;

        SetHealthView(healthpoints);
    }

    public void ReduceHealthView()
    {        
        lifeViews[lifeViewNumber-1].enabled = false;
        lifeViewNumber--;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        menuButton.onClick.RemoveListener(() => OnMenuButtonClicked());        
        backToGameButton.onClick.RemoveListener(() => OnBackToGameButtonClicked());
        exitToMainMenuButton.onClick.RemoveListener(() => ExitToMainMenuButtonClicked?.Invoke());

        this.notifier.Notification -= Notify;
        this.notifier.LevelChanged -= DisaplayLevelText;
    }

    public void OnGameOver()
    {
        pauseMenu.gameObject.SetActive(true);
        backToGameButton.interactable = false;
    }

    private void OnMenuButtonClicked()
    {        
        pauseMenu.gameObject.SetActive(true);

        MenuButtonClicked?.Invoke();
    }

    private void OnBackToGameButtonClicked()
    {
        pauseMenu.gameObject.SetActive(false);

        BackToGameButtonClicked?.Invoke();
    }

    private void DisaplayLevelText(string levelNumber)
    {
        levelText.text = "Level " + levelNumber;
    }

    private void Notify(string message)
    {
        notificationQeue.Enqueue(EnlageNotificationScale(message));

        if(!coroutineCoordinatorActive)
        {
            coroutineCoordinatorActive = true;
            StartCoroutine(CoroutineCoordinator());
        }        
    }

    IEnumerator CoroutineCoordinator()
    {
        while (coroutineCoordinatorActive)
        {
            while (notificationQeue.Count > 0)
                yield return StartCoroutine(notificationQeue.Dequeue());

            coroutineCoordinatorActive = false;            
        }
    }

    IEnumerator EnlageNotificationScale(string message)
    {
        notificationText.text = message;

        notificationText.gameObject.SetActive(true);

        float time = 0;

        while (time < notificationDisplayTime)
        {
            notificationText.gameObject.GetComponent<RectTransform>().localScale = Vector3.Lerp(originalNotificationScale, enlargedNotificationScale, time);

            time += 0.02f;

            if (time < notificationDisplayTime) yield return new WaitForSecondsRealtime(Time.deltaTime / 2f);

            else break;
        }

        notificationText.gameObject.GetComponent<RectTransform>().localScale = originalNotificationScale;

        notificationText.gameObject.SetActive(false);
    }

    private void SetHealthView(int number)
    {
        lifeViewNumber = number;

        lifeViews = new Image[number];

        for (int i = 0; i < number; i++)
        {
            lifeViews[i] = Instantiate(lifeViewPrefab, healthPanel);
            lifeViews[i].enabled = true;
        }
    }

      
}
