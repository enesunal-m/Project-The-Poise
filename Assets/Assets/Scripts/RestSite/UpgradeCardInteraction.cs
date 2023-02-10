using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    CardUpgradeController cardUpgradeController;
    List<CardDatabaseStructure.ICardInfoInterface> upgradedCardList;
    CardDatabaseStructure.Root upgradedCardDatabaseJson;

    // Start is called before the first frame update
    void Start()
    {
        cardUpgradeController = GameObject.FindGameObjectWithTag("cardUpgradeController").GetComponent<CardUpgradeController>();

        if (!File.Exists(Application.streamingAssetsPath + Constants.URLConstants.cardTempDatabaseJsonBaseUrl))
        {
            using (File.Create(Application.streamingAssetsPath + Constants.URLConstants.cardTempDatabaseJsonBaseUrl)) ;
        }
        else
        {
            upgradedCardDatabaseJson = LanguageManager.getCardDatabaseWithLanguage(true);
            upgradedCardList = CardDatabase.initalizecardsList(upgradedCardDatabaseJson, true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        CardDisplay cardObject = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<CardDisplay>();

        cardUpgradeController.selectedCard =
            GameManager.Instance.cardsList.Where(card => card.id == cardObject.cardId).First();
        cardUpgradeController.upgraded_selectedCard =
            upgradedCardList.Where(card => card.id == cardObject.cardId).First();

        cardUpgradeController.card.GetComponent<Canvas>().sortingOrder = 20;
        cardUpgradeController.card.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);

        GameObject currentCard = Instantiate(cardUpgradeController.card);
        GameObject upgradedCard = Instantiate(cardUpgradeController.card);


        currentCard.GetComponent<CardDisplay>().initializeCard(cardUpgradeController.selectedCard);

        CardDatabaseStructure.ICardInfoInterface upgradedCardInfo = cardUpgradeController.selectedCard;

        upgradedCard.GetComponent<CardDisplay>().initializeCard(cardUpgradeController.upgraded_selectedCard);

        currentCard.transform.parent = cardUpgradeController.currentCardPlace;
        currentCard.transform.position = cardUpgradeController.currentCardPlace.position;
        upgradedCard.transform.parent = cardUpgradeController.upgradedCardPlace;
        upgradedCard.transform.position = cardUpgradeController.upgradedCardPlace.position;

        currentCard.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
        upgradedCard.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);

        cardUpgradeController.upgradeSelectedCardPanel.SetActive(true);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 15, this.transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 15, this.transform.position.z);
    }
}
