using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> patrolPoints = new List<GameObject>();
    public List<GameObject> PatrolPoints { get { return patrolPoints; } }

    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
                instance.PatrolPoints.AddRange(GameObject.FindGameObjectsWithTag("PatrolPoint"));
                instance.patrolPoints = instance.patrolPoints.OrderBy(waypoint => waypoint.name).ToList();
            }
            return instance;
        }
    }
}
