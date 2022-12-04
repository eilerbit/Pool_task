using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class VSyncTest : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate;
        
        void Start()
        {
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.vSyncCount = 0;
        }        
    }
}