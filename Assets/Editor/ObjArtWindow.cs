using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ObjArtWindow : EditorWindow
{

    List<bool[]> _bools=new List<bool[]>();

    List<Vector3[]> _trs = new List<Vector3[]>();
    List<Object> prefabSearchAux= new List<Object>();
    GameObject _go;
    bool _generateObjs;

    Transform _referencePosition;
    string _referencePositionName="_objArtWindowComplement";
    string _errorMsg = "Es necesario elegir un Game Object a instanciar";
    GameObject _referenceGameObject;
    Vector2 _matrixLenght;
    Vector2 _posdistance;
    Vector2 _matrixLengthFunctionValue;
    bool _generateMatrix;

    string _folderPath = "Assets/Prefabs";
    string newFoldersName ="ArtificialPrefabs";
    string _prefabName="";


[MenuItem("CustomWindows/ObjArtWindow")]
    public static void Openwindow()
    {
        ObjArtWindow _thisWindow = GetWindow<ObjArtWindow>();

        _thisWindow.minSize = new Vector2(250, 250);
    }
    private void OnGUI()
    {
        _matrixLenght = EditorGUILayout.Vector2Field("Cantidad de elementos:", _matrixLenght);
        _matrixLenght = new Vector2((int)_matrixLenght.x, (int)_matrixLenght.y);
        if (_matrixLenght.x < 1)
            _matrixLenght.x = 1;
        if (_matrixLenght.y < 1)
            _matrixLenght.y = 1;
        _posdistance = EditorGUILayout.Vector2Field("Distancia entre objetos:", _posdistance);

        _referencePosition = EditorGUILayout.ObjectField("Transform donde se instancian los objetos:", _referencePosition, typeof(Transform), true) as Transform;
        _go = EditorGUILayout.ObjectField("Objeto que se va a instanciar:", _go, typeof(GameObject), true) as GameObject;


       
      

            _generateMatrix = GUILayout.Button("Generar Grilla");
        if (_generateMatrix)
        {
            _bools.Clear();
            _trs.Clear();
            _matrixLengthFunctionValue = _matrixLenght;
        }
        _generateObjs = GUILayout.Button("Generar Objetos");
        if (_go == null)
        {
            EditorGUILayout.HelpBox(_errorMsg, MessageType.Error);
        }
        else
        {
            if (_generateObjs)
            {
                GenerateOBJS(_matrixLengthFunctionValue);
            }
        }
        ReferenceExistence();
        BoolMatrix(_matrixLengthFunctionValue);

        _referencePosition = EditorGUILayout.ObjectField("Prefab que se guardara", _referencePosition, typeof(Transform), true) as Transform;
        _prefabName = EditorGUILayout.TextField("Nombre del prefab a guardar: ", _prefabName);


        if (GUILayout.Button("Guardar prefab"))
        {
            SavePrefab();
        }

    }
    public static void SavePrefab(GameObject _go,string _path ,string _name)
    {
        PrefabUtility.SaveAsPrefabAsset(_go, _path + _name + ".prefab");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private void SavePrefab()
    {
        if (_prefabName != null)
        {
            if (AssetDatabase.IsValidFolder(_folderPath + "/" + $"{newFoldersName}"))
                SavePrefab(_referencePosition.gameObject, _folderPath + "/" + newFoldersName + "/", _prefabName);
            else
                AssetDatabase.CreateFolder(_folderPath, $"{newFoldersName}");
        }
        else
            EditorGUILayout.HelpBox("El prefab debe tener un nombre", MessageType.Error);
    }


    private void BoolMatrix(Vector2 _matrixLengthFV)
    {
        for(int county = 0; county < _matrixLengthFV.y; county++)
        {
                EditorGUILayout.BeginHorizontal();
            for (int countx = 0; countx < _matrixLengthFV.x; countx++)
            {

                if (_bools.Count < _matrixLengthFV.y)
                    _bools.Add(new bool[(int)_matrixLengthFV.x]);




                if (_trs.Count < _matrixLenght.y)
                    _trs.Add(new Vector3[(int)_matrixLengthFV.x]);

               
                if (_referencePosition != null)
                {
                    int _xdis = countx- _trs[county].Length/2;
                    int _ydis = county - _trs.Count / 2;
                    _trs[county][countx]= _referencePosition.position - 
                        new Vector3(_posdistance.x*_xdis, _posdistance.y*_ydis, _referencePosition.position.z);
                }

                _bools[county][countx] = EditorGUILayout.Toggle(_bools[county][countx]);
            }

            EditorGUILayout.EndHorizontal();
        }
      
    }
    private void ReferenceExistence()
    {
        if (_referencePosition == null)
        {
            _referenceGameObject = new GameObject();
            _referencePosition = _referenceGameObject.transform;
            _referencePosition.position = Vector3.zero;
            _referencePosition.rotation = Quaternion.Euler(0, 0, 0);
            _referencePosition.localScale = Vector3.one;
            _referencePosition.name = _referencePositionName;
        }
    }
    private void GenerateOBJS(Vector2 _matrixLengthFV)
    {
        if (_referencePosition == null)
            return;
        for (int county = 0; county < _matrixLengthFV.y; county++)
        {
            for (int countx = 0; countx < _matrixLengthFV.x; countx++)
            {
                if (_bools[county][countx])
                {
                    var child = Instantiate(_go, _trs[county][countx],_referencePosition.rotation);
                    child.transform.parent = _referencePosition;
                }

            }
        }
    }

    public void OnDestroy()
    {
        if (_referencePosition.gameObject.name == _referencePositionName)
            DestroyImmediate(_referencePosition.gameObject);
    }

   
}
