import { Component, OnInit, Inject } from "@angular/core";
import { ChartOptions, ChartType, ChartDataSets } from "chart.js";
import { Label } from "ng2-charts";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "app-openedchart",
    templateUrl: "./openedchart.component.html",
    styleUrls: ["./openedchart.component.scss"]
})
export class OpenedchartComponent implements OnInit {
    constructor(
        @Inject("BASE_URL") private baseUrl: string,
        private http: HttpClient
    ) {
        this.http
            .get<any>(`${baseUrl}/Dialog/GetOpenedChatsAsJson`, {
                params: {}
            })
            .toPromise()
            .then(res => {
                console.log(res);
            }, console.log);
    }

    ngOnInit() {}

    public barChartOptions: ChartOptions = {
        responsive: true,
        // We use these empty structures as placeholders for dynamic theming.
        scales: { xAxes: [{}], yAxes: [{}] },
        plugins: {
            datalabels: {
                anchor: "end",
                align: "end"
            }
        }
    };

    public barChartLabels: Label[] = [
        "2006",
        "2007",
        "2008",
        "2009",
        "2010",
        "2011",
        "2012"
    ];
    public barChartType: ChartType = "bar";
    public barChartLegend = true;
    public barChartPlugins = [];

    public barChartData: ChartDataSets[] = [
        { data: [65, 59, 80, 81, 56, 55, 40], label: "Series A" },
        { data: [28, 48, 40, 19, 86, 27, 90], label: "Series B" }
    ];
}
