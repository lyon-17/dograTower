using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovePointScript : MonoBehaviour
{

    private int yellowKeys = 0;

    //private bool canMove = true;

    private Vector2 currentPos;

    public TMP_Text text_yk;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "YellowDoor")
        {
            currentPos = transform.position;
            if (yellowKeys > 0)
            {
                Debug.Log("OPEN IT");
                collision.gameObject.SetActive(false);
                yellowKeys--;
                text_yk.text = "Yellow keys: " + yellowKeys;
            }
            else
            {
                Debug.Log("NO KEYS");
                Debug.Log(currentPos);
                collision.gameObject.layer = 6;
                //canMove = false;
            }
        }

    }
}
