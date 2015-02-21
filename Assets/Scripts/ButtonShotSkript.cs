using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ButtonShotSkript : MonoBehaviour
{
    public Transform ShotPrefab;
    public Vector2 ShotForce = new Vector2(1, 0);

    List<ButtonShotManager> buttons;


	// Use this for initialization
	void Start () 
    {
        buttons = new List<ButtonShotManager>();
        foreach (var button in GetComponentsInChildren<ButtonShotManager>())
	    {
            buttons.Add(button);
	    } 
	}

    public void shootButton()
    {
        var query = from button in buttons where button.WasNotShotJet select button;
        if (query.Any())
        {
            // Create a new shot
            var shotTransform = Instantiate(ShotPrefab, transform.position, 
                                            Quaternion.Euler(new Vector3(0, 0, 0))) 
                                            as Transform;

            int direction = transform.parent.localScale.x > 0 ? 1 : (transform.parent.localScale.x < 0 ? -1 : 0);
            Vector3 scale = shotTransform.localScale;
            scale.x = scale.x * direction;
            shotTransform.localScale = scale;

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = false;
            }

            shotTransform.rigidbody2D.AddForce(direction * ShotForce, ForceMode2D.Impulse);

            query.First().WasShot();
        }
    }
	
}
