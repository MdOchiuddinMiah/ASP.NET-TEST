import { Injectable } from '@angular/core';
import { ApiBaseService } from './api-base.service';
import config from '../../config';
import { CommonService } from './common.service';
import { NgxConfigureService } from 'ngx-configure';

@Injectable({
    providedIn: 'root'
})

export class HeaderService {
    constructor(private http: ApiBaseService,
        private configService: NgxConfigureService,
        public commonservice: CommonService
    ) { }

}