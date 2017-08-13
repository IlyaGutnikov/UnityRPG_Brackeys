using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instanse;

    public int Slots = 20;

    private List<Item> _items = new List<Item>();

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
            if (_items.Count >= Slots)
            {
                Debug.Log("Inventory is full");
                return false;
            }

            _items.Add(item);

            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);

        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }
}
