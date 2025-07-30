using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : MonoBehaviour
{   //�и��� �ε����� ������ ������ ���
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // PlayerHealth ������Ʈ �ȿ� TakeDamage �ż��� ����
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(damage); 
        }
    }
}
