module NancyExt
    open Nancy
    type Nancy.Response with
        static member String(text:string) = Response.op_Implicit(text)
        static member View(view:System.Action<System.IO.Stream>) = Response.op_Implicit(view)
        static member StatusCode(code:int) = Response.op_Implicit(code)
        static member HttpStatusCode(code:HttpStatusCode) = Response.op_Implicit(code)
    
    type Nancy.NancyModule with
        member self.RenderView(viewName:string, model) = Response.View(self.View.[viewName, model])
  
    let (?) (inst:Nancy.DynamicDictionary) name = 
        let dictValue = inst.[name] :?> Nancy.DynamicDictionaryValue
        if dictValue.HasValue then dictValue.Value else null

    let (?<-) (inst:Nancy.DynamicDictionary) name value = 
        inst.[name] <- value

        
    let ToActionFunc f = System.Func<System.Object,Nancy.Response> f