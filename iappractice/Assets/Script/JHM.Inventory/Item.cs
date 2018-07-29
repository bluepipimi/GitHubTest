using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler{

    public Items_Info item_Ability;

    public int slot_Location;

    public int amount;

    private Inventory_Controller inventory;

    
    private void Awake()
    {
        amount = 1;
        inventory = GameObject.Find("Inventory_Controller").GetComponent<Inventory_Controller>();
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item_Ability != null)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item_Ability != null)
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item_Ability != null)
        {
            this.transform.SetParent(inventory.slot[slot_Location].transform);
            this.transform.localPosition = Vector2.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
    




} // class










