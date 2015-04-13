module Bot

open System.Windows.Forms 
open WebAccess


let mutable private browser : WebBrowser = new WebBrowser()

let performAnalysis =
    let test = ""
    ()

let start =
    WebAccess.getTravianMain(fun b -> 
        browser <- b
        performAnalysis |> ignore) |> ignore
    ()