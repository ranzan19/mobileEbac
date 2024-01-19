using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;

    [Header("text")]
    public TextMeshPro uiTextPowerUp;

    public bool invencible = true;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [SerializeField] private BounceHelper _bounceHelper;

    [SerializeField] private PlayerAnimation playerAnimation;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startposition;
    private float _baseSpeedToAnimation = 6;


    private void Start()
    {
        _startposition = transform.position;
        ResetSpeed();
    }

    public void Bounce()
    {
        if(_bounceHelper != null)
        _bounceHelper.Bounce();
    }


    public void ScalePlayer()
    {
        if(playerAnimation != null)
        playerAnimation.ScalePlayer();
    }


    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy)
        {
            if (!invencible)
            {
                MoveBack(collision.transform);
                EndGame(AnimatorManager.AnimationType.DEATH);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }


    private void MoveBack(Transform t)
    {
        t.DOMoveZ(1f, .3f).SetRelative();
    }


    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StarToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }


    #region POWERUPS
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }


    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startposition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startposition.y + amount, animationDuration).SetEase(ease);//.OnComplete(ResetHeight);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        /*var p = transform.position;
        p.y = _startposition.y;
        transform.position = p;*/

        transform.DOMoveY(_startposition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
