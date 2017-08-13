using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name = "New Item";

    public Sprite Icon = null;
    public bool IsDefaultItem = false;


}
