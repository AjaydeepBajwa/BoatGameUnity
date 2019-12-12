using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LeftBoatMoveScript : MonoBehaviourPun
{
    public int speed;
    private Vector2 direction;
    public float minimumX, maximumX;
    bool p1Dash;
   
    int score1 = 0;
;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.left;
        p1Dash = false;

        Hashtable hash = new Hashtable();
        hash.Add("LeftObstacleCollided", false);
        PhotonNetwork.SetPlayerCustomProperties(hash);

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            movePlayer();
        }
    }

    private void movePlayer()
    {
            if (Input.mousePosition.x <= Screen.width / 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("clicked");
                    //play boat move sound
                    //uiManager.playBoatMoveSound();
                    if (direction == Vector2.zero)
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
            }
            float moveAmount = speed * Time.deltaTime;
            transform.Translate(moveAmount * direction);

            //x coordinate for left boat should be -2.75 to -0.9
            Vector2 currentPosition = transform.position;

            //Using Clamp function to keep the boat between two lanes i.e. between minimumX to maximumX
            currentPosition.x = Mathf.Clamp(currentPosition.x, minimumX, maximumX);
            transform.position = currentPosition;

        Hashtable hash = new Hashtable();
        hash.Add("boat1Pos", transform.position.x);
        PhotonNetwork.SetPlayerCustomProperties(hash);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Circle")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                score1++;
                Hashtable hash = new Hashtable();
                hash.Add("score", +score1);
                PhotonNetwork.SetPlayerCustomProperties(hash);
            }

            PhotonNetwork.Destroy(collision.gameObject);
      
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Player p1 = PhotonNetwork.PlayerList[0];
                p1Dash = (bool)p1.CustomProperties["boat1Dash"];
              
                if (p1Dash == true)
                {
                    PhotonNetwork.Destroy(collision.gameObject);
                    score1++;
                    score1++;
                }

                else if (p1Dash == false)
                {
                    if(score1 >= 2)
                    {
                        score1 = score1 - 2;
                    }
                    else
                    {
                        score1 = 0;
                    }
                    PhotonNetwork.Destroy(collision.gameObject);
                }
                Hashtable hash = new Hashtable();
                hash.Add("score", +score1);
                hash.Add("LeftObstacleCollided", true);
                hash.Add("LeftObstacleCollidePos", collision.gameObject.transform.position.x);

                PhotonNetwork.SetPlayerCustomProperties(hash);
            }
        }
    }
}
