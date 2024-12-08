using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover_balas : MonoBehaviour
{
    float vel = 4.0f;// Start is called before the first frame update
    int time = 2000;
    [SerializeField] private GameObject Bala;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2 (vel*Time.deltaTime, 0));
        time--;
        if (time == 0)
        {
            Destroy(Bala);
            time = 2000;
        }
    }
}
