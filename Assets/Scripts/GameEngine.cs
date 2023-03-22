using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEngine : MonoBehaviour
{

    public Tile.State turn = Tile.State.X;

    public Tile MidCenter;
    public Tile TopCenter;
    public Tile BotCenter;
    public Tile MidLeft;
    public Tile TopLeft;
    public Tile BotLeft;
    public Tile MidRight;
    public Tile TopRight;
    public Tile BotRight;

    Tile[,] TileGrid;
    int turnCount = 0;

    public static GameEngine Instance { get; private set; }

    public GameObject playAgainButton;
    public TMP_Text statusText;

    void Awake(){
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        TileGrid = new Tile[3,3]{{TopLeft,TopCenter,TopRight},{MidLeft,MidCenter,MidRight},{BotLeft,BotCenter,BotRight}};      
        playAgainButton.SetActive(false);
        statusText.SetText("X's Turn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset(){
        for(int row = 0; row < 3; ++row){
            for(int col = 0; col < 3; ++col){
                TileGrid[row,col].state = Tile.State.None;
                TileGrid[row,col].locked = false;
            }
        }
        turnCount = 0;
        turn = Tile.State.X;
        playAgainButton.SetActive(false); 
        statusText.SetText("X's Turn");
    }

    public void lockAll(){
        for(int row = 0; row < 3; ++row){
            for(int col = 0; col < 3; ++col){
                TileGrid[row,col].locked = true;
            }
        }
    }

    public void Turn(int row, int col){
        //check for victory        
        if(checkHorz(row) || checkVert(col) || checkDiag(row,col)){
            statusText.SetText(string.Format("{0} Wins!",turn == Tile.State.X ? "X" : "O"));
            playAgainButton.SetActive(true);
            lockAll(); 
        }else if(++turnCount == 9){
            statusText.SetText("Tie Game!");
            playAgainButton.SetActive(true);
            lockAll();  
        }
        else{
            turn = turn == Tile.State.X ? Tile.State.O : Tile.State.X;
            statusText.SetText(string.Format("{0}'s Turn",turn == Tile.State.X ? "X" : "O"));
        }        
    }

    bool checkHorz(int row){
        for(int col = 0; col < 3; ++col){
            if(TileGrid[row,col].state != turn){
                return false;
            }
        }
        return true;
    }

    bool checkVert(int col){
        for(int row = 0; row < 3; ++row){
            if(TileGrid[row,col].state != turn){
                return false;
            }
        }
        return true;
    }

    bool checkDiag(int row, int col){
        if(row == col){
            for(int rc = 0; rc < 3; ++rc){
                if(TileGrid[rc,rc].state != turn){
                    return false;
                }
            }
        }else{
            int r = 2;
            for(int c = 0; c < 3; ++c){
                if(TileGrid[r,c].state != turn){
                    return false;
                }
                --r;
            }
        }

        return true;
    }
}
