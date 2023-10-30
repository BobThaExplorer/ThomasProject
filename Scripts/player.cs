using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 800.0f;
	public const float JumpVelocity = -750.0f;
	public string current_dir = "none"; // Vi gemmer retning her
	private Vector2 temp = Vector2.Zero;
	private Timer delayTimer;
	AnimatedSprite2D anim;
	
		public override void _Ready()
	{
		delayTimer = GetNode<Timer>("Timer");
		anim =  GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim.Play("Idle");
	}
	
	// Gravity
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		
		//Movement
		if ( Input.IsKeyPressed(Key.D)){
			current_dir = "right";
			PlayAnimation(1);
			velocity.X = Speed;
		}

		else if( Input.IsKeyPressed(Key.A)){
			current_dir = "left";
			PlayAnimation(1);
			velocity.X = -Speed;
		}
		
		else{
			velocity.X = 0;
			PlayAnimation(0);
		}
		
		// Jump.
		if (Input.IsKeyPressed(Key.W) && IsOnFloor()){
			current_dir = "up";
			PlayAnimation(1);
			velocity.Y = JumpVelocity;
		}
		
		if (Input.IsKeyPressed(Key.S) && IsOnFloor()){
			PlayAnimation(1);
			current_dir = "down";
			velocity.X = 0;
			if (Input.IsActionJustPressed("ui_up") && IsOnFloor()){
			current_dir = "up";
			PlayAnimation(1);
			velocity.Y = JumpVelocity * 3/2;
			}
		}
		
		if (Input.IsKeyPressed(Key.E) && IsOnFloor()){
			PlayAnimation(0);
			velocity.X = 0;
			current_dir = "Taunt";
		}
		
		Velocity = velocity;
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
				anim.Play("Tension");
			if (movement == 0)
				anim.Play("Idle");
		}
		else if ( dir == "up") {
			if (movement == 1)
				anim.Play("Jump");
		}
		else if ( dir == "Taunt") {
			if (movement == 0)
				anim.Play("ThumbUp");
		}
	}


private void _on_area_2d_area_exited(Area2D area)
{
	delayTimer.Start();
}
private void _on_area_2d_area_entered(Area2D area)
{
	GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	GD.Print("Ooga");
}

private void _on_timer_timeout()
{
	
	GD.Print("test");
	GetTree().ChangeSceneToFile("res://Scenes/level_1.tscn");
}



}



