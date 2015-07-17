using UnityEngine;
using System.Collections;

public class BGImage : MonoBehaviour
{

    // Use this for initialization
    public enum FixType {NONE,BOTTOM_CAM,TOP_CAM,BOTTOM,TOP }
    public GameObject[] BGList;
    public string imageName;
    public int BGHeight = 1280;//用于底对齐
    public FixType type;
    private Vector3 extends;
    void Start()
    {
        if (imageName != "")
        {
            for (int i = 0; i < BGList.Length; i++)
            {
                extends = LoadBG(BGList[i]);
            }
        }
        else
        {
            extends = GetComponentInChildren<SpriteRenderer>().bounds.extents;
        }

        switch(type)
        {
            case FixType.BOTTOM_CAM:
                gameObject.transform.position = new Vector3(0, (extends.y - Camera.main.orthographicSize), 0);
                break;
            case FixType.TOP_CAM:
                gameObject.transform.position = new Vector3(0, (Camera.main.orthographicSize - extends.y), 0);
                break;
            case FixType.TOP:
                gameObject.transform.position = new Vector3(0, (BGHeight / 200.0f - extends.y), 0);
                break;
            case FixType.BOTTOM:
            default:
                gameObject.transform.position = new Vector3(0, (extends.y - BGHeight / 200.0f), 0);
                break;
        }
            
        if (BGList.Length == 3)
        {
            BGList[0].transform.localPosition = new Vector3(-extends.x * 2, 0, 0);
            BGList[2].transform.localPosition = new Vector3(extends.x * 2, 0, 0);
        }

    }

    void Reset()
    {
        if (BGList.Length == 3)
        {
            BGList[0].transform.localPosition = new Vector3(-extends.x * 2, 0, 0);
            BGList[1].transform.localPosition = new Vector3(0, 0, 0);
            BGList[2].transform.localPosition = new Vector3(extends.x * 2, 0, 0);
        }
    }

    private Vector3 LoadBG(GameObject bg)
    {
        SpriteRenderer sr = bg.GetComponent<SpriteRenderer>();
        Sprite image = Resources.Load("image/" + imageName, typeof(Sprite)) as Sprite;
        if (image)
            sr.sprite = image;
        else
        {
            sr.sprite = null;
            Debug.Log("error load:" + imageName);
        }
        return sr.bounds.extents;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
