using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Controller : MonoBehaviour {

    public static Inventory_Controller instance;

    private GameObject inventory_Panel;
    private GameObject inventory_Slot_Panel;

    private Item_Json_DataBase item_DataBase;

    public GameObject slot_Prefab;
    public GameObject item_Prefab;

    public List<Items_Info> item = new List<Items_Info>();
    public List<GameObject> slot = new List<GameObject>();

    private int slot_Count;

    private bool is_Stackable;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        // ---------------
        inventory_Panel = GameObject.Find("Inventory_Panel");
        inventory_Slot_Panel = inventory_Panel.transform.Find("Slot_Panel").gameObject;
        item_DataBase = GameObject.Find("Inventory_Json_Data").GetComponent<Item_Json_DataBase>();
    }

    private void Start()
    {
        is_Stackable = false;
        slot_Count = 20;
        
        for(int i = 0; i<slot_Count; i++)
        {
            item.Add(new Items_Info());
            slot.Add(Instantiate(slot_Prefab));
            slot[i].GetComponent<Slot>().slot_Id = i;
            slot[i].transform.SetParent(inventory_Slot_Panel.transform);
        }
        Default_Item_Add();
    }

    public void Add_Item(int _id)
    {
        Items_Info add_Item = item_DataBase.Search_For_Item(_id);

        if (add_Item.stackable)
        {
            if (!is_Stackable)
            {
                is_Stackable = true;
                // -----------------
                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].id == -1)
                    {
                        item[i] = add_Item;
                        GameObject item_Obj = Instantiate(item_Prefab);
                        item_Obj.GetComponent<Item>().item_Ability = add_Item;
                        item_Obj.GetComponent<Item>().slot_Location = i;
                        item_Obj.transform.SetParent(slot[i].transform);
                        item_Obj.transform.localPosition = Vector2.zero;
                        item_Obj.GetComponent<Image>().sprite = add_Item.item_Img;
                        item_Obj.name = add_Item.slug;
                        break;
                    }
                }
            }
            else
            {
                for(int i = 0; i < item.Count; i++)
                {
                    if(item[i].id == 30002)
                    {
                        GameObject potion = slot[i].transform.GetChild(0).gameObject;
                        potion.GetComponent<Item>().amount += 2;
                        potion.transform.GetChild(0).GetComponent<Text>().text = potion.GetComponent<Item>().amount.ToString();
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i].id == -1)
                {
                    item[i] = add_Item;
                    GameObject item_Obj = Instantiate(item_Prefab);
                    item_Obj.GetComponent<Item>().item_Ability = add_Item;
                    item_Obj.GetComponent<Item>().slot_Location = i;
                    item_Obj.transform.SetParent(slot[i].transform);
                    item_Obj.transform.localPosition = Vector2.zero;
                    item_Obj.GetComponent<Image>().sprite = add_Item.item_Img;
                    item_Obj.name = add_Item.slug;
                    break;
                }
            }
        }
    }

    private void Default_Item_Add()
    {
        Add_Item(30001);
        for (int i = 0; i < 3; i++)
        {
            Add_Item(30002);
        }      
    }
    
    
} // class










