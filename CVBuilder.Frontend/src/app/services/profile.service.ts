import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Profile, Profiles } from '../interfaces/types';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private apiService: ApiService) { }

  getProfiles = (): Observable<Profiles> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiProfile);
  }

  getProfile = (id: number): Observable<Profiles> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiProfile + id);
  }

  postProfile = (profile: Profile): Observable<number> => {
    return this.apiService.post<any>(environment.apiBaseUrl + environment.apiProfile, profile);
  }

  putProfile = (profile: Profile, profileId: number): Observable<number> => {
    return this.apiService.put<any>(environment.apiBaseUrl + environment.apiProfile + profileId, profile);
  }

  deleteProfile = (id: number): Observable<number> => {
    return this.apiService.delete<any>(environment.apiBaseUrl + environment.apiProfile + id);
  }

  updateProfileTags = (tagIds: number[], profileId: number): Observable<number> => {
    return this.apiService.put<any>(environment.apiBaseUrl + environment.apiProfile + environment.apiTags + profileId, { tagIds });
  }
}
