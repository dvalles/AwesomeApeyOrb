using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Animate the hands that come with the Oculus Integration
 * A fork from their script which hand much more dependencies
 * YOU NEED XRControllerInput (another of my packages) IN THE PROJECT
 */

public class HandAnimator : MonoBehaviour
{
    public const string ANIM_LAYER_NAME_POINT = "Point Layer";
    public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";
    public const float THRESH_COLLISION_FLEX = 0.9f;

    public const float INPUT_RATE_CHANGE = 20.0f;

    public const float COLLIDER_SCALE_MIN = 0.01f;
    public const float COLLIDER_SCALE_MAX = 1.0f;
    public const float COLLIDER_SCALE_PER_SECOND = 1.0f;

    public const float TRIGGER_DEBOUNCE_TIME = 0.05f;
    public const float THUMB_DEBOUNCE_TIME = 0.15f;

    public bool isLeftHand = false;
    [SerializeField]
    private Animator m_animator = null;
    [SerializeField]
    private HandPose m_defaultGrabPose = null;

    private Collider[] m_colliders = null;
    private bool m_collisionEnabled = true;

    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;

    private bool m_isPointing = false;
    private bool m_isGivingThumbsUp = false;
    private float m_pointBlend = 0.0f;
    private float m_thumbsUpBlend = 0.0f;

    private bool m_restoreOnInputAcquired = false;

    void OnEnable()
    {
        //subscribe
        if (isLeftHand)
        {
            XRControllerInput.leftGripAxis += SetFlex;
            XRControllerInput.leftTriggerAxis += SetPinch;
            XRControllerInput.leftThumbTouchHeld += SetThumbRest;
            XRControllerInput.leftIndexTouchHeld += SetIndexTouch;
        }
        else
        {
            XRControllerInput.rightGripAxis += SetFlex;
            XRControllerInput.rightTriggerAxis += SetPinch;
            XRControllerInput.rightThumbTouchHeld += SetThumbRest;
            XRControllerInput.rightIndexTouchHeld += SetIndexTouch;
        }
    }
    
    void OnDisable()
    {
        //unsubscribe
        if (isLeftHand)
        {
            XRControllerInput.leftGripAxis -= SetFlex;
            XRControllerInput.leftGripAxis -= SetPinch;
            XRControllerInput.leftThumbTouchHeld -= SetThumbRest;
            XRControllerInput.leftIndexTouchHeld -= SetIndexTouch;
        }
        else
        {
            XRControllerInput.rightGripAxis -= SetFlex;
            XRControllerInput.rightTriggerAxis -= SetPinch;
            XRControllerInput.rightThumbTouchHeld -= SetThumbRest;
            XRControllerInput.rightIndexTouchHeld -= SetIndexTouch;
        }
    }

    private void Start()
    {
        // Get animator layer indices by name, for later use switching between hand visuals
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);

    }

    private void Update()
    {
        m_pointBlend = InputValueRateChange(m_isPointing, m_pointBlend);
        m_thumbsUpBlend = InputValueRateChange(m_isGivingThumbsUp, m_thumbsUpBlend);

        UpdateAnimStates();

        m_isGivingThumbsUp = true;
        m_isPointing = true;
    }

    private float InputValueRateChange(bool isDown, float value)
    {
        float rateDelta = Time.deltaTime * INPUT_RATE_CHANGE;
        float sign = isDown ? 1.0f : -1.0f;
        return Mathf.Clamp01(value + rateDelta * sign);
    }

    //set their thumbrest value
    void SetThumbRest()
    {
        m_isGivingThumbsUp = false;
    }

    //set their index rest value
    void SetIndexTouch()
    {
        m_isPointing = false;
    }

    //set the flex value based on input
    private void SetFlex(float flex)
    {
        m_animator.SetFloat(m_animParamIndexFlex, flex);
    }

    //set the pinch value based on input
    private void SetPinch(float pinch)
    {
        m_animator.SetFloat("Pinch", pinch);
    }

    private void UpdateAnimStates()
    {
        //in case you want to do different hand poses later (like a special one for grabbing a certain item)
        HandPose grabPose = m_defaultGrabPose;
        HandPoseId handPoseId = grabPose.PoseId;
        m_animator.SetInteger(m_animParamIndexPose, (int)handPoseId);

        // Point
        bool canPoint = grabPose.AllowPointing;
        float point = canPoint ? m_pointBlend : 0.0f;
        m_animator.SetLayerWeight(m_animLayerIndexPoint, point);

        // Thumbs up
        bool canThumbsUp = grabPose.AllowThumbsUp;
        float thumbsUp = canThumbsUp ? m_thumbsUpBlend : 0.0f;
        m_animator.SetLayerWeight(m_animLayerIndexThumb, thumbsUp);
    }

    private float m_collisionScaleCurrent = 0.0f;
}
