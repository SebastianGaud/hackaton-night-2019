import { Component, OnInit } from "@angular/core";
import { ChartOptions, ChartType, ChartDataSets } from "chart.js";
import { Label } from "ng2-charts";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment.prod";

@Component({
    selector: "app-opened-chart",
    templateUrl: "./opened-chart.component.html",
    styleUrls: ["./opened-chart.component.scss"]
})
export class OpenedChartComponent implements OnInit {
    public barChartType: ChartType = "bar";
    public barChartLegend = true;
    public barChartLabels: Label[] = [];
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

    constructor(private http: HttpClient) {}

    ngOnInit() {
        var chartData = {
            data: []
        };
        this.http
            .get<Response>(
                `${environment.base_url}/Dialog/GetOpenedChatsAsJson`,
                {
                    params: {}
                }
            )
            .toPromise()
            .then(res => {
                res.openedChats.forEach(e => {
                    this.barChartLabels.push(e.day.toDateString());
                    chartData.data.push(e.openedChats);
                });
            }, console.log);
        this.barChartData.push(chartData);
    }
}

interface Response {
    openedChats: Array<ResponeObj>;
}

interface ResponeObj {
    day: Date;
    openedChats: number;
}
