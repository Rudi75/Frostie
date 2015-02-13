using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class MoveableScript : MonoBehaviour {

	private float movement = 0;
    private Vector3 oldPlayerPosition;
    private float speedReduction = 0.5f;

    public GameObject playerLeft;
    public GameObject playerRight;

        void OnCollisionStay2D(Collision2D collision)
        {
            if(!Input.GetKey(KeyCode.LeftControl))
            {
                string otherObject = collision.gameObject.tag;
                if (otherObject == "Player") 
                {
                    Edges edge = CollisionHelper.getCollisionEdge (collision);

                    FrostieScript frostieScript = collision.gameObject.GetComponent<FrostieScript>();

                    float speed = frostieScript.speed;
			
                    if (Edges.RIGHT.Equals(edge) && frostieScript.isGrounded()) {
                        movement= -1 * speed;
                    }
			
                    if (Edges.LEFT.Equals(edge) && frostieScript.isGrounded()){
                        movement= speed;
                    }
                }
            }
        }


        void FixedUpdate()
        { 
            bool isPull = handlePull();
            if(!isPull)
            {
                float speedX = movement;
                float speedY = rigidbody2D.velocity.y;
                rigidbody2D.velocity = new Vector2(speedX,speedY);
            }
            movement *= speedReduction ;
            
            
        }


    
            private bool handlePull()
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    
                    Vector3 movement = Vector3.zero;
                     playerLeft = CollisionHelper.getCollidingObject(collider2D, Edges.LEFT, 0.5f);
                     playerRight = CollisionHelper.getCollidingObject(collider2D, Edges.RIGHT, 0.5f);

                    if ((playerLeft == null || playerLeft.tag != "Player") && (playerRight == null || playerRight.tag != "Player"))
                    {
                        Debug.Log("Pull");
                        oldPlayerPosition = Vector3.zero;
                        return false;
                    }
                    GameObject player;
                    if (playerRight != null && playerRight.tag == "Player")
                        player = playerRight;
                    else
                        player = playerLeft;

                    Vector3 playerPosition = player.transform.position;

                    

                    if (!Vector3.zero.Equals(oldPlayerPosition))
                    {
                        movement.x = playerPosition.x - oldPlayerPosition.x;
                    }

                    oldPlayerPosition = playerPosition;
                    if ((playerRight != null && movement.x > 0)
                        || (playerLeft != null && movement.x < 0))
                    {
                        transform.Translate(movement, Space.World);
                        return true;
                    }
                    else
                        return false;
                }
                oldPlayerPosition = Vector3.zero;
                return false;
            }
            

}