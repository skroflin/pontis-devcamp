import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

/** DOMAIN */
import { Employee } from './employee.model';
import { PagedListDto } from '@shared/entities/models/paged-list-dto';
import { TableMetadata } from '@shared/entities/models/table-metadata';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private url: string = `${environment.baseUrl}/employee`;
  private httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'X-Api-Key': `${environment.apiKey}`
  });
  private requestOptions = {headers: this.httpHeaders};
  constructor(private httpClient: HttpClient) {}

  get(id: number): Observable<Employee> {
    return this.httpClient.get<Employee>(`${this.url}/${id}`, this.requestOptions);
  }

  getAll(): Observable<Employee[]> {
    return this.httpClient.get<Employee[]>(`${this.url}`, this.requestOptions);
  }

  getPaged(tableMetadata?: TableMetadata): Observable<PagedListDto<Employee>> {
    return this.httpClient.post<PagedListDto<Employee>>(
      `${this.url}/paged`,
      tableMetadata, 
      this.requestOptions
    );
  }

  add(entity: Employee) {
    return this.httpClient.post(this.url, entity, this.requestOptions);
  }

  update(entity: Employee) {
    return this.httpClient.patch(this.url, entity, this.requestOptions);
  }

  delete(id: number) {
    return this.httpClient.delete(`${this.url}/${id}`, this.requestOptions);
  }
}