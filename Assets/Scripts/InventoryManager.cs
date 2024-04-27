using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public void AddItem(Item item)
    {
        inventory.Add(item);
        //Update UI
    }
    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        //Update UI
    }
}
