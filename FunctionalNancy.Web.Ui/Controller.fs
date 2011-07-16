module Routing
    open System
    open Nancy
    open NancyExt
    type RouteModule () as self = 
        inherit NancyModule ()
        do
            self.Get.["/{name}"] <- ToActionFunc self.RootPath
            self.Post.["/{name}"] <- ToActionFunc self.RootPath

            self.Get.["/AsyncTest"] <- ToActionFunc self.AsyncTest

        member self.RootPath (urlParams) = 
            let name = (urlParams:?>DynamicDictionary)?name.ToString();
            let model = new DynamicDictionary()
            let message = 
                match self.Request.Method with
                | "GET" -> "Hello World"
                | "POST" -> "this was posted back: " + ((self.Request.Form:?>DynamicDictionary)?userMessage:?>string)
                | _ -> "Unsupported Method"
            model?Message <- message
            model?UrlMessage <- if String.IsNullOrWhiteSpace(name) then "" else "this was on the url : " + name 
            model?Time <- DateTime.Now
            self.RenderView("index.cshtml", model)
        
        member self.AsyncTest urlParams = 
            let res = 
                async {
                    do! Async.Sleep 5000
                    let! resp = async.Return(Response.String("hello"))
                    return resp
                } |> Async.StartAsTask
            res.Wait()
            res.Result