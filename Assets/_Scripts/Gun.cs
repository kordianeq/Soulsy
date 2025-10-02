using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] float shootForce;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
        if (Input.GetMouseButton(1))
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.transform.forward * 1000 * shootForce);
        Destroy(spawnedBullet, 5);
    }
}
