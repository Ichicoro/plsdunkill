using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Button: NetworkBehaviour {
    void Start() {
        
    }


    void Update() {
        
    }

    void CalledOnClick() {

    }

    [Command(ignoreAuthority = true)]
    public void ExecuteAction() {
        CalledOnClick();
    }
}
