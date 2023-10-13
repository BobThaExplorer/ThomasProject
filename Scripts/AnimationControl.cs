using Godot;
using System;
 
public partial class AnimationControl : CharacterBody2D
{
	public const float speed = 660.0f;
	public const float Jumpvelocity = -9000.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public string current_dir = "none"; // Vi gemmer retning her
	private Vector2 temp;
	AnimatedSprite2D anim;
 
	public override void _Ready()
	{
		anim =  GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim.Play("Idle");
	}
 
	public override void _PhysicsProcess(double delta)
	{
		PlayerMovement(delta);
		Vector2 velocity = Velocity;
				// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
	}
	
 
	public void PlayerMovement(double delta)
	{
		Vector2 velocity = Velocity;
		//Movement
		if ( Input.IsKeyPressed(Key.D)){
			current_dir = "right";
			PlayAnimation(1);
			temp = new Vector2(speed,gravity);
			Velocity = temp;
		}
		else if( Input.IsKeyPressed(Key.A)){
			current_dir = "left";
			PlayAnimation(1);
			temp = new Vector2(-speed,gravity);
			Velocity = temp;
		}
		else {
			PlayAnimation(0);
			Velocity = new Vector2(0,gravity);
		}
		
		if (Input.IsKeyPressed(Key.W) && IsOnFloor()){
			temp = new Vector2(0,Jumpvelocity);
			Velocity = temp;
		}
			
		if (Input.IsKeyPressed(Key.S) && IsOnFloor()){
			current_dir = "down";
			temp = new Vector2(0,0);
			anim.Play("Tension");
			Velocity = temp;
			
			if (Input.IsKeyPressed(Key.W) && IsOnFloor()){
			current_dir = "Up";
			anim.Play("Jump");
			temp = new Vector2(0,Jumpvelocity*2);
			Velocity = temp;
			}
		}
		if (Input.IsKeyPressed(Key.E) && IsOnFloor()){
			current_dir = "Taunt";
		}

	
		MoveAndSlide();
	}
 
	void PlayAnimation(int movement){
 
		string dir = current_dir;
 
		if ( dir == "right" ) {
			anim.FlipH = false;
			if (movement == 1) {
				anim.Play("Walk");
			}
			else if ( movement == 0 ){
				anim.Play("Idle");
			}
		}
		else if ( dir == "left") {
			anim.FlipH = true;
			if (movement == 1)
				anim.Play("Walk");
			else if (movement == 0)
				anim.Play("Idle");
		}
		else if ( dir == "down") {
			if (movement == 0)
				anim.Play("Idle");
		}
		else if ( dir == "up") {
			if (movement == 1)
				anim.Play("Jump");
			else anim.Play("Idle");
		}
		else if ( dir == "Taunt") {
			if (movement == 0)
				anim.Play("ThumbUp");
		}
	}
}
