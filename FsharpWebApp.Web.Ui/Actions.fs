module Actions

    open System
    open System.Dynamic
    open System.Collections.Generic
    open System.Threading.Tasks
    open Nancy
    open NancyExt

    let ActionMethod (f, context:NancyModule) = 
        let view(inputs:Object) = 
            f(context, inputs)
        new Func<Object, Response>(view)

    let RootPath (context:NancyModule) = ActionMethod((fun (context:NancyModule, inputs:Object) ->
        let model = new DynamicDictionary():> System.Object
        let message = 
            match context.Request.Method with
            | "GET" -> "Hello World"
            | "POST" -> "This was posted back: " + (context.Request.Form?userMessage:?>string)
            | _ -> "Unsupported Method"

        model?Message <- message
        model?Time <- DateTime.Now
        context.RenderView("index.cshtml", model)), context)


    let AsyncTest (context:NancyModule) = 
        let view (inputs:Object) = 
            let res = 
                async {
                    do! Async.Sleep 5000
                    let! resp = async.Return(Response.FromString("hello"))
                    return resp
                } |> Async.StartAsTask
            res.Wait()
            res.Result
        new Func<Object, Response>(view)

    