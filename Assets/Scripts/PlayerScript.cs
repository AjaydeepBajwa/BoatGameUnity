using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float minX, maxX;
    public int speed;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            if(direction == Vector2.zero)
            {
                direction = Vector2.right;
            }
            else if (direction == Vector2.right)
            {
                direction = Vector2.left;
            }
            else if (direction == Vector2.left)
            {
                direction = Vector2.right;
            }
        }
        float amountToMove = speed * Time.deltaTime;
        transform.Translate(amountToMove * direction);

        //x coordinate for left boat should be -2.75 to -0.9
        Vector2 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);//modify the player to keep it between minX and MaxX
        transform.position = currentPosition;
    }
}
