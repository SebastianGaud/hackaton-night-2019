import { Component } from "@angular/core";

@Component({
    selector: "app-root",
    template: `
        <nb-layout>
            <nb-layout-header fixed>
                <!-- Insert header here -->
            </nb-layout-header>
            <nb-layout-column>
                <app-charts></app-charts>
            </nb-layout-column>

            <nb-layout-footer fixed>
                <!-- Insert footer here -->
            </nb-layout-footer>
        </nb-layout>
    `,
    styles: []
})
export class AppComponent {
    title = "charts";
}
