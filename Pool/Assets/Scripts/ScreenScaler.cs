using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class ScreenScaler : MonoBehaviour
{    
    public event Action<float> WideScreenDetected;

    [SerializeField] private float WideScreenMatchWidthOrHeight = 0.5f;

    private const float FullHDAspectRatio = 0.5625f;          
    
    private Camera myCamera;

    public void Initialize()
    { 
        Screen.orientation = ScreenOrientation.Portrait;

        myCamera = GetComponent<Camera>();
    }

    public void SetCameraFieldofView()
    {
        float aspectRatio = myCamera.aspect;

        //This math formula represents the field of view and aspect ratio dependency to preserve the same view
        //for all device screens that are narrower than fullHD

        if (aspectRatio <= FullHDAspectRatio) myCamera.fieldOfView = 2f * (6.405f - 6f * aspectRatio) / 0.1001f;
        
        else WideScreenDetected?.Invoke(WideScreenMatchWidthOrHeight);        
    }        
}