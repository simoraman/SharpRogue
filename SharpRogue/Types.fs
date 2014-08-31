namespace SharpRogue
module Types =
    type Coordinate = { x:int; y:int; }

    type Hero = {
        currentPosition : Coordinate;
        oldPosition : Coordinate;
    }

    type Input = 
        Up 
        | Down
        | Left
        | Right
        | Exit
