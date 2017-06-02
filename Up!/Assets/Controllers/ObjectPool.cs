using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Classe da pool de Objetos
*************************************************/

public class ObjectPool {

    private List<GameObject> pool;
    private GameObject poolObj;
    private int maxSize;
    private int initialSize;

    //A pool de objetos guarda instâncias de um objetos para serem reutilizadas pelo jogo, quando necessários.
    //Ao instanciar uma pool, a mesma cria um determinado número de objetos prontos para serem utilizados,
    //e ativa-os a medida que forem requisitados.
    public ObjectPool(GameObject obj, int initialSize, int maxSize){

        pool = new List<GameObject>();

        //Inicia uma pool criando um número "initialSize" de objetos e guardando-os na pool.
        //Todos objetos criados iniciam-se desativados.
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

    //Caso seja solicitado um objeto dessa pool, retorna um objeto da pool.
    public GameObject GetObject(){

        //Verifica se há algum objeto para ser utilizado na pool, ou seja, que está desativado.
        //Se sim, ativa o objeto e o retorna.
        for (int i = 0; i < pool.Count; i++){

            if (pool[i].activeSelf == false){
                pool[i].SetActive(true);
                return pool[i];
            }

        }

        //Caso todos os objetos criados estejam sendo usados no momento, e se a pool não estiver no tamanho
        //máximo determinado, cria um novo objeto, adiciona-o na pool e retorna o mesmo.
        if (this.maxSize > this.pool.Count){

            GameObject nObj = GameObject.Instantiate(poolObj, Vector3.zero, Quaternion.identity) as GameObject;
            nObj.SetActive(true);
            pool.Add(nObj);

            return nObj;

        }

        return null;

    }

    //Função para desativar todos os objetos da pool. Utilizado ao carregar uma nova cena.
    public void desactivateAll(){

        for (int i = 0; i < pool.Count; i++){
            pool[i].SetActive(false);
        }

    }

}