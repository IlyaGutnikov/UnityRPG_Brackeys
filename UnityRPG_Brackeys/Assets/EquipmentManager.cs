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

    public SkinnedMeshRenderer TargetMesh;

    private Equipment[] currentEquipments;
    private SkinnedMeshRenderer[] currentMeshRenderers;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);

    public OnEquipmentChange OnEquipmentChanged;

    void Start()
    {
        var numOfSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipments = new Equipment[numOfSlots];
        currentMeshRenderers = new SkinnedMeshRenderer[numOfSlots];
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
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newEquipment.MeshRenderer);
        newMesh.transform.parent = TargetMesh.transform;

        newMesh.bones = TargetMesh.bones;
        newMesh.rootBone = TargetMesh.rootBone;
        currentMeshRenderers[slotIndex] = newMesh;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipments[slotIndex] == null)
        {
           return; 
        }

        if (currentMeshRenderers[slotIndex] == null)
        {
            return;
        }

        Destroy(currentMeshRenderers[slotIndex].gameObject);

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
