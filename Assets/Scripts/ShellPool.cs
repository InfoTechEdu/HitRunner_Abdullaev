using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPool : MonoBehaviour
{
    public static ShellPool SharedInstance;
    public List<Rigidbody> pooledObjects;
    public Rigidbody shellPrefab;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<Rigidbody>();
        Rigidbody tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(shellPrefab);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public Rigidbody GetPooledShell()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
