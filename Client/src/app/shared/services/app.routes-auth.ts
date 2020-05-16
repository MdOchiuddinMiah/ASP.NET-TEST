import { Injectable, Component } from '@angular/core';
import { CanActivate, Router, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import config from '../../config';
import { CommonService } from './common.service';
import { Storage } from './storage.service';

@Injectable()
export class IsAuthenticated implements CanActivate {
    constructor(
        private router: Router,
        private commonservice: CommonService,
        private storage: Storage
    ) { }

    canActivate(): Observable<boolean> | boolean {
        let isAuthorized = false;
        isAuthorized = true;

        return isAuthorized;
    }

}