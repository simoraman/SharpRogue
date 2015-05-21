namespace SharpRogue
module Types =
    type Coordinate = { x:int; y:int; }

    type ICreature =
        abstract currentPosition: Coordinate
        abstract oldPosition: Coordinate
        abstract avatar: char

    type Creature = 
        {currentPosition : Coordinate;
        oldPosition : Coordinate;
        avatar : char;
        health: int}
        interface ICreature with
            member x.currentPosition = x.currentPosition
            member x.oldPosition = x.oldPosition
            member x.avatar = x.avatar

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
