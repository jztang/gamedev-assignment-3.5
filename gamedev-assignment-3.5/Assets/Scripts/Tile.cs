using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int row;
    public int col;
    public int type;
    public Color[] colors;

    private GameGrid grid;

    private void Start() {
        grid = GameObject.Find("GameGrid").GetComponent<GameGrid>();
    }

    private void Update() {
        
    }

    public void SetTile(int row, int col, int t) {
        this.row = row;
        this.col = col;
        this.SetTile(t);
    }

    public void SetTile(int t) {
        type = t;
        GetComponent<SpriteRenderer>().color = colors[type];
    }

    private void OnMouseOver() {
        if(type == 2) { // Tile is grey
            this.SetTile(3); // Highlight tile

            if(Input.GetMouseButtonDown(0)) { // Grey tile is clicked
                grid.PlaceTile(row, col);
            }
        }
    }

    private void OnMouseExit() {
        if(type == 3) { // Tile is grey + highlighted
            this.SetTile(2); // Un-highlight tile
        }
    }
}
