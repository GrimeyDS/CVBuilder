import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Tags } from '../interfaces/types';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  constructor(private apiService: ApiService) { }

  getTags = (): Observable<Tags> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiTag);
  }

  postTag = (tag: string): Observable<number> => {
    return this.apiService.post<any>(environment.apiBaseUrl + environment.apiTag, { title: tag });
  }
}
