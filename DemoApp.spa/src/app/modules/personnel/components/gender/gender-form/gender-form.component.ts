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
import { Gender } from '../gender.model';

/** SERVICES */
import { GenderService } from '../gender.service';


@Component({
    selector: 'app-gender-form',
    templateUrl: './gender-form.component.html',
})
export class GenderFormComponent implements OnChanges {
    @Input() entity: Gender;
    @Output() onChange = new EventEmitter();
    @Output() onCloseForm = new EventEmitter();

    formGroup: FormGroup;
    isEdit = false;
    isAdd = false;

    constructor(
        private fb: FormBuilder,
        private genderService: GenderService
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
                            if (this.entity.genderId) {
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
            genderId: [''],
            genderShort: [''],
            genderName: [''],
            dateCreated: [''],
            dateModified: [''],

        });
    }

    populateFormGroup() {
        this.genderService
            .get(this.entity.genderId)
            .subscribe((entity: Gender) => {
                this.formGroup = this.fb.group({
                    genderId: [entity.genderId],
                    genderShort: [entity.genderShort],
                    genderName: [entity.genderName],
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
            this.genderService
                .add(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        } else {
            this.genderService
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