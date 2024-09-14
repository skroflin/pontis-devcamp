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
import { NationalIdType } from '../national-id-type.model';

/** SERVICES */
import { NationalIdTypeService } from '../national-id-type.service';


@Component({
    selector: 'app-national-id-type-form',
    templateUrl: './national-id-type-form.component.html',
})
export class NationalIdTypeFormComponent implements OnChanges {
    @Input() entity: NationalIdType;
    @Output() onChange = new EventEmitter();
    @Output() onCloseForm = new EventEmitter();

    formGroup: FormGroup;
    isEdit = false;
    isAdd = false;

    constructor(
        private fb: FormBuilder,
        private nationalIdTypeService: NationalIdTypeService
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
                            if (this.entity.id) {
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
            id:[''],
            nationalIdTypeName:[''],
        });
    }

    populateFormGroup() {
        this.nationalIdTypeService
            .get(this.entity.id)
            .subscribe((entity: NationalIdType) => {
             this.formGroup = this.fb.group({
                id: [entity.id],
                nationalIdTypeName:[entity.nationalIdTypeName],
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
            this.nationalIdTypeService
                .add(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        } else {
            this.nationalIdTypeService
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