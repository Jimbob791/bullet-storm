using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    Vector3 originalPosition;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        originalPosition = transform.localPosition;
    }

    public void ShakeCamera(float duration, float strength)
    {
        StartCoroutine(ShakeCameraCoroutine(duration, strength));
    }

    private IEnumerator ShakeCameraCoroutine(float duration, float strength)
    {
        yield return new WaitForSeconds(0.0001f);
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float mult = 1 - elapsed / duration;
            transform.localPosition = originalPosition + new Vector3(Random.Range(-strength, strength) * mult, Random.Range(-strength, strength) * mult, 0);
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
