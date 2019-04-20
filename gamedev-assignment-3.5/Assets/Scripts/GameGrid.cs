using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGrid : MonoBehaviour {
    public GameObject[,] tiles; // Matrix that represents the hexagonal grid
    public GameObject tilePrefab;

    public Text whiteScoreText;
    public Text blackScoreText;
    private int whiteScore = 3;
    private int blackScore = 3;

    public Text whiteMove;
    public Text blackMove;
    private bool whiteToMove = true; // True = white to move, False = black to move
    private bool animating = false;

    private void Start() {
        tiles = new GameObject[11, 11];
        whiteToMove = true;
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
        int[] rowTemp = {0, 1, 8, 9, 10, 0, 1, 9, 10, 0, 9, 10, 0, 10, 10, 
                         10, 0, 10, 0, 9, 10, 0, 1, 9, 10, 0, 1, 8, 9, 10};
        int[] colTemp = {0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 
                         6, 7, 7, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10};

        for(int i = 0; i < rowTemp.Length; i++) {
            Destroy(tiles[rowTemp[i], colTemp[i]]);
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

    	if(whiteToMove) {
            Debug.Log("White places a tile at [" + row + ", " + col + "]");
    		curTile.SetTile(0);
    	} else {
            Debug.Log("Black places a tile at [" + row + ", " + col + "]");
    		curTile.SetTile(1);
    	}

    	CheckForFlips(row, col);
    	whiteToMove = !whiteToMove;
        whiteMove.enabled = whiteToMove;
        blackMove.enabled = !whiteToMove;
    }

    // Check for tiles to flip as a result of a placed tile
    private void CheckForFlips(int row, int col) {
        animating = true;

        // Check up
        

        // Check up-right


        // Check down-right


        // Check down


        // Check down-left


        // Check up-left


        animating = false;
    }

    private void FlipTile(int row, int col) {
    	
    }
}