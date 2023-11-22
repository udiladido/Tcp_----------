using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Opp : MonoBehaviour
{

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] ParticleSystem _particleSystem;


    [SerializeField] SerializeCharacterData serializeCharacterData;

    [SerializeField] float speed;

    public CharacterData characterData => InGameManager.inst.spawnData.GetCharacterData(serializeCharacterData.code);
    Enemy_Opp targetEnemy;

    public void SetupCharacterBullet(SerializeCharacterData serializeCharacterData, Enemy_Opp targetEnemy)
    {

        this.serializeCharacterData = serializeCharacterData;
        this.targetEnemy = targetEnemy;
        spriteRenderer.color = characterData.color;

        var particleMain = _particleSystem.main;
        particleMain.startColor = characterData.color;

        StartCoroutine(AttackCo());


    }



    IEnumerator AttackCo()
    {

        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
            yield return null;

            if ((transform.position - targetEnemy.transform.position).sqrMagnitude < speed * Time.deltaTime)
            {
                transform.position = targetEnemy.transform.position;
                break;


            }


        }
        // 데미지를 가함
        int totalAttackDamage = Utils.TotalAttackDamage(characterData.BasicAttackDamage, serializeCharacterData.level);

        if (targetEnemy != null)
        {
            targetEnemy.Damaged(totalAttackDamage);
            var damageTMP = ObjectPooler.instance.SpawnFromPool("damageTMP", targetEnemy.transform.position, Utils.QI).transform.GetComponent<DamageTMP>();
            damageTMP.Setup(targetEnemy.transform, totalAttackDamage);
            InGameManager.inst.damageTMPs.Add(damageTMP);
        }

        Die();

    }



    void Die()
    {
        spriteRenderer.enabled = false;
        _particleSystem.Play();
    }

    
   



}
