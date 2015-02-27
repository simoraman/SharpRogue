namespace SharpRogue
module Types =
    type Coordinate = { x:int; y:int; }

    type Creature = {
        currentPosition : Coordinate;
        oldPosition : Coordinate;
        avatar : char
    }

    type Hero = Creature

    type MapTile = {
        coordinate : Coordinate;
        tile : char;
    }

    type World = {
        tiles : MapTile list
        hero : Hero
        monster : Creature
    }

    type Input = 
        Up 
        | Down
        | Left
        | Right
        | Open
        | Exit
