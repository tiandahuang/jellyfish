using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TriggerableAction : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputs;
    public UnityEvent Trigger;
    bool triggered = false;
    void Start()
    {
        InputManager.Instance.FloatInputAction.AddListener(OnTriggered);
    }

    public void OnTriggered(float Triggered)
    {
        if (Triggered <= 0)
            return;
        // do once and then die...
        if (!triggered)
        {
            Trigger.Invoke();
            var x = transform.position;
            x.y = 100;
            DOTween.Kill("TweenBob");
            transform.DOMove(x, 5.0f).OnComplete(() =>
            {
                // get fucked!
                Destroy(gameObject);
            });
            triggered = true; 
        }
    }
}
