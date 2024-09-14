import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

/** DOMAIN */
import { NationalIdType } from './national-id-type.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class NationalIdTypeService {
  private url: string = `${environment.baseUrl}/nationalidtype`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<NationalIdType> {
    return this.httpClient.get<NationalIdType>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<NationalIdType[]> {
    return this.httpClient.get<NationalIdType[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<NationalIdType>> {
    return this.httpClient.post<PagedListDto<NationalIdType>>(
      `${this.url}/paged`,
      tableMetadata, 
      this.requestOptions
    );
  }

  add(entity: NationalIdType) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: NationalIdType) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}