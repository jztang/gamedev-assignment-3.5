using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int row;
    public int col;
    public int type;
    public Color[] colors;

    private GameGrid grid;
    //public Animator anim;

    //public bool check;

    private void Start() {
        grid = GameObject.Find("GameGrid").GetComponent<GameGrid>();
        //anim = GetComponent<Animator>();
    }

    private void Update() {
        //check = false;
        //anim.SetBool("check", check);
    }

    public void SetTile(int row, int col, int t) {
        this.row = row;
        this.col = col;
        this.SetTile(t);
    }

    public void SetTile(int t) {
        this.type = t;
        GetComponent<SpriteRenderer>().color = colors[type];
    }

    private void OnMouseOver() { // Highlight grey tile on hover
        if(type == 2) this.SetTile(3);
    }

    private void OnMouseExit() { // Un-highlight grey tile after hover
        if(type == 3) this.SetTile(2);
    }

    private void OnMouseDown() {
        //check = true;
        grid.PlaceTile(row, col);
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
