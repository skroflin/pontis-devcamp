import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';


/** DOMAIN */
import { Region } from './region.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class RegionService {
  private url: string = `${environment.baseUrl}/region`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<Region> {
    return this.httpClient.get<Region>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<Region[]> {
    return this.httpClient.get<Region[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<Region>> {
    return this.httpClient.post<PagedListDto<Region>>(
      `${this.url}/paged`,
      tableMetadata,
      this.requestOptions
    );
  }

  add(entity: Region) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: Region) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}