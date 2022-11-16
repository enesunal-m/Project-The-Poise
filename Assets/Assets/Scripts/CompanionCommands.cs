using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCommands : MonoBehaviour
{
    public void Attack(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies[0].GetComponent<Enemy>().getDamage(4);
        GameManager.Instance.GetComponent<TurnController>().endTurn();
    }
    public void Guard(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().changeShield(4);
        GameManager.Instance.GetComponent<TurnController>().endTurn();
    }
    public void Heal(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().changeHealth(4);
        GameManager.Instance.GetComponent<TurnController>().endTurn();
    }
}
