using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot EquipmentSlot;
    public SkinnedMeshRenderer MeshRenderer;
    public EquipmentMeshRegions[] CoveredMeshRegiones;

    public int ArmorModifier;
    public int DamageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }
}
