//Weapon.cs
//PoolMgr에서 받아온 무기들을 관리해주는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기의 ID 변수
    public int prefabID;

    public float damgage;   // 무기의 데미지
    public float speed;     // 무기의 회전속도 // 원거리의 경우 발사속도
    public int count;       // 무기의 갯수

    float timer;            // 원거리 공격 전용 타이머

    Player player;

    public void LevelUP(float damage, int count) // 무기 레벨업
    {
        this.damgage += damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch(id)
        {
            
            case 0:             // bullet 0 
                speed = 150;
                Batch();
                break;

            case 1:             // bullet 1
                speed = .5f;
                break;
            default:
                break;
             
        }
    }

    void Batch()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet;

            // 무기의 초과 생성을 막고 기존 오브젝트 재활용을 위함
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.PoolMgr.Get(prefabID).transform;

                bullet.parent = this.transform;
            }

            // 무기 위치 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // 무기 배치시 위치에 맞는 회전
            Vector3 rotVect = Vector3.forward * 360 * i / count; // count 수에 맞게 360도를 나누어 배치됨 

            bullet.Rotate(rotVect);
            bullet.Translate(bullet.up * 1.5f, Space.World); // Space.World 기준 플레이어와 1.5f 거리만큼 이동

            bullet.GetComponent<Bullet>().Init(damgage, -1,Vector3.zero); // (Damgae,Per) 데미지와 관통갯수를 전달 ==> -1은 무한관통


        }
    }

    private void Start()
    {
        player = GetComponentInParent<Player>();
        Init();
    }

    private void Update()
    {
        // 무기의 회전 
        switch (id)
        {
            case 0:     // bullet 0 
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            case 1:     // bullet 1 
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;

            default:
                break;

        }

        //Debug
        if (Input.GetButtonDown("Jump"))
        {
            LevelUP(10, 1);
        }
    }

    // 원거리 무기 발사
    void Fire()
    {
        // 플레이어 근처 타겟이 없다면 발사하지 않음
        if (!player.scanner.nearestTarget)  
            return;

        // 플레이어와 가장 가까운 타겟의 거리와 방향을 구함
        Vector3 targetpos = player.scanner.nearestTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        Transform bullet;
        bullet = GameManager.instance.PoolMgr.Get(prefabID).transform;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,dir); // 총알을 타겟 방향으로 z축 기준으로 회전

        bullet.GetComponent<Bullet>().Init(damgage, count, dir); // (Damgae,Per,방향) 데미지와 관통갯수,방향을 전달
    }
}
