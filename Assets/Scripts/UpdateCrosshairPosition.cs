using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCrosshairPosition : MonoBehaviour {
    public GameObject cameraContainer;
    public Camera camera;
    public Vector3 worldSpaceCrosshairPosition;
    public LayerMask layerMask;
    
    // Start is called before the first frame update
    void Start() {
        worldSpaceCrosshairPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate() {
        updateCrosshair();
    }

    private void updateCrosshair() {
        RaycastHit hit;
        if (Physics.Raycast(cameraContainer.transform.position, cameraContainer.transform.forward, out hit,
            Mathf.Infinity, layerMask)) {
            Debug.DrawRay(cameraContainer.transform.position, cameraContainer.transform.forward, Color.yellow);
            worldSpaceCrosshairPosition = hit.point;
            var screenPoint = camera.WorldToScreenPoint(hit.point);
            // Debug.Log(screenPoint);
            transform.position = new Vector2(screenPoint.x, screenPoint.y);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(worldSpaceCrosshairPosition, .25f);
    }
}
