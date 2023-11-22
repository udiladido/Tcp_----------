using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[System.Serializable]

public class CharacterData {

    public int code;
    public Sprite sprite;
    public Color color;
    public int BasicAttackDamage;

}

[CreateAssetMenu(fileName = "SpawnData_SO", menuName = "Scriptable Object/SpawnerData_SO")]
public class SpawnData : ScriptableObject
{

    public CharacterData[] characterData;
    [SerializeField] Vector2[] OriginCharacterPosition;
    [SerializeField] Vector2[] OppCharacterPosition;
    public CharacterData GetCharacterData(int code) => Array.Find(characterData, x=>x.code == code);

    public CharacterData GetRandomCharacterData() => characterData[UnityEngine.Random.Range(0, characterData.Length)];

    public Vector2 GetOriginCharacterPosition(int index) => OriginCharacterPosition[index];
    public Vector2 GetOppCharacterPosition(int index) => OppCharacterPosition[index];

}
