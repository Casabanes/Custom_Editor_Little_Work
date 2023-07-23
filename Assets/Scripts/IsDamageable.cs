using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsDamageable : MonoBehaviour
{

    public float _life;
    public float _maxLife;
    const int _constZero = 0;
    const int _constOne = 1;

    public ParticleSystem _ps;
    public Vector3 _explosionSize;
    public float _explosionSizeCorrector;
    public SpriteRenderer[] _sr; // Esta variable es un array para los gameobjects que vienen en partes, para así enrojecer todos los sprites al mismo tiempo
    public Vector3 _explosionPosCorrector;



    public bool _isInvulnerable;
    public float _currentTime;
    
    

    public float _orTimeToChange;
    public bool _isRed;
    public float _timeToChange;
    public float _flickeringT;//take damage variables


    public int _layerEnemy;
    public int _layerOfThis;
    public int _layerInvulnerable;

    public FollowLiveEntity _followLiveEntity;
    public int _colissionDamage;
    public AudioSource _colissionSound;

    

    public Image _lifebar;
    public Vector3 _lifebarScaleAdjust;
    public Transform _referenceToPositionForLifeBar;
    public Vector3 _lifebarDistanceAdjust;
    public void Start()
    {
        if (_referenceToPositionForLifeBar == null)
            _referenceToPositionForLifeBar = transform;
        _life = _maxLife;
        if (_lifebarScaleAdjust == new Vector3(_constZero, _constZero, _constZero))
            _lifebarScaleAdjust = new Vector3(_constOne, _constOne, _constOne);
        if (_followLiveEntity.isActiveAndEnabled!=true)
        {
            _followLiveEntity= Instantiate(_followLiveEntity);
            _followLiveEntity.transform.localScale = _lifebarScaleAdjust;
            _followLiveEntity.SetObjetive(_referenceToPositionForLifeBar, _lifebarDistanceAdjust);
            _lifebar = _followLiveEntity._lifebar;

        }
        else
        {
            _followLiveEntity.SetObjetive(transform, _lifebarDistanceAdjust);
            _followLiveEntity.transform.localScale = _lifebarScaleAdjust;

            _lifebar = _followLiveEntity._lifebar;
        }
            _lifebar.fillAmount= _life / _maxLife;


        if (_sr == null)
        {
            for(int count = _constZero; count <= _sr.Length; count++)
            {
            _sr[count] = GetComponent<SpriteRenderer>();
            }
        }
    }

    public void Update()
    {
        ChangeColor();
    }
    public void TakeDamage(int _damage)
    {
        if (_isInvulnerable)
            return;
        _life -= _damage;

        if (_life <= _constZero)
        {
            _life = _constZero;
            Death();
        }
        gameObject.layer = _layerInvulnerable;
        _lifebar.fillAmount = _life / _maxLife;
        _isInvulnerable = true;
    }



    
    void ChangeColor()
    {
        if (_isInvulnerable)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _timeToChange)
            {
                if (_isRed)
                {
                    for (int count = _constZero; count <_sr.Length; count++)
                    {
                        {
                            _sr[count].color = Color.white;
                        }
                            _isRed = false;
                    }
                }
                else
                {
                    for (int count = _constZero; count < _sr.Length; count++)
                    {
                        _sr[count].color = Color.red;
                    }
                        _isRed = true;
                }
                _timeToChange += _orTimeToChange;
            }
            if (_currentTime >= _flickeringT)
            {
                for (int count = _constZero; count < _sr.Length; count++)
                {
                    _sr[count].color = Color.white;
                }
                    _currentTime = _constZero;
                    _timeToChange = _constZero;
                    _isInvulnerable = false;
                    gameObject.layer = _layerOfThis;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer ==_layerEnemy)
        {
            TakeDamage(_colissionDamage);
            if (!_colissionSound.isPlaying)
            {
                _colissionSound.Play();
            }
        }

    }

    public void Death()
    {
        _ps.transform.localScale = _explosionSize;
        Instantiate(_ps, transform.position+_explosionPosCorrector, _ps.transform.rotation);
        _ps.Play();
        Destroy(_followLiveEntity.gameObject);
        Destroy(gameObject);
    }
}