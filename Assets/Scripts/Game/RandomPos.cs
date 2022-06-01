using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    //limits
    //limits spawn position of the cheese inside the area of canvas.
    //Limit set to see and interact with it correctly
    Vector2 LimitX = new Vector2 (-888, 888);
    Vector2 LimitY = new Vector2 (-488, 488);

    //reference to the object
    public GameObject Cheese;

    //reference to canvas, to instantiate cheese inside it
    public GameObject Canvas;
    //prueba
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomPosition();
        }
    }
    //randomizes position of the spawner
    public void RandomPosition()
    {
        transform.localPosition = new Vector2(Random.Range(LimitX.x, LimitX.y), Random.Range(LimitY.x, LimitY.y));
    }
    //cheese is duplicated and activated in scene in order to interact with it
    public void SpawnObject()
    {
        GameObject a = Instantiate(Cheese, transform.position, Quaternion.identity);
        a.transform.parent = Canvas.transform;
        a.SetActive(true);
    }
}
