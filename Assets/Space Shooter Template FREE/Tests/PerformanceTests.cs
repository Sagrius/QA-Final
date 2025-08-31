using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.PerformanceTesting;
using UnityEngine.SceneManagement;

public class PerformanceTests
{
    [UnityTest, Performance, Order(0)]
    public IEnumerator MeasureFindAnyObjestOfType()
    {
        SceneManager.LoadScene(0);
        yield return null;
        
        LevelController level = Object.FindAnyObjectByType<LevelController>();
        Assert.IsNotNull(level);

        while (!level.ReadyToTransition)
        {
            yield return null;
        }

        Measure.Method(() =>
        {
             Object.FindAnyObjectByType<Enemy>();
        })
        .SampleGroup("Method Time")
        .WarmupCount(5)
        .MeasurementCount(20)
        .Run();
    }


    [UnityTest, Performance, Order(1)]
    public IEnumerator FrameRateOnAllWavesSpawnedLevel1()
    {

        SceneManager.LoadScene(0);
        yield return null;

        LevelController level = Object.FindAnyObjectByType<LevelController>();
        Assert.IsNotNull(level);
        Player.isInvulnerable = true;
        Time.timeScale = 20;

        yield return new WaitUntil(() => level.StartingToTestForLEvelTransition);

        yield return Measure.Frames()
            .Scope("TestingEndOfLevel");


        Player.isInvulnerable = false;
        Time.timeScale = 1;
    }

    [UnityTest, Performance, Order(2)]
    public IEnumerator FrameRateOnAllWavesSpawnedLevel2()
    {

        SceneManager.LoadScene(1);
        yield return null;

        LevelController level = Object.FindAnyObjectByType<LevelController>();
        Assert.IsNotNull(level);

        yield return new WaitForSeconds(level.LastDelay + 0.5f);


        yield return Measure.Frames()
            .SampleGroup("FPS")
            .MeasurementCount(30)
            .Run();
    }




}
