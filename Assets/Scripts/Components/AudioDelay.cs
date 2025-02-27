using System.Collections;
using UnityEngine;

public class AudioDelay : MonoBehaviour
{
    public AudioSource sourceWait;
    public float delay;
    private IEnumerator Start()
    {
        sourceWait.Play();
        yield return new WaitForSecondsRealtime(sourceWait.clip.length + delay);
        GetComponent<AudioSource>().Play();
    }

}
