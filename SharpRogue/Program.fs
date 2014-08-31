// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open Microsoft.FSharp.Collections

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

let rec getInput() = 
    let input = System.Console.ReadKey(true) 
    match input.Key with
        | System.ConsoleKey.RightArrow -> Right
        | System.ConsoleKey.DownArrow -> Down
        | System.ConsoleKey.LeftArrow -> Left
        | System.ConsoleKey.UpArrow -> Up
        | System.ConsoleKey.Q -> Exit
        | _ -> getInput()

let findTile coord elem = (fst elem) = coord

let drawHero (hero:Hero, world:((int * int) * char) list) = 
    System.Console.SetCursorPosition(hero.currentPosition.x, hero.currentPosition.y)
    System.Console.Write '@'

    let found = List.find (findTile (hero.oldPosition.x, hero.oldPosition.y)) world
    System.Console.SetCursorPosition(hero.oldPosition.x, hero.oldPosition.y)
    snd found |> System.Console.Write
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
let s = seq { 
            for row in 0 .. 100 do 
                for col in 0 .. 100 do 
                    let y = [(row, col)] |> Seq.ofList
                    yield y
            }
let realMap = [for str in map1 -> [for c in str -> c]]

//mutable crap
let generateCoordinates =
    let mutable y' = 0
    let mutable coord = List.empty
    for row in realMap do
        let mutable x' = 0
        for col in row do
            coord <- List.append coord [((x',y'),col)]
            x' <- x' + 1
        y' <- y' + 1
    coord

let drawTile (tile:(int * int) * char) = 
            let x = tile |> fst |> fst
            let y = tile |> fst |> snd
            let ch = tile |> snd
            System.Console.SetCursorPosition(x,y)
            System.Console.Write ch
        
let drawWorld world = 
    System.Console.Clear()
    List.map (fun x -> drawTile(x)) world |> ignore



let rec gameLoop(hero) =

    let tryNew newCoordinates = 
        let found = List.find (findTile (newCoordinates.x, newCoordinates.y)) generateCoordinates  
        match (snd found) with
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