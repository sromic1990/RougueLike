using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Professor : MonoBehaviour
{
    public float Speed;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    private ProfessorStates _currentState;
    public ProfessorStates CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            ProfessorStates prevValue = _currentState;
            _currentState = value;
            StateChanged ( prevValue, _currentState );
        }
    }

    private Direction _currentDirection;
    public Direction CurrentDirection
    {
        get
        {
            return _currentDirection;
        }
        set
        {
            _currentDirection = value;
        }
    }

    #region Animation Specific
    private void StateChanged( ProfessorStates prevValue, ProfessorStates currentState )
    {
        ResetAllTriggers ( );
        switch ( currentState )
        {
            case ProfessorStates.Idle:
                SetIdle ( );
                break;

            case ProfessorStates.Walking:
                SetWalking ( );
                break;
        }
    }

    private void SetWalking()
    {
        switch ( CurrentDirection )
        {
            case Direction.Up:
                animator.SetTrigger ( "WalkUp" );
                break;

            case Direction.Left:
                animator.SetTrigger ( "WalkLeft" );
                break;

            case Direction.Right:
                animator.SetTrigger ( "WalkRight" );
                break;

            case Direction.Down:
                animator.SetTrigger ( "WalkDown" );
                break;
        }
    }

    private void SetIdle()
    {
        switch ( CurrentDirection )
        {
            case Direction.Up:
                animator.SetTrigger ( "IdleUp" );
                break;

            case Direction.Left:
                animator.SetTrigger ( "IdleLeft" );
                break;

            case Direction.Right:
                animator.SetTrigger ( "IdleRight" );
                break;

            case Direction.Down:
                animator.SetTrigger ( "IdleDown" );
                break;
        }
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger ( "WalkUp" );
        animator.ResetTrigger ( "WalkDown" );
        animator.ResetTrigger ( "WalkLeft" );
        animator.ResetTrigger ( "WalkRight" );
        animator.ResetTrigger ( "IdleUp" );
        animator.ResetTrigger ( "IdleDown" );
        animator.ResetTrigger ( "IdleLeft" );
        animator.ResetTrigger ( "IdleRight" );
    }
    #endregion

    #region Mono Methods
    private void Awake()
    {
        animator = GetComponent<Animator> ( );
        rigidBody = GetComponent<Rigidbody2D> ( );
        boxCollider = GetComponent<BoxCollider2D> ( );
        CurrentDirection = Direction.Down;
        CurrentState = ProfessorStates.Idle;
    }

	void Update ()
    {
        if ( !Input.anyKeyDown )
        {
            CurrentState = ProfessorStates.Idle;
        }

        var move = new Vector3 ( Input.GetAxis ( "Horizontal" ), Input.GetAxis ( "Vertical" ), 0 );
        transform.position += move * Speed * Time.deltaTime;

        bool isWalking = false;
        if ( Input.GetKey ( KeyCode.W ) )
        {
            CurrentDirection = Direction.Up;
            isWalking = true;
        }
        else if ( Input.GetKey ( KeyCode.S ) )
        {
            CurrentDirection = Direction.Down;
            isWalking = true;
        }
        else if ( Input.GetKey ( KeyCode.A ) )
        {
            CurrentDirection = Direction.Left;
            isWalking = true;
        }
        else if ( Input.GetKey ( KeyCode.D ) )
        {
            CurrentDirection = Direction.Right;
            isWalking = true;
        }

        if ( isWalking )
        {
            CurrentState = ProfessorStates.Walking;
        }
	}
    #endregion
}

public enum ProfessorStates
{
    Idle,
    Walking
}

public enum Direction
{
    Down,
    Up,
    Left,
    Right
}
