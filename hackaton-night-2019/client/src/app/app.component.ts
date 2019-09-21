import { Component } from "@angular/core";

@Component({
    selector: "app-root",
    template: `
        <nb-layout>
            <nb-layout-header fixed>
                Data and charts
            </nb-layout-header>

            <nb-layout-column>
                <app-charts></app-charts>
            </nb-layout-column>
        </nb-layout>
    `,
    styles: []
})
export class AppComponent {
    title = "charts";
}
