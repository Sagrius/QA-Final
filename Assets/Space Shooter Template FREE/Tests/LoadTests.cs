using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class LoadTests 
{
    [UnityTest]
    public IEnumerator SpawnXEnemiesInScene()
    {

        int amountToSpawn = 1000;
        int existingCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        GameObject dummyEnemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        dummyEnemy.tag = "Enemy";

        for (int i = 1; i < amountToSpawn; i++)
        {
            GameObject.Instantiate(dummyEnemy);
        }

        yield return null;

        int newCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        Assert.AreEqual(amountToSpawn + existingCount, newCount);
    }

}
