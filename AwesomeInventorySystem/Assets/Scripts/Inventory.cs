using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel; // 인벤토리 패널 - 타이틀 패널과 슬롯 패널이 들어있다.
    GameObject slotPanel; // 슬롯 패널 (슬롯이 20개 들어간다)
    ItemDatabase database; // 아이템이 종류별로 정보가 정리되어 있다.
    public GameObject inventorySlot; // 슬롯 하나 (안에 아이템이 들어간다.)
    public GameObject inventoryItem; // 슬롯에 들어있는 아이템
                                     // ItemData 클래스 컴포넌트를 갖고 있다. 
                                     // 이놈은 자식이 있는데 Stack Amount 라는 이름이다. 
      // Stack Amount에 text 컴포넌트가 있어서 여기에 아이템 개수를 표시할 수 있다.
                                    
    int slotAmount;
    public List<Item> items = new List<Item>(); // 16개의 아이템이 생성된다.
    // pulblic List의 원소가 클래스라면 인스펙터에 표시되지 않는다.
    public List<GameObject> slots = new List<GameObject>(); // 16대의 슬롯이 생성된다.
    // public List의 원소가 클래스가 아니라면 인스펙터에 표시된다.
    void Start()
    {
        database = GetComponent<ItemDatabase>();
        // 이 Inventory 클래스가 붙어 있는 게임오브젝트에서 ItemDatabase 클래스를 받아온다.

        slotAmount = 16;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;
        // 이 Find 함수는 자식에서 "Slot Panel"이라는 이름의 오브젝트를 찾는다.
        for(int i =0; i<slotAmount; i++) 
            // 아이템리스트에 아이템을 20개 넣고 빈슬롯을 20개씩 만든다.
        {
            items.Add(new Item()); // id = -1 로 아이템이 슬롯수만큼 채워진다.
            slots.Add(Instantiate(inventorySlot));  // 슬롯을 20개 만든다.
            slots[i].GetComponent<Slot>().id = i; // 슬롯에 id가 생겼다.
            slots[i].transform.SetParent(slotPanel.transform);
            // 슬롯을 슬롯패널에 자식으로 넣어준다.
            // 슬롯 패널에 Grid Layout Group 컴포넌트가 있어서 알아서 정렬된다.
        }
        // item 클래스는 itemDatabase.cs 에 있다.

        // 테스트 - 정상동작한다.
        // Instantiate(inventoryItem, inventoryPanel.transform);


        AddItem(0);
        AddItem(0); // 0번은 슬롯에 하나만 넣을 수 있는 아이템이다.
        AddItem(1); // 1번은 슬롯에 여러개 넣을 수 있는 아이템이다.
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id); 
        // 아이템 데이터베이스에서 id가 같은 아이템 정보를 불러온다.
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))
            // 아이템이 슬롯에 여러개 들어갈 수 있고 같은 아이템이 들어있는 슬롯이 있다면
        {
            for (int i = 0; i < items.Count; i++) // 16번 돌린다.
            {
                if (items[i].ID == id) // X번 아이템이 슬롯중에 있다면
                {
                    // slot에서 자식에 접근해서 ItemData 컴포넌트에 접근한다.
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    // 슬롯에는 이미 같은 아이템이 하나 들어 있다.
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = 
                        data.amount.ToString(); // 자식에 접근하는 방법을 보고 계십니다.
                    break;
                }
            }
        }
        else // 슬롯에 여러개 못들어가는 아이템일 때
        {
            for (int i = 0; i < items.Count; i++) // 아이템 리스트 개수 = 슬롯 개수 = 16
            {
                if (items[i].ID == -1) // 아이템이 비어있는 곳에
                {
                    items[i] = itemToAdd; // 아이템 리스트의 비어있는 첫번째 칸에 넣는다.
                    GameObject itemObj = Instantiate(inventoryItem);
                       // ,slots[i].transform);
                    // InventoryItem 프리팹 추가
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    // 슬롯의 자식으로 만들고
                    itemObj.transform.localPosition = Vector2.zero;
                    // 슬롯 정중앙에 배치한다
                    //itemObj.transform.position = Vector2.zero;
                    // 이것으로 하면 슬롯 좌상단에 배치된다.
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    // 그림을 바꿔준다.
                    itemObj.name = itemToAdd.Title; // 이름도 바꿔준다.
                    break;
                }
            }
        }


    }
    // 어떤 아이템이 인벤토리에 있으면 true 없으면 false 리턴
    bool CheckIfItemIsInInventory(Item item)  // Items 리스트 안에 넣으려는 Item과 같은 ID가 있으면 true 없으면 false
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
                return true; // ID가 같은 아이템이 있다.
        }
        return false; // ID가 같은 아이템이 없다.
    }
}
