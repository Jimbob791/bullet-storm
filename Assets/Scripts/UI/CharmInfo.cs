using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmInfo : MonoBehaviour
{
    public static CharmInfo instance;
    public GameObject clickSFX;
    [SerializeField] Transform infoParent;

    public GameObject infoPrefab;
    List<GameObject> infos = new List<GameObject>();

    Animator anim;

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

        anim = GetComponent<Animator>();
    }

    public IEnumerator DisplayCharmInfo(List<ItemObject> items)
    {
        // Stop time and display panel
        Player.instance.GetComponent<PlayerShooting>().stopInput = true;
        HelperFunctions.SetTime(0f);
        anim.SetBool("open", true);
        yield return new WaitForSecondsRealtime(0.4f);

        // Create infos
        foreach (ItemObject itemObject in items)
        {
            GameObject newInfo = Instantiate(infoPrefab, infoParent);
            newInfo.GetComponent<ItemInfo>().itemInfo = itemObject;
            infos.Add(newInfo);
        }        
    }

    public IEnumerator CloseInfo()
    {
        Instantiate(clickSFX);
        // Remove infos
        for (int i = infos.Count - 1; i >= 0; i--)
        {
            Destroy(infos[i]);
            infos.RemoveAt(i);
        }

        anim.SetBool("open", false);
        yield return new WaitForSecondsRealtime(4f / 12f);
        HelperFunctions.SetTime(1f);
        Player.instance.GetComponent<PlayerShooting>().stopInput = false;
    }
}
