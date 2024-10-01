import {
    Component,
    EventEmitter,
    Input,
    OnChanges,
    Output,
    SimpleChanges,
} from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

/** DOMAIN */
import { Employee } from '../employee.model';

/** SERVICES */
import { EmployeeService } from '../employee.service';


@Component({
    selector: 'app-employee-form',
    templateUrl: './employee-form.component.html',
})
export class EmployeeFormComponent implements OnChanges {
    @Input() entity: Employee;
    @Output() onChange = new EventEmitter();
    @Output() onCloseForm = new EventEmitter();

    formGroup: FormGroup;
    isEdit = false;
    isAdd = false;

    constructor(
        private fb: FormBuilder,
        private employeeService: EmployeeService
    ) { }

    ngOnChanges(changes: SimpleChanges) {
        for (const propName in changes) {
            if (changes.hasOwnProperty(propName)) {
                switch (propName) {
                    case 'entity': {
                        if (this.entity) {
                            if (!this.formGroup) {
                                this.createFormGroup();
                            }
                            if (this.entity.employeeId) {
                                this.populateFormGroup();
                            } else {
                                this.formGroup.enable();
                                this.formGroup.reset();
                                this.isAdd = true;
                            }
                        }
                    }
                }
            }
        }
    }

    createFormGroup() {
        this.formGroup = this.fb.group({
            employeeId: [''],
            username: [''],
            firstname: [''],
            lastname: [''],
            nationalIdNumber: [''],
            nationalIdTypeId: [''],
            genderId: [''],
            birthdate: [''],
            address: [''],
            placeId: [''],
            countryId: [''],
            dateCreated: [''],
            dateModified: [''],

        });
    }

    populateFormGroup() {
        this.employeeService
            .get(this.entity.employeeId)
            .subscribe((entity: Employee) => {
                this.formGroup = this.fb.group({
                    employeeId: [entity.employeeId],
                    username: [entity.username],
                    firstname: [entity.firstname],
                    lastname: [entity.lastname],
                    nationalIdNumber: [entity.nationalIdNumber],
                    nationalIdTypeId: [entity.nationalIdTypeId],
                    genderId: [entity.genderId],
                    birthdate: [entity.birthdate],
                    address: [entity.address],
                    placeId: [entity.placeId],
                    countryId: [entity.countryId],
                    dateCreated: [entity.dateCreated],
                    dateModified: [entity.dateModified],

                });
                this.formGroup.disable();
            });
    }

    edit() {
        this.isEdit = true;
        this.formGroup.enable();
    }

    save() {
        if (this.isAdd) {
            this.employeeService
                .add(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        } else {
            this.employeeService
                .update(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        }
    }

    changeFinished() {
        this.onChange.emit();
        this.closeForm();
    }

    cancel() {
        this.isEdit = false;
        this.isAdd = false;
        this.formGroup.disable();
    }

    closeForm() {
        this.cancel();
        this.onCloseForm.emit();
    }
}