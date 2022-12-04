using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Canvas Settings", menuName = "Canvas Settings")]
public class CanvasSettings : ScriptableObject
{
    public RenderMode Mode;
    public float MatchWidthOrHeight;
}
