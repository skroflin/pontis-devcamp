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
import { Application } from '../application.model';

/** SERVICES */
import { ApplicationService } from '../application.service';


@Component({
    selector: 'app-application-form',
    templateUrl: './application-form.component.html'
})
export class ApplicationFormComponent implements OnChanges {
    @Input() entity: Application;
    @Output() onChange = new EventEmitter();
    @Output() onCloseForm = new EventEmitter();

    formGroup: FormGroup;
    isEdit = false;
    isAdd = false;

    constructor(
        private fb: FormBuilder,
        private applicationService: ApplicationService
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
                            if (this.entity.applicationId) {
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
            applicationId:[''],
            applicationName: [''],
            dateCreated: [''],
            dateModified: [''],

        });
    }

    populateFormGroup() {
        this.applicationService
            .get(this.entity.applicationId)
            .subscribe((entity: Application) => {
                this.formGroup = this.fb.group({
                    applicationId:[entity.applicationId],
                    applicationName: [entity.applicationName],
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
            this.applicationService
                .add(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        } else {
            this.applicationService
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