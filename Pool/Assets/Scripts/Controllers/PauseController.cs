using System.Collections.Generic;
using UnityEngine;

public class PauseController
{
    public static PauseController Instance;

    public PauseController()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("It's not allowed to have multiple PauseController instances on the scene");
    }
    
    private List<IPausable> pausables = new List<IPausable>();

    public void RegisterPauseClient(IPausable pausable)
    {
        pausables.Add(pausable);
    }

    public void Pause(bool pause)
    {
        for (int i = 0; i < pausables.Count; i++)
        {
            pausables[i].Pause(pause);
        }
    }
}
