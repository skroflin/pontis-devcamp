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
import { Region } from '../region.model';

/** SERVICES */
import { RegionService } from '../region.service';


@Component({
    selector: 'app-region-form',
    templateUrl: './region-form.component.html',
})
export class RegionFormComponent implements OnChanges {
    @Input() entity: Region;
    @Output() onChange = new EventEmitter();
    @Output() onCloseForm = new EventEmitter();

    formGroup: FormGroup;
    isEdit = false;
    isAdd = false;

    constructor(
        private fb: FormBuilder,
        private regionService: RegionService
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
                            if (this.entity.regionId) {
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
            regionId: [''],
            regionName: [''],
            countryId: [''],
            dateCreated: [''],
            dateModified: [''],

        });
    }

    populateFormGroup() {
        this.regionService
            .get(this.entity.regionId)
            .subscribe((entity: Region) => {
                this.formGroup = this.fb.group({
                    regionId: [entity.regionName],
                    regionName: [entity.regionName],
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
            this.regionService
                .add(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        } else {
            this.regionService
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