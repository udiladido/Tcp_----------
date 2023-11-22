using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class CharacterOpp : MonoBehaviour
{
    public static CharacterOpp instance;


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




    public void oppmerge(int _merge)
    {

        GameObject[] EnemyAll = GameObject.FindGameObjectsWithTag("CharacterOpp");
        GameObject targetObj = Array.Find(EnemyAll, x => x.GetComponent<CharacterOpp>().serializeCharacterData.index == _merge);
        
        var targetCharacter = targetObj.GetComponent<CharacterOpp>();


        int nextLevel = serializeCharacterData.level + 1;

        targetCharacter.SetStar(Mathf.Clamp(nextLevel, 1, Utils.Max_Character_level));
        gameObject.SetActive(false);

        

    }


    public void AttackCo(int target, int star)
    {

        Enemy_Opp targetEnemy = InGameManager.inst.enemiesopp[target];
                
        var characterBulletObj = ObjectPooler.instance.SpawnFromPool("bulletOpp", stars[star].position, Utils.QI);
        characterBulletObj.GetComponent<Bullet_Opp>().SetupCharacterBullet(serializeCharacterData, targetEnemy);


    }



}
