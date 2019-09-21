import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
    NbThemeModule,
    NbLayoutModule,
    NbChatModule,
    NbSpinnerModule
} from "@nebular/theme";
import { NbEvaIconsModule } from "@nebular/eva-icons";
import { HttpClientModule } from "@angular/common/http";
import { OpenedchartComponent } from "./openedchart/openedchart.component";
import { ChartsModule } from "ng2-charts";

@NgModule({
    declarations: [AppComponent, OpenedchartComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        BrowserAnimationsModule,
        NbThemeModule.forRoot({ name: "default" }),
        NbLayoutModule,
        NbEvaIconsModule,
        NbChatModule,
        NbSpinnerModule,
        ChartsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {}
