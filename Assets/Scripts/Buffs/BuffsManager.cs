using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    #region Singleton

    private static BuffsManager _instance;
    public static BuffsManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    public List<Buff> availableBuffs;
    public List<Buff> spawnedBuffs { get; set; }

    [Range(0, 100)]
    public float BuffChance;

    private float buffSize = 0.4f;

    private void Start()
    {
        spawnedBuffs = new List<Buff>();
    }
    public void ClearSpawnedBuffs()
    {
        foreach (Buff buff in spawnedBuffs.ToList())
        {
            spawnedBuffs.Remove(buff);
            Destroy(buff.gameObject);
        }
    }

    public void GenerateBuff(Transform brickPos)
    {
        float spawnChance = Random.Range(0, 100f);

        if (spawnChance <= BuffChance)
            SpawnBuff(brickPos);
    }

    private Buff SpawnBuff(Transform brickPos)
    {
        List<Buff> buffList;

        buffList = availableBuffs;

        int buffIndex = Random.Range(0, buffList.Count);

        Buff prefab = buffList[buffIndex];
        Buff newBuff = Instantiate(prefab, brickPos.position, Quaternion.identity);
        newBuff.transform.localScale = new Vector3(buffSize, buffSize, buffSize);

        spawnedBuffs.Add(newBuff);

        return newBuff;
    }
}
