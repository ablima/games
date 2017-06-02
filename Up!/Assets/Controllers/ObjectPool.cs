using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool {

    private List<GameObject> pool;
    private GameObject poolObj;
    private int maxSize;
    private int initialSize;

    public ObjectPool(GameObject obj, int initialSize, int maxSize){

        pool = new List<GameObject>();

        for (int i = 0; i < initialSize; i++){

            GameObject newObj = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
            newObj.SetActive(false);
            pool.Add(newObj);
            GameObject.DontDestroyOnLoad(newObj);

        }

        this.maxSize = maxSize;
        this.poolObj = obj;
        this.initialSize = initialSize;

    }

    public GameObject GetObject(){

        for (int i = 0; i < pool.Count; i++){

            if (pool[i].activeSelf == false){
                pool[i].SetActive(true);
                return pool[i];
            }

        }

        if (this.maxSize > this.pool.Count){

            GameObject nObj = GameObject.Instantiate(poolObj, Vector3.zero, Quaternion.identity) as GameObject;
            nObj.SetActive(true);
            pool.Add(nObj);

            return nObj;

        }

        return null;

    }

}