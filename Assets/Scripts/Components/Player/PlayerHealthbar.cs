using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconBar : MonoBehaviour
{
    [SerializeField] GameObject pipPrefab;

    private List<GameObject> pips = new List<GameObject>();

    public void UpdateBar(int max, int current)
    {
        for (int i = pips.Count - 1; i >= 0; i--)
        {
            Destroy(pips[i]);
            pips.RemoveAt(i);
        }

        // Create new pips
        for (int i = 0; i < max; i++) 
        {
            GameObject newPip = Instantiate(pipPrefab, transform);
            newPip.transform.GetChild(0).GetComponent<Image>().enabled = i < current;
            pips.Add(newPip);
        }

    }
}
