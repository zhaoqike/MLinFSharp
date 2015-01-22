// 在 http://fsharp.net 上了解有关 F# 的更多信息
// 请参阅“F# 教程”项目以获取更多帮助。
open System    
open System.Windows
open System.Windows.Controls
open System.Windows.Forms.Integration
open System.Windows.Forms.DataVisualization.Charting
open System.Windows.Forms
open MSDN.FSharp.Charting
open System.Drawing // for Color
open System.IO
//open System.Diagnostics
//open Microsoft.Office.Interop.Excel

let openFileNew filename = 
    let reader=File.OpenText(filename)
    let lines=reader.ReadToEnd();
    //printfn "lines"
    //printfn "%A" lines
    
    
    let linesreplace=lines.Replace(System.Environment.NewLine.ToString(),",")
    //printfn "linesreplace"
    //printfn "%A" linesreplace
    let lineArr=linesreplace.Split([|','|])
    //lines.
    //printfn "%A" lineArr
    //printfn "lineArr"
    //printfn "%A" lineArr
    let lineList=Array.toList(lineArr)
    let trans (s:string)=
        let words=s.Split([|'\t'|])
        assert(words.Length=3)
        let (x, y, belong)=(Convert.ToDouble(words.[0]), Convert.ToDouble(words.[1]), Convert.ToInt32(words.[2]))
        let point=Tuple.Create(x,y,belong)
        point

    let pointList=List.map trans lineList 
    pointList

let getpointGroup (pointlist:List<float * float * int>) (i:int)=
    let pointGroup=List.filter (fun (x,y,belong)->belong=i) pointlist
    let points=List.map (fun (x,y,belong)->(x,y)) pointGroup
    points

let draw filename=
    let pointList=openFileNew filename
    let pointList1=getpointGroup pointList 0
    pointList1

let openFile filename1 =
    let reader=File.OpenText(filename1)
    //let arr=Array.Resize( [||].Initialize()
    let arr=[||]
    while not reader.EndOfStream do
        let line=reader.ReadLine()
        let words=line.Split([|'\t'|])
        assert(words.Length=3)
        let x=Convert.ToDouble(words.[0])
        let y=Convert.ToDouble(words.[1])
        let belong=Convert.ToInt32(words.[2])
        let point=(x,y,belong)
        let arr = Array.append arr [|point|] 
        ignore arr
    printfn "%A" arr
       // for word in words do
           // printfn "%A" point

let pointChart=
    let pointList=openFileNew "in.txt"
    let pointGroup0=getpointGroup pointList 0
    let pointGroup1=getpointGroup pointList 1
    printfn "%A %A %A" pointList.Length pointGroup0.Length pointGroup1.Length
    let points0=pointGroup0|>FSharpChart.Point
    let points1=pointGroup1|>FSharpChart.Point
    new ChartControl(FSharpChart.Combine [points0;points1])

//let app=new ApplicationClass(Visible=true)
//let workbook=app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet)
//let worksheet = (workbook.Worksheets.[1] :?> Worksheet)
//
//
//let rnd = new Random()
//let titles = [| "No"; "Maybe"; "Yes" |]
//let names = Array2D.init 10 1 (fun i _ -> string('A' + char(i)))
//let data = Array2D.init 10 3 (fun _ _ -> rnd.NextDouble())
//
//// Populate data into Excel worksheet
//worksheet.Range("C2", "E2").Value2 <- titles
//worksheet.Range("B3", "B12").Value2 <- names
//worksheet.Range("C3", "E12").Value2 <- data
//
//// Add new item to the charts collection
//let chartobjects = (worksheet.ChartObjects() :?> ChartObjects) 
//let chartobject = chartobjects.Add(400.0, 20.0, 550.0, 350.0) 
//
//// Configure the chart using the wizard
//chartobject.Chart.ChartWizard
//  ( Title = "Stacked column chart", 
//    Source = worksheet.Range("B2", "E12"),
//    Gallery = XlChartType.xl3DColumnStacked100, 
//    PlotBy = XlRowCol.xlColumns)
//
//// Set graphical style of the chart
//chartobject.Chart.ChartStyle <- 2

///Create a chart containing a default area and show it on a form
//let chart = new Chart(Dock = DockStyle.Fill)
//let form = new Form(Visible = true, Width = 700, Height = 500)
//chart.ChartAreas.Add(new ChartArea("MainArea"))
//form.Controls.Add(chart)

//// Create series and add it to the chart
//let series = new Series(ChartType = SeriesChartType.BoxPlot)
//chart.Series.Add(series)
//
//// Add data to the series in a loop
//[| -12.7; 11.6; -8.3; 6.4; -2.5; -1.6 |]
//|> Array.map box |> series.Points.AddY |> ignore
//
///// Add data series of the specified chart type to a chart
//let addSeries typ (chart:Chart) =
//    let series = new Series(ChartType = typ) 
//    chart.Series.Add(series)
//    series


let mainNoXaml() = 
    let formsHost =new  WindowsFormsHost(Child = pointChart)//(Child=starckbar)
    let window = Window()
    window.Content <- formsHost
    let app =new System.Windows.Application() in app.Run(window) |> ignore


[<EntryPoint>]
[<STAThread>]
let main argv = 
    //printfn "hello world"
    //printfn "%A" argv
    //openFile "in.txt"
    //openFileNew "in.txt"
    //let chart, series = createChart SeriesChartType.Line
    //series.BorderWidth <- 3
    //series.Points.DataBindY(data)
    do mainNoXaml()

    0 // 返回整数退出代码

