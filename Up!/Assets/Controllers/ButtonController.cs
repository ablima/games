using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************
Controller que oferece funções para os botões
do menu.
*************************************************/

public class ButtonController : MonoBehaviour {

    //Carrega uma nova cena
    public void loadLevel(string sceneName){

        Application.LoadLevel(sceneName);

    }

    //Sair do jogo
    public void exit(){

        Application.Quit();

    }

}
