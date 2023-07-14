using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    public Transform []gunPoint;
    public GameObject enemyBullet;
    public GameObject enemyFlash;
    public GameObject enemyExplosionPrefab;
    public Healthbar healthbar;
    public GameObject damageEffect;
    public GameObject coinPrefab;
    public GameObject powerupPrefab;
    public float speed = 1f;
    public float health = 10f;
    public AudioClip bulletSound;
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    float barSize = 1f;
    float damageAmount = 0;
    public float enemyBulletSpawnTime = 0.5f;
    void Start()
    {
        enemyFlash.SetActive(false);
        StartCoroutine(EnemyShooting());
        damageAmount = barSize / health;
    }
    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            DamageHealthbar(0.5f,collision);
          
        }
        if (collision.gameObject.tag == "missile")
        {
            Destroy(collision.gameObject);
            DamageHealthbar(1f, collision);
          

        }

    }

    public void DamageHealthbar(float damageAmount, Collider2D collision)
    {
        if (collision.gameObject)
        {
          
            audioSource.PlayOneShot(damageSound, 0.5f);
            GameObject damageVfx = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.5f);
            Debug.Log("damage");
        }
        
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
            Destroy(gameObject);
            GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(enemyExplosion, 0.4f);
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            //Instantiate(powerupPrefab, transform.position, Quaternion.identity);
        }

        if (health > 0)
        {
            health -= 1;
            barSize = barSize - damageAmount;
            healthbar.Setsize(barSize);
        }
    }

    void EnemyFire()
        {
            for(int i = 0; i<gunPoint.Length; i++)
            {
                Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
            }
        }
       IEnumerator EnemyShooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(enemyBulletSpawnTime);
                EnemyFire();
                audioSource.PlayOneShot(bulletSound, 0.5f);
                enemyFlash.SetActive(true);
                yield return new WaitForSeconds(0.08f);
                enemyFlash.SetActive(false);
            }
        }
}