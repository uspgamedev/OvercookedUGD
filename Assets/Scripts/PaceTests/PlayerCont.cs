using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    //Tables
    [SerializeField] LayerMask tableLayer;

    //Movimento
    public float speed;
    private float horizontal;
    private float vertical;

    //Tuples
    public Tuple currentTuple;

    private void Awake()
    {
        currentTuple = Tuple.None;
    }

    void Update()
    {
        //Movimento: talvez um pouco de aceleração (mas deaceleração provavelmente não) possa ajudar
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (CheckTable())
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                GetTable().SubstituteAdd();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GetTable().UseTable();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(currentTuple.ingredient.ingredientArray.Count());
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(horizontal, vertical), speed * Time.fixedDeltaTime);
    }

    public bool CheckTable()
    {
        //transform.up é só temporário
        return Physics2D.Raycast(transform.position, transform.position + transform.up, tableLayer);
    }

    public TableScript GetTable()
    {
        //Talvez tanto null acabe gerando bugs imprevistos
        //transform.up é só temporário
        Vector3 ray = transform.up;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, ray, tableLayer);
        if (Physics2D.Raycast(transform.position, ray, tableLayer))
        {
            Debug.Log("k1");
            if (hit.transform.GetComponent<TableScript>() != null)
            {
                Debug.Log("k2");
                return hit.transform.GetComponent<TableScript>();
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
