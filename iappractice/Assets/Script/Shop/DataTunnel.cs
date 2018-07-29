using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데이터가 씬을 왔다갔다하는 것을 도와주는 터널 역할을 해준다.
// 장바구니라고도 생각할 수 있다.

public class DataTunnel : MonoBehaviour {

    private static List<Items_Info> itemInfos = new List<Items_Info>();

    public static List<Items_Info> ItemInfos // 프로퍼티
    {
        get { return itemInfos; }
    }

    private void Start()
    {
        //Debug.Log("DataTunnel의 아이템 개수 : " + itemInfos.Count);
    }

    public static bool AddItem(Items_Info itemInfo)
    {
        if(itemInfos.Count < 3)
        {
            itemInfos.Add(itemInfo);
            //Debug.Log("DataTunnel의 아이템 개수 : " + itemInfos.Count);
            return true;
        }
        //Debug.Log("DataTunnel의 아이템 개수 : " + itemInfos.Count);
        return false;

    }


    
}
