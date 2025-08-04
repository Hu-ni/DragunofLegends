using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Portal: MonoBehaviour
{
    private static readonly int ClosePortal = Animator.StringToHash("ClosePortal");
    private static readonly int EnablePortal = Animator.StringToHash("isClear");

    [SerializeField]
    private Animator _anim;

    public bool TestMode = false;

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (TestMode)
            OnClear();
#endif
    }

    public void OnClear()
    {
        _anim.SetTrigger(EnablePortal);
    }
    private void OnTriggerEnter2D(Collider2D e)
    {
        if (e.gameObject.tag == Constants.PlayerTag)
        {
            _anim.SetTrigger(ClosePortal);
            GameManager.instance.NextStage();
            e.transform.position = new Vector3(0, -3);
        }
    }
}
