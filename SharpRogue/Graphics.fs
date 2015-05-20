namespace SharpRogue
module Graphics =
    open Types

    let hideCursor() = System.Console.SetCursorPosition(0,0)

    let inline drawCreature (creature:Coordinate*Coordinate*char, world:MapTile list) = 
        let (newPosition, oldPosition, avatar) = creature
        System.Console.SetCursorPosition(newPosition.x, newPosition.y)
        
        System.Console.Write avatar

        let found = List.find (Utils.findTile oldPosition) world
        System.Console.SetCursorPosition(oldPosition.x, oldPosition.y)
        found.tile |> System.Console.Write
        hideCursor()

    let drawTile (tile:MapTile) = 
        System.Console.SetCursorPosition(tile.coordinate.x, tile.coordinate.y)
        System.Console.Write tile.tile
            
    let drawWorld world = 
        System.Console.Clear()
        List.map (fun x -> drawTile(x)) world |> ignore

    let drawOpenDoor coordinate =
        System.Console.SetCursorPosition(coordinate.x, coordinate.y)
        System.Console.Write '-'
        hideCursor()

