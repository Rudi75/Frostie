using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;

public class WaterReserveSkript : MonoBehaviour 
{
    public WaterReserveDisplaySkript reserveDisplay;
    public Transform iceCubePrefab;
    public int waterReserveLimit = 4;

    private WaterReserve reserve;
    private Collider2D bottomCollider;
    private Collider2D topCollider;

	// Use this for initialization
	void Start () 
    {
        reserve = new WaterReserve(reserveDisplay, waterReserveLimit);
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        bottomCollider = CollisionHelper.getBottomCollider(colliders);
        topCollider = CollisionHelper.getTopCollider(colliders);
	}

    public void TakeWater()
    {
        if (reserve.canTakeMoreWater())
        {
            IEnumerable<GameObject> collisions = CustomCollisionHelper.getCollidingObjectsInBetween(topCollider, bottomCollider, Enums.Direction.VERTICAL);
            var query = from hit in collisions from water in hit.GetComponents<RemovableWaterScript>() where water != null && water.CanRemove() select water;
            foreach (var water in query)
            {
                water.Remove();
                reserve.increaseWater();
            }

            if (query.Any())
            {
                return;
            }

            var ground = CollisionHelper.getCollidingObject(bottomCollider, Enums.Edges.BOTTOM, 0.1f);
            var groundCollider = CustomCollisionHelper.getBiggestCollider(ground.GetComponentsInChildren<Collider2D>());
            Enums.Edges direction = transform.parent.localScale.x > 0 ? Enums.Edges.RIGHT : (transform.parent.localScale.x < 0 ? Enums.Edges.LEFT : Enums.Edges.NONE);

            var left = CollisionHelper.getCollidingObject(groundCollider, direction, 0.3f);
            if (left != null)
            {
                var removableWater = left.GetComponent<RemovableWaterScript>();  
                if (removableWater != null && removableWater.CanRemove())
                {
                    removableWater.Remove();
                    reserve.increaseWater();
                    return;
                }
            }
        }
    }

    public void SpawnIceCube()
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
