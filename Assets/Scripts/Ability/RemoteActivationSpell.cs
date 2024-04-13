using UnityEngine;

public class RemoteActivationSpell : MonoBehaviour
{
	public void RemoteActivate(RaycastHit hit)
	{
		if (!hit.transform.TryGetComponent<Interactible>(out var interactible) || !interactible.CanRemoteInteract)
		{
			Destroy(gameObject);
			return;
		}

		interactible.OnInteract.Invoke();
	}
}