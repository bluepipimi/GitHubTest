using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private const float CAMERA_TRANSITION_SPEED = 3.0f;

    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;
    public GameObject shopButtonPrefab;
    public GameObject shopButtonContainer;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt; // 카메라가 보길 바라는 위치

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels"); // Resources/Levels 폴더 아래에서 Sprite를 전부 불러와서 thmubnails 배열에 넣는다.
        foreach(Sprite thumnail in thumbnails) // 배열에 들어있는 개수만큼 반복한다.
        {
            GameObject container = Instantiate(levelButtonPrefab) as GameObject; // 버튼 프리팹 생성
            container.GetComponent<Image>().sprite = thumnail; // 버튼의 이미지를 thumnail로 바꾼다.
            container.transform.SetParent(levelButtonContainer.transform, false); // levelButtonContainer를 부모로 바꾼다.
        }

        Sprite[] textures = Resources.LoadAll<Sprite>("Player");
        foreach(Sprite texture in textures)
        {
            GameObject container = Instantiate(shopButtonPrefab) as GameObject; // 버튼 프리팹 생성
            container.GetComponent<Image>().sprite = texture; // 버튼의 이미지를 thumnail로 바꾼다.
            container.transform.SetParent(shopButtonContainer.transform, false); // levelButtonContainer를 부모로 바꾼다.
        }
    }

    private void Update()
    {
        if(cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation,
                cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
        }
    }

    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LookAtMenu(Transform menuTransform)
    {
        //Camera.main.transform.LookAt(menuTransform.position);
        cameraDesiredLookAt = menuTransform;
    }
}
