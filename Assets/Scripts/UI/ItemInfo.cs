using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public ItemObject itemInfo;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    private void Start()
    {
        titleText.text = itemInfo.displayName;
        descriptionText.text = itemInfo.description;
    }

    public void SelectItem()
    {
        Player.instance.GetComponent<PlayerItems>().AddItem(itemInfo.GetItem());
        CharmInfo.instance.StartCoroutine(CharmInfo.instance.CloseInfo());
    }
}
