import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonService, Storage } from './shared';
import { ChangeDetectorRef } from '@angular/core';
import { Subscription } from "rxjs";
import config from './config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit, OnDestroy {
  loading: boolean;
  color = 'primary';
  theme = 'candy-app-theme';
  private loadingModule: Subscription;
  colorArray = ['primary', 'accent', 'warn'];
  constructor(
    private commonService: CommonService,
    private cdref: ChangeDetectorRef,
    private storage: Storage
  ) {
    let color = this.storage.get(config.storage.theme);
    color &&
      (this.theme = color);
  }

  ngOnInit() {
    this.loadingModule = this.commonService.loadingAction.subscribe(data => {
      this.loading = data;
      let randomNumber = Math.floor(Math.random() * this.colorArray.length);
      this.color = this.colorArray[randomNumber];
      this.cdref.detectChanges();
    });

  }

  ngOnDestroy() {
    this.loadingModule.unsubscribe();
  }

}
