using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{    
    [SerializeField] protected UIController UIController;

    protected SceneLoader SceneLoader;

    private ScreenScaler screenScaler;       

    private void Awake()
    {    
        SceneLoader = new SceneLoader();

        screenScaler = Camera.main.GetComponent<ScreenScaler>();
                
        screenScaler.Initialize();

        UIController.PreInitialize(screenScaler);

        screenScaler.SetCameraFieldofView();
    }        
}
