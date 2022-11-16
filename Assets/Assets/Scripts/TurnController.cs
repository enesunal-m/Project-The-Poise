using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls turns and turn structure, and starts and ends the turns
/// </summary>
public class TurnController : MonoBehaviour
{
    public int turnCount;

    public float waitTillEndTurn;
    
    public GameObject enemy_;
    private GameObject[] cardsOnDeck;

    private bool justOnceForLiarmeter70, justOnceForLiarmeter85 = false;
    [SerializeField] GameObject companionButtons; 

    // Start is called before the first frame update
    void Start()
    {
        // Will be changes - Temporary solution
        List<EnemyTier> enemyTierListNormal = new List<EnemyTier>() { EnemyTier.Tier1, EnemyTier.Tier2, EnemyTier.Tier3 };
        List<EnemyTier> enemyTierListElite = new List<EnemyTier>() { EnemyTier.Tier1, EnemyTier.Tier2 };
        List<EnemyTier> enemyTierListBoss = new List<EnemyTier>() { EnemyTier.Tier1 };

        Dictionary<string, List<EnemyTier>> enemyTypeTierList = new Dictionary<string, List<EnemyTier>>()
        {
            { "Normal" , enemyTierListNormal },
            { "Elite" , enemyTierListElite },
            { "Boss" , enemyTierListBoss }
        };
        Dictionary<string, EnemyType> enemyTypeList = new Dictionary<string, EnemyType>()
        {
            { "Normal", EnemyType.Normal },
            { "Elite", EnemyType.Elite },
            { "Boss", EnemyType.Boss } };

        string enemyType = PlayerPrefs.GetString("EnemyType");

        startFight(enemyTypeList[enemyType], enemyTypeTierList[enemyType].TakeRandom(1).First(), 1);

        /*List<SceneType> sceneTypes = new List<SceneType>() { SceneType.RestSite, SceneType.Shop };
        SceneType nextScene = sceneTypes.TakeRandom(1).First();

        PlayerPrefs.SetString("NextScene", nextScene.ToString());*/
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startFight(EnemyType enemyType, EnemyTier enemyTier, int enemyCount)
    {

        // Initialize player controller
        GameManager.Instance.initializePlayerController();

        // Spawn enemies
        GameManager.Instance.GetComponent<EnemySpawner>().spawnEnemies(enemyType, enemyTier, enemyCount);

        // Pass turn to Player
        GameManager.Instance.turnSide = Characters.Player;

        startNewTurn();
    }

    public void endTurn()
    {
        if (!GameManager.Instance.isFightEnded)
        {
            PlayerPrefsController.SavePlayerInfo();
            companionButtons.SetActive(false);
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
            GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
            foreach (var item in cards)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in lines)
            {
                Destroy(item.gameObject);
            }
            GameManager.Instance.turnSide = decideTurnSide(GameManager.Instance.turnSide);
            if (GameManager.Instance.turnSide == Characters.Player)
            {

                GameManager.Instance.playerController.normalizeDamageToEnemyMultipliers();
                CardManager.Instance.CheckDeck();
            }
            startNewTurn();
        }
    }
    public void companionTurn()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        foreach (var item in cards)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in lines)
        {
            Destroy(item.gameObject);
        }
        companionButtons.SetActive(true);
    }

    public void startNewTurn()
    {
        if (GameManager.Instance.turnSide == Characters.Player)
        {
            JsonController.createCardJsonTempWithPath(Constants.URLConstants.cardTempDatabaseJsonBaseUrl, new CardManager().getAllCards());
            // TODO
            // create enemy intentions
            turnCount += 1;

            GameManager.Instance.playerController.shield = 0;

            int liarValue = GameManager.Instance.transform.GetComponent<LiarMeterConroller>().liarValue;

            Debug.Log("Liar Value" + liarValue);
            if (60 <= liarValue && liarValue < 70)
            {
                LiarmeterEffects.Instance.LiarmeterEffect60("demonicAttack");
            }

            GameManager.Instance.GetComponent<CardSpawner>().SpawnerStarter();
            GameManager.Instance.GetComponent<CardSpawner>().spawnOnce = true;


            GameManager.Instance.playerController.playerMana = Constants.PlayerConstants.initialMana;

            GameManager.Instance.playerController.applyNextTurnDeltas();

            EnemyController.Instance.decideEnemyIntention_all();

            //LiarmeterEffects

            foreach (var item in GameObject.FindGameObjectsWithTag("BuffEffect"))
            {
                Destroy(item.gameObject);
            }

            // GameManager.Instance.playerController.applyStateEffects();
        }
        else if (GameManager.Instance.turnSide == Characters.Enemy)
        {
            GameManager.Instance.GetComponent<CardSpawner>().HandDiscarder();
            // TODO
            EnemyController.Instance.applyDecidedIntentions_all();
            GameManager.Instance.GetComponent<CardSpawner>().spawnOnce = false;

            int liarValue = GameManager.Instance.transform.GetComponent<LiarMeterConroller>().liarValue;

            if ((liarValue <= 30 && 15 < liarValue) || (70 <= liarValue && liarValue < 80))
            {
                LiarmeterEffects.Instance.LiarmeterEffect70();
            }
            else if (liarValue <= 15 || 80 <= liarValue)
            {
                LiarmeterEffects.Instance.LiarmeterEffect85();
            }
            else if (liarValue < 60 && liarValue > 30)
            {
                LiarmeterEffects.Instance.ResetLiarmeterPenalty();
            }

            Invoke("endTurn", waitTillEndTurn);
            EnemyController.Instance.applyNextTurnDamageMultiplier_all();
            // apply enemy effects on enemies
            // wait at least 1.5 secs
        }
    }

    public void EndFight()
    {
        PlayerPrefs.SetInt("mapGenerated", 1);
        PlayerPrefs.SetInt("playerCoin", PlayerPrefs.GetInt("playerCoin") + Constants.TurnConstants.coinPerTurn);
        GameManager.Instance.playerController.coin = PlayerPrefs.GetInt("playerCoin");

        PlayerPrefsController.SavePlayerInfo();

        SceneRouter.GoToScene(SceneType.Map);
    }

    public void changeLanguage()
    {
        if (GameManager.Instance.gameLanguage == Language.tr)
        {
            PlayerPrefs.SetString("Language", "en");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
        else if (GameManager.Instance.gameLanguage == Language.en)
        {
            PlayerPrefs.SetString("Language", "tr");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private Characters decideTurnSide(Characters currentSide)
    {
        if (currentSide == Characters.Player)
        {
            return Characters.Enemy;
        }
        else
        {
            return Characters.Player;
        }
    }
}