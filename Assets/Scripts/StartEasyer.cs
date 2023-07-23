using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StartEasyer : MonoBehaviour
{
    public bool _x, _y, _z, _negative,_snap;
    Vector3 _v3;
    BoxCollider _bc;
    CapsuleCollider _cc;
    SphereCollider _sc;
   public List <BoxCollider> childs=new List<BoxCollider>();
    public List <CapsuleCollider> childs2 = new List<CapsuleCollider>();
    public List<SphereCollider> childs3 = new List<SphereCollider>();
    public List<GameObject> _objectsToModifie;
    public GameObject[] _auxGameobjects;
    
    void Update()
    {
        if (_snap)
        {
        BoxColliderSnap();
        CapsuleColliderSnap();
        SphereColliderSnap();
        }
    }
    public void BoxColliderSnap()
    {

        foreach (BoxCollider item in childs)
        {
            if (_x)
            {
                if (_negative)
                {
                    _v3 = -item.transform.right;
                }
                else
                {
                    _v3 = item.transform.right;

                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _bc = item.GetComponent<BoxCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_bc.size.x / 2));
                }
            }
            if (_y)
            {
                if (!_negative)
                {
                    _v3 = -item.transform.up;
                }
                else
                {
                    _v3 = item.transform.up;

                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _bc = item.GetComponent<BoxCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_bc.size.y / 2));
                }
            }
            if (_z)
            {

                if (_negative)
                {
                    _v3 = -item.transform.forward;
                }
                else
                {
                    _v3 = item.transform.forward;
                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _bc = item.GetComponent<BoxCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_bc.size.z / 2));
                }
            }
        }

    }
    public void CapsuleColliderSnap()
    {
        foreach (CapsuleCollider item in childs2)
        {

            if (_x)
            {
                if (_negative)
                {
                    _v3 = -item.transform.right;
                }
                else
                {
                    _v3 = item.transform.right;

                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _cc = item.GetComponent<CapsuleCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_cc.radius / 2));
                }
            }
            if (_y)
            {
                if (!_negative)
                {
                    _v3 = -item.transform.up;
                }
                else
                {
                    _v3 = item.transform.up;

                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _cc = item.GetComponent<CapsuleCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_cc.height / 2));
                }
            }
            if (_z)
            {

                if (_negative)
                {
                    _v3 = -item.transform.forward;
                }
                else
                {
                    _v3 = item.transform.forward;
                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _cc = item.GetComponent<CapsuleCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_cc.radius / 2));
                }
            }
        }
    }
    public void SphereColliderSnap()
    {
        foreach (SphereCollider item in childs3)
        {
            if (_x)
            {
                if (_negative)
                {
                    _v3 = -item.transform.right;
                }
                else
                {
                    _v3 = item.transform.right;

                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _sc = item.GetComponent<SphereCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_sc.radius ));
                }
            }
            if (_y)
            {
                if (!_negative)
                {
                    _v3 = -item.transform.up;
                }
                else
                {
                    _v3 = item.transform.up;

                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _sc = item.GetComponent<SphereCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_sc.radius));
                    
                }
            }
            if (_z)
            {

                if (_negative)
                {
                    _v3 = -item.transform.forward;
                }
                else
                {
                    _v3 = item.transform.forward;
                }
                RaycastHit _ray;
                if (Physics.Raycast(item.transform.position, item.transform.TransformDirection(_v3), out _ray))
                {
                    _sc = item.GetComponent<SphereCollider>();
                    item.transform.position += _v3 * (_ray.distance - (_sc.radius));
                }
            }
        }
    }

    public void ChildAsigner(GameObject[] _gameobjects)
    {
        if (_gameobjects.Length<1)
            return;

        if (_auxGameobjects == _gameobjects)
            return;
        childs.Clear();
        childs2.Clear();
        childs3.Clear();


        for (int count = 0; count < _gameobjects.Length; count++)
        {
            BoxCollider _auxbc = _gameobjects[count].GetComponent<BoxCollider>();
            if (_auxbc != null)
            {
                if (childs.Count<count+1)
                childs.Add(_auxbc);
                else
                {
                    childs[count] = _auxbc;
                }
            }
            CapsuleCollider _auxcc = _gameobjects[count].GetComponent<CapsuleCollider>();
            if (_auxcc != null)
            {
                    childs2.Add(_auxcc);
            }

            SphereCollider _auxsc = _gameobjects[count].GetComponent<SphereCollider>();

            if (_auxsc != null)
            {
                    childs3.Add(_auxsc);
            }
        }
    }
}
