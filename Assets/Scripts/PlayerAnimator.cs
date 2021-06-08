using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public float animationFPS;
    public Sprite[] idleAnimation;
    public float frameTimer;
    private int frameIndex;

    public Sprite[] walkLAnimation;
    public Sprite[] walkRAnimation;
    public Sprite[] walkDownAnimation;
    public Sprite[] walkUpAnimation;

    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    public AnimationState state = AnimationState.Idle;
    private Dictionary<AnimationState, Sprite[]> animationAtlas;

    public enum AnimationState
    {
        Idle,
        WalkL,
        WalkR,
        WalkUp,
        WalkDown
    }


    // Start is called before the first frame update
    void Start()
    {
        animationAtlas = new Dictionary<AnimationState, Sprite[]>();

        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();

        animationAtlas.Add(AnimationState.Idle, idleAnimation);
        animationAtlas.Add(AnimationState.WalkUp, walkUpAnimation);
        animationAtlas.Add(AnimationState.WalkDown, walkDownAnimation);
        animationAtlas.Add(AnimationState.WalkR, walkRAnimation);
        animationAtlas.Add(AnimationState.WalkL, walkLAnimation);


    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;
 
        AnimationState newState = GetAnimationState();
        if(state != newState)
        {
            TransitionToState(newState);
        }

        if(frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            Sprite[] anim = animationAtlas[state];
            frameIndex %= anim.Length;
            sRenderer.sprite = anim[frameIndex];
            frameIndex++;

        }

    }

    void TransitionToState(AnimationState newState)
    {
        frameTimer = 0.0f;
        frameIndex = 0;
        state = newState;
    }

    AnimationState GetAnimationState()
    {
        if(rb.velocity.magnitude < 0.5f)
        {
            return AnimationState.Idle;
        }

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y)){

            if(rb.velocity.x > 0)
            {
                return AnimationState.WalkR;
            }
            else
            {
                return AnimationState.WalkL;
            }

        }

        else
        {
            if(rb.velocity.y > 0)
            {
                return AnimationState.WalkUp;
            }


            else
            {
                return AnimationState.WalkDown;
            }
        }
            
    }
}
