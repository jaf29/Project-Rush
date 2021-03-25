using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //Variables ---------------------------------
    public bool wasHit;
    public int startHealth = 50;
    public int currentHealth;
    public int attackDamage;
    //Variables ---------------------------------

    //Initialize ---------------------------------
    void Start () {
        currentHealth = startHealth;
        wasHit = false;
    }
    //Initialize ---------------------------------

    //Update ---------------------------------
    void Update () {
		if(currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.instance.SetEnemyHealthText();
            Destroy(gameObject);
        }
	}
    //Update ---------------------------------
}
