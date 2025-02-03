import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  constructor(private httpClient: HttpClient) { }

  getFile(containerName: string, blobName: string): Observable<Blob> {
    return this.httpClient.get(`${environment.apiBaseUrl}File?blobName=${blobName}&containerName=${containerName}`, { responseType: 'blob' });
  }

  postFile(containerName: string, file: File, previousName: string): Observable<string> {
    const formData = new FormData();
    formData.append('File', file);
    formData.append('Name', previousName);
    formData.append('ContainerName', containerName)
    return this.httpClient.post(`${environment.apiBaseUrl}${environment.apiFile}`, formData, {responseType: 'text'});
  }
}
