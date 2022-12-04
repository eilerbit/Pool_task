using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.InputSystem.Utilities;
using System;

public class InputController : MonoBehaviour, IPausable
{
    private IDraggable draggable;

    private IMovable movable;

    private Touch touch;   

    private bool objectHasBeenReleased;

    private bool paused;
    
    private void Update()
    {
        if (!paused && Touch.activeFingers.Count > 0) Drag(Touch.activeTouches);
    }

    public void Initialize(IDraggable draggable, IMovable movable)
    {
        EnhancedTouchSupport.Enable();

        PauseController.Instance.RegisterPauseClient(this);

        this.draggable = draggable;
        this.movable = movable;
    }

    public void Pause(bool paused)
    {
        this.paused = paused;
    }

    public void Restart()
    {
        objectHasBeenReleased = false;        
    }

    private void Drag(ReadOnlyArray<Touch> touch)
    {        
        if (touch.Count < 1 || objectHasBeenReleased) return;

        if (touch[0].phase == TouchPhase.Moved)
        {
            draggable.Drag(touch[0].delta.x, touch[0].delta.y);                        
        }

        else if (touch[0].phase == TouchPhase.Ended)
        {
            objectHasBeenReleased = true;                                    

            movable.Launch(draggable.TargetDirection, draggable.ArrowLenth);

            draggable.Release();                       
        }
    }
}
