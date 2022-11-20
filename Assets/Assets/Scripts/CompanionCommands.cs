using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class CompanionCommands : MonoBehaviour
{
    [SerializeField] GameObject buttonsParent;
    [SerializeField] TextMeshProUGUI[] buttons;
    public bool companionTurn = false;

    private void Update()
    {
        if (companionTurn)
        {
            buttonsParent.SetActive(true);
            GenerateRandomEvent();
        }
    }
    public void GenerateRandomEvent()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int randomNumber = Random.Range(0, 3); // Şu an 3 yazıyor ama her yeni fonksiyon eklendiğinde sayının bir artması lazım
            switch (randomNumber)
            {
                case 0: // Attack
                    buttons[i].text = "Attack";
                    buttons[i].transform.parent.name = "Attack";
                    break;
                case 1: // Guard
                    buttons[i].text = "Guard";
                    buttons[i].transform.parent.name = "Guard";
                    break;
                case 2: // Heal
                    buttons[i].text = "Heal";
                    buttons[i].transform.parent.name = "Heal";
                    break;
            }

        }
        companionTurn = false;
    }
    public void ApplyGeneratedEvent(){
        switch(EventSystem.current.currentSelectedGameObject.name){
            case "Attack": // Attack
                Attack();
                break;
            case "Guard": // Attack
                Guard();
                break;
            case "Heal": // Attack
                Heal();
                break;
        }
    }

    public void Attack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies[0].GetComponent<Enemy>().getDamage(4);
        GameManager.Instance.GetComponent<TurnController>().endTurn();
    }
    public void Guard()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().changeShield(4);
        GameManager.Instance.GetComponent<TurnController>().endTurn();
    }
    public void Heal()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().changeHealth(4);
        GameManager.Instance.GetComponent<TurnController>().endTurn();
    }
}
