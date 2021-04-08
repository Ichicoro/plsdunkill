using System;
using Fragsurf.Movement;
using Interfaces;
using Mirror;
using UnityEngine;

public class Elevator: NetworkBehaviour, ISingleActionReceiver {
    [SyncVar] public bool active = false;

    // Update is called once per frame
    private void Update() {
        if (isServer && active/* && isLocalPlayer*/) {
            transform.position += Vector3.up * (2 * Time.deltaTime);
            var casts = Physics.BoxCastAll(gameObject.transform.position, new Vector3(4f, 4f, 4f), Vector3.up);
            foreach (var raycastHit in casts) {
                Debug.DrawRay(gameObject.transform.position, raycastHit.point);
                Debug.Log("something inside");
                SurfCharacter surf = raycastHit.collider.GetComponent<SurfCharacter>();
                if (surf != null) {
                    surf.AddPosition(Vector3.up * (Time.deltaTime * 2));
                }
            }
        }
    }
    

    private void OnCollisionEnter(Collision other) {
        if (!isServer && !active) return;

        Debug.Log("Well well");
        
        SurfCharacter surf = other.collider.GetComponent<SurfCharacter>();
        if (surf != null) {
            surf.AddPosition(Vector3.up * (Time.deltaTime * 2));
        }
    }

    // private void OnCollisionStay(Collision other) {
    //     Debug.Log("Well well");
    //
    //     if (!isServer && !isLocalPlayer && !active) return;
    //
    //     Debug.Log("Well well 2");
    //     
    //     SurfCharacter surf = other.collider.GetComponent<SurfCharacter>();
    //     if (surf != null) {
    //         surf.RpcAddVelocity(Vector3.up * (Time.deltaTime * 3));
    //     }
    // }
    
    void OnCollisionStay(Collision collisionInfo) {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts) {
            Debug.Log("collision");
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }

    /* private void OnCollisionEnter(Collision other) {
        var surf = other.collider.GetComponent<SurfCharacter>();
        if (surf != null) {
            _playersOnTheElevator.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other) {
        var surf = other.collider.GetComponent<SurfCharacter>();
        if (surf != null) {
            _playersOnTheElevator.Remove(other.gameObject);
        }
    } */
    
    public void ExecuteAction() {
        if (!isServer) return;
        Debug.Log("Btn pressed");
        active = !active;
    }
}
