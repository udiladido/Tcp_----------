using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


public class DamageTMP : MonoBehaviour
{

    Transform target;
    float totalTime;

    [SerializeField] TMP_Text damageTMP;
    [SerializeField] float minOffsetY;
    [SerializeField] float maxOffsetY;




    public void Setup(Transform target, int damageAmount)
    {
        gameObject.SetActive(true);
        this.target = target;
        totalTime = 0f;


        damageTMP.text = damageAmount.ToString();

        StartCoroutine(DamageTMPCo(damageAmount));

    }

    IEnumerator DamageTMPCo(int damageAmount)
    {

        // 위에 존재
        while (totalTime <= 0.5f)
        {

            if (target != null)
            {
                var targetPos = target.position;
                targetPos.y += minOffsetY;
                transform.position = targetPos;



            }

            totalTime += Time.deltaTime;
            yield return null;
        }


        // 점점 올라가며 페이드 아웃

        totalTime = 0f;
        while (totalTime <= 0.2f)
        {
            float lerpTime = totalTime * 2;

            if (target != null)
            {
                var targetPos = target.position;
                targetPos.y += Mathf.Lerp(minOffsetY, maxOffsetY, totalTime * 2);
                transform.position = targetPos;

                damageTMP.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), lerpTime);
            }

            totalTime += Time.deltaTime;
            yield return null;
        }



        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        InGameManager.inst.damageTMPs.Remove(this);
        target = null;
        totalTime = 0f;
        damageTMP.text = "";
    }

}
