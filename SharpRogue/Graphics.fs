namespace SharpRogue
module Graphics =
    open Types

    let hideCursor() = System.Console.SetCursorPosition(0,0)

    let drawCreature (creature:ICreature, world:MapTile list) = 
        System.Console.SetCursorPosition(creature.currentPosition.x, creature.currentPosition.y)
        System.Console.Write creature.avatar
        let found = List.find (Utils.findTile creature.oldPosition) world
        System.Console.SetCursorPosition(creature.oldPosition.x, creature.oldPosition.y)
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

