import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

/** DOMAIN */
import { District } from './district.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class DistrictService {
  private url: string = `${environment.baseUrl}/district`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<District> {
    return this.httpClient.get<District>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<District[]> {
    return this.httpClient.get<District[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<District>> {
    return this.httpClient.post<PagedListDto<District>>(
      `${this.url}/paged`,
      tableMetadata,
      this.requestOptions
    );
  }

  add(entity: District) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: District) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}