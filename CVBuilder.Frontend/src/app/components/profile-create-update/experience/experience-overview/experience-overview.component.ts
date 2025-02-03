import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { Message } from 'primeng/message';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { FeedbackMessagesComponent } from '../../../feedback-messages/feedback-messages.component';
import { ConfirmationService } from 'primeng/api';
import { ExperienceService } from '../../../../services/experience.service';
import { Experience } from '../../../../interfaces/types';
import { Messages } from '../../../../../constants/constants';

@Component({
  selector: 'app-experience-overview',
  standalone: true,
  imports: [CommonModule, DialogModule, ButtonModule, Message, FeedbackMessagesComponent, TableModule],
  templateUrl: './experience-overview.component.html',
  styleUrl: './experience-overview.component.css'
})
export class ExperienceOverviewComponent {
  @Input() experience: any;
  @Output() editExperienceCallBack = new EventEmitter<any>();
  @Output() deleteExperienceCallBack = new EventEmitter<any>();

  constructor(private confirmationService: ConfirmationService, private experienceService: ExperienceService) { }

  editExperience(experience: Experience) {
    this.editExperienceCallBack.emit(experience);
  }

  toggleToForm() {
    this.editExperienceCallBack.emit(null);
  }

  confirmDelete(experience: Experience) {
    this.confirmationService.confirm({
      message: `${Messages.deleteDialog} "${experience.title}"?`,
      acceptLabel: 'Delete',
      rejectLabel: 'Cancel',
      acceptIcon: 'pi pi-trash',
      rejectIcon: 'pi pi-times',
      acceptButtonStyleClass: 'p-button-danger',
      rejectButtonStyleClass: 'p-button-secondary',
      header: Messages.confirmDelete,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteExperience(experience.id);
      }
    });
  }

  deleteExperience(id: number) {
    this.experienceService.deleteExperience(id).subscribe({
      next: () => {
        this.experience = this.experience.filter((exp: Experience) => exp.id !== id);
        this.deleteExperienceCallBack.emit(id);
      }
    })
  }
}
