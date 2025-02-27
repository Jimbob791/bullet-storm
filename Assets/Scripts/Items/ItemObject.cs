using UnityEngine;

[CreateAssetMenu(fileName = "NewItemObject", menuName = "ScriptableObjects/ItemObject")]
public class ItemObject : ScriptableObject
{
    public ItemReference itemReference;
    public string displayName;
    [TextArea(2, 10)] public string description;

    public Item GetItem()
    {
        switch (itemReference)
        {
            case ItemReference.RegenerativeCore:
                return new RegenerativeCore();
            case ItemReference.ReinforcedArmor:
                return new ReinforcedArmor();
            case ItemReference.AmmoPack:
                return new AmmoPack();
            case ItemReference.BurstCore:
                return new BurstCore();
            case ItemReference.TimeStorage:
                return new TimeStorage();
            case ItemReference.RapidRecharge:
                return new RapidRecharge();
            case ItemReference.CyberLegs:
                return new CyberLegs();
            case ItemReference.TurboBoots:
                return new TurboBoots();
            case ItemReference.HonedBlade:
                return new HonedBlade();
            case ItemReference.BallisticRounds:
                return new BallisticRounds();
            default:
                return null;
        }
    }
}

public enum ItemReference
{
    RegenerativeCore,
    ReinforcedArmor,
    AmmoPack,
    BurstCore,
    TimeStorage,
    RapidRecharge,
    CyberLegs,
    TurboBoots,
    HonedBlade,
    BallisticRounds
}