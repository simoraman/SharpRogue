namespace SharpRogue
module Graphics =
    open Types

    let drawHero (hero:Hero, world:MapTile list) = 
        System.Console.SetCursorPosition(hero.currentPosition.x, hero.currentPosition.y)
        System.Console.Write '@'

        let found = List.find (Utils.findTile hero.oldPosition) world
        System.Console.SetCursorPosition(hero.oldPosition.x, hero.oldPosition.y)
        found.tile |> System.Console.Write
        System.Console.SetCursorPosition(0,0)

    let drawTile (tile:MapTile) = 
        System.Console.SetCursorPosition(tile.coordinate.x, tile.coordinate.y)
        System.Console.Write tile.tile
            
    let drawWorld world = 
        System.Console.Clear()
        List.map (fun x -> drawTile(x)) world |> ignore