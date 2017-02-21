using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Ship : MonoBehaviour 
{
	private new Rigidbody2D rigidbody;
    [SerializeField]
    private Transform planettransform;
    public LineRenderer linerender;
    public bool shouldrag = false, shoulshot=false;
    public float maxrange = 4;
    private void Awake ()

    
	{
		rigidbody = GetComponent<Rigidbody2D> ();
        linerender = GetComponent<LineRenderer>();
	}
	
	void Update () 
	{
        if (Input.GetMouseButtonDown(0))


        {
            shouldrag = true;

        }

        if (shouldrag==true)
        {
            drag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            shouldrag = false;

            shoulshot = true;
            shoot();
        }

        if (shoulshot == true)
        {
            shoot();
        }


        linerender.SetPosition(0, transform.position);
        linerender.SetPosition(1, planettransform.position);

    }

	private void LookTowards (Vector2 direction)
	{
		
		transform.localRotation = Quaternion.LookRotation (Vector3.forward, direction);
	}

    public void drag()
    {
        Vector3 v3T = Input.mousePosition;

        v3T = Camera.main.ScreenToWorldPoint(v3T);


        LookTowards(planettransform.position - new Vector3(v3T.x, v3T.y, 0));
        transform.position = new Vector3(v3T.x, v3T.y);
        transform.position = new Vector3
      (
          Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, planettransform.position.x - maxrange, planettransform.position.x + maxrange),
          Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, planettransform.position.y - maxrange, planettransform.position.y),
          0.0f

      );
    } 


    public void shoot ()
    {

        rigidbody.AddForce((planettransform.position - transform.position)*100);
        shoulshot = false;
        linerender.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        planettransform = other.transform;
        linerender.enabled = true;

        rigidbody.velocity = new Vector3(0,0,0);
    }
  

    
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "planet")
        {

            Debug.Log("salio");
        planettransform = null;

        }

    }
}
