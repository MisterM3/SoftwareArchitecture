using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEditor;


public class SpawnerTest
{

    private Spawner spawner;
    [SerializeField] Object[] holdList;

    

    GameObject[] differentEnemies;



    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        yield return new WaitForSeconds(0.5f);

        spawner = GameObject.FindObjectOfType<Spawner>();

        // differentEnemies = AssetDatabase.LoadAllAssetsAtPath("Assets/Prefabs") as GameObject[];
        differentEnemies = new GameObject[3];

        differentEnemies = GameObject.FindObjectOfType<PREFABS_FOR_TESTING>().prefabsDifferentEnemies;

        //   differentEnemies = (GameObject[])holdList;

    }


IEnumerator DestroyAllEnemiesFromPreviousTests()
    {
        EnemyUnit[] units = GameObject.FindObjectsOfType<EnemyUnit>();
        yield return new WaitForEndOfFrame();

        if (units != null)
        {
            foreach (EnemyUnit enemy in units) GameObject.Destroy(enemy.gameObject);
        }
        yield return new WaitForEndOfFrame();
    }


    [UnityTest]
    public IEnumerator SPAWNER_IS_IN_SCENE()
    {

        Assert.NotNull(spawner);
        return null;
    }

    [UnityTest]
    public IEnumerator SPAWNER_SPAWNS_ENEMYS()
    {

        DestroyAllEnemiesFromPreviousTests();

        spawner.SpawnEnemy(differentEnemies[0]);

        yield return new WaitForEndOfFrame();

        EnemyUnit[] units = GameObject.FindObjectsOfType<EnemyUnit>();

        Assert.AreEqual(1, units.Length);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SPAWNER_SPAWNS_ENEMY_TYPES()
    {
        DestroyAllEnemiesFromPreviousTests();

        for (int i = 0; i < differentEnemies.Length; i++)
        {
            spawner.SpawnEnemy(differentEnemies[i]);
        }

        yield return new WaitForEndOfFrame();

        EnemyUnit[] units = GameObject.FindObjectsOfType<EnemyUnit>();

        Assert.AreEqual(3, units.Length);
        yield return null;

    }
    [UnityTest]
    public IEnumerator SPAWNER_DOESNT_SPAWN_A_NON_ENEMY_THROWS_ERROR()
    {
        DestroyAllEnemiesFromPreviousTests();


        yield return new WaitForEndOfFrame();

        //Expects a 
        LogAssert.Expect(LogType.Error, "TRIED TO SPAWN A NON ENEMY UNIT");
        
        spawner.SpawnEnemy(spawner.gameObject);



    }
}
