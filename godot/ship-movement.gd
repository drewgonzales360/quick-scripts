extends RigidBody

# If you use this script and make improvements, please
# put a PR up with your changes! There aren't a lot of examples
# so we need to help each other out. :)

var thrust_sensitivity = 10
var roll_torque_sensitivity = 5
var pitch_torque_sensitivity = 5
func _integrate_forces(state):
	var pitch_input = 0
	var roll_input = 0
	var thrust_input = 75
	if Input.is_key_pressed(KEY_R):
		thrust_input = 2

	if Input.is_key_pressed(KEY_F):
		thrust_input = -2

	if Input.is_action_pressed("ui_up"):
		pitch_input = -1

	if Input.is_action_pressed("ui_down"):
		pitch_input = 1

	if Input.is_action_pressed("ui_left"):
		roll_input = -1

	if Input.is_action_pressed("ui_right"):
		roll_input = 1

	state.add_central_force(-transform.basis.z * thrust_input * thrust_sensitivity)
	state.add_torque(transform.basis.x * pitch_input * pitch_torque_sensitivity)
	state.add_torque(-transform.basis.z * roll_input * roll_torque_sensitivity)

func _ready():
	angular_damp = .5
	linear_damp = 1.5

