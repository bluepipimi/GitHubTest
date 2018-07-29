using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Analytics;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

public class IAP_Manager : MonoBehaviour, IStoreListener {

    public Text m_Text;

    // 상점 제어용 클래스 변수
    public static IStoreController m_StoreController;
    public static IExtensionProvider m_ExtensionProvider;

    public static Product[] m_List_Product = new Product[0];

    // 상점 물품 리스트
    public const string m_productId_01 = "shop_1000_gold";
    public const string m_productId_02 = "shop_10000_gold";
   

    List<Shop_Item> m_List_Item_Button = new List<Shop_Item>();

    [SerializeField]
    GameObject m_Item_Button;

    [SerializeField]
    Transform m_Viewport;

    // Use this for initialization
    void Start () {

        InitializeShop();
        

       // m_List_Item_Button = transform.GetComponentsInChildren<Shop_Item>();
        


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    // 상점 제어 관련 클래스 변수가 null 여부를 확인
    bool IsInitialized()
    {
        return (m_StoreController != null && m_ExtensionProvider != null);
    }

    // 상점에 대한 초기화 진행
    void InitializeShop()
    {
        // 상점 제어 클래스 변수가 null 이 아니면 중단
        if(IsInitialized())
        {
            Log_Controller.Log("상점 정보 있어서 취소");
            return;
        }

        Log_Controller.Log("상점초기화 시작");

        var t_Module = StandardPurchasingModule.Instance();

        ConfigurationBuilder t_Builder = ConfigurationBuilder.Instance(t_Module);
       
        // 1번 상품 정보 입력 (게임에서 관리할 ID, 처리 방식 (반복 구매가능, 1회 구매 제한 등), 각 마켓별 ID 정보)
        t_Builder.AddProduct(m_productId_01, ProductType.Consumable,
            new IDs
                {
                    // 구글 마켓 등록 아이디 및 구글마켓이름으로 설정
                    { m_productId_01, GooglePlay.Name }
                    // 앱스토어 등록 아이디 및 앱스토어마켓이름으로 설정
                    //,{m_productId_01, AppleAppStore.Name }
                }
            
            );

        t_Builder.AddProduct(m_productId_02, ProductType.Consumable,
            new IDs
                {
                    {m_productId_02, GooglePlay.Name }
                }
            );


        // 상속 설정된 IStoreListener 를 this 로 첫번째 매개변수로 지정하고 
        // 위에서 임시 선언한 ConfigurationBuilder t_Builder 두번째 매개변수로 지정
        UnityPurchasing.Initialize(this, t_Builder);
    }

    // 초기화가 완료되면 리턴되는 함수를 통해 IStoreController 및 IExtensionProvider 를 받아서 설정
    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Log_Controller.Log("상점초기화 성공");
        // 상품 구매 처리 등에 사용
        m_StoreController = controller;
        m_ExtensionProvider = extensions;

        // 상품 ID 목록을 받아서 저장
        m_List_Product = m_StoreController.products.all;
        ListChk();
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
    {
        Log_Controller.Log("상점초기화 실패");
    }

    // 상품 구매 성공
    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs e)
    {
        Log_Controller.Log("ProcessPurchase 처리 시작");

        bool t_IsBuy = true;

#if UNITY_ANDROID || UNITY_IOS
        var t_Validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
        Log_Controller.Log("ProcessPurchase 처리 시작-10");
        try
        {
            Log_Controller.Log("ProcessPurchase 처리 시작-11");
            var t_Result = t_Validator.Validate(e.purchasedProduct.receipt);
            m_Text.text += " 정상 구매에 따른 정보 \n";

            Log_Controller.Log("정상 구매에 따른 정보:");
            foreach (IPurchaseReceipt productReceipt in t_Result)
            {
                
                m_Text.text += productReceipt.productID.ToString() + '\n';
                m_Text.text += productReceipt.purchaseDate.ToString() + '\n';
                m_Text.text += productReceipt.transactionID.ToString() + '\n';
      
                Log_Controller.Log(productReceipt.productID.ToString());
                Log_Controller.Log(productReceipt.purchaseDate.ToString());
                Log_Controller.Log(productReceipt.transactionID.ToString());

                m_Text.text += Log_Controller.s_LogPath + '\n';

                Analytics.Transaction(productReceipt.productID, e.purchasedProduct.metadata.localizedPrice, e.purchasedProduct.metadata.isoCurrencyCode, productReceipt.transactionID, null);
                m_Text.text += "전송 완료 \n";
                //Analytics.Transaction(productReceipt.productID, e.purchasedProduct.metadata.localizedPrice, e.purchasedProduct.metadata.isoCurrencyCode, e.purchasedProduct.receipt, productReceipt.transactionID.ToString());

            }
        }
        catch (IAPSecurityException)
        {
            Log_Controller.Log("비정상 구매로 확인됨");
            t_IsBuy = false;
        }
#endif
        Log_Controller.Log("ProcessPurchase 처리 시작-20");
        if (t_IsBuy)
        {
            // 정상 구매 성공시 처리
            Log_Controller.Log("정상 구매 확인되어 아이템 지급 처리");
            

        }

        return PurchaseProcessingResult.Complete;
    }

    // 상품 구매 실패
    void IStoreListener.OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Log_Controller.Log("상품 구매 실패");
    }

    void ListChk()
    {
        for (int i = 0; i < m_List_Product.Length; i++)
        {
            var t_Item_Button = Instantiate(m_Item_Button);
            t_Item_Button.transform.SetParent(m_Viewport);

            m_List_Item_Button.Add(t_Item_Button.GetComponent<Shop_Item>());

            m_List_Item_Button[i].Set_Index_Product(i);
        }
    }
}
