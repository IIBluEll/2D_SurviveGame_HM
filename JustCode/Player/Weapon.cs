//Weapon.cs
//PoolMgr���� �޾ƿ� ������� �������ִ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // ������ ID ����
    public int prefabID;

    public float damgage;   // ������ ������
    public float speed;     // ������ ȸ���ӵ� // ���Ÿ��� ��� �߻�ӵ�
    public int count;       // ������ ����

    float timer;            // ���Ÿ� ���� ���� Ÿ�̸�

    Player player;

    public void LevelUP(float damage, int count) // ���� ������
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

            // ������ �ʰ� ������ ���� ���� ������Ʈ ��Ȱ���� ����
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.PoolMgr.Get(prefabID).transform;

                bullet.parent = this.transform;
            }

            // ���� ��ġ �ʱ�ȭ
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // ���� ��ġ�� ��ġ�� �´� ȸ��
            Vector3 rotVect = Vector3.forward * 360 * i / count; // count ���� �°� 360���� ������ ��ġ�� 

            bullet.Rotate(rotVect);
            bullet.Translate(bullet.up * 1.5f, Space.World); // Space.World ���� �÷��̾�� 1.5f �Ÿ���ŭ �̵�

            bullet.GetComponent<Bullet>().Init(damgage, -1,Vector3.zero); // (Damgae,Per) �������� ���밹���� ���� ==> -1�� ���Ѱ���


        }
    }

    private void Start()
    {
        player = GetComponentInParent<Player>();
        Init();
    }

    private void Update()
    {
        // ������ ȸ�� 
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

    // ���Ÿ� ���� �߻�
    void Fire()
    {
        // �÷��̾� ��ó Ÿ���� ���ٸ� �߻����� ����
        if (!player.scanner.nearestTarget)  
            return;

        // �÷��̾�� ���� ����� Ÿ���� �Ÿ��� ������ ����
        Vector3 targetpos = player.scanner.nearestTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        Transform bullet;
        bullet = GameManager.instance.PoolMgr.Get(prefabID).transform;

        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,dir); // �Ѿ��� Ÿ�� �������� z�� �������� ȸ��

        bullet.GetComponent<Bullet>().Init(damgage, count, dir); // (Damgae,Per,����) �������� ���밹��,������ ����
    }
}
