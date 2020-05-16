import { Component, OnInit } from '@angular/core';
import { CommonService, Storage } from '../../shared';
import { Router } from '@angular/router';
import { DashBoardService } from '../shared/dash-board.service';
import config from '../../config';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.scss']
})
export class DashBoardComponent implements OnInit {
  loginUser: any;
  postList: any[] = [];

  constructor(
    private commonService: CommonService,
    private storage: Storage,
    private dashboardService: DashBoardService
  ) { }

  ngOnInit() {
    this.loginUser = this.storage.get(config.storage.app);
    if (this.loginUser) {
      this._renderData();
    }
  }

  _renderData() {
    this.postList = [];
    this.commonService.EnableLoading(true);
    this.dashboardService.getPosts().subscribe(data => {
      let responseData = this.commonService.handleResult(data, false);
      if (responseData) {
        this.postList = responseData;
      }
    }, error => {
      this.commonService.handleError(error);
    });

  }

  likeunlike(row, likedislike) {
    let isLike = (likedislike == 'like') ? true : false;
    this.commonService.EnableLoading(true);
    this.dashboardService.updateFeedback(this.loginUser.id, row.commentId, isLike).subscribe(data => {
      let responseData = this.commonService.handleResult(data, true);
      if (responseData) {
        this._renderData();
      }
    }, error => {
      this.commonService.handleError(error);
    });
  }

}
