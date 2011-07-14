module NancyExt
    open Nancy
    type Nancy.Response with
        static member FromString(text:string) = Response.op_Implicit(text)
        static member FromView(view:System.Action<System.IO.Stream>) = Response.op_Implicit(view)
        static member FromStatusCode(code:int) = Response.op_Implicit(code)
        static member FromHttpStatusCode(code:HttpStatusCode) = Response.op_Implicit(code)
    
    type Nancy.NancyModule with
        member this.RenderView(viewName:string, model) = Response.FromView(this.View.[viewName, model])
  
    let (?) (inst:obj) name = 
        let dict = inst :?> Nancy.DynamicDictionary
        let dictValue = dict.[name] :?> Nancy.DynamicDictionaryValue
        if dictValue.HasValue then dictValue.Value else null

    let (?<-) (inst:obj) name value = 
        let dict = inst :?> Nancy.DynamicDictionary
        dict.[name] <- value