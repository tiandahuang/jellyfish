using UnityEngine;

[RequireComponent(typeof(FollowSpline))]
public class JellyControl : MonoBehaviour
{
    private InputManager inputManager;
    private FollowSpline followSpline;
    private void Start()
    {
        followSpline = GetComponent<FollowSpline>();
        inputManager = InputManager.Instance;
        if (inputManager != null)
        {
            inputManager.FloatInputAction.AddListener(ControlJelly);
        }
    }

    void ControlJelly(float value)
    {
        //start a coroutine and run the tween library for the speed.
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
