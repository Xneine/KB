using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midelScript : MonoBehaviour
{
    public logicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("masok pak eko");
        if(collision.gameObject.layer == 3){
            logic.addScore(1);
        }
    }
}

