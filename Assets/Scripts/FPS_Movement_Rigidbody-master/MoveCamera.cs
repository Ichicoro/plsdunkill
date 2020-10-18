using UnityEngine;
using Mirror;

public class MoveCamera : NetworkBehaviour {

    public Transform player;

    void Update() {
        if (base.hasAuthority)
            transform.position = player.transform.position;
    }
}
