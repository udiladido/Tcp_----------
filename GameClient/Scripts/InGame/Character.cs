using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{

    [Header("Conponents")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Values")]
   
    [SerializeField] Transform[] stars;
    [SerializeField] Order order;

    public int StarCount;

    public SerializeCharacterData serializeCharacterData { get; private set; }

    public CharacterData characterData => InGameManager.inst.spawnData.GetCharacterData(serializeCharacterData.code);


    public void SetUpcharacter(SerializeCharacterData serializeCharacterData)
    {
        this.serializeCharacterData = serializeCharacterData;
        var charData = InGameManager.inst.spawnData.GetCharacterData(serializeCharacterData.code);

        spriteRenderer.sprite = characterData.sprite;


        SetStar(serializeCharacterData.level);



        for (int i = 0; i < Utils.Max_Character_level; i++)
        {
            stars[i].GetComponent<SpriteRenderer>().color = characterData.color;
        }


        if (gameObject.activeSelf)
            StartCoroutine(AttackCo());

    }

    public void SetStar(int level)
    {

        Vector2[] positions = Utils.GetStarPositions(level);
        int starCount = 0;

        for (int i = 0; i < Utils.Max_Character_level; i++)
        {
            stars[i].gameObject.SetActive(i < level);
            stars[i].localPosition = i < level ? positions[i] : Vector2.zero;

            if (i < level)
                starCount++;
        }

        this.StarCount = starCount;

    }



    private void OnDisable()
    {
        serializeCharacterData = null;
        spriteRenderer.sprite = null;
        SetStar(0);

    
    }

    public void OnMouseDown()
    {
        order.SetMostFrontOrder(true);

    }


    public void OnMouseDrag()
    {
        transform.position = Utils.MousePos;

    }

    public void OnMouseUp()
    {


        MoveTransform(true, InGameManager.inst.GetOriginCharacterPosition(serializeCharacterData.index), 0.2f, () => order.SetMostFrontOrder(false));


        // 같은 레벨, 코드와 합쳐지는 거 
        GameObject[] raycastAll = InGameManager.inst.GetRayCastAll(Utils.Character_Layer);
  
        GameObject targetCharacterObj = Array.Find(raycastAll, x => x.gameObject != gameObject);

        

        if (targetCharacterObj != null)
        {

            var targetCharacter = targetCharacterObj.GetComponent<Character>();

            

            if (serializeCharacterData.code == targetCharacter.serializeCharacterData.code
                && serializeCharacterData.level == targetCharacter.serializeCharacterData.level)
            {

               
                int nextLevel = targetCharacter.serializeCharacterData.level + 1;
                if (nextLevel > Utils.Max_Character_level)
                    return;


                ClientSend.TargetMerge(targetCharacter.serializeCharacterData.index, serializeCharacterData.index); // marge 대상, 자신 index
                


                //  합쳐졌을때 렌덤하게 변하도록 하는 코드
                //  var targetSerializeCharacter = targetCharacter.serializeCharacterData;
                // targetSerializeCharacter.code = InGameManager.inst.spawnData.GetRandomCharacterData().code;
                // targetSerializeCharacter.level = nextLevel;


                // targetCharacter.SetupCharacter(targetSerialzeCharacterData);    
                // targetCharacter.SetStar(Mathf.Clamp(nextLevel, 1, Utils.Max_Character_level));


    
                targetCharacter.SetStar(Mathf.Clamp(nextLevel, 1, Utils.Max_Character_level));
                gameObject.SetActive(false);

            }
        }

    }





    void MoveTransform(bool useDotween, Vector2 targetPos, float duration = 0f, TweenCallback action = null)
    {

        if (useDotween)
        {
            transform.DOMove(targetPos, duration).OnComplete(action);
        }

        else
        {
            transform.position = targetPos;
        }


    }

   


    IEnumerator AttackCo()
    {

        while (true)
        {

            for (int i = 0; i < StarCount; i++)
            {

                int targetnum = InGameManager.inst.randomnum();
                Enemy targetEnemy = InGameManager.inst.GetRandomEnemy(targetnum);


                if (targetEnemy != null)
                {
                    ClientSend.TargetOpp(targetnum, serializeCharacterData.index, i); //적, 누가 쏘는지, 별 위치
                    var characterBulletObj = ObjectPooler.instance.SpawnFromPool("bullet", stars[i].position, Utils.QI);

                    characterBulletObj.GetComponent<Bullet>().SetupCharacterBullet(serializeCharacterData, targetEnemy);

                }
            }
        
            yield return Utils.delayCharacterBulletSpawn;
        }


        
    }



}
