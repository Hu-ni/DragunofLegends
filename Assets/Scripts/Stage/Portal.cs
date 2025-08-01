using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Portal: MonoBehaviour
{
    private const string ClosePortal = "ClosePortal";
    private const string EnablePortal = "isClear";

    private const string PlayerTag = "Player";

    private bool isClear = false;

    [SerializeField]
    private Animator _anim;

    public bool TestMode = false;

    private void OnEnable()
    {
        if (TestMode)
            OnClear();
    }

    public void OnClear()
    {
        isClear = true;
        _anim.SetTrigger(EnablePortal);
    }
    private void OnTriggerEnter2D(Collider2D e)
    {
        if (e.gameObject.tag == PlayerTag)
        {
            isClear = false;
            _anim.SetTrigger(ClosePortal);
            StageManager.Instance.CreateStage();
        }
    }
}
