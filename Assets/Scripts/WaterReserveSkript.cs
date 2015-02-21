using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;

public class WaterReserveSkript : MonoBehaviour 
{
    public WaterReserveDisplaySkript reserveDisplay;
    public Transform iceCubePrefab;
    public KeyCode KeyToTakeWater = KeyCode.V;
    public KeyCode KeyToSpawnIceCube = KeyCode.C;
    public int waterReserveLimit = 4;

    private WaterReserve reserve;
    private Collider2D bottomCollider;

	// Use this for initialization
	void Start () 
    {
        reserve = new WaterReserve(reserveDisplay, waterReserveLimit);
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        bottomCollider = CollisionHelper.getBotomCollider(colliders);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyToTakeWater))
        {
            takeWater();
        }
        if (Input.GetKeyDown(KeyToSpawnIceCube))
        {
            spawnIceCube();
        }
	}

    private void takeWater()
    {
        if (reserve.canTakeMoreWater())
        {
            var ground = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
            var groundCollider = CustomCollisionHelper.getBiggestCollider(ground.GetComponentsInChildren<Collider2D>());
            Enums.Edges direction = transform.parent.localScale.x > 0 ? Enums.Edges.RIGHT : (transform.parent.localScale.x < 0 ? Enums.Edges.LEFT : Enums.Edges.NONE);

            var left = CollisionHelper.getCollidingObject(groundCollider, direction, 0.3f);
            var water = left.GetComponent<RemovableWaterScript>();
            if (water != null && water.CanRemove())
            {
                water.Remove();
                reserve.increaseWater();
            }
        }
    }

    private void spawnIceCube()
    {
        if (reserve.canSpendMoreWater())
        {
            int direction = transform.parent.localScale.x > 0 ? 1 : (transform.parent.localScale.x < 0 ? -1 : 0);
            var position = transform.position;
            position.x = position.x + (bottomCollider.bounds.size.x + 0.1f) * direction;
            var iceCube = Instantiate(iceCubePrefab, position, new Quaternion()) as Transform;
            Vector3 scale = iceCube.localScale;
            scale.x = scale.x * direction;
            iceCube.localScale = scale;
            var animator = iceCube.GetComponentInChildren<Animator>();
            if (animator != null) animator.enabled = true;
            reserve.decreaseWater();
        }
    }

    private class WaterReserve
    {
        private WaterReserveDisplaySkript reserveDisplay;

        private int waterReserve;
        private int waterReserveLimit;

        public WaterReserve(WaterReserveDisplaySkript display, int limit)
        {
            waterReserveLimit = limit;
            reserveDisplay = display;
            reserveDisplay.CreateImages(limit);
        }

        public bool canTakeMoreWater()
        {
            return waterReserve < waterReserveLimit;
        }

        public bool canSpendMoreWater()
        {
            return waterReserve > 0;
        }

        public void increaseWater()
        {
            waterReserve++;
            reserveDisplay.EnableLowestDisabled();
        }

        public void decreaseWater()
        {
            waterReserve--;
            reserveDisplay.DisableTopEnabled();
        }
    }
}
