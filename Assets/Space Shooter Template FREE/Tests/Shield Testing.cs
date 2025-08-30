using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShieldTesting
{
    [Test]
    public void ShieldSetTest()
    {
        GameObject EnemyObject = new GameObject("Test Enemy", typeof(Enemy));
        Enemy _enemy = EnemyObject.GetComponent<Enemy>();

        int ShieldInput = 2;
        

        _enemy.SetupShield(ShieldInput);


        Assert.AreEqual(_enemy.Shield, ShieldInput);



    }

    [Test]
    public void ShieldTakeDamageTest()
    {
        GameObject EnemyObject = new GameObject("Test Enemy", typeof(Enemy));
        Enemy _enemy = EnemyObject.GetComponent<Enemy>();

        int ShieldInput = 2;

        _enemy.SetupShield(ShieldInput);

        int ShieldBeforeDamage = _enemy.Shield;

        _enemy.GetDamage(1);

        Assert.AreNotEqual(_enemy.Shield, ShieldBeforeDamage);

    }

    [Test]
    public void IsShieldNullified()
    {
        GameObject EnemyObject = new GameObject("Test Enemy", typeof(Enemy));
        Enemy _enemy = EnemyObject.GetComponent<Enemy>();

        int ShieldInput = 2;

        _enemy.SetupShield(ShieldInput);

        _enemy.GetDamage(_enemy.Shield + 1);

        Assert.AreEqual(_enemy.Shield, 0);

    }

    [Test]
    public void DamageAfterShield()
    {
        GameObject EnemyObject = new GameObject("Test Enemy", typeof(Enemy));
        Enemy _enemy = EnemyObject.GetComponent<Enemy>();

        int ShieldInput = 2;


        _enemy.SetupShield(ShieldInput);

        int HPBeforeDamage = _enemy.health;

        _enemy.GetDamage(_enemy.Shield + 1);

        Assert.AreNotEqual( _enemy.health, HPBeforeDamage);

    }
}
