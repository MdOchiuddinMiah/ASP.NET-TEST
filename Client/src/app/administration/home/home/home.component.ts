import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService, Storage } from '../../../shared/index';
import config from '../../../config';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  loginUser: any;
  constructor(
    private storage: Storage,
    private commonService: CommonService,
    private router: Router
  ) { }

  ngOnInit() {
    this.storage.clear();
    let testUser = { 'id': 1, 'name': 'User1', 'token': '12@12#' };
    this.storage.set(config.storage.app, testUser);
    this.loginUser = this.storage.get(config.storage.app);
  }

}
