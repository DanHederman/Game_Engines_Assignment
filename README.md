# Game Engines Assignment

**Version 1.0.0**

A repository for my Game Engines assignment 

## Contributers
- Daniel Hederman <C15410232@mydit.ie>

## Licence & Copyright

© Dan Hederman, Student Number: C15410232, DIT Games Engines 2018

## Proposal

The idea I have in mind is to create either a wormhole/blackhole or tunnel in which a person/creature will be propelled down with random shapes passing the subject of the program changing colours, 
spinning in differnt directions, this may be changed to the wormhole/black hole changing colours using a colour gradient. The following are the videos that inspired the idea.

* Californication - Red Hot Chili Peppers: https://www.youtube.com/watch?v=YlUKcNNmywk 
* Saturnz Barz - Gorillaz: https://www.youtube.com/watch?v=5qJp6xlKEug 
* Do I Wanna Know - Arctic Monkeys: https://www.youtube.com/watch?v=bpOSxM0rNPM

## Updated Proposal

The project ended up taking a slightly differnt direction then originally intended. The idea for a tunnel was still kept as the main feature for end product. The idea to have a creature propelled
down the tunnel was scrapped because it would take away from the main focus, the tunnel. Instead it was decided a better approach would be to implement audio features. The audio features would 
contribute to:

* Controlling the speed of the tunnel
* Adding effects to a spiral(s) going down the centre of the tunnel
* Allowing Audio to be taken in from the mic or from a track directly fed to the program

Changes inspired by: 

* https://www.youtube.com/watch?v=paAq8k55K9A

A colour gradient will be used to alter the colour of both the tunnel and the spirals.

## How It Works

In its default state when the play button is pressed unity will begin by starting the audio track and forming the trails that will make up both of the spirals and the main shape of the tunnel.
The default shape of the tunnel is a spiral, this makes it clear that the spirals running down the centre are independent of the main tunnel. There are a number of shapes that are avaliable and
they can be applied by using the number keys as follows.

* 1 Triangle
* 2 Square
* 3 Five Point Star 
* 4 Narrow tunnel with wide trail effects
* 5 Narrow tunnel, multiple star points on outside of tunnel 
* 6 Narrow tunnel, similar star points to 5 (more points)
* 7 Hexagon
* 0 Spiral (default shape)

There are also additional controls that are avaliable for the project

* i Changes the direction of the spin of the tunnel
* c Enables limited player control of the tunnel
* x Disables the player control of the tunnel (this is the default)

It is also possible to change the gradient colours of both the spirals and the tunnel itself. 

* Navigate to the inspector of the GameObject to be changed (MainTunnelShape, TrailClockwise, TrailAntiClockwise - All are children of Tunnel)
* Under the trail renderer component a new colour can be selected from either the existing presets or a new colour gradient can be created.
* The duration of the trail, its width, material etc can also be adjusted here

