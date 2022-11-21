using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneAligner : MonoBehaviour
{
    [SerializeField] Transform leftOfScene;
    [SerializeField] Transform rightOfScene;
    [SerializeField] Transform midOfScene;

    public float distanceOnLeft, distanceOnRight;
    void Start()
    {
        distanceOnLeft = Vector3.Distance(midOfScene.position, leftOfScene.position);

        distanceOnRight = Vector3.Distance(midOfScene.position, rightOfScene.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        distanceOnLeft = Vector3.Distance(midOfScene.position, leftOfScene.position);

        distanceOnRight = Vector3.Distance(midOfScene.position, rightOfScene.position);

        AlignEnemies();
        Debug.Log(FindClosestEnemy().name);
    }

    public float CalculateDistanceToMiddle(Transform myTransform){
        var distanceToMiddle = Vector3.Distance(myTransform.position, midOfScene.position);
        return distanceToMiddle;
    }
    public GameObject FindClosestEnemy(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<float> distances = new List<float>();
        foreach (GameObject enemy in enemies){
            var x = CalculateDistanceToMiddle(enemy.transform);
            distances.Add(x);
        }
        int closestEnemyIndex = distances.IndexOf(distances.Min());
        return enemies[closestEnemyIndex];
        
    }
    private void AlignEnemies(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float alignmentSpace = distanceOnRight / (enemies.Length + 1);
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = new Vector3(midOfScene.position.x + ((i + 1) * alignmentSpace) - 2, midOfScene.position.y - 2, midOfScene.position.z);
        }
    }
}
