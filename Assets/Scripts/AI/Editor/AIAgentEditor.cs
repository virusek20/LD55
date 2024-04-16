using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIAgent))]
public class AIAgentEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
    public static void DrawHandles(AIAgent t, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;

        if (t.PatrolPoints == null) return;
        for (int i = 0; i < t.PatrolPoints.Count; i++)
        {
            var nodePosition1 = t.PatrolPoints[i].position;

            if (i != t.PatrolPoints.Count - 1)
            {
                var nodePosition2 = t.PatrolPoints[i + 1].position;
                Gizmos.DrawLine(nodePosition1, nodePosition2);
            }
            
            Gizmos.DrawSphere(nodePosition1, 0.25f);
        }
    }
}
