namespace SharpRogue
module main =
    open Microsoft.FSharp.Collections
    open Types

    let rec getInput() = 
        let input = System.Console.ReadKey(true) 
        match input.Key with
            | System.ConsoleKey.RightArrow -> Right
            | System.ConsoleKey.DownArrow -> Down
            | System.ConsoleKey.LeftArrow -> Left
            | System.ConsoleKey.UpArrow -> Up
            | System.ConsoleKey.Q -> Exit
            | _ -> getInput()

    let findTile coord elem = elem.coordinate = coord

    let drawHero (hero:Hero, world:MapTile list) = 
        System.Console.SetCursorPosition(hero.currentPosition.x, hero.currentPosition.y)
        System.Console.Write '@'

        let found = List.find (findTile hero.oldPosition) world
        System.Console.SetCursorPosition(hero.oldPosition.x, hero.oldPosition.y)
        found.tile |> System.Console.Write
        System.Console.SetCursorPosition(0,0)
            

    let map1 = [ 
            "##############";
            "#>           #          ######";
            "#            ############    #";
            "#            -          +    #";
            "#    ~~      ############    #";
            "#     ~~     #          #    #";
            "#      ~~    #          # <  #";
            "##############          ######" ]
    
    let tokenizeMap map = [for str in map -> [for c in str -> c]]

    //mutable crap
    let generateCoordinates =
        let mutable y' = 0
        let mutable coord = List.empty
        for row in tokenizeMap map1 do
            let mutable x' = 0
            for col in row do
                coord <- List.append coord [{ coordinate = { x = x'; y = y'; }; tile = col; }]
                x' <- x' + 1
            y' <- y' + 1
        coord

    let drawTile (tile:MapTile) = 
                System.Console.SetCursorPosition(tile.coordinate.x, tile.coordinate.y)
                System.Console.Write tile.tile
            
    let drawWorld world = 
        System.Console.Clear()
        List.map (fun x -> drawTile(x)) world |> ignore



    let rec gameLoop(hero) =

        let tryNew newCoordinates = 
            let found = List.find (findTile newCoordinates) generateCoordinates  
            match found.tile with
            | '#' -> hero
            | '+' -> hero         
            | _ -> {hero with currentPosition = newCoordinates; oldPosition = hero.currentPosition}     

        let move direction = 
            match direction with
                | Right -> tryNew { x = hero.currentPosition.x + 1; y = hero.currentPosition.y; }
                | Down -> tryNew { x = hero.currentPosition.x; y = hero.currentPosition.y + 1; }
                | Left -> tryNew { x = hero.currentPosition.x - 1; y = hero.currentPosition.y; }
                | Up -> tryNew { x = hero.currentPosition.x; y = hero.currentPosition.y - 1; } 
                | _ -> hero
            |> gameLoop

        drawHero (hero, generateCoordinates)
        let input = getInput()
        match input with
            | Exit -> ()
            | _ -> move input    


    [<EntryPoint>]
    let main argv = 
        generateCoordinates |> drawWorld
        gameLoop { oldPosition = {x = 1; y = 1;}; currentPosition = {x = 1; y = 1;}; }
        0 // return an integer exit code