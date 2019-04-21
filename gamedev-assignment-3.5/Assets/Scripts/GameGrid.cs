using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGrid : MonoBehaviour {
    public GameObject tilePrefab;
    public GameObject[,] tiles = new GameObject[11, 11]; // Matrix that represents the hexagonal grid
    private List<List<Tile>> tilesToFlip = new List<List<Tile>>();

    public Text whiteScoreText;
    public Text blackScoreText;
    private int whiteScore = 3;
    private int blackScore = 3;

    public Text whiteMove;
    public Text blackMove;
    private bool white = true; // True = white to move, False = black to move
    private bool animating = false;

    private void Start() {
        whiteMove.enabled = true;
        blackMove.enabled = false;
        GenerateGrid();
    }

    private void Update() {
        if(Input.GetKeyDown("r")) { // Restart key for debugging
            Debug.Log("Restarting...");
            SceneManager.LoadScene("Game");
        }

        whiteScoreText.text = "White: " + whiteScore;
        blackScoreText.text = "Black: " + blackScore;
    }

    // Get the position for a tile based on its index in the matrix
    private Vector2 GetPosByIndex(int row, int col) {
        float x = -3.9f + (0.78f * col);
        float y = 4.05f - (0.9f * row);
        if(col % 2 == 1) y += 0.45f;
        return new Vector2(x, y);
    }

    // Generate the starting grid of tiles
    private void GenerateGrid() {
        // Create the matrix of tiles
        for(int row = 0; row < 11; row++) {
            for(int col = 0; col < 11; col++) {
                GameObject newTile = Instantiate(tilePrefab);
                newTile.transform.position = GetPosByIndex(row, col);
                newTile.GetComponent<Tile>().SetTile(row, col, 2);
                tiles[row, col] = newTile;
            }
        }

        // Destroy the tiles that aren't part of the board
        int[] rowtilesThisDir = {0, 1, 8, 9, 10, 0, 1, 9, 10, 0, 9, 10, 0, 10, 10, 
                         10, 0, 10, 0, 9, 10, 0, 1, 9, 10, 0, 1, 8, 9, 10};
        int[] coltilesThisDir = {0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 
                         6, 7, 7, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10};

        for(int i = 0; i < rowtilesThisDir.Length; i++) {
            Destroy(tiles[rowtilesThisDir[i], coltilesThisDir[i]]);
        }

        // Set the starting tiles
        tiles[4, 4].GetComponent<Tile>().SetTile(0);
        tiles[6, 5].GetComponent<Tile>().SetTile(0);
        tiles[4, 6].GetComponent<Tile>().SetTile(0);
        tiles[5, 4].GetComponent<Tile>().SetTile(1);
        tiles[4, 5].GetComponent<Tile>().SetTile(1);
        tiles[5, 6].GetComponent<Tile>().SetTile(1);
        Debug.Log("Grid generated");
    }

    // Player places a tile
    public void PlaceTile(int row, int col) {
        Tile curTile = tiles[row, col].GetComponent<Tile>();

    	if(white) {
            Debug.Log("White places a tile at [" + row + ", " + col + "]");
    		curTile.SetTile(0);
            whiteScore++;
    	} else {
            Debug.Log("Black places a tile at [" + row + ", " + col + "]");
    		curTile.SetTile(1);
            blackScore++;
    	}

    	CheckForFlips(row, col);
    	white = !white;
        whiteMove.enabled = white;
        blackMove.enabled = !white;
    }

    // Check for tiles to flip as a result of a placed tile
    private void CheckForFlips(int row, int col) {
        animating = true;
        int thisColor = white ? 0 : 1;
        int otherColor = white ? 1 : 0;

        // Check up
        List<Tile> tilesThisDir = new List<Tile>();
        int curRow = row;
        int curCol = col;
        while(!(curRow == 2 && curCol == 0) && !(curRow == 2 && curCol == 1) && !(curRow == 1 && curCol == 2) && !(curRow == 1 && curCol == 3) && !(curRow == 0 && curCol == 4) && !(curRow == 0 && curCol == 5) && !(curRow == 0 && curCol == 6) && !(curRow == 1 && curCol == 7) && !(curRow == 1 && curCol == 8) && !(curRow == 2 && curCol == 9) && !(curRow == 2 && curCol == 10)) {
            curRow--;
            Tile next = tiles[curRow, curCol].GetComponent<Tile>();
            if(next.type == otherColor) {
                tilesThisDir.Add(next);
            } else if(next.type == thisColor) {
                tilesToFlip.Add(tilesThisDir);
                break;
            } else {
                break;
            }
        }

        // Check up-right
        tilesThisDir = new List<Tile>();
        curRow = row;
        curCol = col;
        while(!(curRow == 0 && curCol == 5) && !(curRow == 0 && curCol == 6) && !(curRow == 1 && curCol == 7) && !(curRow == 1 && curCol == 8) && !(curRow == 2 && curCol == 9) && !(curRow == 2 && curCol == 10) && !(curRow == 3 && curCol == 10) && !(curRow == 4 && curCol == 10) && !(curRow == 5 && curCol == 10) && !(curRow == 6 && curCol == 10) && !(curRow == 7 && curCol == 10)) {
            if(curCol % 2 == 1) curRow--;
            curCol++;
            Tile next = tiles[curRow, curCol].GetComponent<Tile>();
            if(next.type == otherColor) {
                tilesThisDir.Add(next);
            } else if(next.type == thisColor) {
                tilesToFlip.Add(tilesThisDir);
                break;
            } else {
                break;
            }
        }

        // Check down-right
        tilesThisDir = new List<Tile>();
        curRow = row;
        curCol = col;
        while(!(curRow == 2 && curCol == 10) && !(curRow == 3 && curCol == 10) && !(curRow == 4 && curCol == 10) && !(curRow == 5 && curCol == 10) && !(curRow == 6 && curCol == 10) && !(curRow == 7 && curCol == 10) && !(curRow == 8 && curCol == 9) && !(curRow == 8 && curCol == 8) && !(curRow == 9 && curCol == 7) && !(curRow == 9 && curCol == 6) && !(curRow == 10 && curCol == 5)) {
            if(curCol % 2 == 0) curRow++;
            curCol++;
            Tile next = tiles[curRow, curCol].GetComponent<Tile>();
            if(next.type == otherColor) {
                tilesThisDir.Add(next);
            } else if(next.type == thisColor) {
                tilesToFlip.Add(tilesThisDir);
                break;
            } else {
                break;
            }
        }

        // Check down
        tilesThisDir = new List<Tile>();
        curRow = row;
        curCol = col;
        while(!(curRow == 7 && curCol == 10) && !(curRow == 8 && curCol == 9) && !(curRow == 8 && curCol == 8) && !(curRow == 9 && curCol == 7) && !(curRow == 9 && curCol == 6) && !(curRow == 10 && curCol == 5) && !(curRow == 9 && curCol == 4) && !(curRow == 9 && curCol == 3) && !(curRow == 8 && curCol == 2) && !(curRow == 8 && curCol == 1) && !(curRow == 7 && curCol == 0)) {
            curRow++;
            Tile next = tiles[curRow, curCol].GetComponent<Tile>();
            if(next.type == otherColor) {
                tilesThisDir.Add(next);
            } else if(next.type == thisColor) {
                tilesToFlip.Add(tilesThisDir);
                break;
            } else {
                break;
            }
        }

        // Check down-left
        tilesThisDir = new List<Tile>();
        curRow = row;
        curCol = col;
        while(!(curRow == 10 && curCol == 5) && !(curRow == 9 && curCol == 4) && !(curRow == 9 && curCol == 3) && !(curRow == 8 && curCol == 2) && !(curRow == 8 && curCol == 1) && !(curRow == 7 && curCol == 0) && !(curRow == 6 && curCol == 0) && !(curRow == 5 && curCol == 0) && !(curRow == 4 && curCol == 0) && !(curRow == 3 && curCol == 0) && !(curRow == 2 && curCol == 0)) {
            if(curCol % 2 == 0) curRow++;
            curCol--;
            Tile next = tiles[curRow, curCol].GetComponent<Tile>();
            if(next.type == otherColor) {
                tilesThisDir.Add(next);
            } else if(next.type == thisColor) {
                tilesToFlip.Add(tilesThisDir);
                break;
            } else {
                break;
            }
        }

        // Check up-left
        tilesThisDir = new List<Tile>();
        curRow = row;
        curCol = col;
        while(!(curRow == 7 && curCol == 0) && !(curRow == 6 && curCol == 0) && !(curRow == 5 && curCol == 0) && !(curRow == 4 && curCol == 0) && !(curRow == 3 && curCol == 0) && !(curRow == 2 && curCol == 0) && !(curRow == 2 && curCol == 1) && !(curRow == 1 && curCol == 2) && !(curRow == 1 && curCol == 3) && !(curRow == 0 && curCol == 4) && !(curRow == 0 && curCol == 5)) {
            if(curCol % 2 == 1) curRow--;
            curCol--;
            Tile next = tiles[curRow, curCol].GetComponent<Tile>();
            if(next.type == otherColor) {
                tilesThisDir.Add(next);
            } else if(next.type == thisColor) {
                tilesToFlip.Add(tilesThisDir);
                break;
            } else {
                break;
            }
        }

        FlipTiles();
        animating = false;
    }

    // Flip the tiles given by CheckForFlips()
    private void FlipTiles() {
        for(int i = 0; i < 9; i++) { // 9 max flips in one direction
            for(int dir = 0; dir < tilesToFlip.Count; dir++) { // Each tilesThisDir
                if(i < tilesToFlip[dir].Count) {
                    if(white) {
                        tilesToFlip[dir][i].SetTile(0);
                        whiteScore++;
                        blackScore--;
                    } else {
                        tilesToFlip[dir][i].SetTile(1);
                        whiteScore--;
                        blackScore++;
                    }
                }
            }
        }

        tilesToFlip = new List<List<Tile>>(); // Reset the list
    }
}