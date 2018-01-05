using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Equipment[] DefaultItems;

    private Equipment[] currentEquipments;
    private SkinnedMeshRenderer[] currentMeshRenderers;

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);

    public OnEquipmentChange OnEquipmentChanged;

    void Start()
    {
        var numOfSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipments = new Equipment[numOfSlots];
        currentMeshRenderers = new SkinnedMeshRenderer[numOfSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int) newEquipment.EquipmentSlot;
        var oldItem = Unequip(slotIndex);

        if (OnEquipmentChanged != null)
        {
            OnEquipmentChanged.Invoke(newEquipment, oldItem);
        }

        SetEquipmentBlendShapes(newEquipment, 100);

        currentEquipments[slotIndex] = newEquipment;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newEquipment.MeshRenderer);
        newMesh.transform.parent = TargetMesh.transform;

        newMesh.bones = TargetMesh.bones;
        newMesh.rootBone = TargetMesh.rootBone;
        currentMeshRenderers[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipments[slotIndex] == null)
        {
           return null; 
        }

        if (currentMeshRenderers[slotIndex] == null)
        {
            return null;
        }

        Destroy(currentMeshRenderers[slotIndex].gameObject);

        var oldItem = currentEquipments[slotIndex];
        SetEquipmentBlendShapes(oldItem, 0);
        Inventory.Instanse.AddItem(oldItem);

        if (OnEquipmentChanged != null)
        {
            OnEquipmentChanged.Invoke(null, oldItem);
        }

        currentEquipments[slotIndex] = null;

        return oldItem;
    }

    public void UnequipAll()
    {
        if (currentEquipments != null && currentEquipments.All(x => x.IsDefaultItem))
        {
            return;
        }

        for (int i = 0; i < currentEquipments.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    private void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (var equipmentMeshRegion in item.CoveredMeshRegiones)
        {
            TargetMesh.SetBlendShapeWeight((int) equipmentMeshRegion, weight);
        }
    }

    private void EquipDefaultItems()
    {
        foreach (var equipment in DefaultItems)
        {
            Equip(equipment);
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
