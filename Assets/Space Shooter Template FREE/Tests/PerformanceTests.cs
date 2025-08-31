using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.PerformanceTesting;
using UnityEngine.SceneManagement;

public class PerformanceTests
{
    [UnityTest, Performance, Order(0)]
    public IEnumerator MeasureFindAnyObjestOfTypeEnemy()
    {
        SceneManager.LoadScene(0);
        yield return null;
        
        LevelController level = Object.FindAnyObjectByType<LevelController>();
        Assert.IsNotNull(level);

        yield return new WaitForSeconds(2);

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
    public IEnumerator MeasureFrameRateFor10000Frames()
    {

        SceneManager.LoadScene(0);
        yield return null;

        LevelController level = Object.FindAnyObjectByType<LevelController>();
        Assert.IsNotNull(level);
        Player.isInvulnerable = true;

        yield return Measure.Frames()
            .SampleGroup("FPS_Test")
            .WarmupCount(5)
            .MeasurementCount(10000)
            .Run();

        Player.isInvulnerable = false;

        // 1000 / Avg = FPS 
    }






}
