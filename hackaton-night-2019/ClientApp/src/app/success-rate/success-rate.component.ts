import { Component, OnInit } from "@angular/core";
import { ChartOptions, ChartType } from "chart.js";
import { Label } from "ng2-charts";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "app-success-rate",
    templateUrl: "./success-rate.component.html",
    styleUrls: ["./success-rate.component.scss"]
})
export class SuccessRateComponent {
    public pieChartOptions: ChartOptions = {
        responsive: true,
        legend: {
            position: "top"
        },
        plugins: {
            datalabels: {
                formatter: (value, ctx) => {
                    const label = ctx.chart.data.labels[ctx.dataIndex];
                    return label;
                }
            }
        }
    };

    public pieChartLabels: Label[] = [
        "Chat portate a buon termine",
        "Chat con apertura ticket"
    ];
    public pieChartData: number[] = [];
    public pieChartType: ChartType = "pie";
    public pieChartColors = [
        {
            backgroundColor: [
                "rgba(255,0,0,0.3)",
                "rgba(0,255,0,0.3)",
                "rgba(0,0,255,0.3)"
            ]
        }
    ];

    constructor(private http: HttpClient) {
        this.http
            .get<Response>(
                `${environment.base_url}/Dialog/GetSuccessRateAsJson`,
                {
                    params: {}
                }
            )
            .toPromise()
            .then(res => {
                console.log(res);
                this.pieChartData.push(res.successRate);
                this.pieChartData.push(100 - res.successRate);
            }, console.log);
    }
}

interface Response {
    successRate: number;
}
