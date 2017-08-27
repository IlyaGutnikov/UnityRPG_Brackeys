using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;
    private InventorySlot[] _inventorySlots;
    public Transform itemsParens;

    public GameObject InventoryGameObject;

    // Use this for initialization
    void Start ()
	{
	    _inventory = Inventory.Instanse;
	    _inventory.OnItemChangedCallback += UpdateUI;
	    _inventorySlots = itemsParens.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetButtonDown("Inventory"))
	    {
            InventoryGameObject.SetActive(!InventoryGameObject.activeSelf);
	    }

	}

    void UpdateUI()
    {
        Debug.Log("Update UI");
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < _inventory.Items.Count)
            {
                _inventorySlots[i].AddItem(_inventory.Items[i]);
            }
            else
            {
                _inventorySlots[i].ClearSlot();
            }
        }
    }
}
