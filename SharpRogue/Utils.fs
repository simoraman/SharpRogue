namespace SharpRogue
module Utils =
    open Types
    let findTile coord elem = elem.coordinate = coord

    let tokenizeMap map = [for row in map -> [for column in row -> column]]