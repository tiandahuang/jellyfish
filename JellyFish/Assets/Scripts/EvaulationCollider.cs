using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaulationCollider : MonoBehaviour
{
    public GameObject[] evaulateUIs;
    public AudioSource[] audioSources;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JellyFish"))
        {
            float rightTriggerValue = InputManager.Instance.CurrentRightTriggerValue;
            if (rightTriggerValue <= 0.4)
            {
                evaulateUIs[0].SetActive(true);
                StartCoroutine(DeactivateAfterDelay(evaulateUIs[0], 1.0f));
            } else if(rightTriggerValue <= 0.6 && rightTriggerValue > 0.4)
            {
                evaulateUIs[1].SetActive(true);
                StartCoroutine(DeactivateAfterDelay(evaulateUIs[1], 1.0f));
            }
            else if (rightTriggerValue <= 0.8 && rightTriggerValue > 0.6)
            {
                evaulateUIs[2].SetActive(true);
                StartCoroutine(DeactivateAfterDelay(evaulateUIs[2], 1.0f));
            }
            else
            {
                evaulateUIs[3].SetActive(true);
                StartCoroutine(DeactivateAfterDelay(evaulateUIs[3], 1.0f));
            }

        }
        
    }

    private IEnumerator DeactivateAfterDelay(GameObject targetObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(false);
    }
}
