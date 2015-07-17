using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockFactory : MonoBehaviour {

	// Use this for initialization
    public GameObject[] blockList;
    public GameObject[] bonusList;
    public Transform lastBlock;
    public float gap;

    private List<GameObject> createdObject;

    private GameObject _initBlock;
    void Awake()
    {
        _initBlock = lastBlock.gameObject;
    }
    private static BlockFactory _instant;
	void Start () {
        createdObject = new List<GameObject>();
        _instant = this;
	}

    public static BlockFactory getInstance()
    {
        return _instant;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
    }


    public void CreatBlock()
    {
        GameData.blockNum ++;

        Object prefab;
        prefab = blockList[Random.Range(0, blockList.Length)];

        GameObject obj = Instantiate(prefab) as GameObject;
        createdObject.Add(obj);

        obj.transform.position = new Vector3(lastBlock.transform.position.x+gap, lastBlock.transform.position.y);
        lastBlock.GetComponent<BlockController>().nextBlock = obj;
        lastBlock = obj.transform;
        obj.GetComponent<Rigidbody2D>().gravityScale += 0.02f * Mathf.FloorToInt(GameData.blockNum / 5);

        if (Random.Range(0, 1.0f) < 0.2f)
        {
            GameObject bonusPrefab = bonusList[Random.Range(0, bonusList.Length)];
            GameObject bonus = Instantiate(bonusPrefab) as GameObject;
            bonus.transform.SetParent(obj.transform);
            bonus.transform.localPosition = new Vector3(0, 0.5f);
            createdObject.Add(bonus);
        }

    }

    public void Reset()
    {
        for (int i = 0; i < createdObject.Count; i++)
        {
            Destroy(createdObject[i]);
        }
        createdObject.Clear();
        GameData.blockNum = 0;
        lastBlock = _initBlock.transform;
    }

}
