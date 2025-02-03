import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ExperienceOverviewComponent } from './experience-overview/experience-overview.component';
import { ExperienceFormComponent } from './experience-form/experience-form.component';
import { Experience } from '../../../interfaces/types';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';
import { Messages } from '../../../../constants/constants';
import { isEqual } from 'lodash';


@Component({
  selector: 'app-experience',
  templateUrl: './experience.component.html',
  standalone: true,
  imports: [ExperienceOverviewComponent, ExperienceFormComponent, CommonModule],
  styleUrl: './experience.component.css'
})
export class ExperienceComponent {
  @Input() experience: Experience[] = [];
  @Input() profileId: number = 0;
  @Input() experienceType: string = '';
  @Output() deleteExperienceCallBack = new EventEmitter<any>();

  selectedExperience = {} as Experience;
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

  handleForm(experience: any): void {
    if (experience) {
      this.selectedExperience = experience;
    }
    this.toggleToForm();
  }

  handleFormClose(updatedExperience?: Experience): void {
    if (updatedExperience) {
      const foundExperience = this.experience.find((exp) => exp.id === updatedExperience.id);
      if (foundExperience) {
        const isChanged = !isEqual(foundExperience, updatedExperience);
        if (isChanged) {
          Object.assign(foundExperience, updatedExperience);
          this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.experience} ${Messages.updateComplete}` });
        }
      } else {
        this.experience.push(updatedExperience);
        this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.experience} ${Messages.createComplete}` });
      }
    }
    this.selectedExperience = {} as Experience;
    this.toggleToOverview();
  }

  deleteExperience(id: number): void {
    this.deleteExperienceCallBack.emit(id);
    this.messageService.add({ severity: 'success', summary: 'Success', detail: `${Messages.experience} ${Messages.deleteComplete}` });
  }
}
