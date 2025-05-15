using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

[RequireComponent(typeof(PhotonView))]
public class NetworkPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform leftHandTransform;
    [SerializeField]
    private Transform rightHandTransform;
    [SerializeField]
    private Transform headTransform;

    private Transform leftHandRig;
    private Transform rightHandRig;
    private Transform headRig;
    private PhotonView photonView;

    [SerializeField]
    private InputActionManager inputActionManager;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        XROrigin origin = FindFirstObjectByType<XROrigin>();
        leftHandRig = origin.transform.Find("Camera Offset/Left Controller");
        rightHandRig = origin.transform.Find("Camera Offset/Right Controller");
        headRig = origin.transform.Find("Camera Offset/Main Camera");
        // inputActionManager = FindFirstObjectByType<InputActionManager>();
        // inputActionManager.enabled = false;

        if (photonView.IsMine)
        {
            // head.gameObject.SetActive(false);
            // leftHand.gameObject.SetActive(false);
            // rightHand.gameObject.SetActive(false);
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
            // inputActionManager.enabled = true;
        }
        else
        {
        }


    }
    void Update()
    {
        if (!photonView.IsMine)
            return;

        MapPosition(headTransform, headRig);
        MapPosition(leftHandTransform, leftHandRig);
        MapPosition(rightHandTransform, rightHandRig);
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.SetPositionAndRotation(rigTransform.position, rigTransform.rotation);
    }
}
