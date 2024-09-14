import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

/** DOMAIN */
import { Application } from './application.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class ApplicationService {
  private url: string = `${environment.baseUrl}/application`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<Application> {
    return this.httpClient.get<Application>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<Application[]> {
    return this.httpClient.get<Application[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<Application>> {
    return this.httpClient.post<PagedListDto<Application>>(
      `${this.url}/paged`,
      tableMetadata,
      this.requestOptions
    );
  }

  add(entity: Application) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: Application) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}