import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxConfigureModule, NgxConfigureOptions } from 'ngx-configure';

export const MY_FORMATS = {
    parse: {
        dateInput: 'DD/MM/YYYY',
    },
    display: {
        dateInput: 'DD/MM/YYYY',
        monthYearLabel: 'MMM YYYY',
        dateA11yLabel: 'LL',
        monthYearA11yLabel: 'MMMM YYYY',
    },
};


@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        NgxConfigureModule.forRoot()
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [
        NgxConfigureOptions
    ]
})
export class ShareModule {
}