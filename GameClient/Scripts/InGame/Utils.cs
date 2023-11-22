using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SerializeCharacterData
{

    public int index;
    public bool isFull;
    public int code;
    public int level;

    public SerializeCharacterData(int index, bool isFull, int code, int level)
    {

        this.index = index;
        this.isFull = isFull;
        this.code = code;
        this.level = level;

    }


}

public class Utils : MonoBehaviour
{


    public const int Max_Character_level = 3;
    public const int Character_Layer = 3; //LayerMask 번호 3
    public const int Character_Opp = 6; //LayerMask 6번
    public const int Enemy_Opp = 7; //LayerMask 7번
    public static readonly Quaternion QI = Quaternion.identity;


    public static Vector3 MousePos
    {

        get
        {

            var result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = 0;
            return result;
        }

    }


  /*  public static GameObject[] GetRayCastAll(int layerMask)
    {
        var mousePos = MousePos;
        mousePos.z = -100f;
        var raycastHit2D = Physics2D.RaycastAll(mousePos, Vector3.forward, float.MaxValue, 1 << layerMask);

        return Array.ConvertAll(raycastHit2D, x => x.collider.gameObject);
    }
*/
  
    public static Vector2[] GetStarPositions(int level) => level switch
    {

        1 => new Vector2[] { new Vector2(-0.15f,-0.23f) },
        2 => new Vector2[] { new Vector2(-0.15f, -0.23f), new Vector2(0, -0.23f)},
        3 => new Vector2[] { new Vector2(-0.15f, -0.23f), new Vector2(0, -0.23f), new Vector2(0.15f,-0.23f)},
        _ => new Vector2[] { Vector2.zero }

    };
    

    public static int TotalAttackDamage(int basicAttackDamage, int level)
    {

        int result = basicAttackDamage + level * 2;

        return result;

    }



    public static readonly Vector2 spawnPos1 = new Vector2(-2.38f, -2.21f);
    public static readonly Vector2 spawnPos2 = new Vector2(2.38f, 4.26f);

    public static readonly WaitForSeconds delayWave = new WaitForSeconds(11f);
    public static readonly WaitForSeconds delayshot = new WaitForSeconds(3f);


    public static readonly WaitForSeconds delayCharacterBulletSpawn = new WaitForSeconds(1f);
    





}
