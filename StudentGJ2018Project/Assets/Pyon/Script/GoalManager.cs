using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : SingletonMonoBehaviour<GoalManager>
{
    List<Goal> goalObject;

    Vector3 nearPoint;

    /// <summary>
    /// 一番近いゴールの要素数をもつ
    /// </summary>
    int num;

    Vector3 GetNearGoalPosition(Vector3 humanPos)
    {
        if (goalObject.Count == 0)
            return Vector3.zero;

        float a = 100000000000000000;

        for(int i = 0; i < goalObject.Count; i++)
        {
            float d = (humanPos - goalObject[i].transform.position).magnitude;

            if(a > d)
            {
                d = a;
                num = i;
            }
        }

        return goalObject[num].transform.position;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
