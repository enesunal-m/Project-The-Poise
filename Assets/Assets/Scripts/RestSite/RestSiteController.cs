using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class RestSiteController : MonoBehaviour
{
    public bool healthIncreased = false;
    CardUpgradeController cardUpgradeController;

    void Start()
    {
        cardUpgradeController = GameObject.FindGameObjectWithTag("cardUpgradeController").GetComponent<CardUpgradeController>();

    }
    public void Rest()
    {

        if (!healthIncreased)
        {
            PlayerPrefs.SetFloat("playerHealth", PlayerPrefs.GetFloat("playerHealth") * 1.3f);
            healthIncreased = true;

            PlayerPrefs.SetInt("notStartOfRun", 1);
            SceneRouter.GoToScene(SceneType.Map);
        }
    }

    public void UpgradeSelectedCard()
    {
        CardDatabaseStructure.ICardInfoInterface cardInfo = cardUpgradeController.upgraded_selectedCard;
        GameManager.Instance.cardsList.Remove(GameManager.Instance.cardsList.Where(card => card.id == cardInfo.id).First());
        GameManager.Instance.cardsList.Add(cardInfo);
        JsonController.createCardJsonTempWithPath(Constants.URLConstants.cardTempDatabaseJsonBaseUrl, GameManager.Instance.cardsList);

        PlayerPrefs.SetInt("notStartOfRun", 1);
        SceneRouter.GoToScene(SceneType.Map);
    }

    public void CancelUpgrade()
    {
        cardUpgradeController.upgradeSelectedCardPanel.SetActive(false);
    }
}
