using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField] TMP_Text healthTMP;

    public float distance;
    public int waynum = 0;

    public static Vector2[] waypoint = { new Vector2(-2.38f, -2.21f), new Vector2(-2.38f, 0.5f), new Vector2(2.38f,  0.6f), new Vector2(2.38f, -2.21f) };
   

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
            InGameManager.inst.TotalSP += 10;
            
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

                ClientSend.playerHealth(1);

                // 적 삭제
                gameObject.SetActive(false);

                // HP 감소
                InGameManager.inst.DecreaseHeart();

                
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
