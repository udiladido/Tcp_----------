using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{

    [SerializeField] float speed = 5f;

    public float distance;
    public int waynum = 0;
    

    public static Vector2[] waypoint = { new Vector2 (-2.3f, -3.5f) , new Vector2(-2.3f, -0.9f), new Vector2(2.3f, 0.9f), new Vector2(2.3f, -0.9f) };
    //public static Vector2[] waypoint_opp = { new Vector2(-2.3f, 3.5f), new Vector2(-2.3f, 0.7f), new Vector2(2.3f, 0.7f), new Vector2(2.3f, 3.5f) };
  
    


    IEnumerator MovePath()
    {
        while (true)
        {

            transform.position = Vector2.MoveTowards(transform.position, waypoint[waynum], speed * Time.deltaTime); ;

            distance += speed * Time.deltaTime;

            if ((Vector2)transform.position == waypoint[waynum])
                waynum++;

            if ((Vector2)transform.position == waypoint[waypoint.Length])
            {


                // 적 삭제


                // HP 감소
                
                
                yield break;

            }

            yield return null;
        }
    
    }



}
