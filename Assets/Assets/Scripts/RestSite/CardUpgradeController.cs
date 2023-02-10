using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUpgradeController : MonoBehaviour
{

    public GameObject card;

    public List<CardDatabaseStructure.ICardInfoInterface> cardList = new List<CardDatabaseStructure.ICardInfoInterface>();

    public RectTransform viewPortContent;

    public CardDatabaseStructure.ICardInfoInterface selectedCard;
    public CardDatabaseStructure.ICardInfoInterface upgraded_selectedCard;

    public RectTransform currentCardPlace;
    public RectTransform upgradedCardPlace;
    public GameObject upgradeSelectedCardPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawnCards");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator spawnCards()
    {
        yield return new WaitForSeconds(0.1f * Time.deltaTime);

        cardList = GameManager.Instance.cardsList;
        Debug.Log(cardList.Count);

        List<GameObject> spawnedCardList = new List<GameObject>();

        foreach (CardDatabaseStructure.ICardInfoInterface cardInfo in cardList)
        {
            GameObject spawnedCard = Instantiate(card);
            spawnedCard.GetComponent<CardDisplay>().initializeCard(cardInfo);

            spawnedCard.transform.parent = viewPortContent;

            spawnedCard.GetComponent<Canvas>().overrideSorting = false;

            ObjectPool.SharedInstance.ReturnToPool(spawnedCard);

            spawnedCardList.Add(spawnedCard);
        }

        foreach (GameObject spawnedCard in spawnedCardList)
        {
            spawnedCard.SetActive(true);
            yield return new WaitForSeconds(0.06f);
        }

    }
}
