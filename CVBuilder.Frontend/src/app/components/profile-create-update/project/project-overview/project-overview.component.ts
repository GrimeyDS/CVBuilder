import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { Message } from 'primeng/message';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { FeedbackMessagesComponent } from '../../../feedback-messages/feedback-messages.component';
import { ConfirmDialog } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { ProjectService } from '../../../../services/project.service';
import { Project } from '../../../../interfaces/types';
import { Messages } from '../../../../../constants/constants';


@Component({
  selector: 'app-project-overview',
  standalone: true,
  imports: [CommonModule, DialogModule, ButtonModule, Message, FeedbackMessagesComponent, TableModule, ConfirmDialog],
  templateUrl: './project-overview.component.html',
  styleUrl: './project-overview.component.css'
})

export class ProjectOverviewComponent {
  @Input() projects: any;
  @Output() editProjectCallBack = new EventEmitter<any>();
  @Output() deleteProjectCallBack = new EventEmitter<any>();

  constructor(private confirmationService: ConfirmationService, private projectService: ProjectService) { }

  editProject(project: Project) {
    this.editProjectCallBack.emit(project);
  }

  toggleToForm() {
    this.editProjectCallBack.emit(null);
  }

  confirmDelete(project: Project) {
    this.confirmationService.confirm({
      message: `${Messages.deleteDialog} "${project.title}"?`,
      acceptLabel: 'Delete',
      rejectLabel: 'Cancel',
      acceptIcon: 'pi pi-trash',
      rejectIcon: 'pi pi-times',
      acceptButtonStyleClass: 'p-button-danger',
      rejectButtonStyleClass: 'p-button-secondary',
      header: Messages.confirmDelete,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteExperience(project.id);
      }
    });
  }

  deleteExperience(id: number) {
    this.projectService.deleteProject(id).subscribe({
      next: () => {
        this.projects = this.projects.filter((p: Project) => p.id !== id);
        this.deleteProjectCallBack.emit(id);
      }
    })
  }
}
