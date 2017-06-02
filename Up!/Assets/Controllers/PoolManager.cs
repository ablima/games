using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {

    private static volatile PoolManager instance;
    private Dictionary<string, ObjectPool> objectPools;
    private static object syncRoot = new System.Object();

    private PoolManager(){

        this.objectPools = new Dictionary<string, ObjectPool>();

    }

    public static PoolManager Instance{

        get{

            if (instance == null){
                lock (syncRoot){
                    if (instance == null)
                        instance = new PoolManager();
                }
            }

            return instance;

        }

    }

    public bool CreatePool(GameObject obj, int initialSize, int maxSize){

        if (PoolManager.Instance.objectPools.ContainsKey(obj.name)){
            return false;
        }else{
            ObjectPool pool = new ObjectPool(obj, initialSize, maxSize);
            PoolManager.Instance.objectPools.Add(obj.name, pool);
            return true;
        }

    }

    public GameObject GetObject(string name){

        return PoolManager.Instance.objectPools[name].GetObject();

    }

}
