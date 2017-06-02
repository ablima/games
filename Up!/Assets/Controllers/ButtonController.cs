using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public void loadLevel(string sceneName){

        Application.LoadLevel(sceneName);

    }

    public void exit(){

        Application.Quit();

    }

}
