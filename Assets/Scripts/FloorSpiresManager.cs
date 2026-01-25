using System.Collections;
using UnityEngine;

public class FloorSpiresManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spires;
    [SerializeField] private float timeDelay = 3f;
    
    private AudioSource[] audioSources;
    
    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
    }


    
    public void ActivateTrap()
    {
        Debug.Log(" === ACTIVATEEE");
        
        
        StartCoroutine(PlaySound(0, 0));
        StartCoroutine(PlaySound((timeDelay/3), 0));
        StartCoroutine(PlaySound(((2*timeDelay)/3), 0)) ;
        StartCoroutine(PlaySound((timeDelay-0.5f), 1)) ;
        StartCoroutine(DelayedAction(timeDelay));

    }

    IEnumerator PlaySound(float delayTime, int i)
    {
        yield return new WaitForSeconds(delayTime);
        audioSources[i].Play();
    }

    IEnumerator DelayedAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified time
        foreach (var spire in spires)
        {
            FloorSpire spireController = spire.GetComponent<FloorSpire>();
            spireController.Activate();
        }
    }
}
