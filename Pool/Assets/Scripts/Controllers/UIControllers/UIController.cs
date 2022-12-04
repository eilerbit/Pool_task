using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIController : MonoBehaviour
{
    [SerializeField] private Canvas[] canvas;
    [SerializeField] private CanvasScaler[] canvasScaler;
    [SerializeField] private CanvasSettings[] canvasSettings;

    private ScreenScaler screenScaler;

    public void PreInitialize(ScreenScaler screenScaler)
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].renderMode = canvasSettings[i].Mode;
            canvasScaler[i].matchWidthOrHeight = canvasSettings[i].MatchWidthOrHeight;
        }

        this.screenScaler = screenScaler;

        this.screenScaler.WideScreenDetected += OnWideScreenDetected;        
    }

    private void OnWideScreenDetected(float matchWidthOrHeight)
    {
        for (int i = 0; i < canvasScaler.Length; i++)
        {
            canvasScaler[i].matchWidthOrHeight = matchWidthOrHeight;
        }
    }

    protected virtual void OnDestroy()
    {
        screenScaler.WideScreenDetected -= OnWideScreenDetected;
    }

}
