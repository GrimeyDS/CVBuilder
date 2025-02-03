import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { environment } from './../../environments/environment';
import { Templates } from '../interfaces/types';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PDFService {

  constructor(private apiService: ApiService, private httpClient: HttpClient) { }

  getTemplates = (): Observable<Templates> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiPDF);
  }

  getPDF(profileId: number, templateId: number): Observable<Blob> {
    return this.httpClient.get(`${environment.apiBaseUrl}${environment.apiPDF}${profileId}/${templateId}`, { responseType: 'blob' });
  }
}
