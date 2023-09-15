using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    private List<GameObject> objectsToPool;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        objectsToPool = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject pulledObject = (GameObject)Instantiate(_objectToPool);
            pulledObject.SetActive(false);
            objectsToPool.Add(pulledObject);
            pulledObject.transform.SetParent(this.transform);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectsToPool.Count; i++)
        {
            if (!objectsToPool[i].activeInHierarchy)
            {
                return objectsToPool[i];
            }
        }
        return null;
    }

}
