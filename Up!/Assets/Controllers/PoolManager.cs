using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Classe de PoolManager
*************************************************/

public class PoolManager {

    private static volatile PoolManager instance;
    private Dictionary<string, ObjectPool> objectPools;
    private static object syncRoot = new System.Object();

    //Esta classe é responsável pelo controle de pools existentes no jogo. Caso um controller deseje instanciar
    //algum objeto de alguma pool, deve requisitar ao PoolManager que irá buscar o objeto requisitado.
    private PoolManager(){

        this.objectPools = new Dictionary<string, ObjectPool>();

    }

    //Retorna a classe PoolManager para algum controller que o requesite. Caso ainda não esteja iniciado,
    //cria uma nova instância da classe PoolManager
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

    //Cria uma nova pool de um determinado objeto. O parâmetro "initialSize" determina quantos objetos vão ser criados
    //prontos para uso pela pool, e o parâmetro "maxSize" determina o número máximo de objetos criados nesta pool.
    public bool CreatePool(GameObject obj, int initialSize, int maxSize){

        if (PoolManager.Instance.objectPools.ContainsKey(obj.name)){
            return false;
        }else{
            ObjectPool pool = new ObjectPool(obj, initialSize, maxSize);
            PoolManager.Instance.objectPools.Add(obj.name, pool);
            return true;
        }

    }

    //Quando um certo objeto é requisitado, busca pela pool deste objeto, e solicita um objeto da pool,
    //retornando o mesmo
    public GameObject GetObject(string name){

        return PoolManager.Instance.objectPools[name].GetObject();

    }

    //Desativa todos os objetos de todas as pools criadas. Usado ao carregar uma nova cena.
    public void desactivateAll(){

        foreach(KeyValuePair<string, ObjectPool> obj in objectPools)
            obj.Value.desactivateAll();

    }

}
