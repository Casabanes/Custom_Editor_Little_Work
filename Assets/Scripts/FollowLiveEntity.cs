using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowLiveEntity : MonoBehaviour
{
    public Transform _transform;
    public Vector3 _adjust;
    public Image _lifebar;
    public bool _isAPlayerCamera;
    const int _constTen = 10;

    private void LateUpdate()
    {
        if(_transform!=null)
        transform.position = _transform.position + _adjust;

    }
    public void SetObjetive(Transform _parent,Vector3 _adjustv) 
    {
        _transform = _parent;
        if (_isAPlayerCamera)
        {
            _adjust = new Vector3(_adjustv.x, _adjustv.y, -_constTen);
        }
        else
        {
        _adjust = _adjustv;
        }
    }
}
