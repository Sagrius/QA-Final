using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

//We hook for errors
public class StressTests
{
  
    [UnityTest]
    public IEnumerator RepeatLevel1Reload()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return SceneManager.LoadSceneAsync(0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    [UnityTest]
    public IEnumerator RepeatLevel2Reload()
    {
        for (int i = 0; i < 30; i++)
        {
            yield return SceneManager.LoadSceneAsync(1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    [Test]
    public void CreateAndDestroyRapidly()
    {
        for (int i = 0; i < 1000; i++)
        {
            var go = new GameObject("Temp",typeof(Enemy));
            GameObject.DestroyImmediate(go);
        }
        Assert.Pass();
    }
}
