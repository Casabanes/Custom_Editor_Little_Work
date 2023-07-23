using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(IsDamageable))]
public class IsDamageableEditor : Editor
{
    IsDamageable _isDamageable;
    bool _objetosAsociados;
    bool _vida;
    bool _explosion;
    bool _layers;
    bool _changeColor;
    GUIStyle _style;
    bool _srToggle;
    int _srLengthSize;
    bool _button1;
    bool _button2;
    bool _button3;
    bool _button4;
    bool _button5;
    int _index;
    private void OnEnable()
    {
         _objetosAsociados=true;
         _vida = true;
         _explosion = true;
         _layers = true;
         _changeColor = true;
        _isDamageable = (IsDamageable)target;
        _style = new GUIStyle();
        _style.fontSize = 15;
        _style.alignment = TextAnchor.MiddleCenter;


   
    }
    public override void OnInspectorGUI()
    {
        GUILayout.Space(10);

        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
        EditorGUILayout.LabelField("Variables de vida",_style);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);

        GUILayout.Space(10);

        _vida= EditorGUILayout.Foldout(_vida, "Variables de vida");
        if (_vida)
        {
            GUILayout.Space(10);

            _isDamageable._maxLife = EditorGUILayout.FloatField("Vida Maxima:", _isDamageable._maxLife);
            if (_isDamageable._maxLife == 0)
            {
                _isDamageable._maxLife = 1;
            }
            if (_isDamageable._maxLife > 3000)
            {
                _isDamageable._maxLife = 3000;
            }
            if (_isDamageable._maxLife < 0)
            {
                _isDamageable._maxLife = 0;
            }
            GUILayout.Space(10);
            EditorGUI.ProgressBar(GUILayoutUtility.GetRect(300, 15), _isDamageable._life / _isDamageable._maxLife, "Vida actual: " + _isDamageable._life);
            if (_isDamageable._life > _isDamageable._maxLife)
                _isDamageable._life = _isDamageable._maxLife;
            GUILayout.Space(10);
            _isDamageable._colissionDamage = EditorGUILayout.IntField("Daño de Colission (recibido)", _isDamageable._colissionDamage);
            if (_isDamageable._colissionDamage < 0)
                _isDamageable._colissionDamage = 0;
            GUILayout.Space(10);
            _isDamageable._lifebarDistanceAdjust = EditorGUILayout.Vector3Field("Distancia de la lifebar (ajuste)", _isDamageable._lifebarDistanceAdjust);
            GUILayout.Space(10);
            _isDamageable._lifebarScaleAdjust = EditorGUILayout.Vector3Field("Tamaño de la lifebar (ajuste)", _isDamageable._lifebarScaleAdjust);
            GUILayout.Space(10);

            _button1 = GUILayout.Button("Valores de vida por defecto");
            if (_button1)
            {
                PorDefectoLife();
            }
        }
        GUILayout.Space(10);

        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
        EditorGUILayout.LabelField("Objetos Asociados", _style);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
        GUILayout.Space(10);

        _objetosAsociados = EditorGUILayout.Foldout(_objetosAsociados, "Objetos asociados");
      
        if (_objetosAsociados)
        {
            GUI.skin.label.wordWrap = true;
            _isDamageable._followLiveEntity = EditorGUILayout.ObjectField("Canvas enemigo:", _isDamageable._followLiveEntity, typeof(FollowLiveEntity),true) as FollowLiveEntity;
            _isDamageable._ps = EditorGUILayout.ObjectField("Sistema de particulas (muerte):", _isDamageable._ps, typeof(ParticleSystem), true) as ParticleSystem;
            _isDamageable._colissionSound = EditorGUILayout.ObjectField("Audio source (para sonidos de daño):", _isDamageable._colissionSound, typeof(AudioSource), true) as AudioSource;
            _isDamageable._referenceToPositionForLifeBar = EditorGUILayout.ObjectField("Posicion de barra de vida:", _isDamageable._referenceToPositionForLifeBar, typeof(Transform), true) as Transform;
            _isDamageable._lifebar = EditorGUILayout.ObjectField("Barra de vida (se auto asigna):", _isDamageable._lifebar, typeof(Image), true) as Image;


            _srToggle = EditorGUILayout.Foldout(_srToggle, "Sprite Renderer");
            if(_srToggle)
            {
                if (_srLengthSize < 0)
                    _srLengthSize = 0;
                _isDamageable._sr = new SpriteRenderer[_srLengthSize];
                _srLengthSize = EditorGUILayout.IntField("Size",_isDamageable._sr.Length);

                if (_isDamageable._sr.Length > 0)
                {
                    for(int count = 0; count < _isDamageable._sr.Length; count++)
                    {
                        _isDamageable._sr[count] = EditorGUILayout.ObjectField("Sprite Renderer:", _isDamageable._sr[count], typeof(SpriteRenderer), true) as SpriteRenderer;
                    }
                }

            }
        }

        GUILayout.Space(10);

        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
       
        EditorGUILayout.LabelField("Variables de la Explosión", _style);
       
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
       
        GUILayout.Space(10);
        _explosion = EditorGUILayout.Foldout(_explosion, "Variables de explosion");
        if (_explosion)
        {
            GUILayout.Space(10);
            _isDamageable._explosionPosCorrector = EditorGUILayout.Vector3Field("Posición de explosión", _isDamageable._explosionPosCorrector);
            GUILayout.Space(10);
            _isDamageable._explosionSize = EditorGUILayout.Vector3Field("Tamaño de explosión", _isDamageable._explosionSize);
            GUILayout.Space(10);
            _isDamageable._explosionSizeCorrector = EditorGUILayout.FloatField("Corrector de tamaño", _isDamageable._explosionSizeCorrector);
            GUILayout.Space(10);

            _button2 = GUILayout.Button("Por defecto");
            if (_button2)
            {
                ExplosionAutoFill();
            }
        }
        GUILayout.Space(10);

        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
        
        EditorGUILayout.LabelField("Variables de Layers", _style);
        
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
        
        GUILayout.Space(10);
        _layers = EditorGUILayout.Foldout(_layers, "Variables de layers");
        if (_layers)
        {
            GUILayout.Space(10);
            _isDamageable._layerOfThis = EditorGUILayout.IntField("Layer de este objeto:", _isDamageable._layerOfThis);
            if (_isDamageable._layerOfThis < 0 )
                _isDamageable._layerOfThis = 0;
            if (_isDamageable._layerOfThis > 31)
                _isDamageable._layerOfThis = 31;

            GUILayout.Space(10);

            _isDamageable._layerEnemy = EditorGUILayout.IntField("Layer del enemigo:", _isDamageable._layerEnemy);
            if (_isDamageable._layerEnemy < 0)
                _isDamageable._layerEnemy = 0;
            if (_isDamageable._layerEnemy > 31)
                _isDamageable._layerEnemy = 31;
            GUILayout.Space(10);
            _isDamageable._layerInvulnerable = EditorGUILayout.IntField("Layer invulnerable:", _isDamageable._layerInvulnerable);
            if (_isDamageable._layerInvulnerable < 0)
                _isDamageable._layerInvulnerable = 0;
            if (_isDamageable._layerInvulnerable > 31)
                _isDamageable._layerInvulnerable = 31;
            GUILayout.Space(10);
            _button3 = GUILayout.Button("Layers por defecto");
            if (_button3)
            {
                AutoFill();
            }
        }
        GUILayout.Space(10);

        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
        
        EditorGUILayout.LabelField("Variables del cambio de color (stun)", _style);
        
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, 5), Color.black);
        
        GUILayout.Space(10);
        _changeColor = EditorGUILayout.Foldout(_changeColor, "Variables del cambio de color (stun)");
        if (_changeColor)
        {
            _isDamageable._orTimeToChange = EditorGUILayout.FloatField("Frecuencia de cambio de color:", _isDamageable._orTimeToChange);
            if (_isDamageable._orTimeToChange < 0)
                _isDamageable._orTimeToChange = 0;
            GUILayout.Space(10);

            _isDamageable._flickeringT = EditorGUILayout.FloatField("Tiempo de flickering(colores):", _isDamageable._flickeringT);
            if (_isDamageable._flickeringT < 0)
                _isDamageable._flickeringT = 0;
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUI.skin.label.wordWrap = true;
            EditorGUILayout.LabelField("Timer de daño: (solo lectura)");
            EditorGUILayout.LabelField(_isDamageable._currentTime + "");
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUI.skin.label.wordWrap = true;
            EditorGUILayout.LabelField("Tiempo para cambiar de color (solo lectura):  " + _isDamageable._timeToChange);

            GUILayout.Space(10);

            EditorGUILayout.Toggle("Invulnerable", _isDamageable._isInvulnerable);

            GUILayout.Space(10);

            EditorGUILayout.Toggle("Está Rojo", _isDamageable._isRed);

            GUILayout.Space(10);
            _button4 = GUILayout.Button("Timers por defecto");
            if (_button4)
            {
                AutoFillColorChange();
            }
        }
        GUILayout.Space(10);

        _button5 = GUILayout.Button("Valores de todo por defecto");
        if (_button5)
        {
            AllDefault();
        }
        GUILayout.Space(10);

    }
    

    float _maxLife         = 100;
    int   _collisionDamage = 10;
    Vector3 _lifeBarPosAdjust = new Vector3(0, 0, 0);
    Vector3 _lifeBarScale = new Vector3(1, 1, 1);

    Vector3 _explosionPos = new Vector3(0, 0, 0);
    Vector3 _explosionScale =  new Vector3(1, 1, 1);
    float _explosionSizeCorrector = 0;

    

    private void PorDefectoLife()
    {
        _isDamageable._maxLife = _maxLife;
        _isDamageable._life = _maxLife;
        _isDamageable._colissionDamage = _collisionDamage;
        _isDamageable._lifebarDistanceAdjust = _lifeBarPosAdjust;
        _isDamageable._lifebarScaleAdjust = _lifeBarScale;
    }
    private void ExplosionAutoFill()
    {
        _isDamageable._explosionPosCorrector = _explosionPos;
        _isDamageable._explosionSize = _explosionScale;
        _isDamageable._explosionSizeCorrector = _explosionSizeCorrector;
    }
    private void AutoFill()
    {
        if (_isDamageable._layerOfThis != 8 && _isDamageable._layerOfThis != 9)
        {
            _isDamageable.gameObject.layer = 9;
            _isDamageable._layerOfThis = 9;
        }
        else
        {
            _isDamageable._layerOfThis = _isDamageable.gameObject.layer;
        }

        if (_isDamageable._layerOfThis == 8)
        {
            _isDamageable._layerEnemy = 9;
        }
        else
        {
            _isDamageable._layerEnemy= 8;
        }
        _isDamageable._layerInvulnerable = 10;
    }
    private void AutoFillColorChange()
    {
        _isDamageable._orTimeToChange = 0.2f;
        _isDamageable._flickeringT = 0.2f;
    }
    private void AllDefault()
    {
        PorDefectoLife();
        ExplosionAutoFill();
        AutoFill();
        AutoFillColorChange();
    }
}
