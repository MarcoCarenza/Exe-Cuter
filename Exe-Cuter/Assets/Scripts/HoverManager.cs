using UnityEngine;

public class HoverManager : MonoBehaviour
{
    private Drawer currentDrawer;
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Drawer hitDrawer = null; 
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hitDrawer = hit.collider.GetComponent<Drawer>(); // 每帧查看是否指向抽屉
        }
        
        if (hitDrawer is not null && currentDrawer != hitDrawer)
        {
            currentDrawer = hitDrawer;
            currentDrawer.OnHoverEnter();
        }
        
        if (currentDrawer is not null && currentDrawer != hitDrawer) 
        {
            currentDrawer.OnHoverExit();
            currentDrawer = null;
        }

    }
}
