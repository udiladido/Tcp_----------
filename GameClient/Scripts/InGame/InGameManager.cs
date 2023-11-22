using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UIElements;
using System.Linq;
using System.Reflection;

public class InGameManager : MonoBehaviour
{

    public static InGameManager inst { get; private set; }
    void Awake() => inst = this;


    public int TotalSp;
    public int CostSp;
    public bool isready = false;

    public int maxCount;
    public int readyCount;

    public bool GameNow;
    public bool IsDie;


    [SerializeField] Vector2[] originCharacterPosition;
    [SerializeField] SerializeCharacterData[] serializeCharacterDatas;
    [SerializeField] GameObject[] HeartImages;
    [SerializeField] GameObject[] HeartImagesOpp;
    [SerializeField] TMP_Text totalSPTMP;
    [SerializeField] TMP_Text CostSPTMP;

    [SerializeField] TMP_Text MaxCountTMP;
    [SerializeField] TMP_Text ReadyCountTMP;
    [SerializeField] TMP_Text WinloseTMP;

    public SpawnData spawnData;

    public List<Enemy> enemies;
    public List<Enemy_Opp> enemiesopp;
    public List<DamageTMP> damageTMPs;
 

    public int oppindex;

    void Start()
    {
        GameStart_setting();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Keypad0))
            StartCoroutine(StartWaveCo());

        ArrangeEnemies();
        ArrangeDamageTMPs();
    }


    public void GameStart_setting()
    {

        TotalSP = 100;
        CostSP = 10;
        MaxCount = 0;
        ReadyCount = 0;
    }


    public void GameStart()
    {
    
        StartCoroutine(StartWaveCo());


    }



    public void checkReady(int _ready)
    {
        ReadyCount += _ready;

    }

    public void LinkCheck(int Totalplayer)
    {
        MaxCount = Totalplayer;

    }

    public void Winner(bool win)
    {

        if (win)
            WinloseTMP.text = "WIN";
        else
            WinloseTMP.text = "LOSE";
    }


    public void SpawnClick()
    {

        if (TotalSP >= CostSP)
        {

            if (TryRandomSpawn())
            {
                TotalSP -= CostSP;
                CostSP += 10;

     
            }
        }



    }


    public int randomnum()
    {

        return Random.Range(0, enemies.Count);

    }


    public Enemy GetRandomEnemy(int rand)
    {

        if (enemies.Count <= 0)
            return null;

        return enemies[rand];


    }

  


    public int CostSP
    {
        get => CostSp;

        set
        {

            CostSPTMP.text = value.ToString();
            CostSp = value;
        }
    }

    public int TotalSP
    {
        get => TotalSp;

        set
        {

            totalSPTMP.text = value.ToString();
            TotalSp = value;
        }
    }



    public int MaxCount
    {
        get => maxCount;

        set
        {

            MaxCountTMP.text = value.ToString();
            maxCount = value;
        }
    }


    public int ReadyCount
    {
        get => readyCount;

        set
        {

            ReadyCountTMP.text = value.ToString();
            readyCount = value;

        }
    }




    public bool TryRandomSpawn(int level = 1)
    {

        var emptySerializeCharacterData = Array.FindAll(serializeCharacterDatas, x => x.isFull == false);
       

        if (emptySerializeCharacterData.Length <= 0)
            return false;

        int randIndex = emptySerializeCharacterData[Random.Range(0, emptySerializeCharacterData.Length)].index;

        ClientSend.spawnChar(randIndex);

        // 캐릭터 소환
        var characterobj = ObjectPooler.instance.SpawnFromPool("character", spawnData.GetOriginCharacterPosition(randIndex), Utils.QI);

        int RanCode = spawnData.GetRandomCharacterData().code;

        ClientSend.charCode(RanCode);

        var serializeCharacterData = new SerializeCharacterData(randIndex, true, RanCode, level);
        
        characterobj.GetComponent<Character>().SetUpcharacter(serializeCharacterData);

        serializeCharacterDatas[randIndex] = serializeCharacterData;
   


        return true;
    }



    void SpawnEnemy()
    {
        var enemyObj = ObjectPooler.instance.SpawnFromPool("enemy1", Utils.spawnPos1, Utils.QI);
        enemies.Add(enemyObj.GetComponent<Enemy>());


    }

    void SpawnEnemy_opp()
    {

        var enemy_Opp = ObjectPooler.instance.SpawnFromPool("enemy2", Utils.spawnPos2 ,Utils.QI);
        enemiesopp.Add(enemy_Opp.GetComponent<Enemy_Opp>());

    }


    public void SpawnOpp(int code)
    {

        var characterobj = ObjectPooler.instance.SpawnFromPool("CharOpp", spawnData.GetOppCharacterPosition(oppindex), Utils.QI);
        var serializeCharacterData = new SerializeCharacterData(oppindex, true, code, 1);
        characterobj.GetComponent<CharacterOpp>().SetUpcharacter(serializeCharacterData);
       

    }


 

    IEnumerator StartWaveCo()
    {
        for (int i = 0; i < 10; i++)
        {

            SpawnEnemy();
            SpawnEnemy_opp();
            
            yield return Utils.delayWave;

        }

    }


    public void DecreaseHeart()
    {

        if (IsDie)
            return;


        for (int i = 0; i < HeartImages.Length; i++)
        {

            if (HeartImages[i].activeSelf)
            {

                HeartImages[i].SetActive(false);
   

                if (Array.TrueForAll(HeartImages, x => x.activeSelf == false))
                {
                    IsDie = true;
                    UIManager.instance.EndGame();
                    Winner(false);
                    Stop();
                }

                break;
            }

        

        }




    }



    public void Stop()
    {
        
        Time.timeScale = 0;
    }


    public void DecreaseHeart_opp()
    {


        if (IsDie)
            return;

        for (int i = 0; i < HeartImagesOpp.Length; i++)
        {

            if (HeartImagesOpp[i].activeSelf)
            {

                HeartImagesOpp[i].SetActive(false);



                if (Array.TrueForAll(HeartImagesOpp, x => x.activeSelf == false))
                {
                    IsDie = true;
                    UIManager.instance.EndGame();
                    Winner(true);
                    Stop();
                }


                break;
            }

        }

    }



    public Vector2 GetOriginCharacterPosition(int index) => originCharacterPosition[index];



    public GameObject[] GetRayCastAll(int layerMask)
    {


        var mousePos = Utils.MousePos;
        mousePos.z = -100f;

        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(mousePos, Vector3.forward, float.MaxValue, 1 << layerMask);
        var results = Array.ConvertAll(raycastHit2D, x => x.collider.gameObject);

        return results;


    }



    void ArrangeEnemies()
    { 
    
    enemies.Sort((x, y) => x.distance.CompareTo(y.distance));



        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Order>().SetOrder(i);
        }


    }


    void ArrangeDamageTMPs()
    {

        for (int i = 0; i < damageTMPs.Count; i++)
        {
            damageTMPs[i].GetComponent<Order>().SetOrder(i);
        }; 
    
    }




}







