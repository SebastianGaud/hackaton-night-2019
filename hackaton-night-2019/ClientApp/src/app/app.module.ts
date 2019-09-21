import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NbThemeModule, NbLayoutModule, NbCardModule } from "@nebular/theme";
import { NbEvaIconsModule } from "@nebular/eva-icons";
import { ChartsModule } from "ng2-charts";
import { HttpClientModule } from "@angular/common/http";
import { ChartsComponent } from './charts/charts.component';

@NgModule({
    declarations: [AppComponent, ChartsComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        BrowserAnimationsModule,
        NbThemeModule.forRoot({ name: "default" }),
        NbLayoutModule,
        NbEvaIconsModule,
        NbCardModule,
        ChartsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {}
