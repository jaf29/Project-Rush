using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //Variables ---------------------------------
    public GameObject player;
    public GameObject battleFieldPoint;

	private Vector3 offset;
    //Variables ---------------------------------

    //Initialize ---------------------------------
    void Start () {
		offset = transform.position - player.transform.position;
	}
    //Initialize ---------------------------------

    //Update ---------------------------------
    void LateUpdate () {
        if (GameManager.instance.gameStart)
        {
            transform.position = player.transform.position + offset;
        }
        
        if(GameManager.instance.battleStart)
        {
            transform.position = battleFieldPoint.transform.position + offset;
        }
	}
    //Update ---------------------------------
}
