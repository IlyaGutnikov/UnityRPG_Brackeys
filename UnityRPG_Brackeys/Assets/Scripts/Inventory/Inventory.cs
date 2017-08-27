using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instanse;

    public int Slots = 16;

    [HideInInspector]
    public List<Item> Items = new List<Item>();

    public delegate void OnItemChanged();

    public OnItemChanged OnItemChangedCallback;

    void Awake()
    {
        if (Instanse != null)
        {
            Debug.LogError("Inventory alredy exist");
            return;
        }

        Instanse = this;
    }

    public bool AddItem(Item item)
    {
        if (!item.IsDefaultItem)
        {
            if (Items.Count >= Slots)
            {
                Debug.Log("Inventory is full");
                return false;
            }

            Items.Add(item);

            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);

        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }
}
