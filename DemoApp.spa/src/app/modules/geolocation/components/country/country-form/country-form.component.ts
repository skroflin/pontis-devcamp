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
import { Country } from '../country.model';

/** SERVICES */
import { CountryService } from '../country.service';


@Component({
    selector: 'app-country-form',
    templateUrl: './country-form.component.html',
})
export class CountryFormComponent implements OnChanges {
    @Input() entity: Country;
    @Output() onChange = new EventEmitter();
    @Output() onCloseForm = new EventEmitter();

    formGroup: FormGroup;
    isEdit = false;
    isAdd = false;

    constructor(
        private fb: FormBuilder,
        private countryService: CountryService
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
                            if (this.entity.countryId) {
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
            countryId:[''],
            countryCode: [''],
            countryName: [''],
            dateCreated: [''],
            dateModified: [''],

        });
    }

    populateFormGroup() {
        this.countryService
            .get(this.entity.countryId)
            .subscribe((entity: Country) => {
                this.formGroup = this.fb.group({
                    countryId:[entity.countryId],
                    countryCode: [entity.countryCode],
                    countryName: [entity.countryName],
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
            this.countryService
                .add(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        } else {
            this.countryService
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