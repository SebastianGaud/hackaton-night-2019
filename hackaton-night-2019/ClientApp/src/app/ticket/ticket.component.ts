import { Component, OnInit } from "@angular/core";
import { ChartType, ChartDataSets, ChartOptions } from "chart.js";
import { Label } from "ng2-charts";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment.prod";

@Component({
    selector: "app-ticket",
    templateUrl: "./ticket.component.html",
    styleUrls: ["./ticket.component.scss"]
})
export class TicketComponent {
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
            .get<Response>(
                `${environment.base_url}/Dialog/GetOpenedTicketsAsJson`,
                {
                    params: {}
                }
            )
            .toPromise()
            .then(res => {
                res.openedTicket.forEach(e => {
                    this.barChartLabels.push(e.date);
                    chartData.data.push(e.openedTicket);
                });
            }, console.log);
        this.barChartData.push(chartData);
    }
}

interface Response {
    openedTicket: Array<ResponeObj>;
}

interface ResponeObj {
    date: string;
    openedTicket: number;
}
