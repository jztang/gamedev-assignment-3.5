using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int row;
    public int col;
    public int type; // Index in colors[]
    public Color[] colors; // [white, black, green, green hover]

    private GameGrid grid;
    //public Animator anim;

    private void Start() {
        grid = GameObject.Find("GameGrid").GetComponent<GameGrid>();
        //anim = GetComponent<Animator>();
    }

    private void Update() {
        //anim.SetBool("check", check);
    }

    public void SetTile(int row, int col, int t) {
        this.row = row;
        this.col = col;
        SetTile(t);
    }

    public void SetTile(int t) {
        this.type = t;
        GetComponent<SpriteRenderer>().color = colors[type];
    }

    private void OnMouseOver() { // Highlight empty tile on hover
        if(type == 2 && !grid.animating) this.SetTile(3);
    }

    private void OnMouseExit() { // Un-highlight empty tile after hover
        if(type == 3 && !grid.animating) this.SetTile(2);
    }

    private void OnMouseDown() { // Player clicks to place a tile
        if(type > 1 && !grid.animating) grid.PlaceTile(row, col);
    }

    /*public void FillWhite() {
        anim.Play("FillWhite");
    }

    public void FillBlack() {
        anim.Play("FillBlack");
    }

    public void FlipWhiteBlack() {
        anim.Play("FlipWhiteBlack");
    }

    public void FlipBlackWhite() {
        anim.Play("FlipBlackWhite");
    }*/
}
