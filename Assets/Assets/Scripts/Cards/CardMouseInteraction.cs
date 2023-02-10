using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class CardMouseInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    CardDisplay cardDisplay;
    GameObject hand;
    public LayerMask IgnoreMe;
    private GameObject castingPlace;
    public GameObject[] line;
    private GameObject highlightEffect;
    private LineController lineController;
    private GameObject highlightedCard;

    private void Start()
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        cardDisplay = this.GetComponent<CardDisplay>();

        if (!cardDisplay.isSelectionCard)
        {
            hand = this.transform.parent.gameObject;
            castingPlace = GameObject.FindGameObjectWithTag("CastingPlace");
            lineController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LineController>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject != CardManager.Instance.selectedCard && !GameManager.Instance.areCardsSpawning)
        {
            if (cardDisplay.isSelectionCard)
            {
                highlightedCard = this.gameObject;
                this.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 15, this.transform.position.z);
                this.GetComponent<Canvas>().sortingOrder += 100;
            }
            else
            {
                highlightedCard = this.gameObject;
                this.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 100, this.transform.position.z);
                this.GetComponent<Canvas>().sortingOrder += 100;
            }
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject != CardManager.Instance.selectedCard && !GameManager.Instance.areCardsSpawning)
        {
            if (cardDisplay.isSelectionCard)
            {
                highlightedCard = null;
                this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 15, this.transform.position.z);
                this.GetComponent<Canvas>().sortingOrder -= 100;
            }
            else
            {
                highlightedCard = null;
                this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 100, this.transform.position.z);
                this.GetComponent<Canvas>().sortingOrder -= 100;
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (!cardDisplay.isSelectionCard && !GameManager.Instance.areCardsSpawning)
        {
            if (GameManager.Instance.isCardSelected)
            {
                return;
            }
            if ((PlayerController.Instance.playerMana >= highlightedCard.GetComponent<CardDisplay>().cost || highlightedCard.GetComponent<CardDisplay>().cost == 0) && !GameManager.Instance.isCardSelected)
            {
                CardManager.Instance.selectedCard = eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
                highlightEffect = CardManager.Instance.selectedCard.transform.GetChild(0).gameObject;
                highlightEffect.SetActive(true);
                
                var description = CardManager.Instance.selectedCard.GetComponent<CardDisplay>().description.text.ToString();
                TooltipSystem.current.Show(description, "Description");

                GameManager.Instance.isSelectedCardUsed = false;
                castCard();
            }
            else if (PlayerController.Instance.playerMana <= 0)
            {
                // PlayerPrefs.SetInt("playerCoin", PlayerPrefs.GetInt("playerCoin") + 30);
                PlayerController.Instance.playerMana = 0;
            }
        }
        else
        {
            GameManager.Instance.GetComponent<CardSelectorController>().selectCard(gameObject.GetComponent<CardDisplay>());
            GameManager.Instance.isSelectedCardUsed = true;
        }

    }
    private void Update()
    {
        if (GameManager.Instance.isCardSelected && !cardDisplay.isSelectionCard)
        {
            if (GameManager.Instance.isAnyCardSelected && CardManager.Instance.selectedCard.GetComponent<CardDisplay>().cardTarget == CardTarget.ClosestEnemy &&Input.GetMouseButtonDown(0) && !GameManager.Instance.isSelectedCardUsed)
            {
                Enemy selectedEnemy = GameManager.Instance.GetComponent<FightSceneAligner>().FindClosestEnemy().GetComponent<Enemy>();
                CardManager.Instance.selectedEnemies.Add(selectedEnemy);
                CardManager.Instance.UseSelectedCard(CardTarget.ClosestEnemy);
                GameManager.Instance.isSelectedCardUsed = true;
            }
            if (Input.GetMouseButtonDown(1))
            {
                line = GameObject.FindGameObjectsWithTag("Line");
                foreach (var item in line)
                {
                    Destroy(item.gameObject);
                }
                highlightEffect = CardManager.Instance.selectedCard.transform.GetChild(0).gameObject;
                highlightEffect.SetActive(false);
                CardManager.Instance.selectedCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                CardManager.Instance.selectedCard.transform.parent = hand.transform;
                CardManager.Instance.selectedCard.transform.position = hand.transform.position;
                CardManager.Instance.selectedCard = null;
                GameManager.Instance.isCardSelected = false;
                GameManager.Instance.isAnyCardSelected = false;
                TooltipSystem.current.Hide();
            }
        }
    }

    void castCard()
    {
        lineController.drawLine = true;
        Debug.Log(CardManager.Instance.selectedCard);
        CardManager.Instance.selectedCard.transform.parent = castingPlace.transform;
        CardManager.Instance.selectedCard.transform.position = castingPlace.transform.position;
        GameManager.Instance.isCardSelected = true;
        GameManager.Instance.isAnyCardSelected = true;
    }
}
