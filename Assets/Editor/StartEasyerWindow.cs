using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class StartEasyerWindow : EditorWindow
{

    private GameObject _snaper;
    private string _snaperName="_snaper";
    StartEasyer _se;
    GameObject[] _auxiliarGameObjects;

    bool _alinearEnElPrimero;
    bool _snap;
    bool _x, _y, _z, _negative;

   [MenuItem("CustomWindows/Snaper&Aligner")]
   public static void StartEasy()
    {
        StartEasyerWindow _starteasywindow = GetWindow<StartEasyerWindow>();

        _starteasywindow.minSize=new Vector2(250, 250);
    }
    private void OnGUI()
    {
        SnaperExistence();

         if (GUILayout.Button("StepPhysics"))
         {
            StepPhysics();
         }
        
        _auxiliarGameObjects=Selection.gameObjects;
        for (int count = 0; count < _auxiliarGameObjects.Length; count++)
        {
            _auxiliarGameObjects[count] = EditorGUILayout.ObjectField("Targets", _auxiliarGameObjects[count], typeof(GameObject), true) as GameObject;
        }
        _alinearEnElPrimero = EditorGUILayout.Toggle("Alinear en el primero", _alinearEnElPrimero);
        
        if (GUILayout.Button("Alinear en X"))
        {
            AlignX();
        }
        if (GUILayout.Button("Alinear en Y"))
        {
            AlignY();
        }
        if (GUILayout.Button("Alinear en Z"))
        {
            AlignZ();
        }


        _x = EditorGUILayout.Toggle("x", _x);
        _se._x = _x;
        _y = EditorGUILayout.Toggle("y", _y);
        _se._y = _y;
        _z = EditorGUILayout.Toggle("z", _z);
        _se._z = _z;
        _negative = EditorGUILayout.Toggle("Snapear en negativo", _negative);
        _se._negative = _negative;

       

        
        _snap = EditorGUILayout.Toggle("Mantener Snap", _snap);
        if (_snap)
        {
            _se._snap = true;
        }
        else
        {
            _se._snap = false;
        }


    }
  
    private void SnaperExistence()
    {
        if (_snaper == null)
        {
            _snaper = new GameObject();
            _snaper.name = _snaperName;
            _snaper.AddComponent<StartEasyer>();
            _se = _snaper.GetComponent<StartEasyer>();
        }
        if (_se == null)
        {
            if (_snaper.GetComponent<StartEasyer>())
                _se = _snaper.GetComponent<StartEasyer>();
            else
            {
                _snaper.AddComponent<StartEasyer>();
                _se = _snaper.GetComponent<StartEasyer>();
            }
        }
        if (_se.isActiveAndEnabled == false)
            _se.enabled = true;
    }
    public void Update()
    {
        _auxiliarGameObjects = Selection.gameObjects;
        if (_se != null)
        {
            _se.ChildAsigner(_auxiliarGameObjects);
        }
      
    }
    private void StepPhysics()
    {

        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime);
        Physics.autoSimulation = true;
    }

    private void AlignX()
    {
        if (_alinearEnElPrimero)
        {
            for (int count = 0; count < _auxiliarGameObjects.Length; count++)
            {
                _auxiliarGameObjects[count].transform.position = new Vector3
                    (_auxiliarGameObjects[0].transform.position.x, _auxiliarGameObjects[count].transform.position.y, _auxiliarGameObjects[count].transform.position.z);

            }
        }
        else
        {

        float _promedioX=0;
        for (int count = 0; count < _auxiliarGameObjects.Length; count++)
        {
            _promedioX +=_auxiliarGameObjects[count].transform.position.x;
            
        }
        _promedioX = _promedioX / _auxiliarGameObjects.Length;
        for (int count = 0; count < _auxiliarGameObjects.Length; count++)
        {
            _auxiliarGameObjects[count].transform.position =new Vector3
                (_promedioX, _auxiliarGameObjects[count].transform.position.y, _auxiliarGameObjects[count].transform.position.z);

        }
        }
    }
    private void AlignY()
    {
        if (_alinearEnElPrimero)
        {
            for (int count = 0; count < _auxiliarGameObjects.Length; count++)
            {
                _auxiliarGameObjects[count].transform.position = new Vector3
                    (_auxiliarGameObjects[count].transform.position.x, _auxiliarGameObjects[0].transform.position.y, _auxiliarGameObjects[count].transform.position.z);

            }
        }
        else
        {

            float _promedioY = 0;
            for (int count = 0; count < _auxiliarGameObjects.Length; count++)
            {
                _promedioY += _auxiliarGameObjects[count].transform.position.y;

            }
            _promedioY = _promedioY / _auxiliarGameObjects.Length;
            for (int count = 0; count < _auxiliarGameObjects.Length; count++)
            {
                _auxiliarGameObjects[count].transform.position = new Vector3
                    (_auxiliarGameObjects[count].transform.position.x, _promedioY, _auxiliarGameObjects[count].transform.position.z);

            }
        }
    }
    private void AlignZ()
    {
        if (_alinearEnElPrimero)
        {
            for (int count = 0; count < _auxiliarGameObjects.Length; count++)
            {
                _auxiliarGameObjects[count].transform.position = new Vector3
                    (_auxiliarGameObjects[count].transform.position.x, _auxiliarGameObjects[count].transform.position.y, _auxiliarGameObjects[0].transform.position.z);

            }
        }
        else
        {

            float _promedioZ = 0;
            for (int count = 0; count < _auxiliarGameObjects.Length; count++)
            {
                _promedioZ += _auxiliarGameObjects[count].transform.position.z;

            }
            _promedioZ = _promedioZ / _auxiliarGameObjects.Length;
            for (int count = 0; count < _auxiliarGameObjects.Length; count++)
            {
                _auxiliarGameObjects[count].transform.position = new Vector3
                    (_auxiliarGameObjects[count].transform.position.x, _auxiliarGameObjects[count].transform.position.z, _promedioZ);

            }
        }
    }

    private void OnDestroy()
    {
        DestroyImmediate(_snaper.gameObject);
    }

}
