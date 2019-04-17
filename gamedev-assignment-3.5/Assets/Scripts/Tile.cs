using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int type;
    public Color[] colors;

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void SetTile(int t) {
        type = t;
        GetComponent<SpriteRenderer>().color = colors[type];
    }

    void OnMouseOver() {
        if(type == 2) {
            this.SetTile(3);
        }
    }

    void OnMouseExit() {
        if(type == 3) {
            this.SetTile(2);
        }
    }
}
