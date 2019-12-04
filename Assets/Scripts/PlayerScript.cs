using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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
        float moveAmount = speed * Time.deltaTime;
        transform.Translate(moveAmount * direction);

    }
}
