using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<ItemListing> items = new List<ItemListing>();

    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == item.GetName())
            {
                items[i].stacks += 1;
                items[i].item.OnPickup(items[i].stacks);
                return;
            }
        }
        items.Add(new ItemListing(item, item.GetName(), 1));
        items[items.Count - 1].item.OnPickup(1);
    }

    public void Reload()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].item.OnReload(items[i].stacks);
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].item.OnShoot(items[i].stacks);
        }
    }

    public void Parry()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].item.OnParry(items[i].stacks);
        }
    }

    public void Hit(GameObject enemy)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].item.OnHit(enemy, items[i].stacks);
        }
    }

    public void Death(GameObject enemy)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].item.OnKill(enemy, items[i].stacks);
        }
    }
}


public class ItemListing
{
    public ItemListing(Item item, string name, int stacks)
    {
        this.item = item;
        this.name = name;
        this.stacks = stacks;
    }

    public string name;
    public Item item;
    public int stacks;
}