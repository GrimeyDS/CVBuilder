import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HomeComponent } from "./home/home.component";
import { provideHttpClient } from "@angular/common/http";
import { HeaderComponent } from "./layout/header/header.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from "primeng/config";
import Lara from '@primeng/themes/lara'
import { ConfirmDialogModule } from "primeng/confirmdialog";
import { ConfirmationService, MessageService } from "primeng/api";


@NgModule({
    declarations: [
      AppComponent
    ],
    imports: [
      BrowserModule,
      BrowserAnimationsModule,
      AppRoutingModule,
      HeaderComponent,
      FooterComponent,
      HomeComponent,
      ConfirmDialogModule
    ],
  providers: [
    provideHttpClient(),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Lara,
        options: {
          prefix: 'p',
          darkModeSelector: 'p-light',
          cssLayer: false
        }
      }
    }),
    ConfirmationService,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
