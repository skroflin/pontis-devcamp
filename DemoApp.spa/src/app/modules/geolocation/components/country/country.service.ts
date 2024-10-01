import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

/** DOMAIN */
import { Country } from './country.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class CountryService {
  private url: string = `${environment.baseUrl}/country`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<Country> {
    return this.httpClient.get<Country>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<Country[]> {
    return this.httpClient.get<Country[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<Country>> {
    return this.httpClient.post<PagedListDto<Country>>(
      `${this.url}/paged`,
      tableMetadata,
      this.requestOptions
    );
  }

  add(entity: Country) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: Country) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}