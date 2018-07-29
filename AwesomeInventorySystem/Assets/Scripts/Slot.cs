using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inv;



    // Use this for initialization
    void Start () {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}

    // Slot 컴포넌트가 붙어있는 오브젝트에 마우스를 드래그하다가 마우스를 땠을 때
    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        // droppedItem은 마우스/터치로 끌고 다니다가 딱 놓아버린 아이템을 의미한다.
        Debug.Log(inv.items[id].ID);
        // eventData.pointerDrag : 드래그 되고 있는 오브젝트
        if (inv.items[id].ID == -1) // 비어있는 슬롯위에서
        {
            inv.items[droppedItem.slot] = new Item(); // 원래 있던 아이템 위치를 비워준다.
            // 다시 말하면 id를 -1로 바꿔서 비어있는 아이템으로 만든다.
            inv.items[id] = droppedItem.item;
            droppedItem.slot = id;  // 떨어뜨리는 아이템의 슬롯을 이 슬롯으로 바꾼다.
        }
        else if(droppedItem.slot != id) // 동일한 칸에 다시 드롭하는 경우는 제외한다.
        // 아이템이 들어있는 슬롯 위에서 
        {

            // A아이템을 슬롯에서 드래그해서 B아이템 슬롯위치에 드롭하는 상황이다.
            // 두아이템 정보를 스왑한다.
            // B아이템 위치 변경
            Transform item = this.transform.GetChild(0); // 원래 슬롯안에 아이템에 접근
            item.GetComponent<ItemData>().slot = droppedItem.slot; 
            item.transform.SetParent(inv.slots[droppedItem.slot].transform);
            item.transform.position = inv.slots[droppedItem.slot].transform.position;

            // A아이템 위치 변경
            droppedItem.slot = id;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            // 아이템 리스트 정보 스왑
            inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inv.items[id] = droppedItem.item;

        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
