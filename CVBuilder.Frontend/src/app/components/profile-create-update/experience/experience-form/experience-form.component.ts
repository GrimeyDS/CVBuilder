import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Message } from 'primeng/message';
import { FeedbackMessagesComponent } from '../../../feedback-messages/feedback-messages.component';
import { ExperienceService } from '../../../../services/experience.service';
import { Experience } from '../../../../interfaces/types';
import { TextareaModule } from 'primeng/textarea';
import { InputTextModule } from 'primeng/inputtext';
import { DatePicker } from 'primeng/datepicker';


@Component({
  selector: 'app-experience-form',
  standalone: true,
  imports: [ButtonModule, FormsModule, ReactiveFormsModule, InputTextModule, DatePicker, CommonModule, DialogModule, Message, FeedbackMessagesComponent, TextareaModule],
  templateUrl: './experience-form.component.html',
  styleUrl: './experience-form.component.css'
})
export class ExperienceFormComponent {
  @Input() experience = {} as Experience;
  @Input() profileId: number = 0;
  @Input() type: string = '';
  @Output() closeFormCallBack = new EventEmitter<any>();

  form: FormGroup;
  error: any = null;

  constructor(private fb: FormBuilder, private experienceService: ExperienceService) {
    this.form = this.fb.group({
      title: [''],
      description: [''],
      startDate: [null],
      endDate: [null]
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['experience']) {

      const endDate = this.experience.endDate;
      const startDate = this.experience.startDate;

      this.form.patchValue({
        title: this.experience.title,
        description: this.experience.description,
        startDate: startDate ? new Date(startDate) : null,
        endDate: endDate ? new Date(endDate) : null
      });
    }
  }

  onSave() {
    this.error = null;

    const experience = this.createExperienceModel();

    if (this.experience.id) {
      this.experienceService.putExperience(experience, this.experience.id).subscribe({
        next: (success) => {
            experience.id = success.id;
            this.closeFormCallBack.emit(experience);
        },
        error: (error) => this.error = error
      });
    }
    else {
      this.experienceService.postExperience(experience).subscribe({
        next: (success) => {
          experience.id = success;
          this.closeFormCallBack.emit(experience);
        },
        error: (error) => this.error = error
      });
    }
  }

  createExperienceModel(): Experience {
    let formStartDate = this.form.get('startDate')?.value;
    let formEndDate = this.form.get('endDate')?.value;
    const startDate = formStartDate ? new Date(formStartDate) : undefined;
    const endDate = formEndDate ? new Date(formEndDate) : undefined;

    const experience = {} as Experience;
    experience.profileId = this.profileId;
    experience.title = this.form.get('title')?.value;
    experience.description = this.form.get('description')?.value;
    experience.type = this.type;
    experience.startDate = startDate;
    experience.endDate = endDate;
    return experience;
  }

  onCancel() {
    this.closeFormCallBack.emit();
  }
}
