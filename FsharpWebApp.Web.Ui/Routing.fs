module Routing
    open System
    open Nancy
    open Actions
    type RouteModule () as this = 
        inherit NancyModule ()
        do
            this.Get.["/"] <- Actions.RootPath this
            this.Post.["/"] <- Actions.RootPath this

            this.Get.["/AsyncTest"] <- Actions.AsyncTest this