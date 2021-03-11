using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPOI : MonoBehaviour
{
    [SerializeField] Vector2Int teleportToPosition; // position to go (in the grid)
    [SerializeField] Grid grid = null; // the grid being used
    public Vector3 destination; // destination in world coordinates

    private void OnDrawGizmos()
    {
        // calculates center of the tile in which the player will teleport to
        destination = new Vector3((teleportToPosition.x + 0.5f) * grid.cellSize.x, (teleportToPosition.y + 0.5f) * grid.cellSize.y);

        // draws a yellow square
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(destination, grid.cellSize);
    }
}
