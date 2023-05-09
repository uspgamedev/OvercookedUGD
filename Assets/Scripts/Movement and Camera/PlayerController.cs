using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    //Tables
    [SerializeField] LayerMask tableLayer;    
    //Tuples
    public Tuple currentTuple;

    private void Awake()
    {
        currentTuple = Tuple.None;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(movement.x, movement.y).normalized;

        animator.SetFloat("SpeedHorizontal", movement.x);
        animator.SetFloat("SpeedVertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetKeyDown(KeyCode.X) && CheckTable())
        {
            GetTable().tableScript.SubstituteAdd();
            UIManager.Instance.ChangeImage(currentTuple.sprite);
        }
        if (Input.GetKeyDown(KeyCode.Z) && CheckTable())
        {
            GetTable().tableScript.UseTable();
            UIManager.Instance.ChangeImage(currentTuple.sprite);
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public bool CheckTable()
    {
        //transform.up é só temporário
        return Physics2D.Raycast(transform.position, transform.position + transform.up, tableLayer);
    }

    public Table GetTable()
    {
        //Talvez tanto null acabe gerando bugs imprevistos
        //transform.up é só temporário
        Vector3 ray = transform.up;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, ray, tableLayer);
        if (Physics2D.Raycast(transform.position, ray, tableLayer))
        {
            Debug.Log("k1");
            Debug.Log("Raycast: " + hit.collider.gameObject);
            if (hit.transform.GetComponent<Table>() != null)
            {
                Debug.Log("k2");
                return hit.transform.GetComponent<Table>();
            }
            else
                return null;
        }
        else
            return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, transform.up));
    }
}
