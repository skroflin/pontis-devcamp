import {
    Component,
    EventEmitter,
    Input,
    OnChanges,
    OnInit,
    Output,
    SimpleChanges,
} from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

/** DOMAIN */
import { Place } from '../place.model';

/** SERVICES */
import { PlaceService } from '../place.service';
import { Authorization } from '@shared/entities/enums/authorization.enum';
import { AuthenticationService } from '../../../../../core/authentication/authentication.service';
import { District } from '../../district/district.model';
import { Region } from '../../region/region.model';
import { DistrictService } from '../../district/district.service';
import { RegionService } from '../../region/region.service';


@Component({
    selector: 'app-place-form',
    templateUrl: './place-form.component.html',
})
export class PlaceFormComponent implements OnInit, OnChanges {
    @Input() entity: Place;
    @Output() onChange = new EventEmitter();
    @Output() onCloseForm = new EventEmitter();

    formGroup: FormGroup;
    isEdit = false;
    isAdd = false;

    canEdit = false;
    canSave = false;

    districts: District[] = [];
    filteredDistricts: District[] = [];

    regions: Region[] = [];
    filteredRegions: Region[] = [];

    constructor(
        private fb: FormBuilder,
        private placeService: PlaceService,
        private districtService: DistrictService,
        private regionService: RegionService,
        private authenticationService: AuthenticationService
    ) { }

    ngOnInit(): void {
        const authorizations = this.authenticationService.getUserAuthorizations();
        this.canEdit = authorizations.includes(Authorization.Write);
        this.canSave = authorizations.includes(Authorization.Save);
    }

    ngOnChanges(changes: SimpleChanges) {
        for (const propName in changes) {
            if (changes.hasOwnProperty(propName)) {
                switch (propName) {
                    case 'entity': {
                        if (this.entity) {
                            if (!this.formGroup) {
                                this.createFormGroup();
                            }
                            if (this.entity.placeId) {
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
            placeId: [''],
            placeNationalCode: [''],
            placeName: [''],
            districtId: [''],
            regionId: [''],
            dateCreated: [''],
            dateModified: [''],

        });
    }

    populateFormGroup() {
        this.placeService
            .get(this.entity.placeId)
            .subscribe((entity: Place) => {
                this.formGroup = this.fb.group({
                    placeId: [entity.placeId],
                    placeNationalCode: [entity.placeNationalCode],
                    placeName: [entity.placeName],
                    districtId: [entity.districtId],
                    regionId: [entity.regionId],
                    dateCreated: [entity.dateCreated],
                    dateModified: [entity.dateModified],

                });
                this.formGroup.disable();
            });

        this.districtService.getAll().subscribe((districts) => {
            this.districts = districts
            this.filteredDistricts = this.districts;
        });
        this.regionService.getAll().subscribe((regions) => {
            this.regions = regions
            this.filteredRegions = this.regions;
        });
    }

    edit() {
        this.isEdit = true;
        this.formGroup.enable();
    }

    save() {
        if (this.isAdd) {
            this.placeService
                .add(this.formGroup.value)
                .subscribe(() => this.changeFinished());
        } else {
            this.placeService
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

    filterDistricts(value: any) {
        this.districts = this.filteredDistricts.filter((district) => district.districtName.toLowerCase().includes(value))
    }

    filterRegions(value: any) {
        this.regions = this.filteredRegions.filter((region) => region.regionName.toLowerCase().includes(value))
    }
}