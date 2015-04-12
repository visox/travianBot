module WebAccess 

open System.Windows.Forms 

let htmlElementCollectionToSeq (collection: HtmlElementCollection) =
    let enumerator = collection.GetEnumerator()
    seq {       
          while enumerator.MoveNext () do 
             yield enumerator.Current :?> HtmlElement 
    }

let private login (browser: WebBrowser) (callback : (unit -> unit)) =
    let elements = htmlElementCollectionToSeq(browser.Document.GetElementsByTagName("input"))
    let nameEl = elements |> Seq.filter(fun e -> e.GetAttribute("name").Equals("name")) |> Seq.exactlyOne
    let passwordEl = elements |> Seq.filter(fun e -> e.GetAttribute("name").Equals("password")) |> Seq.exactlyOne
    let (name, password) = ConstantsProvider.credentials
    nameEl.SetAttribute("value", name)
    passwordEl.SetAttribute("value", password)
    0

let getTravianMain (callback : (unit -> unit))  =
    let browser = new WebBrowser()
    browser.DocumentCompleted.Add
        (fun _ ->  (login browser callback) |> ignore)
    browser.Navigate(ConstantsProvider.mainUrl)
    0

    