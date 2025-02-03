import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Project } from '../../../interfaces/types';
import { ProjectFormComponent } from './project-form/project-form.component';
import { ProjectOverviewComponent } from './project-overview/project-overview.component';
import { MessageService } from 'primeng/api';
import { Messages } from '../../../../constants/constants';
import { isEqual } from 'lodash';


@Component({
  selector: 'app-project',
  standalone: true,
  imports: [CommonModule, ProjectFormComponent, ProjectOverviewComponent],
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent {
  @Input() projects: Project[] = [];
  @Input() profileId: number = 0;
  @Output() deleteProjectCallBack = new EventEmitter<any>();

  selectedProject = {} as Project;
  showForm: boolean = false;
  showOverview: boolean = true;

  constructor(private messageService: MessageService) { }

  toggleToForm(): void {
    this.showForm = true;
    this.showOverview = false;
  }

  toggleToOverview() {
    this.showOverview = true;
    this.showForm = false;
  }

  handleForm(project: any): void {
    if (project) {
      this.selectedProject = project;
    }
    this.toggleToForm();
  }

  handleFormClose(updatedProject?: Project): void {
    if (updatedProject) {
      const foundProject = this.projects.find((exp) => exp.id === updatedProject.id);
      if (foundProject) {
        const isChanged = !isEqual(foundProject, updatedProject);
        if (isChanged) {
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.project} ${Messages.updateComplete}` });
          Object.assign(foundProject, updatedProject);
        }
      } else {
        this.projects.push(updatedProject);
        this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.project} ${Messages.createComplete}` });
      }
    }
    this.selectedProject = {} as Project;
    this.toggleToOverview();
  }

  deleteProject(id: number): void {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.project} ${Messages.deleteComplete}` });
    this.deleteProjectCallBack.emit(id);
  }
}
