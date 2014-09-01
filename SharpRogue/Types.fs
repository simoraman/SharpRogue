namespace SharpRogue
module Types =
    type Coordinate = { x:int; y:int; }

    type Hero = {
        currentPosition : Coordinate;
        oldPosition : Coordinate;
    }

    type MapTile = {
        coordinate : Coordinate;
        tile : char;
    }

    type World = {
        tiles : MapTile list
        hero : Hero
    }

    type Input = 
        Up 
        | Down
        | Left
        | Right
        | Open
        | Exit
