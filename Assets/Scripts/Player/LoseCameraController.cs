using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LoseCameraController : MonoBehaviour
{
    public Vector3 offset;

    private Camera _camera;
    private CameraFollow _follow;

    [SerializeField]
    private RawImage _image;

    [SerializeField]
    private LayerMask _playerMask;

    public PlayerMovementController _mc;
    public NavMeshAgent _nma;
    public GameObject Objectives;
    public GameObject CastingInterface;

    public GameObject Stats;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _follow = GetComponent<CameraFollow>();
    }

    public IEnumerator LoseAnimation()
    {
        yield return new WaitForSeconds(2f);

        _mc.enabled = false;
        _nma.enabled = false;

        CastingInterface.SetActive(false);
        Objectives.SetActive(false);

        var texture = new Texture2D(_camera.activeTexture.width, _camera.activeTexture.height, TextureFormat.RGB24, 0, false);
        texture.filterMode = FilterMode.Point;

        RenderTexture.active = _camera.activeTexture;
        texture.ReadPixels(new Rect(0, 0, _image.texture.width, _image.texture.height), 0, 0);
        texture.Apply();
        _image.texture = texture;

        _camera.clearFlags = CameraClearFlags.SolidColor;
        _camera.backgroundColor = Color.black;
        _camera.cullingMask = _playerMask;

        while (_image.color.a > 0.01f)
        {
            _image.color -= new Color(0, 0, 0, Time.deltaTime / 4f);
            yield return null;
        }

        _image.color = Color.clear;

        float progress = 0;
        Quaternion rotation = _follow.playerTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(2, 0, -2));

        while (progress <= 1f)
        {
            progress += Time.deltaTime / 4f;
            _follow.playerTransform.rotation = Quaternion.Lerp(rotation, targetRotation, progress);
            yield return null;
        }

        var target = new GameObject();
        target.transform.position = _follow.playerTransform.position - _follow.offset + offset;

        _follow.smoothSpeed = 0.01f;
        _follow.playerTransform = target.transform;

        yield return new WaitForSeconds(4f);
        Stats.SetActive(true);
    }
}
