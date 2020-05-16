import { Injectable } from '@angular/core';
import { ApiBaseService } from '../../shared/services/api-base.service';
import config from '../../config';
import { CommonService } from '../../shared';
import { NgxConfigureService } from 'ngx-configure';

@Injectable({
  providedIn: 'root'
})
export class DashBoardService {

  constructor(private http: ApiBaseService,
    public commonservice: CommonService,
    private configService: NgxConfigureService
  ) { }

  getPosts(searchValue, page, pageSize) {
    return this.http.get(this.configService.config.base + config.apiendpoint.posts + '/' + searchValue + '/' + page + '/' + pageSize);
  }

  updateFeedback(userId, commentId, isLike) {
    let data = { userId: userId, commentId: commentId, isLike: isLike };
    return this.http.put(this.configService.config.base + config.apiendpoint.feedback, data);
  }

}
