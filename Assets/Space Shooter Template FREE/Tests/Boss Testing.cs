using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class BossTesting
{
    private GameObject _bossInstance;
    private Enemy _bossBehavior;

    private GameObject _enemyInstance;
    private Enemy _enemyBehavior;


    // sets up the prefabs before all tests. the use of [UnitySetUp] is so it runs before the tests. 
    // For non UnityTests, you can use [Setup] to set everything you wanted up.
    [UnitySetUp]
    public IEnumerator SetupEntities()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Space Shooter Template FREE/Prefabs/Enemies/Enemy_straight_Boss.prefab");
        Assert.IsNotNull(prefab);
        _bossInstance = Object.Instantiate(prefab);
        _bossBehavior = _bossInstance.GetComponent<Enemy>();
        yield return null;

        var BasicEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Space Shooter Template FREE/Prefabs/Enemies/Enemy_straight_Projectile.prefab");
        Assert.IsNotNull(BasicEnemy);

        _enemyInstance = Object.Instantiate(BasicEnemy);
        _enemyBehavior = _enemyInstance.GetComponent<Enemy>();

        yield return null;

    }

    [UnityTest]
    public IEnumerator TestExistanceOfMovementComponent()
    {
        Assert.IsTrue(_bossInstance.TryGetComponent(out BossMovement movement));
        yield return null;
    }


    [UnityTest]
    public IEnumerator BossTestHPAmount()
    {
        Assert.Greater(_bossBehavior.health,_enemyBehavior.health);   
        yield return null;

        while(_bossBehavior.health > 0 && _enemyBehavior.health > 0) 
        {
            _bossBehavior.GetDamage(1);
            _enemyBehavior.GetDamage(1);
        }

        Assert.IsTrue(_bossBehavior.health > 0 && _enemyBehavior.health == 0);
    }

    //Removes all objects after the run of the test. the use of [UnityTearDown] ensures that it runs last. 
    //for non UnityTests, you can use [TearDown]
    [UnityTearDown]
    public IEnumerator ClearEntities()
    {
        _bossBehavior = null;
        _enemyBehavior = null;
        if (_bossInstance) Object.Destroy( _bossInstance );
        if (_enemyInstance) Object.Destroy( _enemyInstance );
        yield return null;
    }

}
