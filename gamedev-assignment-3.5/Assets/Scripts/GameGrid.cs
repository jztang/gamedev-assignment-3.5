using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGrid : MonoBehaviour {
    public GameObject[,] tiles; // Matrix that represents the hexagonal grid
    public GameObject tilePrefab;

    public Text whiteScore;
    public Text blackScore;
    private bool whiteToMove; // White to move = true, black to move = false

    private void Start() {
        tiles = new GameObject[11, 11];
        whiteToMove = true;
        GenerateGrid();
    }

    private void Update() {
        if(Input.GetKeyDown("r")) {
            Debug.Log("Restarting...");
            SceneManager.LoadScene("Game");
        }
    }

    private Vector2 GetPosByIndex(int row, int col) {
        float x = -3.9f + (0.78f * col);
        float y = 4.05f - (0.9f * row);
        if(col % 2 == 1) y += 0.45f;
        return new Vector2(x, y);
    }

    private void GenerateGrid() {
        for(int row = 0; row < 11; row++) {
            for(int col = 0; col < 11; col++) {
                GameObject newTile = Instantiate(tilePrefab);
                newTile.transform.position = GetPosByIndex(row, col);
                newTile.GetComponent<Tile>().SetTile(row, col, 2);
                tiles[row, col] = newTile;
            }
        }

        int[] rowTemp = {0, 1, 8, 9, 10, 0, 1, 9, 10, 0, 9, 10, 0, 10, 10, 
                         10, 0, 10, 0, 9, 10, 0, 1, 9, 10, 0, 1, 8, 9, 10};
        int[] colTemp = {0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 
                         6, 7, 7, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 10};

        for(int i = 0; i < rowTemp.Length; i++) {
            Destroy(tiles[rowTemp[i], colTemp[i]]);
        }

        tiles[4, 4].GetComponent<Tile>().SetTile(0);
        tiles[6, 5].GetComponent<Tile>().SetTile(0);
        tiles[4, 6].GetComponent<Tile>().SetTile(0);

        tiles[5, 4].GetComponent<Tile>().SetTile(1);
        tiles[4, 5].GetComponent<Tile>().SetTile(1);
        tiles[5, 6].GetComponent<Tile>().SetTile(1);
    }

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
    }

    private void CheckForFlips(int row, int col) {

    }

    private void FlipTile(int row, int col) {
    	
    }
}