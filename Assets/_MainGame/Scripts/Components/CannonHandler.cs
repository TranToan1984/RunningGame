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
        if (spawnCanonBulletTime.JustFinished())
        {
            spawnCanonBulletTime.Reset();
            GameObject go = GetFreeBullet();// get bullet from pool object
            go.GetComponent<BulletHandler>().SetActive(transform.forward * forceShoot);
        }
    }

    public GameObject GetFreeBullet()
    {
        BulletHandler bullet = null;
        foreach (var cb in canonBulletList)
        {
            if (!cb.Active()) //if already bullet and it is inactive, get it
            {
                bullet = cb;
                bullet.transform.localPosition = Vector3.zero;
                bullet.transform.localRotation = Quaternion.identity;
                break;
            }
        }

        //if no bullet in pool or still have active bullet
        if (bullet == null)
        {
            bullet = Instantiate(canonBulletPrefab, shootPos.position, Quaternion.identity).GetComponent<BulletHandler>();
            bullet.transform.SetParent(shootPos);
            canonBulletList.Add(bullet); //add bullet in pool
        }

        return bullet.gameObject;
    }
}
