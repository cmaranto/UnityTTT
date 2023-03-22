using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum State{
        None,
        X,
        O,
    }

    private State _state = State.None;

    public State state{
        get{
            return _state;
        }
        set{
            _state = value;
            switch(_state){
                case State.None:
                    spriteRenderer.sprite = null;
                    break;
                case State.X:
                    spriteRenderer.sprite = xSprite;
                    break;
                case State.O:
                    spriteRenderer.sprite = oSprite;
                    break;
            }
        }
    }

    public SpriteRenderer spriteRenderer;
    public Sprite xSprite;
    public Sprite oSprite;
    public int row = 0;
    public int col = 0;
    public bool locked = false;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(!locked){
            state = GameEngine.Instance.turn;
            GameEngine.Instance.Turn(row,col);
            locked = true;
        }
        
    }
}
