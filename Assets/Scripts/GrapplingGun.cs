using Fragsurf.Movement;
using Mirror;
using UnityEngine;

public class GrapplingGun : NetworkBehaviour {
    
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private SurfCharacter _character;

    void Start() {
        _character = player.GetComponent<SurfCharacter>();
    }

    void Update() {
        if (isLocalPlayer) {
            if (Input.GetMouseButtonDown(0)) {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0)) {
                StopGrapple();
            }
        }

        if (isServer) {
            if (IsGrappling()) {
                _character.AddVelocity(joint.currentForce / 10f);
                Debug.DrawRay(_character.transform.position, joint.currentForce, Color.yellow);
            }
        }
    }



    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    [Command]
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            // _character.AddVelocity(Vector3.down * 1);
            
            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.95f; // 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            player.GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    [Command]
    void StopGrapple() {
        Destroy(joint);
        player.GetComponent<Rigidbody>().isKinematic = true;
        // _character.
    }



    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}