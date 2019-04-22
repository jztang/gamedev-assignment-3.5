using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int row;
    public int col;
    public int type; // Index in colors[]
    public Color[] colors; // [white, black, green, green hover]
    private GameGrid grid;

    private void Start() {
        grid = GameObject.Find("GameGrid").GetComponent<GameGrid>();
    }

    public void SetTile(int row, int col, int t) { // Used when creating the tiles at the start
        this.row = row;
        this.col = col;
        SetTile(t);
    }

    public void SetTile(int t) { // Set the tile's type + change the sprite
        this.type = t;
        GetComponent<SpriteRenderer>().color = colors[type];
    }

    private void OnMouseOver() { // Highlight empty tile on hover
        if(type == 2) this.SetTile(3);
    }

    private void OnMouseExit() { // Un-highlight empty tile after hover
        if(type == 3) this.SetTile(2);
    }

    private void OnMouseDown() { // Player clicks to place a tile
        if(type > 1) grid.PlaceTile(row, col);
    }
}
