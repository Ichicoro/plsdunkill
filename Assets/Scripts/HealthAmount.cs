using System.Collections;
using System.Collections.Generic;
using Fragsurf.Movement;
using TMPro;
using UnityEngine;

public class HealthAmount : MonoBehaviour {
    public SurfCharacter player;
    private TextMeshProUGUI tmp;
    
    // Start is called before the first frame update
    void Start() {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        tmp.SetText($"{player.health}");
    }
}
