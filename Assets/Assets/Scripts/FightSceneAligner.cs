using System.Collections;
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
    void Update()
    {
        distanceOnLeft = Vector3.Distance(midOfScene.position, leftOfScene.position);

        distanceOnRight = Vector3.Distance(midOfScene.position, rightOfScene.position);
    }
}
