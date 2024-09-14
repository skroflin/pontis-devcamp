import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

/** DOMAIN */
import { Place } from './place.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class PlaceService {
  private url: string = `${environment.baseUrl}/place`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<Place> {
    return this.httpClient.get<Place>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<Place[]> {
    return this.httpClient.get<Place[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<Place>> {
    return this.httpClient.post<PagedListDto<Place>>(
      `${this.url}/paged`,
      tableMetadata,
      this.requestOptions
    );
  }

  add(entity: Place) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: Place) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}