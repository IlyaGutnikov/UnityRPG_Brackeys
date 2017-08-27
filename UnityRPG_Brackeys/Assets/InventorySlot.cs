using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    public Button RemoveButton;
    public Button UseItemButton;

    private Item _item;

    void Awake()
    {
        RemoveButton.onClick.AddListener(OnRemoveButton);
        UseItemButton.onClick.AddListener(UseItem);
    }

    public void AddItem(Item item)
    {
        _item = item;
        Icon.sprite = item.Icon;
        Icon.enabled = true;
        RemoveButton.interactable = true;
    }

    public void ClearSlot()
    {
        _item = null;
        Icon.sprite = null;
        Icon.enabled = false;
        RemoveButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.Instanse.RemoveItem(_item);
    }

    public void UseItem()
    {
        if (_item == null)
        {
            Debug.Log("No item");
            return;
        }

        _item.Use();
    }
}
