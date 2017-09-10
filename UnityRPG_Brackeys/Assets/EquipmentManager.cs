using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    public static EquipmentManager Instance;

	// Use this for initialization
	void Awake ()
	{
	    Instance = this;
	}

    private Equipment[] currentEquipments;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);

    public OnEquipmentChange OnEquipmentChanged;

    void Start()
    {
        var intOfSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipments = new Equipment[intOfSlots];
    }

    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int) newEquipment.EquipmentSlot;

        Equipment oldItem = null;
        if (currentEquipments[slotIndex] != null)
        {
            oldItem = currentEquipments[slotIndex];
            Inventory.Instanse.AddItem(oldItem);
        }

        if (OnEquipmentChanged != null)
        {
            OnEquipmentChanged.Invoke(newEquipment, oldItem);
        }

        currentEquipments[slotIndex] = newEquipment;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipments[slotIndex] == null)
        {
           return; 
        }

        var oldItem = currentEquipments[slotIndex];
        Inventory.Instanse.AddItem(oldItem);

        if (OnEquipmentChanged != null)
        {
            OnEquipmentChanged.Invoke(null, oldItem);
        }

        currentEquipments[slotIndex] = null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipments.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();;
        }
    }

}
