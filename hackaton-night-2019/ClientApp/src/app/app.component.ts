import { Component } from "@angular/core";

@Component({
    selector: "app-root",
    template: `
        <nb-layout>
            <nb-layout-header fixed>
                <!-- Insert header here -->
            </nb-layout-header>

            <app-charts></app-charts>

            <nb-layout-footer fixed>
                <!-- Insert footer here -->
            </nb-layout-footer>
        </nb-layout>
    `,
    styles: []
})
export class AppComponent {
    title = "c";
}
