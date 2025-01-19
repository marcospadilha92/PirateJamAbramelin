using UnityEngine;

public class LevelController : MonoBehaviour
{
    private float screenColliderWidth = 10f;

    void Start()
    {
        CreateScreenEdgeColliders();
    }
    void CreateScreenEdgeColliders()
    {
        Camera cam = Camera.main;

        Vector3 screenBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float screenWidth = screenTopRight.x - screenBottomLeft.x;
        float screenHeight = screenTopRight.y - screenBottomLeft.y;

        CreateEdgeCollider("top_screen_edge", 
            new Vector2(0, screenTopRight.y + screenColliderWidth / 2), 
            new Vector2(screenWidth + screenColliderWidth, screenColliderWidth));
        CreateEdgeCollider("bottom_screen_edge", 
            new Vector2(0, -screenTopRight.y - screenColliderWidth / 2), 
            new Vector2(screenWidth + screenColliderWidth, screenColliderWidth));
        CreateEdgeCollider("left_screen_edge", 
            new Vector2(-screenTopRight.x - screenColliderWidth / 2, 0), 
            new Vector2(screenColliderWidth, screenHeight + screenColliderWidth));
        CreateEdgeCollider("right_screen_edge", 
            new Vector2(screenTopRight.x + screenColliderWidth / 2, 0), 
            new Vector2(screenColliderWidth, screenHeight + screenColliderWidth));
    }

   void CreateEdgeCollider(string name, Vector2 position, Vector2 size)
    {
        GameObject edge = new GameObject(name);
        edge.transform.position = position;
        BoxCollider2D edgeCollider = edge.AddComponent<BoxCollider2D>();
        edgeCollider.size = size;
    }
}
