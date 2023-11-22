using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_Opp : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField] TMP_Text healthTMP;

    public float distance;
    public int waynum = 0;

    public static Vector2[] waypoint = { new Vector2(2.38f, 4.26f), new Vector2(2.38f,1.58f), new Vector2(-2.38f, 1.58f), new Vector2(-2.38f ,4.26f)};

    public void Start()
    {

        StartCoroutine(MovePath());

    }


    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthTMP.text = value.ToString();


        }


    }


    public void Damaged(int damage)
    {

        Health -= damage;
        Health = Mathf.Max(0, Health);

        if (Health <= 0 && gameObject.activeSelf)
        {
         

            gameObject.SetActive(false);

        }


    }



    IEnumerator MovePath()
    {
        while (true)
        {     
     
            transform.position = Vector2.MoveTowards(transform.position, waypoint[waynum], speed * Time.deltaTime);
            distance += speed * Time.deltaTime;


            if ((Vector2)transform.position == waypoint[waynum])
                waynum++;

            if (waynum == waypoint.Length)
            {

                // Àû »èÁ¦

                gameObject.SetActive(false);
          
                yield break;

            }

            yield return null;
        }

    }



    private void OnEnable()
    {
        health = 20;
    }


    public void OnDisable()
    {


        distance = 0;
        waynum = 0;




    }




}
