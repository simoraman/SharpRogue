// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open Microsoft.FSharp.Collections

type Coordinate = { x:int; y:int; }

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

let drawHero (coordinate:Coordinate) = 
    System.Console.SetCursorPosition(coordinate.x, coordinate.y)
    System.Console.Write '@'

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

let rec gameLoop(coordinate) =
    generateCoordinates |> drawWorld
    drawHero coordinate
    let input = getInput()
    match input with
        | Right -> gameLoop { x = coordinate.x + 1; y = coordinate.y; }
        | Down -> gameLoop { x = coordinate.x; y = coordinate.y + 1; }
        | Left -> gameLoop { x = coordinate.x - 1; y = coordinate.y; }
        | Up -> gameLoop { x = coordinate.x; y = coordinate.y - 1; }
        | Exit -> ()

[<EntryPoint>]
let main argv = 
    gameLoop {x = 0; y = 0;}
    0 // return an integer exit code