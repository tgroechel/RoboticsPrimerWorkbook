# Exercise 2: Sensors

![Sensors](../../../.images/Sensors.png)

## Overview
In this exercise you will learn how to write basic sensors and teleoperation controls for the turtlebot.

## Code Explained

You will be writing code within `PID-Scripts/PIDController.cs`. You will calculate each error and gain:
```C#
"P" : "Proportional"
"I" : "Integral" 
"D" : "Derivative"
```
and add the resulting forces in `UpdateMotorForce()`. The force is then applied to the end of the inverted pendulum. All functions that need coding are marking with `CODE` and found within the `#region CODE`.

## Coding Order and Tips
- If all the comments and extra funcitons are distracting, look into your Editor's ability to do "code folding". An example of this feature can be found [here](https://code.visualstudio.com/docs/editor/codebasics#:~:text=Use%20Shift%20%2B%20Click%20on%20the,uncollapsed%20region%20at%20the%20cursor.).
- Start with calculating the `PError` and then the `PGain` (`kP*PError`)
- You will want to use the `motor` variable to get the motor angle. See `Motor.cs` for details.
- Having the `PGain` calculated should allow you to then apply that force win `UpdateMotorForce()`
- Play around with just the `PGain` until you have a somewhat stable system, the pendulum will likely cycle overshooting and undershooting
- Once you have this osscilating over/undershoot behavior, move onto the `DGain` and then `IGain`
- Rememeber tuning gains happens one at a time and should give you a relative idea of how adjusting each knob affects the system
- The memebers `iError` and `forceDirection` will be helpful
- Zero is the down angle with 180 being the top
- When pressing play, each slider is reset to the default values. The defualt values can be changed in the Inspector view of the `PIDController.cs` script. The script is attached in the Hierarchy under `MotorLink/PendulumLink`
