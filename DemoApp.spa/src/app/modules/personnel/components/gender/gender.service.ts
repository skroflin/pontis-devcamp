import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

/** DOMAIN */
import { Gender } from './gender.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class GenderService {
  private url: string = `${environment.baseUrl}/gender`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<Gender> {
    return this.httpClient.get<Gender>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<Gender[]> {
    return this.httpClient.get<Gender[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<Gender>> {
    return this.httpClient.post<PagedListDto<Gender>>(
      `${this.url}/paged`,
      tableMetadata, 
      this.requestOptions
    );
  }

  add(entity: Gender) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: Gender) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}