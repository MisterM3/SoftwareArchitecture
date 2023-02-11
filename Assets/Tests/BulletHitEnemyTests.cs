using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEditor;
public class BulletHitEnemyTests
{


    private Spawner spawner;
    [SerializeField] Object[] holdList;



    GameObject enemy;
    GameObject bullet;



    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        yield return new WaitForSeconds(0.5f);

        //Grab normal enemy
        enemy = GameObject.FindObjectOfType<PREFABS_FOR_TESTING>().prefabsDifferentEnemies[0];

        //Grab normal bullet
        bullet = GameObject.FindObjectOfType<PREFABS_FOR_TESTING>().prefabsDifferentBullets[0];

        yield return new WaitForSeconds(0.5f);

        //   differentEnemies = (GameObject[])holdList;

    }




    [UnityTest]
    public IEnumerator ONE_BULLET_HITS_ONLY_ONE_ENEMY()
    {

        GameObject enemy1 = GameObject.Instantiate(enemy, new Vector3(10,10,10), Quaternion.identity);
        GameObject enemy2 = GameObject.Instantiate(enemy, new Vector3(10, 10, 10), Quaternion.identity);
        GameObject bullet1 = GameObject.Instantiate(bullet, new Vector3(10, 10, 10), Quaternion.identity);


        yield return new WaitForEndOfFrame();

        EnemyUnit enemyUnit1 = enemy1.GetComponent<EnemyUnit>();
        EnemyUnit enemyUnit2 = enemy2.GetComponent<EnemyUnit>();
        SingleShotBullet singlebullet = bullet1.GetComponent<SingleShotBullet>();

        int damage = singlebullet.damage;
        yield return new WaitForEndOfFrame();

        //Bullet hit the first enemy and not the second
        Assert.AreEqual(100 - damage, enemyUnit1.GetHealth());
        Assert.AreEqual(100, enemyUnit2.GetHealth());
 
    }
 


}
