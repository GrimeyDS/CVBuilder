import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Experience, Experiences } from '../interfaces/types';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExperienceService {

  constructor(private apiService: ApiService) { }

  getExperiences = (): Observable<Experiences> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiExperience);
  }

  getExperience = (id: number): Observable<Experiences> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiExperience + id);
  }

  postExperience = (experience: Experience): Observable<number> => {
    return this.apiService.post<any>(environment.apiBaseUrl + environment.apiExperience, experience);
  }

  putExperience = (experience: Experience, experienceId: number): Observable<any> => {
    return this.apiService.put<any>(environment.apiBaseUrl + environment.apiExperience + experienceId, experience);
  }

  deleteExperience = (experienceId: number): Observable<any> => {
    return this.apiService.delete<any>(environment.apiBaseUrl + environment.apiExperience + experienceId);
  }
}
