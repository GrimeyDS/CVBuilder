import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Project, Projects } from '../interfaces/types';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private apiService: ApiService) { }

  getProjects = (): Observable<Projects> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiProject);
  }

  getProject = (id: number): Observable<Projects> => {
    return this.apiService.get<any>(environment.apiBaseUrl + environment.apiProject + id);
  }

  postProject = (project: Project): Observable<any> => {
    return this.apiService.post<any>(environment.apiBaseUrl + environment.apiProject, project);
  }

  putProject = (project: Project, projectId: number): Observable<any> => {
    return this.apiService.put<any>(environment.apiBaseUrl + environment.apiProject + projectId, project);
  }

  deleteProject = (projectId: number): Observable<number> => {
    return this.apiService.delete<any>(environment.apiBaseUrl + environment.apiProject + projectId);
  }

  updateProjectTags = (tagIds: number[], projectId: number): Observable<number> => {
    return this.apiService.put<any>(environment.apiBaseUrl + environment.apiProject + environment.apiTags + projectId, { tagIds });
  }
}
