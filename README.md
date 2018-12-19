# Game Engines Assignment

**Version 1.0.0**

A repository for my Game Engines assignment 

## Contributers
- Daniel Hederman <C15410232@mydit.ie>

## Licence & Copyright

© Dan Hederman, Student Number: C15410232, DIT Games Engines 2018

## Proposal

The idea is to create either a wormhole/blackhole or tunnel in which a person/creature will be propelled down with random shapes passing the subject of the program changing colours, 
spinning in differnt directions, this may be changed to the wormhole/black hole changing colours using a colour gradient. The following are the videos that inspired the idea.

* Californication - Red Hot Chili Peppers: https://www.youtube.com/watch?v=YlUKcNNmywk 
* Saturnz Barz - Gorillaz: https://www.youtube.com/watch?v=5qJp6xlKEug 
* Do I Wanna Know - Arctic Monkeys: https://www.youtube.com/watch?v=bpOSxM0rNPM

## Updated Proposal

The project ended up taking a slightly differnt direction then originally intended. The idea for a tunnel was still kept as the main feature for end product. The idea to have a creature propelled
down the tunnel was scrapped because it would take away from the main focus, the tunnel. Instead it was decided a better approach would be to implement audio features. The audio features would 
contribute to:

* Controlling the speed of the tunnel.
* Adding effects to a spiral(s) going down the centre of the tunnel.
* Allowing Audio to be taken in from the mic or from a track directly fed to the program.

Changes inspired by: 

* https://www.youtube.com/watch?v=paAq8k55K9A

A colour gradient will be used to alter the colour of both the tunnel and the spirals.

## What The Project Does

The project aims to present a visual experince that is controlled by audio with effects that are unique to the audio track being fed to the program. The project can either use a microphone
to listen to sound and control the tunnel/effects based on noise it picks up. The project can also have a track given to it by the user. The bands that effect the patterns of the spirals can be 
changed to a different band. There will also be a spinning cube that will be transparent that will track down the tunnel.

## How It Works

In its default state when the play button is pressed unity will begin by starting the audio track and forming the trails that will make up both of the spirals and the main shape of the tunnel.
The default shape of the tunnel is a spiral, this makes it clear that the spirals running down the centre are independent of the main tunnel. There are a number of shapes that are avaliable and
they can be applied by using the number keys as follows.

* 1 Triangle.
* 2 Square.
* 3 Five Point Star.
* 4 Narrow tunnel with wide trail effects.
* 5 Narrow tunnel, multiple star points on outside of tunnel. 
* 6 Narrow tunnel, similar star points to 5 (more points).
* 7 Hexagon.
* 0 Spiral (default shape).

There are also additional controls that are avaliable for the project.

* i Changes the direction of the spin of the tunnel.
* c Enables limited player control of the tunnel.
* x Disables the player control of the tunnel (this is the default).

It is also possible to change the gradient colours of both the spirals and the tunnel itself. 

* Navigate to the inspector of the GameObject to be changed (MainTunnelShape, TrailClockwise, TrailAntiClockwise - All are children of Tunnel).
* Under the trail renderer component a new colour can be selected from either the existing presets or a new colour gradient can be created.
* The duration of the trail, its width, material etc can also be adjusted here.

The inspector can also be used in a similar way to enable or disable the mirophone feature.

* Microphone disabled (default state) will result in the audio track that was given to the project to be used to control the tunnel and effects.
* Microphone enabled will see the tunnel controlled by whatever noise the mic picks up.

There are a number of different audio tracks inclued in the project which can be used for the tunnel.

* Open the inspector for the GameObject called Audio there will be a field called Audio Clip.
* In the assets foler titled Audio there are a numeber of tracks that can be used.
* Drag the track from the Audio folder to the Audio Peer script field Audio clip.

Two separate materials were used for the tunnel and the spiral. This allowed the transparency for the main tunnel shape to be altered without impacting the transparency of the trails for the
spirals.

## Development

The Game Engines module was taken with no prior knowledge of Unity. To say any part of this project was developed without any assistance from either online tutorials or examples from class would 
simply be a false claim. The audio reactive aspect of the project was done using a tutorial found on the Unity community site. However, this tutorial was not all conclusive. There were parts 
that did not work and as a result required tweaking in order to give them fuctionality. There were other parts that needed to be altered in order to achieve what I was aiming for. The idea to 
use the phyllotaxis algorithm as the base for the project came from biology. It describes the method by which plants arrange their leaves in patterns. Using a trail renderer to create the patterns 
made sense as it allowed the backend of the tunnel to disipate and made it possible to utilise a colour gradient. The other parts of the project were a combination of tutorials and alterations of 
tutorials to try and achieve the desired effects. 