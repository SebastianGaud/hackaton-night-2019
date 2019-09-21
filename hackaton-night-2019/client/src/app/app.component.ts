import { Component } from "@angular/core";

@Component({
    selector: "app-root",
    template: `
        <nb-layout>
            <nb-layout-header fixed>
                <!-- Insert header here -->
            </nb-layout-header>

            <nb-layout-column> 
              <app-openedchart></opened-chart>
            </nb-layout-column>
        </nb-layout>
    `,
    styles: []
})
export class AppComponent {
    title = "c";
}
