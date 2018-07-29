using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IDropHandler
{
    public int slot_Id;

    private Inventory_Controller inventory;

    
    private void Awake()
    {
        inventory = GameObject.Find("Inventory_Controller").GetComponent<Inventory_Controller>();
    }



    public void OnDrop(PointerEventData eventData)
    {
        Item drop_Item = eventData.pointerDrag.GetComponent<Item>();

        if(inventory.item[slot_Id].id == -1)
        {
            inventory.item[drop_Item.slot_Location] = new Items_Info();
            inventory.item[slot_Id] = drop_Item.item_Ability;
            drop_Item.slot_Location = slot_Id;
        }
        else
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<Item>().slot_Location = drop_Item.slot_Location;
            item.transform.SetParent(inventory.slot[drop_Item.slot_Location].transform);
            item.transform.localPosition = Vector2.zero;

            drop_Item.slot_Location = slot_Id;
            drop_Item.transform.SetParent(this.transform);
            drop_Item.transform.localPosition = Vector2.zero;

            inventory.item[drop_Item.slot_Location] = item.GetComponent<Item>().item_Ability;
            inventory.item[slot_Id] = drop_Item.item_Ability;
        }
    }
} // class









