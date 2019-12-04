using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private static GameDataManager managerInstance;

    private Stack<GameObject> circleStack = new Stack<GameObject>();
    private Stack<GameObject> obstacleStack = new Stack<GameObject>();

    public GameObject[] circleObstaclePrefabs;
public static GameDataManager ManagerInstance
    {
        get
        {
            if (managerInstance == null)
            {
                managerInstance = FindObjectOfType<GameDataManager>();
            }
            return managerInstance;
        }
    }

    public Stack<GameObject> CircleStack { get => circleStack; set => circleStack = value; }

    public Stack<GameObject> ObstacleStack { get => obstacleStack; set => obstacleStack = value; }

    // Start is called before the first frame update
    void Start()
    {
        createCirclesObstacles(8);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createCirclesObstacles(int number)
    {
        //instantiATE THE GAME OBJECT
        //put game object inside stack
        //we want to set all of them inactive

        for(int i = 0; i < number; i++)
        {
            //instantiate game object
            //put game object inside stack
            CircleStack.Push(Instantiate(circleObstaclePrefabs[0]));
            ObstacleStack.Push(Instantiate(circleObstaclePrefabs[1]));

            //set them inactive
            CircleStack.Peek().name = "Circle";
            ObstacleStack.Peek().name = "Obstacle";
            CircleStack.Peek().SetActive(false);
            ObstacleStack.Peek().SetActive(false);
        }
    }
}
