using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rb;

    public LayerMask IgnoreMe;
    private Vector2 _movement;
    private Vector3 _facingDirection = -Vector3.up;

    //Animation
    private Animator _animator;

    //Tuples
    public Tuple currentTuple;

    private void Awake()
    {
        currentTuple = Tuple.None;

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(GameManager.Instance.Paused) return;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _movement = new Vector2(_movement.x, _movement.y).normalized;

        _animator.SetFloat("SpeedHorizontal", _movement.x);
        _animator.SetFloat("SpeedVertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            _animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            _animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
            SetFacingDirection(_movement.x, _movement.y);
        }

        if (Input.GetKeyDown(KeyCode.X) && CheckTable())
        {
            GetTable().tableScript.SubstituteAdd();
            PaintPlayerTuple();
        }
        if (Input.GetKeyDown(KeyCode.Z) && CheckTable())
        {
            GetTable().tableScript.UseTable();
            PaintPlayerTuple();
        }
    }
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }

    public bool CheckTable()
    {
        return Physics2D.Raycast(transform.position, _facingDirection, IgnoreMe);
    }

    public Table GetTable()
    {
        //Talvez tanto null acabe gerando bugs imprevistos
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _facingDirection, IgnoreMe);
        if (Physics2D.Raycast(transform.position, _facingDirection, IgnoreMe))
        {
            if (hit.transform.GetComponent<Table>() != null)
                return hit.transform.GetComponent<Table>();
            
            else
                return null;
        }
        else
            return null;
    }

    private void SetFacingDirection(float horizontalMovement, float verticalMovement)
    {
        if (Mathf.Abs(verticalMovement) > 0.1f)
            _facingDirection = Mathf.Sign(verticalMovement) * Vector3.up;
        
        else if (Mathf.Abs(horizontalMovement) > 0.1f) 
            _facingDirection = Mathf.Sign(horizontalMovement) * Vector3.right;
    }

    public void PaintPlayerTuple()
    {
        UIManager.Instance.ChangeImage(currentTuple.sprite);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, _facingDirection));
    }
}
