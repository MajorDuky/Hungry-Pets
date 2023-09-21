using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private List<GameObject> objectsToPool;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;
    public int amountToPool { get { return _amountToPool; } }

    // Start is called before the first frame update
    void Start()
    {
        objectsToPool = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject pulledObject = (GameObject)Instantiate(_objectToPool);
            pulledObject.SetActive(false);
            objectsToPool.Add(pulledObject);
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
