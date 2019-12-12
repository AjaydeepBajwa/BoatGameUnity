using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ObjectRemoverScript : MonoBehaviour
{

    public GameObject breakAnim;
    public GameObject breakAnim2;
    public float delayTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        breakAnim.SetActive(false);
        breakAnim2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Player p1 = PhotonNetwork.PlayerList[0];
        Player p2 = PhotonNetwork.PlayerList[1];


        if ((bool)p1.CustomProperties["LeftObstacleCollided"] == true)
        {
            Debug.Log("LEFT OBS COLLIDED");
            breakAnim.SetActive(false);
            breakAnim.SetActive(true);
            float breakAnimPos = (float)p1.CustomProperties["LeftObstacleCollidePos"];
            breakAnim.transform.position = new Vector2(breakAnimPos, breakAnim.transform.position.y);

            Hashtable hash = new Hashtable();
            hash.Add("LeftObstacleCollided", false);
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }
        if ((bool)p2.CustomProperties["RightObstacleCollided"] == true)
        {
            Debug.Log("RIGHT OBS COLLIDED");
            breakAnim2.SetActive(false);
            breakAnim2.SetActive(true);
            float breakAnimPos = (float)p2.CustomProperties["RightObstacleCollidePos"];
            breakAnim2.transform.position = new Vector2(breakAnimPos, breakAnim.transform.position.y);

            Hashtable hash = new Hashtable();
            hash.Add("RightObstacleCollided", false);
            PhotonNetwork.SetPlayerCustomProperties(hash);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Obstacle"))
        {
            PhotonNetwork.Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Circle")
        {
            PhotonNetwork.Destroy(collision.gameObject);
        
        }
    }
    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(2);

    }

}
