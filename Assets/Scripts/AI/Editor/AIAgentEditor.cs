using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIAgent))]
public class AIAgentEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
    public static void DrawHandles(AIAgent t, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;

        var coneStart = Quaternion.Euler(0, -t.visionAngle / 2f, 0) * t.transform.forward;
        var coneEnd = Quaternion.Euler(0, t.visionAngle / 2f, 0) * t.transform.forward;
        Handles.color = Color.red;

        Handles.DrawWireArc(t.transform.position, t.transform.up, coneStart, t.visionAngle, t.visionDistance);

        Handles.DrawLine(t.transform.position, coneStart * t.visionDistance + t.transform.position);
        Handles.DrawLine(t.transform.position, coneEnd * t.visionDistance + t.transform.position);

        if (t.PatrolPoints == null) return;
        for (int i = 0; i < t.PatrolPoints.Count; i++)
        {
            if (t.PatrolPoints[i] == null) continue;
            var nodePosition1 = t.PatrolPoints[i].position;

            if (i != t.PatrolPoints.Count - 1)
            {
                if (t.PatrolPoints[i + 1] != null)
                {
                    var nodePosition2 = t.PatrolPoints[i + 1].position;
                    Gizmos.DrawLine(nodePosition1, nodePosition2);
                }  
            }
            
            Gizmos.DrawSphere(nodePosition1, 0.25f);
        }
    }
}
