using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    public float minPitch = 1.0f;
    public float maxPitch = 1.0f;

    private void Start()
    {
        AudioSource source = GetComponent<AudioSource>();

        source.pitch = Random.Range(minPitch, maxPitch);
        Destroy(gameObject, source.clip.length);
    }
}
