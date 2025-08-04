using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    private LayerMask playerLayer;

    public float radius = 2f;
    private bool usedRest = false;
    private void OnDisable()
    {
        _anim.SetBool("usedRest", false);
        usedRest = false;
    }

    private void Awake()
    {
        if(_anim == null)
            _anim = GetComponent<Animator>();
        playerLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if(usedRest)
        {
            _anim.SetBool("usedRest", true);
            return;
        }
        DetectNearByIntearctable();
    }

    void DetectNearByIntearctable()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if(col)
        {
            _anim.SetBool("NearPlayer", true);
        }
        else
        {
            _anim.SetBool("NearPlayer", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Constants.PlayerTag))
        {
            UIManager.Instance.ShowPopupUI<RestPopupUI>();
            usedRest = true;
        }
    }
}
