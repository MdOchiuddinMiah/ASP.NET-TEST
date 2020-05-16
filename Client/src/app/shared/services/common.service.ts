import { Injectable } from '@angular/core';
import config from '../../config';
import { FormControl } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { Storage } from './storage.service';
import { Router } from '@angular/router';
import { MatSnackBar, MatDialog } from '@angular/material';
import { ConfirmationComponent } from '../../shared/components/confirmation/confirmation.component';

@Injectable()
export class CommonService {

    public loadingAction = new Subject<any>();

    constructor(
        private storage: Storage,
        public snackBar: MatSnackBar,
        public dialog: MatDialog
    ) { }

    public EnableLoading(data) {
        this.loadingAction.next(data);
    }

    public _equalFilter(list, option1, value) {
        const filterValue = value && value.toString().toLowerCase();
        return list.filter(option => option[option1].toString().toLowerCase() == filterValue);
    }

    public selectedValueFind(list, option1, value) {
        const filterValue = value && value.toString().toLowerCase();
        return list.find(x => x[option1].toString().toLowerCase() == filterValue);
    }

    handleResult(result, isShowMessage, message?) {
        this.EnableLoading(false);
        message = message ? message : result.message;

        if (result && result.success) {
            isShowMessage && this.showMessage(message);
            return result.data ? result.data : null;
        }
        else if (!result.success && result.message && result.message === 'Invalid token') {
            this.storage.clear();
        }
        else if (!result.success) {
            isShowMessage = true;
        }
        if (result.exception && result.errorCode && result.exception) {
            isShowMessage = true;
            message = 'ErrorCode: ' + result.errorCode + '. ' + message;
        }
        isShowMessage && this.showMessage(message);
        return null;
    }

    showMessage(message) {
        this.snackBar.open(message, 'Close', {
            horizontalPosition: 'center',
            verticalPosition: 'top',
            duration: 180000,
        });
    }

    openConfirmationDialog(message, func) {
        let dialogRef = this.dialog.open(ConfirmationComponent, {
            position: { top: '65px' },
            height: 'auto',
            width: '500px',
            data: message
        });
        dialogRef.afterClosed().subscribe(result => {
            func(result);
        });
    }

    handleError(error) {
        this.EnableLoading(false);
        error.message = error.message.includes('Observable.throw is not a function') ? 'Internal server error' : error.message;
        error.message &&
            this.showMessage(error.message);
        this.storage.clear();
    }
}