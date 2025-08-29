using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;
    private int shield = 0;
    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    private SpriteRenderer SRenderer;
    private Color startingColor;
    public bool isBoss = false;
    [HideInInspector] public int shotChance =50; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin = 0.5f, shotTimeMax = 1f; //max and min time for shooting from the beginning of the path
    #endregion

    private void Start()
    {
        if (isBoss) InvokeRepeating("ActivateShooting", 1, Random.Range(0.3f, 0.6f));
        else Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if(!isBoss) SetupShield(1);
        } 
        

    }

    //coroutine making a shot
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
        {                         
            Instantiate(Projectile,  gameObject.transform.position, Quaternion.identity);             
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage) 
    {
        if (shield > 0)
        {
            shield -= damage;
            if (shield <= 0) SRenderer.color = startingColor;
            return;
        }
       

        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
    }    

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }

    private void SetupShield(int shieldValue)
    {
       SRenderer = GetComponent<SpriteRenderer>();
       startingColor = SRenderer.color;
       SRenderer.color = Color.cyan;
       shield = shieldValue;
        
    }
}
