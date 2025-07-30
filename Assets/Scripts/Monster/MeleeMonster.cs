using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : MonoBehaviour
{   //밀리는 부딪히면 데미지 들어오는 방식
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // PlayerHealth 컴포넌트 안에 TakeDamage 매서드 실행
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(damage); 
        }
    }
}
