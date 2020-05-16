import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule, Http, XHRBackend, RequestOptions, BaseRequestOptions } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ROUTES } from './app.route';
import { ApiBaseService } from './shared/services/api-base.service';
import {
  MaterialModule,
  ShareModule,
  NocontentComponent,
  IsAuthenticated,
  CommonService,
  Storage,
  HeaderComponent,
} from './shared'

import { AppComponent } from './app.component';
import { HomeComponent } from './administration/home/home/home.component';
import { ConfirmationComponent } from './shared/components/confirmation/confirmation.component';

@NgModule({
  entryComponents: [
    ConfirmationComponent
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    NocontentComponent,
    ConfirmationComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    HttpModule,
    MaterialModule,
    ShareModule,
    RouterModule.forRoot(ROUTES, { useHash: true }),
  ],
  providers: [
    IsAuthenticated,
    CommonService,
    ApiBaseService,
    Storage,
    {
      provide: ApiBaseService,
      deps: [XHRBackend, RequestOptions],
      useFactory: (Backend, defaultOptions) => new ApiBaseService(Backend, defaultOptions)
    },
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
