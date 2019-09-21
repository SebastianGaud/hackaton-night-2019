import { Component, OnInit } from "@angular/core";
import { ChartType, ChartDataSets, ChartOptions } from "chart.js";
import { Label } from "ng2-charts";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment.prod";

@Component({
    selector: "app-intent",
    templateUrl: "./intent.component.html",
    styleUrls: ["./intent.component.scss"]
})
export class IntentComponent {
    public barChartType: ChartType = "bar";
    public barChartLabels: Label[] = [];
    public barChartLegend = false;
    public barChartData: ChartDataSets[] = [];
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

    constructor(private http: HttpClient) {
        var chartData = {
            data: []
        };
        this.http
            .get<Response>(`${environment.base_url}/Report/GetIntentAnalisys`, {
                params: {}
            })
            .toPromise()
            .then(res => {
                console.log(res);
                res.data.forEach(e => {
                    this.barChartLabels.push(e.desc);
                    chartData.data.push(e.c);
                });
            }, console.log);
        this.barChartData.push(chartData);
    }
}

interface Response {
    data: Array<ResponseObj>;
}

interface ResponseObj {
    desc: string;
    c: number;
}
