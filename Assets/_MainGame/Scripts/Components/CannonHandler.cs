using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHandler : MonoBehaviour
{
    public GameObject canonBulletPrefab;
    public Transform shootPos;
    public float forceShoot = 850f;
    Timer spawnCanonBulletTime = new Timer();
    List<BulletHandler> canonBulletList = new List<BulletHandler>();
    // Start is called before the first frame update
    void Start()
    {
        spawnCanonBulletTime.SetDuration(3.0f);//shoot a bullet every 3s
    }

    // Update is called once per frame
    void Update()
    {
        spawnCanonBulletTime.Update(Time.deltaTime);
        if (spawnCanonBulletTime.JustFinished()) //after 3s we will shoot a bullet
        {
            spawnCanonBulletTime.Reset();
            GameObject go = GetFreeBullet();// get bullet from pool object
            go.GetComponent<BulletHandler>().SetActive(transform.forward * forceShoot); // active bullet
        }
    }

    public GameObject GetFreeBullet()
    {
        BulletHandler bullet = null;
        //search in the pool
        foreach (var cb in canonBulletList)
        {
            if (!cb.Active()) //if has already bullet and it is inactive, get it
            {
                bullet = cb;
                bullet.transform.localPosition = Vector3.zero; //reset position of bullet
                bullet.transform.localRotation = Quaternion.identity; //reset rotation of bullet
                break;
            }
        }

        //if no bullet in pool or no have inactive bullet
        if (bullet == null)
        {
            bullet = Instantiate(canonBulletPrefab, shootPos.position, Quaternion.identity).GetComponent<BulletHandler>(); // instantiate new bullet
            bullet.transform.SetParent(shootPos);
            canonBulletList.Add(bullet); //add bullet in pool
        }

        return bullet.gameObject;
    }

    private void OnDestroy()
    {
        canonBulletList.Clear();
    }
}
