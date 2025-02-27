using System.Collections.Generic;
using UnityEngine;

public class GiftCharm : MonoBehaviour
{
    public List<ItemObject> items = new List<ItemObject>();

    public void Open()
    {
        CharmInfo.instance.StartCoroutine(CharmInfo.instance.DisplayCharmInfo(items));
        Destroy(gameObject);
    }
}
