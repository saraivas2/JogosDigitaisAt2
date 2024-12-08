using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NPCAttack : MonoBehaviour

{
    float vel = 4.0f;// Start is called before the first frame update
    [SerializeField] private GameObject fire;

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(vel * Time.deltaTime, 0));
        

    }
    
}
