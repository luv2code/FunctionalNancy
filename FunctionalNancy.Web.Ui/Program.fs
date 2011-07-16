
module Program
    open System
    open System.Diagnostics
    open Nancy.Hosting.Self

    [<EntryPoint>]
    let Main (args) = 
        let nancyHost = new NancyHost(new Uri("http://localhost:1234/nancy/"))
        nancyHost.Start()

        Console.WriteLine("Nancy now listening - navigating to http://localhost:1234/nancy/. Press enter to stop");
        Process.Start("http://localhost:1234/nancy/") |> ignore
        Console.ReadKey() |> ignore

        nancyHost.Stop()

        Console.WriteLine("Stopped. Good bye!")
        0