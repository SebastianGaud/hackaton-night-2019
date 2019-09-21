import { Component, OnInit } from "@angular/core";
import { ChartOptions, ChartType, ChartDataSets } from "chart.js";
import { Label } from "ng2-charts";

@Component({
    selector: "app-opened-chart",
    templateUrl: "./opened-chart.component.html",
    styleUrls: ["./opened-chart.component.scss"]
})
export class OpenedChartComponent implements OnInit {
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

    constructor() {}

    ngOnInit() {}
}
