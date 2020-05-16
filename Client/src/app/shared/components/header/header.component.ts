import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Storage } from '../../services/storage.service';
import { CommonService } from '../../services/common.service';
import { HeaderService } from '../../services/header.service';
import { Router } from '@angular/router';
import config from '../../../config';
import { Subscription } from "rxjs";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
  @Input() loginUser: any;

  loginInfo: any;
  route: any;

  constructor(
    private storage: Storage,
    private router: Router,
    private commonService: CommonService,
    private headerService: HeaderService,
    private cdref: ChangeDetectorRef,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    if (this.loginUser) {
      this.loginInfo = this.loginUser;
    }
  }

  ngOnDestroy() {
  }

}
