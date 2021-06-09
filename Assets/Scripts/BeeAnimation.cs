using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAnimation : MonoBehaviour
{
    public float animationFPS;
    public Sprite[] flyAnimation;
    public float frameTimer;
    private int frameIndex;

    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    public AnimationState state = AnimationState.Fly;
    private Dictionary<AnimationState, Sprite[]> animationAtlas;

    public enum AnimationState
    {
        Fly
    }


    // Start is called before the first frame update
    void Start()
    {
        animationAtlas = new Dictionary<AnimationState, Sprite[]>();

        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();

        animationAtlas.Add(AnimationState.Fly, flyAnimation);
     

    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;

        AnimationState newState = GetAnimationState();
        if (state != newState)
        {
            TransitionToState(newState);
        }

        if (frameTimer <= 0.0f)
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
        return AnimationState.Fly;
    }
}
