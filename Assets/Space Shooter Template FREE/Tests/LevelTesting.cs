using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


public class LevelTesting
{

    [UnityTest,Timeout(120_000)]
    public IEnumerator TransitionToLevelTwo()
    {
        SceneManager.LoadScene(0);
        yield return null;

        LevelController level = Object.FindAnyObjectByType<LevelController>();
        Assert.IsNotNull(level);

        while (!level.ReadyToTransition)
        {
            yield return null;
        }

        float timer = 0;
        bool transitioned = false;
        while (timer < 15 && !transitioned)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                transitioned = true;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        Assert.IsTrue(transitioned);

    }
}
