using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;

public class Shop_Item : MonoBehaviour {

    // 현재 상품의 정보
    int m_Index_product = -1;
    bool m_IsViewUpdata = false;

    [SerializeField]
    Text m_Id_Text;
    [SerializeField]
    Text m_Info_Text;
    [SerializeField]
    Text m_Info_Buy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ViewInfo();

    }

    public void Set_Index_Product(int _Index)
    {
        m_Index_product = _Index;
        m_IsViewUpdata = true;
    }

    void ViewInfo()
    {
        if(m_IsViewUpdata == true)
        {
            m_Id_Text.text = IAP_Manager.m_List_Product[m_Index_product].metadata.localizedTitle.ToString();
            m_Info_Text.text = IAP_Manager.m_List_Product[m_Index_product].metadata.localizedDescription.ToString() ;
            m_Info_Buy.text = IAP_Manager.m_List_Product[m_Index_product].metadata.localizedPriceString.ToString();

            Log_Controller.Log(m_Id_Text.text.ToString() + "상품표시");

            m_IsViewUpdata = false;
        }
    }

    public void Buy_Item()
    {
        foreach (var i in IAP_Manager.m_List_Product)
        {
            Log_Controller.Log("구매시작 상품정보 들" + i.ToString() + i.metadata.localizedTitle.ToString());
        }
        

        Log_Controller.Log(IAP_Manager.m_List_Product[m_Index_product].metadata.localizedTitle.ToString() + "상품구매시도");

        try
        {
            IAP_Manager.m_StoreController.InitiatePurchase(IAP_Manager.m_List_Product[m_Index_product]);
            Log_Controller.Log(m_Index_product.ToString() + "상품구매시도 중 오류 없음");
        }
        catch(Exception e)
        {
            Log_Controller.Log("상품구매시도 중 오류 발생" +'\n' + e);
        }


        Log_Controller.Log(IAP_Manager.m_List_Product[m_Index_product].ToString() + "상품구매시도3");
    }
}
