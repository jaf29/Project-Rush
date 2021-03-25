using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Variables ---------------------------------
    public static GameManager instance = null;
    public GameObject autoSpawn;
    public GameObject battleSpawn;
    public GameObject phase;
    public GameObject player;
    public GameObject enemy;
    public GameObject sword;
    public GameObject lance;
    public GameObject axe;
    //public GameObject item;

    public Text timerText;
    public Text phaseText;
    public Text playerHealthText;
    public Text enemyHealthText;
    public Text deathText;
    public Text objectText;
    public Text pointText;

    public Image swordImage;
    public Image lanceImage;
    public Image axeImage;

    public bool playerDead;
    public bool gameStart;
    public bool phaseChange;
    public bool battleStart;
    public bool startPause;

    private PlayerController pc;
    private EnemyController ec;
    private float gameTimer;
    private float phaseTimer;
    private int deathCount;
    private int objectCount;
    private bool swordEquip;
    private bool lanceEquip;
    private bool axeEquip;
    //private int pointCount;
    //Variables ---------------------------------

    //Initialize ---------------------------------
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        pc = player.GetComponent<PlayerController>();
        ec = enemy.GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        //item = GameObject.FindGameObjectWithTag("Item");

        startPause = true;
        gameStart = false;
        phaseChange = false;
        gameTimer = 0f;
        phaseTimer = 3f;
        deathCount = 0;
        objectCount = 0;
        phaseText.text = "";
        deathText.text = ""; //Testing
        objectText.text = ""; //Testing
        //pointText.text = ""; //Testing
        enemyHealthText.text = "";
        sword.active = false;
        lance.active = false;
        axe.active = false;
        swordImage.enabled = false;
        lanceImage.enabled = false;
        axeImage.enabled = false;
        swordEquip = false;
        lanceEquip = false;
        axeEquip = false;
    }
    //Initialize ---------------------------------

    //Update ---------------------------------
    void Update () {
        if(gameStart == true)
        {
            gameTimer += Time.deltaTime;
            SetGameTimerText();
        }

        if (startPause == true)
        {
            if (phaseTimer > 0)
            {
                phaseTimer -= Time.deltaTime;
                SetPhaseTimerText();
            }
            if (phaseTimer <= 0)
            {
                phaseText.text = "";
                gameStart = true;
                startPause = false;
            }
        }

        if (phaseChange == true)
        {
            if(phaseTimer > 0)
            {
                phaseTimer -= Time.deltaTime;
                SetPhaseTimerText();
            }
            if(phaseTimer <= 0)
            {
                phaseText.text = "";
            }
        }
    }
    //Update ---------------------------------

    //Kills Player ---------------------------------
    public void PlayerDeath()
    {
        gameStart = false;
        playerDead = true;
        deathCount++;
        SetDeathCountText();
        //SetPointCountText();
        AutoRespawn();
    }
    //Kills Player ---------------------------------

    //Complete Level ---------------------------------
    public void PlayerComplete()
    {
        gameStart = false;
        phaseChange = true;
        phaseTimer = 3f;
        if (phaseChange == true)
        {
            Invoke("NextPhase", 3f);
        }
    }
    //Complete Level ---------------------------------

    /*
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        gameStart = true;
    }
    */

    //Respawn/Reset Loacation ---------------------------------
    public void AutoRespawn()
    {
        player.transform.position = autoSpawn.transform.position;
        gameStart = true;
    }

    public void BattleReset()
    {
        player.transform.position = battleSpawn.transform.position;
    }
    //Respawn/Reset Loacation ---------------------------------

    //Dealing/Taking Damage ---------------------------------
    public void Damaged()
    {
        pc.currentHealth--;
        SetPlayerHealthText();
        objectCount++;
        SetObjectCountText();
    }

    public void BattleDamage()
    {
        if(ec.currentHealth > 0)
        {
            ec.currentHealth = ec.currentHealth - pc.attackDamage;
            SetEnemyHealthText();
        }
    }
    //Dealing/Taking Damage ---------------------------------

    /*
    public void Points()
    {
        pointCount++;
        SetPointCountText();
    }
    */

    //Change Game Phase ---------------------------------
    private void NextPhase()
    {
        player.transform.position = phase.transform.position;
        phaseChange = false;
        phaseTimer = 3f;
        BattlePhase();
    }

    private void BattlePhase()
    {
        battleStart = true;
        SetEnemyHealthText();
    }
    //Change Game Phase ---------------------------------

    //Text Setting Methods ---------------------------------
    private void SetGameStartText()
    {
        //
    }
    private void SetGameTimerText()
    {
        timerText.text = "Time: " + gameTimer.ToString("0");
    }

    private void SetPhaseTimerText()
    {
        phaseText.text = "Next Phase Starts: " + phaseTimer.ToString("0");
    }

    public void SetPlayerHealthText()
    {
        playerHealthText.text = "Player Health: " + pc.currentHealth.ToString("0");
    }

    public void SetEnemyHealthText()
    {
        enemyHealthText.text = "Enemy Health: " + ec.currentHealth.ToString("0");
    }

    public void SetDeathCountText()
    {
        deathText.text = "Deaths: " + deathCount.ToString("0");
    }

    public void SetObjectCountText()
    {
        objectText.text = "Objects Hit: " + objectCount.ToString("0");
    }

    /*
    public void SetPointCountText()
    {
        pointText.text = "Items Collected: " + pointCount.ToString("0");
    }
    */
    //Text Setting Methods ---------------------------------

    //Weapon Swapping Methods ---------------------------------
    public void SwordSwap()
    {
        sword.active = true;
        lance.active = false;
        axe.active = false;
        swordImage.enabled = true;
        lanceImage.enabled = false;
        axeImage.enabled = false;
        swordEquip = true;
        lanceEquip = false;
        axeEquip = false;
    }

    public void LanceSwap()
    {
        sword.active = false;
        lance.active = true;
        axe.active = false;
        swordImage.enabled = false;
        lanceImage.enabled = true;
        axeImage.enabled = false;
        swordEquip = false;
        lanceEquip = true;
        axeEquip = false;
    }

    public void AxeSwap()
    {
        sword.active = false;
        lance.active = false;
        axe.active = true;
        swordImage.enabled = false;
        lanceImage.enabled = false;
        axeImage.enabled = true;
        swordEquip = false;
        lanceEquip = false;
        axeEquip = true;
    }
    //Weapon Swapping Methods ---------------------------------
}
