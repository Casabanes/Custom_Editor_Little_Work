using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RotationTranslateWindow : EditorWindow
{
    Transform _trs;
    Quaternion _qt;
    GameObject _go;
    Vector3 _v3;
    Vector4 _v4;
    bool _x;
    bool _y;
    bool _z;
    bool _sumar45;
    bool _restar45;
    bool _cero;
    bool _noventa;
    bool _cientoochenta;
    bool _docientossetenta;




    string _goName = "_go";
    [MenuItem("CustomWindows/RotationTranslate")]
    public static void OpenRotationTranslateWindow()
    {
        RotationTranslateWindow _thisWindow = GetWindow<RotationTranslateWindow>();
        _thisWindow.minSize = new Vector2(500, 265);
    }
    private void OnGUI()
    {
        if (_go == null)
        {
            _go = new GameObject();
            _go.name = _goName;
        }


        if (!_trs)
        {



            _trs = _go.transform;
            _qt = _trs.rotation;
            _v3 = _qt.eulerAngles;
            EditorGUILayout.Vector3Field("Rotacion:", _v3);
            _v4 = EditorGUILayout.Vector4Field("En quaterniones:", new Vector4(_qt.x, _qt.y, _qt.z, _qt.w));
        }
        else
        {


            if (_trs.rotation.eulerAngles != _v3)
                _v3 = _trs.rotation.eulerAngles;

            _v3 = EditorGUILayout.Vector3Field("Rotacion:", _v3);
            _qt = Quaternion.Euler(_v3);

            if (_v4 != new Vector4(_qt.x, _qt.y, _qt.z, _qt.w))
                _v4 = new Vector4(_qt.x, _qt.y, _qt.z, _qt.w);

            _v4 = EditorGUILayout.Vector4Field("En quaterniones:", new Vector4(_v4.x, _v4.y, _v4.z, _v4.w));
            _qt.Set(_v4.x, _v4.y, _v4.z, _v4.w);

            if (_qt.eulerAngles != _v3)
                _v3 = _qt.eulerAngles;
            _trs.rotation = _qt;



        }
        _trs = EditorGUILayout.ObjectField("Transform que vamos a usar", _trs, typeof(Transform), true) as Transform;

        _x = EditorGUILayout.Toggle("Eje: x", _x);
        _y = EditorGUILayout.Toggle("Eje: y", _y);
        _z = EditorGUILayout.Toggle("Eje: z", _z);

        _cero = GUILayout.Button("Set 0");
        if (_cero)
            Cero();
        _noventa = GUILayout.Button("Set 90");
        if (_noventa)
            Noventa();
        _cientoochenta = GUILayout.Button("Set 180");
        if (_cientoochenta)
            Cientoochenta();
        _docientossetenta = GUILayout.Button("Set 270");
        if (_docientossetenta)
            Docientossetenta();
        _sumar45 = GUILayout.Button("Sumar 45");
        if (_sumar45)
            Sumar45();
        _restar45 = GUILayout.Button("Restar 45");
        if (_restar45)
            Restar45();

    }
    private void Sumar45()
    {
        if (_x)
            _v3.x += 45;
        if (_y)
            _v3.y += 45;
        if (_z)
            _v3.z += 45;
            _qt=Quaternion.Euler(_v3);
        _trs.rotation = _qt;

    }
    private void Restar45()
    {
        if (_x)
            _v3.x -= 45;
        if (_y)
            _v3.y -= 45;
        if (_z)
            _v3.z -= 45;
        _qt = Quaternion.Euler(_v3);
        _trs.rotation = _qt;

    }
    private void Cero()
    {
        if (_x)
            _v3.x = 0;
        if (_y)
            _v3.y = 0;
        if (_z)
            _v3.z = 0;
        _qt = Quaternion.Euler(_v3);
        _trs.rotation = _qt;


    }

    private void Noventa()
    {
        if (_x)
            _v3.x = 90;
        if (_y)
            _v3.y = 90;
        if (_z)
            _v3.z = 90;
        _qt = Quaternion.Euler(_v3);
        _trs.rotation = _qt;

    }
    private void Cientoochenta()
    {
        if (_x)
            _v3.x = 180;
        if (_y)
            _v3.y = 180;
        if (_z)
            _v3.z = 180;
        _qt = Quaternion.Euler(_v3);
        _trs.rotation = _qt;

    }
    private void Docientossetenta()
    {
        if (_x)
            _v3.x = 270;
        if (_y)
            _v3.y = 270;
        if (_z)
            _v3.z = 270;
        _qt = Quaternion.Euler(_v3);
        _trs.rotation = _qt;

    }

    private void OnDestroy()
    {
        DestroyImmediate(_go.gameObject);
    }

}
