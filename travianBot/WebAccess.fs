module WebAccess 

open System.Windows.Forms 

let private htmlElementCollectionToSeq (collection: HtmlElementCollection) =
    let enumerator = collection.GetEnumerator()
    seq {       
          while enumerator.MoveNext () do 
             yield enumerator.Current :?> HtmlElement 
    }

let private login (browser: WebBrowser) (callback : (WebBrowser -> unit)) =
    let elements = htmlElementCollectionToSeq(browser.Document.GetElementsByTagName("input"))

    let nameEl = elements |> Seq.filter(fun e -> e.GetAttribute("name").Equals("name")) |> Seq.exactlyOne
    let passwordEl = elements |> Seq.filter(fun e -> e.GetAttribute("name").Equals("password")) |> Seq.exactlyOne
    let (name, password) = ConstantsProvider.credentials
    nameEl.SetAttribute("value", name)
    passwordEl.SetAttribute("value", password)

    let lowResEl = elements |> Seq.filter(fun e -> e.GetAttribute("name").Equals("lowRes")) |> Seq.exactlyOne
    lowResEl.SetAttribute("value", "1")

    browser.DocumentCompleted.Add(fun _ -> callback browser)

    browser.Document.GetElementById("s1").InvokeMember("click") |> ignore
    ()

let getTravianMain (callback : (WebBrowser -> unit))  =
    let browser = new WebBrowser()
    browser.DocumentCompleted.Add
        (fun _ ->  (login browser callback) |> ignore)
    browser.Navigate(ConstantsProvider.mainUrl)
    ()

    