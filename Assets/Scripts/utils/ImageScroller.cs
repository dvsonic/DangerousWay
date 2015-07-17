using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ImageScroller : MonoBehaviour, IPointerDownHandler
{
    private List<Transform> itemList;
    private Transform rightItem;
    public float speed;
    public float width;
	// Use this for initialization
	void Awake () {
        itemList = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            rightItem = transform.GetChild(i);
            itemList.Add(rightItem);
        }
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].GetComponent<RectTransform>().localPosition.x + width / 2 + GetComponent<RectTransform>().localPosition.x < -Screen.width/2)
            {
                int lastIndex = i - 1;
                if (lastIndex < 0)
                    lastIndex = itemList.Count - 1;
                itemList[i].gameObject.GetComponent<RectTransform>().localPosition = itemList[lastIndex].GetComponent<RectTransform>().localPosition + new Vector3(width, 0, 0);
                rightItem = itemList[i];
            }
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_IOS
        Application.OpenURL("https://itunes.apple.com/artist/lv1/id448310825");
#else
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Lv1");
#endif
    }
}
